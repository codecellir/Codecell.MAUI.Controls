namespace PersianDatePickerMAUI;
public static class AppHostBuilderExtensions
{
    public static MauiAppBuilder UsePersianDatePickerControl(this MauiAppBuilder builder)
    {
        builder
            .ConfigureFonts(fonts =>
               {
                   fonts.AddEmbeddedResourceFont(typeof(AppHostBuilderExtensions).Assembly, "iranyekan.ttf", "iranyekan");
                   fonts.AddEmbeddedResourceFont(typeof(AppHostBuilderExtensions).Assembly, "MaterialIcons-Regular.ttf", "materialIcon");
               });

        return builder;
    }
}
