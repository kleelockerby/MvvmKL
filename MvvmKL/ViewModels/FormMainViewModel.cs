using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Http;
using System.Windows.Forms;

namespace MvvmKL.ViewModels
{
    public class FormMainViewModel : INotifyPropertyChanged
    {
        private string logonResult;
        public virtual string LogonResult
        {
            get { return logonResult; }
            set
            {
                if (logonResult != value)
                {
                    logonResult = value;
                    RaisePropertyChanged("LogonResult");
                }
            }
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
