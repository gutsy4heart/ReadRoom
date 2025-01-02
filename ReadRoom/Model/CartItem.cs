using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadRoom.Model
{
    public class CartItem : ViewModelBase
    {
        private int? quantity;

        public Books Book { get; set; }

        public int? Quantity
        {
            get => quantity;
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    RaisePropertyChanged(nameof(Quantity));
                }
            }
        }
    }
}
