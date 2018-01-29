using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Common
{
    ///<summary>An ObservableCollection that has property changed events for each item</summary>
    public class TrulyObservableCollection<T> : ObservableCollection<T>
    where T : INotifyPropertyChanged
    {
        /// <summary>Occurs when an item in the collection has its properties altered</summary>
        public event PropertyChangedEventHandler ItemPropertyChanged;

        /// <summary>Initialises a new instance of TrulyObservableCollection</summary>
        public TrulyObservableCollection() : base()
        {
            CollectionChanged += TrulyObservableCollection_CollectionChanged;
        }

        /// <summary>Initialises a new instance of TrulyObservableCollection with an IEnumerable</summary>
        /// <exception cref="Exception">Thrown when failed to initialises a new instance of TrulyObservableCollection.</exception>
        public TrulyObservableCollection(IEnumerable<T> collection) : base(collection)
        {
            try
            {
                CollectionChanged += TrulyObservableCollection_CollectionChanged;

                foreach (var item in this)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialises a new instance of TrulyObservableCollection with an IEnumerable", ex);
            }
        }

        /// <summary>Occurs when an item is added or removed. Subscribes item_PropertyChanged to each of the items that are added</summary>
        /// <exception cref="Exception">Thrown when failed to subscribe or unsubscribe from propery changed event of item(s)</exception>
        private void TrulyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                TrulyObservableCollectionChangedEventArgs ObservableEventArgs;
                ObservableEventArgs = new TrulyObservableCollectionChangedEventArgs(null, "None");

                if (e.NewItems != null)
                {
                    foreach (Object item in e.NewItems)
                    {
                        (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                        TrulyObservableCollectionChangedEventArgs eventArgs = new TrulyObservableCollectionChangedEventArgs(item, "Item Added");
                    }
                }
                if (e.OldItems != null)
                {
                    foreach (Object item in e.OldItems)
                    {
                        (item as INotifyPropertyChanged).PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                        TrulyObservableCollectionChangedEventArgs eventArgs = new TrulyObservableCollectionChangedEventArgs(item, "Item Deleted");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to subscribe or unsubscribe from propery changed event of item(s)", ex);
            }
        }

        /// <summary>Invokes the ItemPropertyChanged event</summary>
        /// <exception cref="Exception">Thrown when failed to invoke item property changed</exception
        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                NotifyCollectionChangedEventArgs a = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
                OnCollectionChanged(a);

                ItemPropertyChanged?.Invoke(sender, e);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to invoke item property changed", ex);
            }

        }
    }

}
