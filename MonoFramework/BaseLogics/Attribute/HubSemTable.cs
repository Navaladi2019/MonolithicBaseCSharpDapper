using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFrame.BaseLogics
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HubSemTableAttribute : Attribute
    {
        public string Table { get; set; }
    }
}
