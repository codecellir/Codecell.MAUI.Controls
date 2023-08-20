using CommunityToolkit.Mvvm.Messaging;
using Mopups.Services;
using PersianDatePickerMAUI.Common;
using PersianDatePickerMAUI.Messages;
using PersianDatePickerMAUI.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace PersianDatePickerMAUI.Pages;

public partial class PersianCalendarPopupPage
{
    public string Year { get; set; }
    public string ShortName { get; set; }
    public string MonthName { get; set; }
    public string PersianDate { get; set; }
    public int SelectedDate { get; set; }
    private int _monthNumber;
    private string _identifier;
    public ObservableCollection<DateCellModel> ItemSource { get; set; } = new();
    public ICommand SelectDateCommand { get; set; }
    public ICommand NextMonthCommand { get; set; }
    public ICommand PrevMonthCommand { get; set; }
    public PersianCalendarPopupPage(string date, string identifier)
    {
        _identifier = identifier;
        PersianDate = CommonHelpers.NormalizePersianDate(date);
        InitItemSource();
        SelectDateCommand = new Command<int>(SelectDate);
        NextMonthCommand = new Command(NextMonth);
        PrevMonthCommand = new Command(PrevMonth);
        InitializeComponent();
    }

    void SelectDate(int day)
    {
        if (day > 0)
        {
            var dateSplited = PersianDate.Split('/');
            var result = $"{dateSplited[0]}/{dateSplited[1]}/{day.ToString("D2")}";
            WeakReferenceMessenger.Default.Send(new PersianDatePickerMessageTransfer($"{_identifier}#{result}"));
            MopupService.Instance.PopAsync();
        }
    }
    void NextMonth()
    {
        if (_monthNumber < 12)
        {
            _monthNumber++;
            var dateSplited = PersianDate.Split('/');
            var day = int.Parse(dateSplited[2]);
            if (_monthNumber > 6 && day == 31)
            {
                day = 1;
                SelectedDate = day;
                PersianDate = $"{Year}/{_monthNumber.ToString("D2")}/{day.ToString("D2")}";
            }
            else
                PersianDate = $"{dateSplited[0]}/{_monthNumber.ToString("D2")}/{dateSplited[2]}";
        }
        else if (_monthNumber == 12)
        {
            _monthNumber = 1;
            var dateSplited = PersianDate.Split('/');
            var year = int.Parse(dateSplited[0]) + 1;
            PersianDate = $"{year}/{_monthNumber.ToString("D2")}/{dateSplited[2]}";
        }
        // await dayGrid.TranslateTo(-330, 0, 100, Easing.SinIn);
        InitItemSource();
        //dayGrid.TranslationX = 0;
    }
    void PrevMonth()
    {
        if (_monthNumber > 1)
        {
            _monthNumber--;
            var dateSplited = PersianDate.Split('/');
            var day = int.Parse(dateSplited[2]);
            if (_monthNumber > 6 && day == 31)
            {
                day = 1;
                SelectedDate = day;
                PersianDate = $"{Year}/{_monthNumber.ToString("D2")}/{day.ToString("D2")}";
            }
            else
                PersianDate = $"{dateSplited[0]}/{_monthNumber.ToString("D2")}/{dateSplited[2]}";
        }
        else if (_monthNumber == 1)
        {
            _monthNumber = 12;
            var dateSplited = PersianDate.Split('/');
            var year = int.Parse(dateSplited[0]) - 1;
            PersianDate = $"{year}/{_monthNumber.ToString("D2")}/{dateSplited[2]}";
        }
        // await dayGrid.TranslateTo(330, 0, 100, Easing.SinIn);
        InitItemSource();
        //  dayGrid.TranslationX = 0;
    }
    void InitItemSource()
    {
        ItemSource.Clear();
        var cells = new List<DateCellModel>();
        var dateSplited = PersianDate.Split('/');
        Year = dateSplited[0];
        SelectedDate = int.Parse(dateSplited[2]);
        var month = _monthNumber = int.Parse(dateSplited[1]);
        var day = int.Parse(dateSplited[2]);
        if (month > 6 && day >= 30)
        {
            if (day == 31)
            {
                day = 30;
            }
            if (month == 12 && !int.Parse(Year).IsLeapYear())
            {
                day = 29;
            }
            SelectedDate = day;
            PersianDate = $"{Year}/{month.ToString("D2")}/{day.ToString("D2")}";
        }
        var georgianDate = new DateTime(int.Parse(dateSplited[0]), int.Parse(dateSplited[1]), day, new PersianCalendar());
        var georgianFirstDate = new DateTime(int.Parse(dateSplited[0]), int.Parse(dateSplited[1]), 1, new PersianCalendar());
        int weekSpan = georgianFirstDate.DayOfWeek.GetWeekSpan();
        MonthName = month.GetMonthName();
        ShortName = $"{georgianDate.DayOfWeek.GetWeekName()}، {day} {MonthName}";

        var maxDay = month <= 6 ? 31 : 30;

        for (int i = 1; i <= 42; i++)
        {
            int dayNumber = 0;
            if (i > weekSpan)
            {
                dayNumber = weekSpan > 0 ? i - (weekSpan) : i;
            }
            if (i >= 37)
            {
                dayNumber = 0;
            }

            cells.Add(new DateCellModel { Day = dayNumber, Background = dayNumber == SelectedDate ? Color.FromArgb("#919191") : Color.FromArgb("#fff") });
        }

        var clearIndex = 42 - (12 - weekSpan);
        if (maxDay == 31)
        {
            clearIndex++;
        }
        if (month == 12 && !int.Parse(Year).IsLeapYear())
        {
            clearIndex--;
        }
        for (int i = clearIndex; i <= 41; i++)
        {
            cells[i].Day = 0;
            cells[i].Background = Color.FromArgb("#fff");
        }

        ItemSource = new ObservableCollection<DateCellModel>(cells);
        OnPropertyChanged(nameof(Year));
        OnPropertyChanged(nameof(MonthName));
        OnPropertyChanged(nameof(ShortName));
        OnPropertyChanged(nameof(ItemSource));
    }
    private void ButtonCancel_Clicked(object sender, EventArgs e)
    {
        MopupService.Instance.PopAsync();
    }
    private void dayGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection[0] is DateCellModel cell)
        {
            SelectDate(cell.Day);
        }
    }
}