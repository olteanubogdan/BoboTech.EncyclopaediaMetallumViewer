using System;
using System.Windows;
using System.Windows.Controls;

namespace BoboTech.EncyclopaediaMetallumViewer.Controls
{
    /// <summary>
    /// Must have a single child with MinHeight specified
    /// </summary>
    public class ForceChildResizePanel : StackPanel
    {
        private Guid _instanceId = Guid.NewGuid();

        protected override Size MeasureOverride(Size constraint)
        {
            Size tmpSize = base.MeasureOverride(constraint);
            tmpSize.Height = (double)(Children[0] as UIElement).GetValue(MinHeightProperty);
            return tmpSize;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            //This works only for one child!
            Children[0].SetCurrentValue(HeightProperty, arrangeSize.Height);
            Children[0].Arrange(new Rect(new Point(0, 0), arrangeSize));

            return arrangeSize;
        }

        public override string ToString() => $"{nameof(ForceChildResizePanel)} ({_instanceId:N})";
    }
}