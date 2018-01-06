using Common;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace MBM.BL
{
    [Serializable]
    [DataContract]
    public class Price : INotifyPropertyChanged, ILoggable
    {
        public Price()
        {

        }

        public Price(decimal amount)
        {
            this.Amount = amount;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private decimal _amount;

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

        public override string ToString()
        {
            string priceInformation;

            priceInformation = this.Amount.ToString();

            return priceInformation;
        }

        public string Log()
        {
            return ToString();
        }

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
