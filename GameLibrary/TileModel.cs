using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace GameLibrary
{
    
    public interface ITileModel {
        int Row { get;set; }
        int Column { get; set; }
        int Value { get;set; }
        string Background {get; set; }
        string Foreground {get;set; }
        bool IsButtonEnabled { get; set; }
    }

     [DataContract]
     public class TileModel : INotifyPropertyChanged, ITileModel {
        [DataMember]
        public int Row { get; set; }
        [DataMember]
        public int Column { get; set; }

        private int _value;

        [DataMember]
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyPropertyChange("Value");

                if (value > 0)
                {
                    _background = Game.BLUE;
                    NotifyPropertyChange("Background");

                    _foreground = Game.WHITE;
                    NotifyPropertyChange("Foreground");
                }
            }
        }

        private string _background;
        [DataMember]
        public string Background
        {
            get { return _background; }
            set
            {
                _background = value;
                NotifyPropertyChange("Background");
            }
        }

        private string _foreground;
         [DataMember]
        public string Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                NotifyPropertyChange("Foreground");
            }
        }

        private bool _isButtonEnabled;
         [DataMember]
        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                _isButtonEnabled = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Action<TileModel> OnValueChanged { get; set; }
    }
}
