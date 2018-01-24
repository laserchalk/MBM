using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    /// <summary>Business layer base class</summary>
    [Serializable]
    [DataContract]
    public abstract class EntityBase : ILoggable
    {
        ///<summary> Gets or sets IsNew</summary>
        public bool IsNew { get; set; }

        ///<summary> Gets or sets HasChanges</summary>
        public bool HasChanges { get; set; }

        ///<summary> Gets IsValid</summary>
        public bool IsValid
        {
            get
            {
                return Validate();
            }
        }

        ///<summary> Occurs when a property is changed</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        ///<summary> Validates the object</summary>
        public virtual bool Validate()
        {
            return true;
        }

        ///<summary>Returns a string that can be used for logging purposes</summary>
        public virtual string Log()
        {
            return this.ToString();
        }

        ///<summary>Invokes the PropertyChanged event</summary>
        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
