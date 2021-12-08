using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmKL.Bindings
{
    public class Binding
    {
        public string SourcePropertyName { get; set; }
        public string TargetPropertyName { get; set; }
        public object SourceObject { get; set; }
        public object TargetObject { get; set; }

        public Binding(string sourcePropertyName, object sourceObject, string targetPropertyName, object targetObject)
        {
            //new Binding("text", txtUserName, viewModel, "UserName")
            this.SourcePropertyName = sourcePropertyName;
            this.SourceObject = sourceObject;
            this.TargetPropertyName = targetPropertyName;
            this.TargetObject = targetObject;
        }
    }
}
