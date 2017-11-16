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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MBM.WPF.ADMIN
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

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            SQLFilterRepository filterRepo = new SQLFilterRepository();
            Filter filter = filterRepo.GetMinMaxValues();
            SQLStockRepository stockRepo = new SQLStockRepository();
            List<StockEntry> stockEntries = stockRepo.GetStockEntries(filter) as List<StockEntry>;
            StockEntriesDataGrid.ItemsSource = stockEntries;
        }
    }
}
