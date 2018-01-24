using Common;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MBM.BL
{
    /// <summary>Represents a positive decimal amount</summary>
    [Serializable]
    [DataContract]
    public class Price : EntityBase
    {
        /// <summary>Initialises a new instance of the <see cref="Price"/></summary>
        public Price()
        {

        }

        /// <summary>Initialises a new instance of the <see cref="Price"/> class with an amount</summary>
        public Price(decimal amount)
        {
            this.Amount = amount;
        }

        private decimal _amount;

        /// <summary>Gets or sets the amount</summary>
        /// <exception cref="ArgumentException">Thrown when setting a negative value</exception>
        [DataMember]
        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value < 0) throw new ArgumentException("Price cannot be negative");
                _amount = value;
                NotifyPropertyChanged("Price");
            }
        }

        /// <summary>Returns the price as a string</summary>
        public override string ToString()
        {
            string priceInformation;

            priceInformation = this.Amount.ToString();

            return priceInformation;
        }

        
    }
}
