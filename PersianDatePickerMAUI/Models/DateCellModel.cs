namespace PersianDatePickerMAUI.Models
{
    public class DateCellModel
    {
        public int Day { get; set; }
        public int FontSize { get; set; } = 12;
        public bool Show => Day > 0;
        public Color Background { get; set; }
        public Color TextColor { get; set; }
    }
}
