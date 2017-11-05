using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.BL
{
    public class Price
    {
        public Price()
        {

        }

        public Price(int type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }

        private int _type;
        public int Type
        {
            get
            {
                return _type;
            }
            set
            {
                if(value < 1 || value > 5) throw new ArgumentException("Price type must be an integer from 1-5");
                _type = value;
            }
        }

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
            }
        }
    }
}
