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

            ResetFilter();
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            SQLStockRepository stockRepo = new SQLStockRepository();
            List<StockEntry> stockEntries = stockRepo.GetStockEntries(WindowFilter) as List<StockEntry>;
            StockEntriesDataGrid.ItemsSource = stockEntries;
        }

        private void ResetFilter()
        {
            SQLFilterRepository filterRepo = new SQLFilterRepository();
            WindowFilter = filterRepo.GetMinMaxValues();
            WindowFilter.Symbols = filterRepo.GetSymbols();
            FilterPanel.DataContext = WindowFilter;
            //openMin.DataContext = WindowFilter.OpenPriceTest;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetFilter();
        }
    }
}
