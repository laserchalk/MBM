﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    [Serializable]
    public class Price
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

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
