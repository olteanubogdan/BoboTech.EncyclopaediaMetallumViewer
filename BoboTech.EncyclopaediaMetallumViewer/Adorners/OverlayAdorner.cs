using System.Collections;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace BoboTech.EncyclopaediaMetallumViewer.Adorners
{
    /// <summary>
    /// Based on https://www.codeproject.com/Articles/54472/Defining-WPF-Adorners-in-XAML
    /// </summary>
    public class OverlayAdorner : Adorner
    {
        readonly FrameworkElement _childElement;

        public OverlayAdorner(FrameworkElement childElement, FrameworkElement adornedElement) : base(adornedElement)
        {
            _childElement = childElement;
            AddLogicalChild(_childElement);
            AddVisualChild(_childElement);
            adornedElement.SizeChanged += AdornedElementSizeChanged;
        }

        private void AdornedElementSizeChanged(object sender, SizeChangedEventArgs e) => InvalidateMeasure();

        protected override Size MeasureOverride(Size constraint)
        {
            _childElement.Measure(constraint);
            return _childElement.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _childElement.Arrange(new Rect(0.0, 0.0, (AdornedElement as FrameworkElement).ActualWidth, (AdornedElement as FrameworkElement).ActualHeight));
            return finalSize;
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index) => _childElement;

        protected override IEnumerator LogicalChildren => new ArrayList { _childElement }.GetEnumerator();

        public void DisconnectChild()
        {
            RemoveLogicalChild(_childElement);
            RemoveVisualChild(_childElement);
        }
    }
}