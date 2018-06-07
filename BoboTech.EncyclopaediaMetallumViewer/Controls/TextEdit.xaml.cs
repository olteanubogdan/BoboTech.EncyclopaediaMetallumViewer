using DevExpress.Mvvm.UI.Interactivity;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BoboTech.EncyclopaediaMetallumViewer.Controls
{
    public partial class TextEdit : UserControl
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
            ownerType: typeof(TextEdit),
            typeMetadata: new FrameworkPropertyMetadata(defaultValue: string.Empty));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            name: "Value",
            propertyType: typeof(string),
            ownerType: typeof(TextEdit),
            typeMetadata: new FrameworkPropertyMetadata(defaultValue: string.Empty, flags: FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register(
            name: "IsReadOnly",
            propertyType: typeof(bool),
            ownerType: typeof(TextEdit),
            typeMetadata: new FrameworkPropertyMetadata(defaultValue: false));

        public TextEdit()
        {
            InitializeComponent();
            Loaded += TextEditLoaded;
        }

        bool _behaviorsAttached;

        private void TextEditLoaded(object sender, RoutedEventArgs e)
        {
            var caller = $"{nameof(TextEdit)}({_instanceId:N}).{nameof(TextEditLoaded)}";
            var debug = new Action<string>(x => Logger.Log.Debug(x, caller));
            debug("Start.");
            if (_behaviorsAttached)
            {
                debug("Behaviors already attached.");
                return;
            }
            foreach (var behavior in Interaction.GetBehaviors(this))
            {
                var newBehavior = behavior.Clone() as Behavior;
                Interaction.GetBehaviors(ValueTextBox).Add(newBehavior);
                debug($"{newBehavior} attached.");
                _behaviorsAttached = true;
            }
            debug("End.");
        }

        public override string ToString() => $"{nameof(TextEdit)} ({_instanceId:N})";
    }
}