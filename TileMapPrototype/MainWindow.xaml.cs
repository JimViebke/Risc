using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;


namespace TileMapPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TileModel> Board { get; set; }
        public TileModel SelectedTile { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var random = new Random();

            Board = new List<TileModel>();

            string background = "";
            string foreground = "";
            bool isButtonEnabled = true;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (i < 6 && j == 0 )
                    {
                        //background = "Gray";
                        
                        //isButtonEnabled = false;
                    }
                    else
                    {
                        background = "Green";
                        foreground = "Transparent";
                        isButtonEnabled = true;
                    }


                    Board.Add(new TileModel()
                    {
                        Row = i,
                        Column = j,
                        Value = 0,
                        Background = background,
                        Foreground = foreground,
                        IsButtonEnabled = isButtonEnabled,
                        //OnValueChanged = OnItemValueChanged
                    });
                }
            }

            Board.ElementAt(72).Value = 10;
              Board.ElementAt(72).Background = "Blue";
            
            DataContext = Board;
        }

        private void OnItemValueChanged(TileModel vm)
        {
            MessageBox.Show("Value Changed!\n" + "Row: " + vm.Row + "\nColumn: " + vm.Column + "\nValue: " + vm.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Controls.Button cb = e.Source as System.Windows.Controls.Button;
            TileModel tileClicked = cb.DataContext as TileModel;
            if (tileClicked.Value != 0 && SelectedTile == null)
            {
                //tileClicked.Value = tileClicked.Value + 1;
                tileClicked.Background = "Blue";
                SelectedTile = tileClicked;
                PaintSurroundingTiles(tileClicked.Column, tileClicked.Row, "White");
            }
            else if ((tileClicked.Value == 0 && tileClicked.Background == "White" && SelectedTile.Value > 1) || (tileClicked.Background == "Blue" && tileClicked != SelectedTile))
            {
                if ((tileClicked.Value == 0))
                {
                    tileClicked.Value = SelectedTile.Value - 1;
                    tileClicked.Background = "Blue";
                    SelectedTile.Value = SelectedTile.Value - SelectedTile.Value + 1;
                }
                else 
                {
                    tileClicked.Value += SelectedTile.Value - 1;
                    tileClicked.Background = "Blue";
                    SelectedTile.Value = SelectedTile.Value - SelectedTile.Value + 1;
                }
                SelectedTile = null;
                PaintAllTiles();
            }
            else 
            {
                PaintAllTiles();
            }

    
        }
        public void PaintAllTiles() {
            foreach (var tile in Board)
            {
                if (tile.Background != "Blue")
                    tile.Background = "Green";
            }
        }

        public void PaintSurroundingTiles(int column, int row, string color)
        {
            List<TileModel> surroundingTiles = new List<TileModel>();

            foreach (var tile in Board)
            {
                if (tile.Background != "Blue")
                {
                    if (tile.Column == column + 1 && tile.Row == row)
                    {
                        surroundingTiles.Add(tile);
                        tile.Background = color;

                    }
                    if (tile.Column == column + 1 && tile.Row == row + 1)
                    {
                        surroundingTiles.Add(tile);
                        tile.Background = color;

                    }

                    if (tile.Column == column - 1 && tile.Row == row - 1)
                    {
                        surroundingTiles.Add(tile);
                        tile.Background = color;

                    }

                    if (tile.Column == column - 1 && tile.Row == row + 1)
                    {
                        surroundingTiles.Add(tile);
                        tile.Background = color;

                    }

                    if (tile.Column == column + 1 && tile.Row == row - 1)
                    {
                        surroundingTiles.Add(tile);
                        tile.Background = color;

                    }

                    if (tile.Column == column && tile.Row == row - 1)
                    {
                        surroundingTiles.Add(tile);
                        tile.Background = color;
                    }

                    if (tile.Column == column && tile.Row == row + 1)
                    {
                        surroundingTiles.Add(tile);
                        tile.Background = color;
                    }

                    if (tile.Column == column - 1 && tile.Row == row)
                    {
                        surroundingTiles.Add(tile);
                        tile.Background = color;
                    }
                }
            }
        }
    }

    
 

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
                    _background = "Blue";
                    NotifyPropertyChange("Background");

                    _foreground = "White";
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
