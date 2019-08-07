using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace XamlTemplateSelector
{
    [ContentProperty(Name = nameof(TemplateEntries))]
    public class XamlTemplateSelector : DataTemplateSelector
    {
        public List<DataTemplateEntry> TemplateEntries { get; set; }

        public XamlTemplateSelector()
        {
            TemplateEntries = new List<DataTemplateEntry>();
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var type = item.GetType();
            var entry = TemplateEntries.FirstOrDefault(x => x.For == type);
            if (entry != null)
            {
                return entry.Template;
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}
