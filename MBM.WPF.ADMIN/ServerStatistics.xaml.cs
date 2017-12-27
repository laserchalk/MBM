using MBM.BL;
using MBM.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MBM.WPF.ADMIN
{
    /// <summary>
    /// Interaction logic for ServerStatistics.xaml
    /// </summary>
    public partial class ServerStatistics : Window
    {
        public ServerStatistics()
        {
            InitializeComponent();

            ServerStat WindowServerStats = new ServerStat();
            StatsPanel.DataContext = WindowServerStats;
        }

        ServerStat WindowServerStats = new ServerStat();

        private void Run_Button_Click(object sender, RoutedEventArgs e)
        {
            ServerStatsRepository ServerStatsRepo = new ServerStatsRepository();
            WindowServerStats = ServerStatsRepo.GetMostRecent();
            StatsPanel.DataContext = WindowServerStats;
        }
    }
}
