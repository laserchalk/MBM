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
        public event PropertyChangedEventHandler ItemPropertyChanged;
        public event EventHandler<TrulyObservableCollectionChangedEventArgs> ObservableCollectionChanged; 

        public TrulyObservableCollection() : base()
        {
            CollectionChanged += TrulyObservableCollection_CollectionChanged;
        }

        public TrulyObservableCollection(IEnumerable<T> collection) : base(collection)
        {
            CollectionChanged += TrulyObservableCollection_CollectionChanged;

            foreach (var item in this)
            {
                (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
            }
        }

        private void TrulyObservableCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TrulyObservableCollectionChangedEventArgs ObservableEventArgs;
            ObservableEventArgs = new TrulyObservableCollectionChangedEventArgs(null, "None");

            if (e.NewItems != null)
            {
                foreach (Object item in e.NewItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                    TrulyObservableCollectionChangedEventArgs eventArgs = new TrulyObservableCollectionChangedEventArgs(item, "Item Added");
                    ObservableCollectionChanged?.Invoke("sd", eventArgs);
                }
            }
            if (e.OldItems != null)
            {
                foreach (Object item in e.OldItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                    TrulyObservableCollectionChangedEventArgs eventArgs = new TrulyObservableCollectionChangedEventArgs(item, "Item Deleted");
                    ObservableCollectionChanged?.Invoke("sd", eventArgs);
                }
            }
        }

        private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs a = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(a);

            ItemPropertyChanged?.Invoke(sender, e);

            TrulyObservableCollectionChangedEventArgs eventArgs = new TrulyObservableCollectionChangedEventArgs(sender, "Item Changed");
            ObservableCollectionChanged?.Invoke("sd", eventArgs);

        }
    }

}
