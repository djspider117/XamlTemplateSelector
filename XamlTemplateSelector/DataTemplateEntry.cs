using System;
using Windows.UI.Xaml;

namespace XamlTemplateSelector
{
    public class DataTemplateEntry
    {
        public Type For { get; set; }
        public DataTemplate Template { get; set; }
    }
}
