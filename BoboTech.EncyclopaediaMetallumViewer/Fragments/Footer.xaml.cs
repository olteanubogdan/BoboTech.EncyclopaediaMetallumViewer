using BoboTech.EncyclopaediaMetallumViewer.Common.Attributes;
using System;
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

        #endregion

        #region Constructor

        public Footer()
        {
            Loaded += GeneratedButtonsLoaded;
            InitializeComponent();
        }

        private void GeneratedButtonsLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is null)
                return;

            DataContext
                .GetType()
                .GetMethods()
                .Select(x => x.GetCustomAttribute<GenerateButtonAttribute>())
                .Where(x => x != null)
                .OrderBy(x => x.Order)
                .ToList()
                .ForEach(generateButtonAttribute =>
                {
                    var textBlock = new TextBlock { Style = (Style)FindResource("buttonText") };

                    textBlock.SetBinding(TextBlock.TextProperty, new Binding(generateButtonAttribute.BindTextTo));

                    var button = new Button { Content = textBlock/*, Margin = new Thickness(3)*/ };

                    button.SetBinding(Button.CommandProperty, new Binding(generateButtonAttribute.BindCommandTo));

                    ButtonsPanel.Children.Add(button);
                });
        }

        #endregion

        #region Public methods

        public override string ToString() => $"{nameof(Footer)} ({_instanceId:N})";

        #endregion
    }
}