using System;
using Windows.UI.Xaml;

namespace GhostCore.UWP.TemplateSelectors
{
    public class XamlTypeHelper : DependencyObject
    {
        public static readonly DependencyProperty TargetTypeProperty =
            DependencyProperty.RegisterAttached("TargetType", typeof(Type), typeof(XamlTypeHelper), new PropertyMetadata(null));

        public static void SetTargetType(DataTemplate element, Type value)
        {
            element.SetValue(TargetTypeProperty, value);
        }
        public static Type GetTargetType(DataTemplate element)
        {
            return (Type)element.GetValue(TargetTypeProperty);
        }
    }

}
