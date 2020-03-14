# XamlTemplateSelector

## Supported platforms

Currently supporting UWP only, from SDK version 1809 and higher.
> NOTE: Any lower SDK and you will get the following compilation error:
>       Failed to create a 'System.Type' from the text '*your class here*'.

## Overview

This small library aims to help in eliminating the need of creating boring template selectors that just check for object type and return a specific template. A common example would be the following.

> NOTE: You will find the definition of the example classes at the bottom of the wiki.

### Creating the template selector

A common template selector would be: 

```csharp
    public class MyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DesktopDeviceTemplate { get; set; }
        public DataTemplate MobileDeviceTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is DesktopDevice)
                return DesktopDeviceTemplate;

            if (item is MobileDevice)
                return MobileDeviceTemplate;

            return base.SelectTemplateCore(item, container);
        }
    }
```

### Usage in XAML
```xml
    <DataTemplate x:Key="Template1">...</DataTemplate>
    <DataTemplate x:Key="Template2">...</DataTemplate>

    <demoapp:MyTemplateSelector x:Key="MySelector" 
                                DesktopDeviceTemplate="{StaticResource Template1}"
                                MobileDeviceTemplate="{StaticResource Template1}"/>
```

This is fine but sometimes you just want to write it directly in XAML

### Our Version
```xml
    <xts:XamlTemplateSelector x:Key="DeviceSelector">
        <DataTemplate xts:XamlTypeHelper.TargetType="models:DesktopDevice">
            <StackPanel Orientation="Horizontal" Padding="5,10">
                <FontIcon FontFamily="{StaticResource SymbolThemeFontFamily}" Glyph="&#xEC77;"/>
                <TextBlock Text="{Binding Guid}" Margin="20,0,0,0"/>
                <TextBlock Text="{Binding ComputerName}" Margin="10,0,0,0"/>
            </StackPanel>
        </DataTemplate>

        <DataTemplate xts:XamlTypeHelper.TargetType="models:MobileDevice">
            <StackPanel Orientation="Horizontal" Padding="5,10" >
                <FontIcon FontFamily="{StaticResource SymbolThemeFontFamily}" Glyph="&#xE8CC;"/>
                <TextBlock Text="Phone" Margin="50,0,0,0"/>
                <TextBlock Text="{Binding Phone}" Margin="10,0,0,0" Foreground="Gray"/>
            </StackPanel>
        </DataTemplate>
    </xts:XamlTemplateSelector>
```
## Quick Setup Guide

Add this namespace to the root element you will be adding the selector to and the reference to the objects that you will be performing selection on.

```xml
xmlns:xts="using:GhostCore.UWP.TemplateSelectors"
xmlns:models="...YOUR NAMSPACE HERE..."
```

You can now create a `XamlTemplateSelector` object
```xml
<xts:XamlTemplateSelector x:Key="MySelector" />
```

You may now add templates that target specific types using the `XamlTypeHelper.TargetType` attached property.

```xml
    <xts:XamlTemplateSelector x:Key="MySelector">
        <DataTemplate xts:XamlTypeHelper.TargetType="models:YOUR_MODEL_HERE1">...</DataTemplate>
        <DataTemplate xts:XamlTypeHelper.TargetType="models:YOUR_MODEL_HERE2">...</DataTemplate>
    </xts:XamlTemplateSelector>
```

You can now link it as a static resource as follows:
```xml
<ListView Name="lvDemo" ItemTemplateSelector="{StaticResource MySelector}" />
```

## Demo objects used in example

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
