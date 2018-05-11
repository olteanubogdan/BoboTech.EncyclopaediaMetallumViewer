using System.Windows;
using System.Windows.Controls;

namespace BoboTech.EncyclopaediaMetallumViewer.Fragments
{
    public partial class Busy : UserControl
    {
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            name: nameof(Status),
            propertyType: typeof(string),
            ownerType: typeof(Busy));

        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public Busy()
        {
            InitializeComponent();
        }
    }
}