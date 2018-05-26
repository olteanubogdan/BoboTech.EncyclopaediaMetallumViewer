using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace BoboTech.EncyclopaediaMetallumViewer.Helpers
{
    public static class UIHelper
    {
        public static IEnumerable<DependencyObject> GetChildObjects(this DependencyObject parent)
        {
            if (parent == null)
                yield break;

            if (parent is Visual)
            {
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                    yield return VisualTreeHelper.GetChild(parent, i);
            }
            else if (parent is ContentElement)
            {
                foreach (var obj in LogicalTreeHelper.GetChildren(parent))
                    if (obj is DependencyObject depObj)
                        yield return depObj;
            }
        }

        public static IEnumerable<T> FindChildren<T>(this DependencyObject source) where T : DependencyObject
        {
            if (source == null)
                yield break;

            foreach (var child in GetChildObjects(source))
            {
                if (child is T t)
                    yield return t;

                foreach (var descendant in FindChildren<T>(child))
                    yield return descendant;
            }
        }
    }
}