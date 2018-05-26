using BoboTech.EncyclopaediaMetallumViewer.Helpers;
using DevExpress.Mvvm.UI.Interactivity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BoboTech.EncyclopaediaMetallumViewer.Behaviours
{
    public class ForwardMouseWheelBehavior : Behavior<UIElement>
    {
        Guid _instanceId = Guid.NewGuid();

        ScrollBar _verticalScrollBar;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseWheel += AssociatedObjectPreviewMouseWheel;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseWheel -= AssociatedObjectPreviewMouseWheel;
            base.OnDetaching();
        }

        void AssociatedObjectPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (_verticalScrollBar is null && AssociatedObject is DependencyObject depObj)
            {
                var scrollBars = depObj.FindChildren<ScrollBar>();
                _verticalScrollBar = scrollBars?.FirstOrDefault(x => x.Orientation == Orientation.Vertical);
                if (_verticalScrollBar is ScrollBar)
                    Debug.Print("Vertical ScrollBar found.");
            }

            var canScroll = !(_verticalScrollBar is null) && (e.Delta > 0 ? _verticalScrollBar.Value > _verticalScrollBar.Minimum : _verticalScrollBar.Value < _verticalScrollBar.Maximum);
            Debug.Print($"e.Delta is {e.Delta} (scrolling {(e.Delta > 0 ? "up" : "down")}), _verticalScrollBar.Value is {(_verticalScrollBar?.Value ?? 0)}, _verticalScrollBar.Minimum is {_verticalScrollBar?.Minimum ?? 0}, _verticalScrollBar.Maximum is {_verticalScrollBar?.Maximum ?? 0}, so {nameof(canScroll)} is {canScroll}");

            if (!canScroll && sender is Control control && control.Parent is UIElement parent)
            {
                parent.RaiseEvent(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
                {
                    RoutedEvent = UIElement.MouseWheelEvent,
                    Source = sender
                });
                Debug.Print("Event forwarded.");
            }
        }

        public override string ToString() => $"{nameof(ForwardMouseWheelBehavior)} ({_instanceId:N})";
    }
}