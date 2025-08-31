using System.Windows.Controls;
using TestCellManager.ViewModels;
using System.Diagnostics;

namespace TestCellManager.Views
{
    /// <summary>
    /// Interaction logic for TesterEventInterfaceView.xaml
    /// </summary>
    public partial class TesterEventInterfaceView : UserControl
    {
        public TesterEventInterfaceViewModel ViewModel { get; private set; }

        public TesterEventInterfaceView()
        {
            InitializeComponent();

            // Initialize ViewModel
            ViewModel = new TesterEventInterfaceViewModel();
            DataContext = ViewModel;

            // Debug: Verify initialization
            Debug.WriteLine($"TesterEventInterfaceView initialized. Initial message count: {ViewModel.MessageCount}");

            // Auto-scroll to bottom when new messages are added
            ViewModel.Messages.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    Dispatcher.BeginInvoke(new System.Action(() =>
                    {
                        MessageScrollViewer.ScrollToBottom();
                    }));
                }
            };
        }

        /// <summary>
        /// Method to be called from the TesterEventInterfaceObj to log inbound messages
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogInboundMessage(string message)
        {
            Debug.WriteLine($"LogInboundMessage called: {message}");

            // Ensure we're on the UI thread
            if (Dispatcher.CheckAccess())
            {
                ViewModel.AddInboundMessage(message);
                Debug.WriteLine($"Message added. Total count: {ViewModel.MessageCount}");
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    ViewModel.AddInboundMessage(message);
                    Debug.WriteLine($"Message added via Dispatcher. Total count: {ViewModel.MessageCount}");
                });
            }
        }

        /// <summary>
        /// Method to be called from the TesterEventInterfaceObj to log outbound messages
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogOutboundMessage(string message)
        {
            Debug.WriteLine($"LogOutboundMessage called: {message}");

            if (Dispatcher.CheckAccess())
            {
                ViewModel.AddOutboundMessage(message);
            }
            else
            {
                Dispatcher.Invoke(() => ViewModel.AddOutboundMessage(message));
            }
        }

        /// <summary>
        /// Update the status display
        /// </summary>
        /// <param name="status">New status</param>
        public void UpdateStatus(string status)
        {
            Debug.WriteLine($"UpdateStatus called: {status}");

            if (Dispatcher.CheckAccess())
            {
                ViewModel.UpdateStatus(status);
            }
            else
            {
                Dispatcher.Invoke(() => ViewModel.UpdateStatus(status));
            }
        }
    }
}