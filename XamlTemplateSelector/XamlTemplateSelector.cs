using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace GhostCore.UWP.TemplateSelectors
{
    /// <summary>
    /// Represents a template selector that will select the template based on the type of the object and can be fully defined in XAML
    /// </summary>
    [ContentProperty(Name = nameof(Templates))]
    public class XamlTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Backing field for the <see cref="Templates"/> property.
        /// </summary>
        protected DataTemplateCollection _templates;
        
        /// <summary>
        /// Represents the mapping between the object types and templates. Used as an equivalent to a switch instruction.
        /// </summary>
        protected Dictionary<Type, DataTemplate> _dataTemplateMapping;

        /// <summary>
        /// Gets or sets if the template selector should throw an exception if duplicate entries are added. Default is **true**.
        /// </summary>
        public bool ThrowExceptionOnDuplicateTypes { get; set; } = true;

        /// <summary>
        /// Gets or sets if an exception should be thrown when selection of the item finds no type mapped. Default is **false**. 
        /// Note that if this value is set to false, the value from base.SelectTemplateCore will be returned.
        /// </summary>
        public bool ThrowExceptionOnUnmappedType { get; set; } = false;

        /// <summary>
        /// The list of data templates.
        /// </summary>
        public virtual DataTemplateCollection Templates
        {
            get { return _templates; }
            set
            {
                _templates = value;
                InitializeTemplateMapping();
            }
        }

        /// <summary>
        /// The template that will be returned in case of a null value.
        /// </summary>
        public DataTemplate NullTemplate { get; set; }


        /// <summary>
        /// Creates an instance of a <see cref="XamlTemplateSelector"/> object.
        /// </summary>

        public XamlTemplateSelector()
        {
            _templates = new DataTemplateCollection();
        }

        /// <summary>
        /// Initializes the template mapping
        /// </summary>
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

        /// <summary>
        /// When implemented by a derived class, returns a specific DataTemplate for a given
        /// item or container. This method should be overriden when creating custom XamlTemplateSelectors.
        /// </summary>
        /// <param name="item">The item to return a template for.</param>
        /// <param name="container">The parent container for the templated item.</param>
        /// <returns>The template to use for the given item and/or container.</returns>
        protected virtual DataTemplate InternalSelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return NullTemplate;

            if (_dataTemplateMapping.TryGetValue(item.GetType(), out DataTemplate rv))
                return rv;

            return null;
        }

        /// <inheritdoc/>
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
