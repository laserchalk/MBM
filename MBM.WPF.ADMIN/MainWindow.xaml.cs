using Common;
using MBM.BL;
using MBM.DL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MBM.WPF.ADMIN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Filter WindowFilter = new Filter();
        ObservableCollection<StockEntry> WindowStockEntriesBound = new ObservableCollection<StockEntry>();

        //Dictionary<string, StockEntry> InsertedIds = new Dictionary<string, StockEntry>();
        Dictionary<uint, uint> DeletedIds = new Dictionary<uint, uint>();
        Dictionary<uint, uint> UpdatedIds = new Dictionary<uint, uint>();


        public MainWindow()
        {
            InitializeComponent();

            StockEntriesDataGrid.ItemsSource = WindowStockEntriesBound;
            ResetFilter();
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WindowFilter.Validate();
                FilterError.Text = "";
                FilterError.Visibility = Visibility.Collapsed;

                SQLStockRepository stockRepo = new SQLStockRepository();
                WindowStockEntriesBound = new ObservableCollection<StockEntry>(stockRepo.GetStockEntries(WindowFilter));

                StockEntriesDataGrid.ItemsSource = WindowStockEntriesBound;
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

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            List<StockEntry> WindowStockEntriesBound = new List<StockEntry>();
            StockEntriesDataGrid.ItemsSource = WindowStockEntriesBound;
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


        private void StockEntriesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            StockEntry stockEntry = new StockEntry();
            stockEntry = e.Row.Item as StockEntry;
            

            //add updated ids
        }

        private void StockEntriesDataGrid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            DataGrid grid = sender as DataGrid;

            if (e.Key == System.Windows.Input.Key.Delete)
            {
                StockEntry stock = new StockEntry();
                stock = grid.SelectedItem as StockEntry;
                MessageBox.Show("deleted: " + stock.ID.ToString());
            }

            //MessageBox.Show(e.Key.ToString());

            //add deleted ids
        }

        private void StockEntriesDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            StockEntry stock = new StockEntry();
            if(e.EditAction == DataGridEditAction.Commit)
            {
                stock = e.Row.Item as StockEntry;

                if(stock.ID == 0)
                {
                    MessageBox.Show("inserted: " + stock.ID.ToString());
                }
                else
                {
                    MessageBox.Show("updated: " + stock.ID.ToString());
                }

                
            }
            
        }
    }
}
