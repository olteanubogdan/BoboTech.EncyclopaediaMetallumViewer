using BoboTech.EncyclopaediaMetallumViewer.Common.Attributes;
using BoboTech.EncyclopaediaMetallumViewer.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace BoboTech.EncyclopaediaMetallumViewer.Fragments
{
    public partial class Footer : UserControl
    {
        #region Fields

        Guid _instanceId = Guid.NewGuid();
        List<LabeledButton> _generatedButtons = new List<LabeledButton>();

        #endregion

        #region Constructor

        public Footer()
        {
            InitializeComponent();
            DataContextChanged += FooterDataContextChanged;
        }

        private void FooterDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _generatedButtons.ForEach(button => ButtonsPanel.Children.Remove(button));
            _generatedButtons.Clear();

            if (DataContext is null)
                return;

            DataContext
                .GetType()
                .GetMethods()
                .Select(methodInfo => new { Attribute = methodInfo.GetCustomAttribute<GenerateButtonAttribute>(), MethodInfo = methodInfo })
                .Where(x => x.Attribute != null)
                .OrderBy(x => x.Attribute.Order)
                .ToList()
                .ForEach(x =>
                {
                    var button = new LabeledButton();
                    button.SetBinding(ButtonBase.CommandProperty, new Binding(string.IsNullOrWhiteSpace(x.Attribute.BindCommandTo) ? $"{x.MethodInfo.Name}Command" : x.Attribute.BindCommandTo));
                    button.SetBinding(LabeledButton.LabelProperty, new Binding(string.IsNullOrWhiteSpace(x.Attribute.BindTextTo) ? $"{x.MethodInfo.Name}Label" : x.Attribute.BindTextTo));
                    ButtonsPanel.Children.Add(button);
                    _generatedButtons.Add(button);
                });
        }

        #endregion

        #region Public methods

        public override string ToString() => $"{nameof(Footer)} ({_instanceId:N})";

        #endregion
    }
}