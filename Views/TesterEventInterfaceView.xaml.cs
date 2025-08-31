using System.Windows.Controls;
using TestCellManager.ViewModels;

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
            ViewModel = new TesterEventInterfaceViewModel();
            DataContext = ViewModel;

            // Auto-scroll to bottom when new messages are added
            ViewModel.Messages.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    MessageScrollViewer.ScrollToBottom();
                }
            };
        }

        /// <summary>
        /// Method to be called from the TesterEventInterfaceObj to log inbound messages
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogInboundMessage(string message)
        {
            ViewModel.AddInboundMessage(message);
        }

        /// <summary>
        /// Method to be called from the TesterEventInterfaceObj to log outbound messages
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogOutboundMessage(string message)
        {
            ViewModel.AddOutboundMessage(message);
        }

        /// <summary>
        /// Update the status display
        /// </summary>
        /// <param name="status">New status</param>
        public void UpdateStatus(string status)
        {
            ViewModel.UpdateStatus(status);
        }
    }
}