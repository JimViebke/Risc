using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GameLibrary
{
    public class Player : INotifyPropertyChanged
    {
        bool IsTurn { get; set; }
        int Units { get; set; }
		string Color { get; set; }

        public Player(string set_color) {
            IsTurn = false;
            Units = 0;
			Color = set_color;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
