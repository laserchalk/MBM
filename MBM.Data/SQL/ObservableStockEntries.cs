using Common;
using MBM.BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    public class ObservableStockEntries<T> : TrulyObservableCollection<T>
    where T : INotifyPropertyChanged
    {
        public ObservableStockEntries()
        {
            this.ItemPropertyChanged += ObservableStockEntries_ItemPropertyChanged;
            this.CollectionChanged += ObservableStockEntries_CollectionChanged;
        }

        

        private void ObservableStockEntries_ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
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

                //Messages.Items.Insert(0, serverResponse);
            }
            catch (Exception ex)
            {
                //Messages.Items.Insert(0, ex.Message.ToString());
            }

        }

        private void ObservableStockEntries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
                {
                    //Messages.Items.Insert(0, serverResponse);
                }

            }
            catch (Exception ex)
            {

                //Messages.Items.Insert(0, ex.Message.ToString());
            }
        }
    }
}
