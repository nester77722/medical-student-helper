using Serilog.Core;
using Serilog.Events;
using System.Windows;

namespace MedicalStudentHelper.WPF.Helpers;
public class MessageBoxSerilogSink : ILogEventSink
{
    public void Emit(LogEvent logEvent)
    {
        if (logEvent == null) throw new ArgumentNullException(nameof(logEvent));
        var messageBoxName = logEvent.MessageTemplate.Text;
        var messageBoxText = logEvent.Exception.Message;

        MessageBox.Show(messageBoxText, messageBoxName, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
