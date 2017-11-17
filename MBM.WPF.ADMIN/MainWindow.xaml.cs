using MBM.BL;
using MBM.DL;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MBM.WPF.ADMIN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Filter WindowFilter = new Filter();

        public MainWindow()
        {
            InitializeComponent();

            SQLFilterRepository filterRepo = new SQLFilterRepository();
            WindowFilter = filterRepo.GetMinMaxValues();
            WindowFilter.Symbols = filterRepo.GetSymbols();
            FilterPanel.DataContext = WindowFilter;
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            Filter filter = GetFilterValues();
            SQLStockRepository stockRepo = new SQLStockRepository();
            List<StockEntry> stockEntries = stockRepo.GetStockEntries(filter) as List<StockEntry>;
            StockEntriesDataGrid.ItemsSource = stockEntries;
        }

        private Filter GetFilterValues()
        {
            SQLFilterRepository filterRepo = new SQLFilterRepository();
            Filter filter = filterRepo.GetMinMaxValues();

            uint Uint = 0;
            if (uint.TryParse(volumeMin.Text, out Uint))
            {
                filter.VolumeStart = Uint;
            }
            
            Console.WriteLine(volumeMin.Text);

            

            return filter;
        }
    }
}
