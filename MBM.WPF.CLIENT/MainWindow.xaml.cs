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
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        /// <summary>Initialises a new instance of MainWindow</summary>
        public MainWindow()
        {
            InitializeComponent();
            Initialise();
        }




        /// <summary>Determines whether or not the application is in online or offline mode</summary>
        bool OfflineMode;

        /// <summary>The filter that is bound to the window</summary>
        Filter FilterBound = new Filter();

        /// <summary>The stock entries that are bound to the grid</summary>
        ObservableCollection<StockEntry> StockEntriesBound = new ObservableCollection<StockEntry>();




        /// <summary>Initialise settings by testing for a web service connection or setting it to offline mode.</summary>
        /// <exception cref="Exception">Thrown when failed to initialise data</exception>
        private void Initialise()
        {
            try
            {
                LoggingService.Log("Initialise main window", "Log.txt");

                if (CanConnectToWFC())
                {
                    ActivateOnlineMode();
                }
                else
                {
                    ActivateOfflineMode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LoggingService.Log(ex, "Log.txt");
            }
        }

        /// <summary>Activates online mode</summary>
        /// <exception cref="Exception">Thrown when failed to activate online mode</exception>
        private void ActivateOnlineMode()
        {
            try
            {
                OfflineMode = false;
                ResetFilter("WCF");
                GetStockEntries("WCF");

                CSVStockRepository stockRepo = new CSVStockRepository();
                stockRepo.AddStockEntries(StockEntriesBound);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to activate online mode", ex);
            }
        }

        /// <summary>Activates offline mode</summary>
        /// <exception cref="Exception">Thrown when failed to activate offline mode</exception>
        private void ActivateOfflineMode()
        {
            try
            {
                MessageBox.Show("Couldn't connect to service. Now running in offline mode.");
                OfflineMode = true;
                OfflineButton.IsChecked = true;
                ResetFilter("CSV");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to activate offline mode", ex);
            }
        }

        /// <summary>Gets stock entries from a web service or from a file using a the Filter</summary>
        private void GetStockEntries(string repositoryType)
        {
            try
            {
                LoggingService.Log("Getting stock entries", "Log.txt");

                Mouse.OverrideCursor = Cursors.Wait;
                FilterBound.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;

                IStockRepository stockRepo = StockRepositoryFactory.GetRepository(repositoryType);
                StockEntriesBound = new ObservableCollection<StockEntry>(stockRepo.GetStockEntries(FilterBound));
                StockEntriesDataGrid.ItemsSource = StockEntriesBound;

                Messages.Items.Insert(0, "Retrieved " + StockEntriesBound.Count.ToString() + " entries");
            }
            catch (Exception ex)
            {
                Messages.Items.Insert(0, ex.Message);
                LoggingService.Log(ex, "Log.txt");
                LoggingService.Log(FilterBound, "Log.txt");
                LoggingService.Log(StockEntriesBound, "Log.txt");
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        /// <summary>Clears the grid</summary>
        private void ClearGrid()
        {
            StockEntriesBound = new ObservableCollection<StockEntry>();
            StockEntriesDataGrid.ItemsSource = StockEntriesBound;
        }

        /// <summary>Retrieves filter values from a web service or file</summary>
        private void ResetFilter(string repositoryType)
        {
            try
            {
                LoggingService.Log("Resetting Filter", "Log.txt");

                Mouse.OverrideCursor = Cursors.Wait;
                IFilterRepository filterRepo = FilterRepositoryFactory.GetRepository(repositoryType);
                FilterBound = filterRepo.GetFilter();
                FilterPanel.DataContext = FilterBound;

                FilterBound.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                FilterError.Text = ex.Message;
                FilterError.Visibility = Visibility.Visible;
                LoggingService.Log(ex, "Log.txt");
                LoggingService.Log(FilterBound, "Log.txt");
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        /// <summary>Tests for a web service connection</summary>
        private bool CanConnectToWFC()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                WCFFilterRepository filterRepo = new WCFFilterRepository();
                filterRepo.GetMinMaxValues();
                Mouse.OverrideCursor = Cursors.Arrow;
                return true;
            }
            catch (Exception)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                return false;
            }

        }

        /// <summary>Occurs when the user documentation button is clicked</summary>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            DocumentationWindow documenationWindow = new DocumentationWindow();
            documenationWindow.Show();
        }

        /// <summary>Occurs when the offline button is clicked</summary>
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

        /// <summary>Occurs when the apply filter button is clicked</summary>
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

        /// <summary>Occurs when the reset filter button is clicked</summary>
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

        /// <summary>Occurs when the clear button is clicked</summary>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid();
        }




    }
}
