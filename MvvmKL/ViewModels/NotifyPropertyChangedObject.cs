using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace MvvmKL.ViewModels
{
    public class NotifyPropertyChangedObject : INotifyPropertyChanged
    {
        private Dictionary<string, dynamic> values;

        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyPropertyChangedObject()
        {
            values = new Dictionary<string, dynamic>();
        }

        private static Dictionary<string, IEnumerable<string>> properties;
        private static bool isAlreadyInitialize = false;

        private void InitializeNotifyAttributes()
        {
            if (isAlreadyInitialize)
            {
                return;
            }

            var prop = from p in this.GetType().GetProperties()
                       where p.GetCustomAttributes(typeof(NotifyAttribute), true).Any()
                       select new
                       {
                           p.Name,
                           Attributes = p.GetCustomAttributes(typeof(NotifyAttribute), true)
                                            .Cast<NotifyAttribute>()
                                            .Select(a => string.IsNullOrEmpty(a.NotifyProperty) ? p.Name : a.NotifyProperty)
                       };

            properties = new Dictionary<string, IEnumerable<string>>();
            prop.ToList().ForEach(p => properties.Add(p.Name, p.Attributes.ToList()));

            isAlreadyInitialize = true;
        }

        public void SetValue<TObject>(Expression<Func<TObject>> expression, dynamic value)
        {
            InitializeNotifyAttributes();
            var key = GetPropertyName(expression);
            SetValue(key, value);
        }

        public T GetValue<T>(string key, T defaultValue = default(T))
        {
            InitializeNotifyAttributes();
            if (values.ContainsKey(key))
            {
                return (T)values[key];
            }
            return defaultValue;
        }

        public void SetValue(string key, dynamic value)
        {
            InitializeNotifyAttributes();
            if (!values.ContainsKey(key))
            {
                values.Add(key, value);
                properties[key].ToList().ForEach(p => this.OnPropertyChanged(p));
            }
            else
            {
                if (values[key] != value)
                {
                    values[key] = value;
                    properties[key].ToList().ForEach(p => this.OnPropertyChanged(p));
                }
            }
        }

        public void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private static PropertyInfo GetProperty<T>(Expression<Func<T>> action)
        {
            return typeof(T).GetProperty(GetPropertyName(action));
        }
    }
}
