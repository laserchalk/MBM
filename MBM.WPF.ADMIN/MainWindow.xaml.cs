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

            ClearGrid();
            ResetFilter();

            
        }

        Filter WindowFilter = new Filter();
        TrulyObservableCollection<StockEntry> WindowStockEntriesBound = new TrulyObservableCollection<StockEntry>();


        private void WindowStockEntriesBound_ItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            StockEntry stockChanged = new StockEntry();
            SQLStockRepository stockRepo = new SQLStockRepository();
            string serverResponse;
            uint stockID;

            stockChanged = sender as StockEntry;
            

            if(stockChanged.ID == 0)
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

            //Messages.Items.Add(serverResponse);
            Messages.Items.Insert(0, serverResponse);

        }

        private void WindowStockEntriesBound_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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

            if(serverResponse != "")
            //Messages.Items.Add(serverResponse);
            Messages.Items.Insert(0, serverResponse);
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowFilter.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;

                SQLStockRepository stockRepo = new SQLStockRepository();
                WindowStockEntriesBound = new TrulyObservableCollection<StockEntry>(stockRepo.GetStockEntries(WindowFilter));
                StockEntriesDataGrid.ItemsSource = WindowStockEntriesBound;
                WindowStockEntriesBound.ItemPropertyChanged += WindowStockEntriesBound_ItemPropertyChanged;
                WindowStockEntriesBound.CollectionChanged += WindowStockEntriesBound_CollectionChanged;
            }
            catch (Exception ex)
            {
                FilterError.Text = ex.Message;
                FilterError.Visibility = Visibility.Visible;
            }  
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
            WindowStockEntriesBound = new TrulyObservableCollection<StockEntry>();
            StockEntriesDataGrid.ItemsSource = WindowStockEntriesBound;
            WindowStockEntriesBound.ItemPropertyChanged += WindowStockEntriesBound_ItemPropertyChanged;
            WindowStockEntriesBound.CollectionChanged += WindowStockEntriesBound_CollectionChanged;
        }

        private void ResetFilter()
        {
            try
            {
                SQLFilterRepository filterRepo = new SQLFilterRepository();
                WindowFilter = filterRepo.GetMinMaxValues();
                WindowFilter.Symbols = filterRepo.GetSymbols() as List<string>;
                FilterPanel.DataContext = WindowFilter;

                WindowFilter.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                FilterError.Text = ex.Message;
                FilterError.Visibility = Visibility.Visible;
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow settingsWindow = new OptionsWindow();
            settingsWindow.Show();
        }
    }
}
