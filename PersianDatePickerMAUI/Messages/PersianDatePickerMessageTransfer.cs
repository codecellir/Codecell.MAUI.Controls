using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PersianDatePickerMAUI.Messages
{
    public class PersianDatePickerMessageTransfer : ValueChangedMessage<string>
    {
        public PersianDatePickerMessageTransfer(string value) : base(value)
        {
        }
    }
}
