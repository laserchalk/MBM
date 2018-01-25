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
        /// <summary>Initialises a new instance of <see cref="Price"/></summary>
        public Price()
        {

        }

        /// <summary>Initialises a new instance of <see cref="Price"/> with an amount</summary>
        public Price(decimal amount)
        {
            this.Amount = amount;
        }


        /// <summary>Gets or sets the amount</summary>
        /// <exception cref="Exception">Thrown when setting a negative value</exception>
        [DataMember]
        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value < 0) throw new Exception("Price cannot be negative");
                _amount = value;
                NotifyPropertyChanged("Price");
            }
        }
        private decimal _amount;

        /// <summary>Returns the price as a string</summary>
        public override string ToString()
        {
            string priceInformation;

            priceInformation = this.Amount.ToString();

            return priceInformation;
        }

        
    }
}
