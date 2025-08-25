using System.Configuration;
using System.Data;
using System.Windows;
using TestCellManager.Executives;

namespace TestCellManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            TCMSystem tCMSystem = TCMSystem.tcmSystem;
        }
    }

}
