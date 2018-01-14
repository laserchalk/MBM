using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TrulyObservableCollectionChangedEventArgs : EventArgs
    {
        public TrulyObservableCollectionChangedEventArgs(object changedObject, string action)
        {
            ChangedObject = changedObject;
            Action = action;
        }

        public object ChangedObject;
        public string Action;
    }
}
