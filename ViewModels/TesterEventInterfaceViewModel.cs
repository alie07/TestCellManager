using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

namespace TestCellManager.ViewModels
{
    public class TesterEventInterfaceViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private string _status = "Initialized";
        private ObservableCollection<MessageItem> _messages;
        private ICommand _clearLogCommand;
        #endregion

        #region Public Properties
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MessageItem> Messages
        {
            get => _messages ??= new ObservableCollection<MessageItem>();
            set
            {
                _messages = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MessageCount));
            }
        }

        public int MessageCount => Messages?.Count ?? 0;

        public ICommand ClearLogCommand
        {
            get => _clearLogCommand ??= new RelayCommand(ClearLog);
        }
        #endregion

        #region Constructor
        public TesterEventInterfaceViewModel()
        {
            Messages = new ObservableCollection<MessageItem>();

            // Add some sample messages for demonstration
            AddSampleMessages();
        }
        #endregion

        #region Public Methods
        public void AddInboundMessage(string message)
        {
            var messageItem = new MessageItem
            {
                Text = message,
                Direction = MessageDirection.Inbound,
                Timestamp = DateTime.Now
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(messageItem);
                OnPropertyChanged(nameof(MessageCount));
            });
        }

        public void AddOutboundMessage(string message)
        {
            var messageItem = new MessageItem
            {
                Text = message,
                Direction = MessageDirection.Outbound,
                Timestamp = DateTime.Now
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(messageItem);
                OnPropertyChanged(nameof(MessageCount));
            });
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
        }
        #endregion

        #region Private Methods
        private void ClearLog()
        {
            Messages.Clear();
            OnPropertyChanged(nameof(MessageCount));
        }

        private void AddSampleMessages()
        {
            AddInboundMessage("TM_SYS_INITILIZE received");
            AddOutboundMessage("Initialization complete");
            AddInboundMessage("TM_SYS_START_SVC received");
            AddOutboundMessage("Service started successfully");
            AddInboundMessage("WindowProc message: 0x0400");
            AddOutboundMessage("Message processed");
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    #region Supporting Classes
    public enum MessageDirection
    {
        Inbound,
        Outbound
    }

    public class MessageItem
    {
        public string Text { get; set; }
        public MessageDirection Direction { get; set; }
        public DateTime Timestamp { get; set; }

        public string DisplayText => $"[{Timestamp:HH:mm:ss.fff}] {Text}";

        public Style MessageStyle
        {
            get
            {
                var resourceName = Direction == MessageDirection.Inbound
                    ? "InboundMessageStyle"
                    : "OutboundMessageStyle";

                return Application.Current.Resources[resourceName] as Style;
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
    #endregion
}