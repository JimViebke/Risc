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
        //private List<TileModel> Board { get; set; }
        //private TileModel SelectedTile { get; set; }

        //private int board_height = 9;
        //private int board_width = 9;

        public const string BLUE = "Blue";
        public const string GREEN = "Green";
        public const string WHITE = "White";
        public const string TRANSPARENT = "Transparent";
        private TileModel SelectedTile { get; set; }
        Game gameInstance;

		public MainWindow()
		{
			InitializeComponent();

            //Instead of new game, you will be getting it from the service
            gameInstance = new Game();


			DataContext = gameInstance.Board;
		}

		private void OnItemValueChanged(TileModel vm)
		{
			MessageBox.Show("Value Changed!\n" + "Row: " + vm.Row + "\nColumn: " + vm.Column + "\nValue: " + vm.Value);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			TileModel tileClicked = (e.Source as Button).DataContext as TileModel;
			if (tileClicked.Value != 0 && SelectedTile == null)
			{
				//tileClicked.Value = tileClicked.Value + 1;
				tileClicked.Background = BLUE;
				SelectedTile = tileClicked;
				gameInstance.PaintSurroundingTiles(tileClicked.Row, tileClicked.Column, WHITE);
			}
			else if ((tileClicked.Value == 0 && tileClicked.Background == WHITE && SelectedTile.Value > 1) || (tileClicked.Background == BLUE && tileClicked != SelectedTile))
			{
				if ((tileClicked.Value == 0))
				{
					tileClicked.Value = SelectedTile.Value - 1;
					tileClicked.Background = BLUE;
					SelectedTile.Value = SelectedTile.Value - SelectedTile.Value + 1;
				}
				else
				{
					tileClicked.Value += SelectedTile.Value - 1;
					tileClicked.Background = BLUE;
					SelectedTile.Value = SelectedTile.Value - SelectedTile.Value + 1;
				}
				SelectedTile = null;
                gameInstance.PaintAllTiles();
			}
			else
			{
                gameInstance.PaintAllTiles();
			}
		}
	}
}
