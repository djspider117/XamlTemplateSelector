# XamlTemplateSelector
A template selector you can define in XAML so you can map types to data templates.
This works on 1809 or higher. Any lower and you will get the following compilation error:
Failed to create a 'System.Type' from the text '*your class here*'.


Usage:

Create your model/view model classes
```csharp
    public class Device
    {
        public string Guid { get; set; }
    }
    
    public class DesktopDevice : Device
    {
        public string ComputerName { get; set; }
    }
    
    public class MobileDevice : Device
    {
        public string Phone { get; set; }
    }
```

Make a data template for each of them and then define the XamlTemplate selector
```xml
        <DataTemplate x:Key="DesktopDeviceTemplate">
            <StackPanel Orientation="Horizontal" Padding="5,10">
                <FontIcon FontFamily="{StaticResource SymbolThemeFontFamily}" Glyph="&#xEC77;"/>
                <TextBlock Text="{Binding Guid}" Margin="20,0,0,0"/>
                <TextBlock Text="{Binding ComputerName}" Margin="10,0,0,0"/>
            </StackPanel>
        </DataTemplate>

        <DataTemplate x:Key="MobileDeviceTemplate">
            <StackPanel Orientation="Horizontal" Padding="5,10" >
                <FontIcon FontFamily="{StaticResource SymbolThemeFontFamily}" Glyph="&#xE8CC;"/>
                <TextBlock Text="Phone" Margin="50,0,0,0"/>
                <TextBlock Text="{Binding Phone}" Margin="10,0,0,0" Foreground="Gray"/>
            </StackPanel>
        </DataTemplate>

        <xamltemplateselector:XamlTemplateSelector x:Key="DeviceSelector">
            <xamltemplateselector:DataTemplateEntry For="models:DesktopDevice" Template="{StaticResource DesktopDeviceTemplate}"/>
            <xamltemplateselector:DataTemplateEntry For="models:MobileDevice" Template="{StaticResource MobileDeviceTemplate}"/>
        </xamltemplateselector:XamlTemplateSelector>
```

Add this to your root element (page/user control): 
```xml
xmlns:xamltemplateselector="using:XamlTemplateSelector"
```
