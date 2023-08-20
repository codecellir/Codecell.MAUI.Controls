using CommunityToolkit.Mvvm.Messaging;
using Mopups.Services;
using PersianDatePickerMAUI.Messages;
using PersianDatePickerMAUI.Pages;

[assembly: XmlnsDefinition("https://codecell.ir/maui/controls/persianDatePicker", "PersianDatePickerMAUI.Controls")]
namespace PersianDatePickerMAUI.Controls;

public partial class PersianDatePicker : ContentView
{
    private string _identifier = Guid.NewGuid().ToString();
    public static readonly BindableProperty PersianDateProperty = BindableProperty.Create(nameof(PersianDate), typeof(string), typeof(PersianDatePicker), string.Empty, BindingMode.TwoWay, propertyChanged: OnDateChange);
    public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(PersianDatePicker), "انتخاب تاریخ");
    public string PersianDate
    {
        get => (string)GetValue(PersianDateProperty);
        set => SetValue(PersianDateProperty, value);
    }
    public string PlaceHolder
    {
        get => (string)GetValue(PlaceHolderProperty);
        set => SetValue(PlaceHolderProperty, value);
    }
    public PersianDatePicker()
    {
        WeakReferenceMessenger.Default.Register<PersianDatePickerMessageTransfer>(this, (sender, arg) =>
        {
            var dataSplited = arg.Value.Split('#');
            if (dataSplited[0] == _identifier)
            {
                PersianDate = dataSplited[1];
            }
        });
        InitializeComponent();
    }

    static void OnDateChange(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is null || string.IsNullOrWhiteSpace(newValue.ToString()))
        {
            var datePicker = bindable as PersianDatePicker;
            datePicker.PersianDate = string.Empty;
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        MopupService.Instance.PushAsync(new PersianCalendarPopupPage(PersianDate, _identifier));
    }

}