using Common;
using MBM.BL;
using MBM.DL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MBM.WPF.ADMIN
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {

        /// <summary>Initialises a new instance of MainWindow</summary>
        public MainWindow()
        {
            InitializeComponent();
            File.Delete("Log.txt");
        }




        /// <summary>The Filter that is bound to the window</summary>
        Filter FilterBound = new Filter();

        /// <summary>The stock entries that are bound to the grid</summary>
        TrulyObservableCollection<StockEntry> StockEntriesBound = new TrulyObservableCollection<StockEntry>();





        /// <summary>Occurs when the windows has loaded</summary>
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            ClearGrid();
            ResetFilter();
        }

        /// <summary>Occurs when a value in the StockEntriesBound collection is changed</summary>
        private void StockEntriesBound_ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                LoggingService.Log("Collection property changed", "Log.txt");

                StockEntry stockChanged = new StockEntry();
                SQLStockRepository stockRepo = new SQLStockRepository();
                string serverResponse;
                uint stockID;

                stockChanged = sender as StockEntry;


                if (stockChanged.ID == 0)
                {
                    //Insert stock
                    serverResponse = stockRepo.AddStockEntry(stockChanged);

                    if (uint.TryParse(serverResponse, out stockID))
                    {
                        stockChanged.ID = stockID;
                        serverResponse = "Stock entry inserted with ID of " + stockID;
                    }

                }
                else
                {
                    //Update stock
                    serverResponse = stockRepo.UpdateStockEntry(stockChanged);
                }

                Messages.Items.Insert(0, serverResponse);
            }
            catch (Exception ex)
            {
                Messages.Items.Insert(0, ex.Message);
                LoggingService.Log(ex, "Log.txt");
                LoggingService.Log(StockEntriesBound, "Log.txt");
            }

        }

        /// <summary>Occurs when an item is added or removed from the StockEntriesBound collection</summary>
        private void StockEntriesBound_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                LoggingService.Log("Collection changed", "Log.txt");

                StockEntry stockChanged = new StockEntry();
                SQLStockRepository stockRepo = new SQLStockRepository();
                string serverResponse = "";

                if (e.OldItems != null)
                {
                    stockChanged = e.OldItems[0] as StockEntry;
                    serverResponse = stockRepo.DeleteStock(stockChanged.ID);
                }

                if (e.NewItems != null)
                {
                    stockChanged = e.NewItems[0] as StockEntry;

                    serverResponse = stockRepo.AddStockEntry(stockChanged);
                    uint stockID;

                    if (uint.TryParse(serverResponse, out stockID))
                    {
                        stockChanged.ID = stockID;
                        serverResponse = "Stock entry inserted with ID of " + stockID;
                    }
                }

                if (serverResponse != "")
                    Messages.Items.Insert(0, serverResponse);
            }
            catch (Exception ex)
            {
                Messages.Items.Insert(0, ex.Message);
                LoggingService.Log(ex, "Log.txt");
                LoggingService.Log(StockEntriesBound, "Log.txt");
            }
        }



        /// <summary>Retrieves the stock entries from the server</summary>
        private void GetStockEntries()
        {
            try
            {
                LoggingService.Log("Getting Stock Entries", "Log.txt");

                Mouse.OverrideCursor = Cursors.Wait;
                FilterBound.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;

                SQLStockRepository stockRepo = new SQLStockRepository();
                StockEntriesBound = null;
                StockEntriesBound = new TrulyObservableCollection<StockEntry>(stockRepo.GetStockEntries(FilterBound));
                StockEntriesDataGrid.ItemsSource = StockEntriesBound;
                StockEntriesBound.ItemPropertyChanged += StockEntriesBound_ItemPropertyChanged;
                StockEntriesBound.CollectionChanged += StockEntriesBound_CollectionChanged;

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
            StockEntriesBound = null;
            StockEntriesBound = new TrulyObservableCollection<StockEntry>();
            StockEntriesDataGrid.ItemsSource = StockEntriesBound;
            StockEntriesBound.ItemPropertyChanged += StockEntriesBound_ItemPropertyChanged;
            StockEntriesBound.CollectionChanged += StockEntriesBound_CollectionChanged;
        }

        /// <summary>Resets the filter values by retrieving the values from the database</summary>
        private void ResetFilter()
        {
            try
            {
                LoggingService.Log("Reseting Filter", "Log.txt");

                Mouse.OverrideCursor = Cursors.Wait;
                SQLFilterRepository filterRepo = new SQLFilterRepository();
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

        /// <summary>Occurs when the apply filter button is clicked</summary>
        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            GetStockEntries();
        }

        /// <summary>Occurs when the reset filter button is clicked</summary>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetFilter();
        }

        /// <summary>Occurs when the clear button is clicked</summary>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid();
        }

        /// <summary>Occurs when the settings button is clicked</summary>
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow settingsWindow = new OptionsWindow();
            settingsWindow.Show();
        }

        /// <summary>Occurs when the documentation button is clicked</summary>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            DocumenationWindow documenationWindow = new DocumenationWindow();
            documenationWindow.Show();
        }

        /// <summary>Occurs when the statistics button is clicked</summary>
        private void ServerStatistics_Click(object sender, RoutedEventArgs e)
        {
            ServerStatistics serverStatsWindow = new ServerStatistics();
            serverStatsWindow.Show();
        }
    }
}
