using System;

namespace BoboTech.EncyclopaediaMetallumViewer.Common.Attributes
{
    /// <summary>
    /// BindCommandTo defaults to "{MethodName}Command". BindTextTo defaults to "{MethodName}Label". Order should be specified.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class GenerateButtonAttribute : Attribute
    {
        public int Order { get; set; }

        public string BindCommandTo { get; set; }

        public string BindTextTo { get; set; }
    }
}