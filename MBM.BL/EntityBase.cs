using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    public abstract class EntityBase : ILoggable
    {
        public bool IsNew { get; set; }
        public bool HasChanges { get; set; }
        public bool IsValid
        {
            get
            {
                return Validate();
            }
        }

        /// <summary>
        /// Occurs when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual bool Validate()
        {
            return true;
        }

        public virtual string Log()
        {
            return this.ToString();
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
