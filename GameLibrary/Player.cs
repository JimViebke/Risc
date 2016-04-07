using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace GameLibrary
{
 
    [DataContract]
    public class Player : INotifyPropertyChanged
    {
        [DataMember]
        bool IsTurn { get; set; }
        [DataMember]
        int Units { get; set; }
        [DataMember]
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
