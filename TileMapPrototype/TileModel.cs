using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TileMapPrototype
{
     public class TileModel : INotifyPropertyChanged
    {
        public int Row { get; set; }

        public int Column { get; set; }

        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyPropertyChange("Value");

                if (value > 0)
                {
                    _background = MainWindow.BLUE;
                    NotifyPropertyChange("Background");

                    _foreground = MainWindow.WHITE;
                    NotifyPropertyChange("Foreground");
                }
            }
        }

        private string _background;
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
