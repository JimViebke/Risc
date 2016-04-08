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
        public bool IsTurn { get; set; }
        [DataMember]
        public  int Units { get; set; }
        [DataMember]
		public string Color { get; set; }
        [DataMember]
        public string Name { get; set; }

        public Player(string set_color, string name) {
            IsTurn = false;
            Units = 5;
			Color = set_color;
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
