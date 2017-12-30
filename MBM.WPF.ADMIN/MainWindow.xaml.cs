using Common;
using MBM.BL;
using MBM.DL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

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


        Filter FilterBound = new Filter();
        TrulyObservableCollection<StockEntry> StockEntriesBound = new TrulyObservableCollection<StockEntry>();

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            ClearGrid();
            ResetFilter();
        }

        private void WindowStockEntriesBound_ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            try
            {
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
                Messages.Items.Insert(0, ex.Message.ToString());
            }

        }

        private void WindowStockEntriesBound_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            try
            {
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

                Messages.Items.Insert(0, ex.Message.ToString());
            }
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                FilterBound.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;

                SQLStockRepository stockRepo = new SQLStockRepository();
                StockEntriesBound = new TrulyObservableCollection<StockEntry>(stockRepo.GetStockEntries(FilterBound));
                StockEntriesDataGrid.ItemsSource = StockEntriesBound;
                StockEntriesBound.ItemPropertyChanged += WindowStockEntriesBound_ItemPropertyChanged;
                StockEntriesBound.CollectionChanged += WindowStockEntriesBound_CollectionChanged;

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
            StockEntriesBound = new TrulyObservableCollection<StockEntry>();
            StockEntriesDataGrid.ItemsSource = StockEntriesBound;
            StockEntriesBound.ItemPropertyChanged += WindowStockEntriesBound_ItemPropertyChanged;
            StockEntriesBound.CollectionChanged += WindowStockEntriesBound_CollectionChanged;
        }

        private void ResetFilter()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                SQLFilterRepository filterRepo = new SQLFilterRepository();
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

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow settingsWindow = new OptionsWindow();
            settingsWindow.Show();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            DocumenationWindow documenationWindow = new DocumenationWindow();
            documenationWindow.Show();
        }

        

        private void ServerStatistics_Click(object sender, RoutedEventArgs e)
        {
            ServerStatistics serverStatsWindow = new ServerStatistics();
            serverStatsWindow.Show();
        }
    }
}
