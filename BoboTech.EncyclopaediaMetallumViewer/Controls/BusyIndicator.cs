using BoboTech.EncyclopaediaMetallumViewer.Adorners;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;

namespace BoboTech.EncyclopaediaMetallumViewer.Controls
{
    /// <summary>
    /// Based on https://www.codeproject.com/Articles/54472/Defining-WPF-Adorners-in-XAML
    /// </summary>
    public class BusyIndicator : ContentControl
    {
        #region Fields

        private AdornerLayer _adornerLayer = null;
        private OverlayAdorner _adorner = null;
        private const double _fadingTime = 0.25;
        private readonly Lazy<FrameworkElement> _defaultTemplate = new Lazy<FrameworkElement>(() => new Fragments.Busy());
        private Guid _instanceId = Guid.NewGuid();

        #endregion

        #region Static properties

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            name: nameof(IsBusy),
            propertyType: typeof(bool),
            ownerType: typeof(BusyIndicator),
            typeMetadata: new FrameworkPropertyMetadata(IsBusyChanged));

        public static readonly DependencyProperty BusyTemplateProperty = DependencyProperty.Register(
            name: nameof(BusyTemplate),
            propertyType: typeof(FrameworkElement),
            ownerType: typeof(BusyIndicator),
            typeMetadata: new FrameworkPropertyMetadata(BusyTemplateChanged));

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            name: nameof(Status),
            propertyType: typeof(string),
            ownerType: typeof(BusyIndicator),
            typeMetadata: new FrameworkPropertyMetadata(StatusChanged));

        #endregion

        #region Static methods

        private static void PropertiesChanged(DependencyObject obj)
        {
            if (obj is BusyIndicator busyIndicator)
                busyIndicator.ShowOrHideAdorner();
        }

        private static void IsBusyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) => PropertiesChanged(obj);

        private static void BusyTemplateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) => PropertiesChanged(obj);

        private static void StatusChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj is BusyIndicator busyIndicator && busyIndicator.BusyContent is Fragments.Busy busyControl)
                busyControl.Status = busyIndicator.Status;
        }

        #endregion

        #region Constructor

        public BusyIndicator()
        {
            Focusable = false;
            DataContextChanged += new DependencyPropertyChangedEventHandler(BusyIndicatorDataContextChanged);
        }

        #endregion

        #region Properties

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        public FrameworkElement BusyTemplate
        {
            get => (FrameworkElement)GetValue(BusyTemplateProperty);
            set => SetValue(BusyTemplateProperty, value);
        }

        public string Status
        {
            get => (string)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }

        public FrameworkElement BusyContent { get => BusyTemplate ?? DefaultTemplate; }

        public FrameworkElement DefaultTemplate { get => _defaultTemplate.Value; }

        #endregion

        #region Private methods

        private void BusyIndicatorDataContextChanged(object sender, DependencyPropertyChangedEventArgs args) => UpdateAdornerDataContext();

        private void UpdateAdornerDataContext()
        {
            if (BusyContent is FrameworkElement element)
                element.DataContext = DataContext;
        }

        private void ShowOrHideAdorner()
        {
            if (IsBusy)
                ShowAdorner();
            else
                HideAdorner();
        }

        private void ShowAdorner()
        {
            if (_adorner != null)
                return;

            if (_adornerLayer == null)
                _adornerLayer = AdornerLayer.GetAdornerLayer(this);

            if (_adornerLayer != null)
            {
                _adorner = new OverlayAdorner(BusyContent, this) { Opacity = 0.0 };
                _adornerLayer.Add(_adorner);

                UpdateAdornerDataContext();
            }

            var animation = new DoubleAnimation(1.0, new Duration(TimeSpan.FromSeconds(_fadingTime)));
            animation.Freeze();
            _adorner.BeginAnimation(OpacityProperty, animation);
        }

        private void HideAdorner()
        {
            if (_adornerLayer == null || _adorner == null)
                return;

            var fadeOutAnimation = new DoubleAnimation(0.0, new Duration(TimeSpan.FromSeconds(_fadingTime)));
            fadeOutAnimation.Completed += FadeOutAnimationCompleted;
            fadeOutAnimation.Freeze();
            _adorner.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }

        private void FadeOutAnimationCompleted(object sender, EventArgs args)
        {
            _adornerLayer.Remove(_adorner);
            _adorner.DisconnectChild();

            _adorner = null;
            _adornerLayer = null;
        }

        #endregion

        #region Public methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ShowOrHideAdorner();
        }

        public override string ToString() => $"{nameof(BusyIndicator)} ({_instanceId:N})";

        #endregion
    }
}