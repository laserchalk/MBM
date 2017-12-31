using MBM.BL;
using MBM.DL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MBM.WPF.CLIENT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResetFilter();
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            ResetFilter();
        }

        Filter FilterBound = new Filter();
        ObservableCollection<StockEntry> StockEntriesBound = new ObservableCollection<StockEntry>();

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ServerStatistics_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            DocumentationWindow documenationWindow = new DocumentationWindow();
            documenationWindow.Show();
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                FilterBound.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;

                WCFStockRepository stockRepo = new WCFStockRepository();
                StockEntriesBound = new ObservableCollection<StockEntry>(stockRepo.GetStockEntries(FilterBound));
                StockEntriesDataGrid.ItemsSource = StockEntriesBound;

                Messages.Items.Insert(0, "Retrieved " + StockEntriesBound.Count.ToString() + " entries");
            }
            catch (Exception ex)
            {
                FilterError.Text = ex.Message;
                FilterError.Visibility = Visibility.Visible;
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetFilter();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid();
        }

        private void ClearGrid()
        {
            StockEntriesBound = new ObservableCollection<StockEntry>();
            StockEntriesDataGrid.ItemsSource = StockEntriesBound;
        }

        private void ResetFilter()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                WCFFilterRepository filterRepo = new WCFFilterRepository();
                FilterBound = filterRepo.GetMinMaxValues();
                FilterBound.Symbols = filterRepo.GetSymbols() as List<string>;
                FilterPanel.DataContext = FilterBound;

                FilterBound.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                FilterError.Text = ex.Message;
                FilterError.Visibility = Visibility.Visible;
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }
    }
}
