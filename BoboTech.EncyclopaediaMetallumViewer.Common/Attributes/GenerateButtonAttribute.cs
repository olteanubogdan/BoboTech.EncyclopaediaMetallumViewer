using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class GenerateButtonAttribute : Attribute
    {
        public int Order { get; set; }

        public string BindCommandTo { get; set; }

        public string BindTextTo { get; set; }
    }
}