using System;
using Windows.UI.Xaml;

namespace GhostCore.UWP.TemplateSelectors
{
    /// <summary>
    /// This class provides helper functionality to the <see cref="XamlTemplateSelector"/> to decide what type maps to what data template.
    /// We decided to use this instead of x:DataType to ensure future compatability if it changes.
    /// </summary>
    public class XamlTypeHelper : DependencyObject
    {
        /// <summary>
        /// Indicates the TargetType attached property.
        /// </summary>
        public static readonly DependencyProperty TargetTypeProperty =
            DependencyProperty.RegisterAttached("TargetType", typeof(Type), typeof(XamlTypeHelper), new PropertyMetadata(null));

        /// <summary>
        /// Sets the TargetType attached property value on the given element.
        /// </summary>
        /// <param name="element">The value.</param>
        /// <param name="value">The target template.</param>
        public static void SetTargetType(DataTemplate element, Type value)
        {
            element.SetValue(TargetTypeProperty, value);
        }

        /// <summary>
        /// Gets the Type associated to the TargetType attached property.
        /// </summary>
        /// <param name="element">The targeted data template.</param>
        /// <returns></returns>
        public static Type GetTargetType(DataTemplate element)
        {
            return (Type)element.GetValue(TargetTypeProperty);
        }
    }

}
