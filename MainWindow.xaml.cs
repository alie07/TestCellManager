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

            pnl.Children.Add(TCMSystem.m_tcm_exec.m_tester_event_interface.View);
        }
    }
}