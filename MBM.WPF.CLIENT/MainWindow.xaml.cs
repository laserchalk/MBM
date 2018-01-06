using Common;
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

            Initialise();
        }

        private void Initialise()
        {
            if (CanConnectToWFC())
            {
                OfflineMode = false;
                ResetFilter("WCF");
                GetStockEntries("WCF");

                CSVStockRepository stockRepo = new CSVStockRepository();
                stockRepo.AddStockEntries(StockEntriesBound);
            }
            else
            {
                MessageBox.Show("Couldn't connect to service. Now running in offline mode.");
                OfflineMode = true;
                OfflineButton.IsChecked = true;
                ResetFilter("CSV");
            }
        }

        bool OfflineMode;
        Filter FilterBound = new Filter();
        ObservableCollection<StockEntry> StockEntriesBound = new ObservableCollection<StockEntry>();

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            DocumentationWindow documenationWindow = new DocumentationWindow();
            documenationWindow.Show();
        }

        private void Offline_Click(object sender, RoutedEventArgs e)
        {
            if (OfflineButton.IsChecked)
            {
                OfflineMode = true;
            }else
            {
                OfflineMode = false;
            }
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (OfflineMode)
            {
                GetStockEntries("CSV");
            }
            else
            {
                GetStockEntries("WCF");
            } 
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (OfflineMode)
            {
                ResetFilter("CSV");
            }
            else
            {
                ResetFilter("WCF");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid();
        }

        private void GetStockEntries(string repositoryType)
        {
            try
            {
                LoggingService.Log("Applying Filter", "Log.txt");

                Mouse.OverrideCursor = Cursors.Wait;
                FilterBound.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;

                IStockRepository stockRepo = StockRepositoryFactory.GetRepository(repositoryType);
                StockEntriesBound = new ObservableCollection<StockEntry>(stockRepo.GetStockEntries(FilterBound));
                StockEntriesDataGrid.ItemsSource = StockEntriesBound;

                Messages.Items.Insert(0, "Retrieved " + StockEntriesBound.Count.ToString() + " entries");

                LoggingService.Log(StockEntriesBound, "Log.txt");
            }
            catch (Exception ex)
            {
                FilterError.Text = ex.Message;
                FilterError.Visibility = Visibility.Visible;
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void ClearGrid()
        {
            StockEntriesBound = new ObservableCollection<StockEntry>();
            StockEntriesDataGrid.ItemsSource = StockEntriesBound;
        }

        private void ResetFilter(string repositoryType)
        {
            try
            {
                LoggingService.Log("Resetting Filter", "Log.txt");

                Mouse.OverrideCursor = Cursors.Wait;
                IFilterRepository filterRepo = FilterRepositoryFactory.GetRepository(repositoryType);
                FilterBound = filterRepo.GetMinMaxValues();
                FilterBound.Symbols = filterRepo.GetSymbols() as List<string>;
                FilterPanel.DataContext = FilterBound;

                FilterBound.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;

                LoggingService.Log(FilterBound, "Log.txt");
            }
            catch (Exception ex)
            {
                FilterError.Text = ex.Message;
                FilterError.Visibility = Visibility.Visible;
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private bool CanConnectToWFC()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                WCFFilterRepository filterRepo = new WCFFilterRepository();
                filterRepo.GetMinMaxValues();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }


    }
}
