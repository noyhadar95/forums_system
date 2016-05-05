using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ForumsSystemClient.PresentationLayer
{
    class AlignStackPanel: StackPanel
    {
        public bool AlignTop { get; set; }

        protected override Size MeasureOverride(Size constraint)
        {
            Size stackDesiredSize = new Size();

            UIElementCollection children = InternalChildren;
            Size layoutSlotSize = constraint;
            bool fHorizontal = (Orientation == Orientation.Horizontal);

            if (fHorizontal)
            {
                layoutSlotSize.Width = Double.PositiveInfinity;
            }
            else
            {
                layoutSlotSize.Height = Double.PositiveInfinity;
            }

            for (int i = 0, count = children.Count; i < count; ++i)
            {
                // Get next child.
                UIElement child = children[i];

                if (child == null) { continue; }

                // Accumulate child size.
                if (fHorizontal)
                {
                    // Find the offset needed to line up the text and give the child a little less room.
                    double offset = GetStackElementOffset(child);
                    child.Measure(new Size(Double.PositiveInfinity, constraint.Height - offset));
                    Size childDesiredSize = child.DesiredSize;

                    stackDesiredSize.Width += childDesiredSize.Width;
                    stackDesiredSize.Height = Math.Max(stackDesiredSize.Height, childDesiredSize.Height + GetStackElementOffset(child));
                }
                else
                {
                    child.Measure(layoutSlotSize);
                    Size childDesiredSize = child.DesiredSize;

                    stackDesiredSize.Width = Math.Max(stackDesiredSize.Width, childDesiredSize.Width);
                    stackDesiredSize.Height += childDesiredSize.Height;
                }
            }

            return stackDesiredSize;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            UIElementCollection children = this.Children;
            bool fHorizontal = (Orientation == Orientation.Horizontal);
            Rect rcChild = new Rect(arrangeSize);
            double previousChildSize = 0.0;

            for (int i = 0, count = children.Count; i < count; ++i)
            {
                UIElement child = children[i];

                if (child == null) { continue; }

                if (fHorizontal)
                {
                    double offset = GetStackElementOffset(child);

                    if (this.AlignTop)
                    {
                        rcChild.Y = offset;
                    }

                    rcChild.X += previousChildSize;
                    previousChildSize = child.DesiredSize.Width;
                    rcChild.Width = previousChildSize;
                    rcChild.Height = Math.Max(arrangeSize.Height - offset, child.DesiredSize.Height);
                }
                else
                {
                    rcChild.Y += previousChildSize;
                    previousChildSize = child.DesiredSize.Height;
                    rcChild.Height = previousChildSize;
                    rcChild.Width = Math.Max(arrangeSize.Width, child.DesiredSize.Width);
                }

                child.Arrange(rcChild);
            }

            return arrangeSize;
        }

        private static double GetStackElementOffset(UIElement stackElement)
        {
            if (stackElement is TextBlock)
            {
                return 5;
            }

            if (stackElement is Label)
            {
                return 0;
            }

            if (stackElement is TextBox)
            {
                return 2;
            }

            if (stackElement is ComboBox)
            {
                return 2;
            }

            return 0;
        }
    }
}
