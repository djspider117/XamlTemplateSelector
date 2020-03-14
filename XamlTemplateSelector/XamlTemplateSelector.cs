using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace GhostCore.UWP.TemplateSelectors
{
    [ContentProperty(Name = nameof(Templates))]
    public class XamlTemplateSelector : DataTemplateSelector
    {
        protected DataTemplateCollection _templates;
        protected Dictionary<Type, DataTemplate> _dataTemplateMapping;

        public bool ThrowExceptionOnDuplicateTypes { get; set; } = true;
        public bool ThrowExceptionOnUnmappedType { get; set; } = false;

        public virtual DataTemplateCollection Templates
        {
            get { return _templates; }
            set
            {
                _templates = value;
                InitializeTemplateMapping();
            }
        }
        public DataTemplate NullTemplate { get; set; }

        public XamlTemplateSelector()
        {
            _templates = new DataTemplateCollection();
        }

        protected virtual void InitializeTemplateMapping()
        {
            if (_templates == null)
            {
                _dataTemplateMapping?.Clear();
                _dataTemplateMapping = null;
                return;
            }

            _dataTemplateMapping = new Dictionary<Type, DataTemplate>();
            foreach (var template in _templates)
            {
                var type = XamlTypeHelper.GetTargetType(template);
                if (_dataTemplateMapping.ContainsKey(type) && ThrowExceptionOnDuplicateTypes)
                    throw new InvalidOperationException("[AdvancedTemplateSelector] Unable to add data template. Duplicate type detected.");

                _dataTemplateMapping.Add(type, template);
            }
        }
        protected virtual DataTemplate InternalSelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return NullTemplate;

            if (_dataTemplateMapping.TryGetValue(item.GetType(), out DataTemplate rv))
                return rv;

            return null;
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (_dataTemplateMapping == null)
                InitializeTemplateMapping();

            var template = InternalSelectTemplate(item, container);
            if (template != null)
                return template;

            if (ThrowExceptionOnUnmappedType)
                throw new InvalidOperationException("[AdvancedTemplateSelector] Unmapped type detected on SelectTemplateCore.");
            else
                return base.SelectTemplateCore(item, container);
        }

    }

}
