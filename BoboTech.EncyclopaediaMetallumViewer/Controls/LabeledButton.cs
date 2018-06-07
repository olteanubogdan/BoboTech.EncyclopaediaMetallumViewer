using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BoboTech.EncyclopaediaMetallumViewer.Controls
{
    public class LabeledButton : Button
    {
        protected Guid _instanceId = Guid.NewGuid();

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            name: "Label",
            propertyType: typeof(string),
            ownerType: typeof(LabeledButton),
            typeMetadata: new FrameworkPropertyMetadata(defaultValue: string.Empty));

        public LabeledButton()
        {
            var textBlock = new TextBlock { Style = (Style)FindResource("buttonText") };

            textBlock.SetBinding(TextBlock.TextProperty, new Binding(nameof(Label)) { Source = this });

            Content = textBlock;
        }

        public override string ToString() => $"{nameof(LabeledButton)} ({_instanceId:N})";
    }
}