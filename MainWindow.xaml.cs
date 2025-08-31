using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestCellManager.SystemTCM.Exec;

namespace TestCellManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TCMSystem.m_msgHandler.AssertPostMessage(MessageID.TM_SYS_INITILIZE, IntPtr.Zero, IntPtr.Zero);
            //TCMSystem.m_msgHandler.OnInitializeShutdown();
        }

        private void btnInitialize_Click(object sender, RoutedEventArgs e)
        {
            TCMSystem.m_msgHandler.AssertPostMessage(MessageID.TM_SYS_INITILIZE, IntPtr.Zero, IntPtr.Zero);
        }

        private void btnStartSvc_Click(object sender, RoutedEventArgs e)
        {
            TCMSystem.m_msgHandler.AssertPostMessage(MessageID.TM_SYS_START_SVC, IntPtr.Zero, IntPtr.Zero);
        }

        private void btnStopSvc_Click(object sender, RoutedEventArgs e)
        {
            TCMSystem.m_msgHandler.AssertPostMessage(MessageID.TM_SYS_STOP_SVC, IntPtr.Zero, IntPtr.Zero);
        }

        private void btnSHowDiag_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var view = TCMSystem.m_tcm_exec.m_tester_event_interface.View;

                if (view == null)
                {
                    MessageBox.Show("View is null!");
                    return;
                }

                // Clear existing children
                pnl.Children.Clear();

                // Add the view
                pnl.Children.Add(view);

                // Test logging a message
                view.LogInboundMessage("Dialog shown - test message");

                // Debug: Check what's in the ViewModel
                var messageCount = view.ViewModel.MessageCount;
                var messages = string.Join("\n", view.ViewModel.Messages.Select(m => m.DisplayText));

                MessageBox.Show($"View added successfully.\nMessage count: {messageCount}\nMessages:\n{messages}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing dialog: {ex.Message}");
            }
        }
    }
}