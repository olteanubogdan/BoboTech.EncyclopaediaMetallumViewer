using BoboTech.EncyclopaediaMetallumViewer.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BoboTech.EncyclopaediaMetallumViewer.Fragments
{
    public partial class Footer : UserControl
    {
        #region Fields

        Guid _instanceId = Guid.NewGuid();
        List<Button> _generatedButtons = new List<Button>();

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
                    var textBlock = new TextBlock { Style = (Style)FindResource("buttonText") };

                    textBlock.SetBinding(TextBlock.TextProperty, new Binding(string.IsNullOrWhiteSpace(x.Attribute.BindTextTo) ? $"{x.MethodInfo.Name}Label" : x.Attribute.BindTextTo));

                    var button = new Button { Content = textBlock };

                    button.SetBinding(Button.CommandProperty, new Binding(string.IsNullOrWhiteSpace(x.Attribute.BindCommandTo) ? $"{x.MethodInfo.Name}Command" : x.Attribute.BindCommandTo));

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