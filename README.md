## Codecell.MAUI.Controls
Custom Persian Date Picker Control for .NET MAUI (Android)

## Installation
You can download the latest version of `Codecell.PersiandatePicker.MAUI` from [Github repository](https://github.com/codecellir/Codecell.MAUI.Controls).
To install via `nuget`:
```
Install-Package Codecell.PersiandatePicker.MAUI -Version 1.0.3
```
Install from [Nuget](https://www.nuget.org/packages/Codecell.PersiandatePicker.MAUI) directly.

## How to use
Register Codecell Persian DatePicker Control to project container in `MauiProgram.cs` file:
``` C#
using PersianDatePickerMAUI;

namespace Sample
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UsePersianDatePickerControl()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
```

use this `xmlns`:
```
xmlns:controls="https://codecell.ir/maui/controls/persianDatePicker"
```
``` XAML
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:controls="https://codecell.ir/maui/controls/persianDatePicker"
             x:Class="Sample.MainPage">

    <ScrollView>
        <VerticalStackLayout
            Spacing="25"
            Padding="30,0"
            VerticalOptions="Center">

            <controls:PersianDatePicker /> 

            <controls:PersianDatePicker PersianDate="1367/01/20" />

            <controls:PersianDatePicker PersianDate="{Binding FromDate}" />

        </VerticalStackLayout>
    </ScrollView>

</ContentPage>

```


## Screenshots

![App Screenshot](https://github.com/codecellir/Codecell.MAUI.Controls/blob/master/screenshots/screen_1.png?raw=true)

![App Screenshot](https://github.com/codecellir/Codecell.MAUI.Controls/blob/master/screenshots/screen_2.png?raw=true)

![App Screenshot](https://github.com/codecellir/Codecell.MAUI.Controls/blob/master/screenshots/screen_3.png?raw=true)

![App Screenshot](https://github.com/codecellir/Codecell.MAUI.Controls/blob/master/screenshots/screen_4.png?raw=true)



## Tutorial video
see the tutorial persian video *[here](https://codecell.ir/course/d85e)* 