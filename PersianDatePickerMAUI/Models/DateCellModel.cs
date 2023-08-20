namespace PersianDatePickerMAUI.Models
{
    public class DateCellModel
    {
        public int Day { get; set; }
        public bool Show => Day > 0;
        public Color Background { get; set; } = Color.FromArgb("#fff");
    }
}
