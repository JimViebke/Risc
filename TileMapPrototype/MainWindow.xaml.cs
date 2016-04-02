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
		private List<TileModel> Board { get; set; }
		private TileModel SelectedTile { get; set; }

		private int board_height = 9;
		private int board_width = 9;

		public const string BLUE = "Blue";
		public const string GREEN = "Green";
		public const string WHITE = "White";
		public const string TRANSPARENT = "Transparent";

		public MainWindow()
		{
			InitializeComponent();

			var random = new Random();

			Board = new List<TileModel>();

			string background = "";
			string foreground = "";
			bool isButtonEnabled = true;

			for (int i = 0; i < board_height; i++)
			{
				for (int j = 0; j < board_width; j++)
				{
					if (i < 6 && j == 0)
					{
						//background = "Gray";

						//isButtonEnabled = false;
					}
					else
					{
						background = GREEN;
						foreground = TRANSPARENT;
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
			Board.ElementAt(72).Background = BLUE;

			DataContext = Board;
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
				PaintSurroundingTiles(tileClicked.Row, tileClicked.Column, WHITE);
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
				PaintAllTiles();
			}
			else
			{
				PaintAllTiles();
			}
		}

		private void PaintAllTiles()
		{
			foreach (var tile in Board)
			{
				if (tile.Background != BLUE)
					tile.Background = GREEN;
			}
		}
		private void PaintSurroundingTiles(int column, int row, string color)
		{
			List<TileModel> surroundingTiles = new List<TileModel>();

			// for each adjacent tile
			for (int x_delta = -1; x_delta <= 1; ++x_delta)
			{
				for (int y_delta = -1; y_delta <= 1; ++y_delta)
				{
					// skip if the tile is the current tile or out of bounds
					if ((x_delta == 0 && y_delta == 0) || !bounds_check(column + x_delta, row + y_delta)) continue;
					
					// save a reference to the tile
					TileModel adjacent_tile = tile_at(column + x_delta, row + y_delta);
					if (adjacent_tile.Background != BLUE)
					{
						surroundingTiles.Add(adjacent_tile);
						adjacent_tile.Background = color;
					}
				}
			}
		}

		private TileModel tile_at(int row, int column) { return Board[(row * board_height) + column]; }
		private bool bounds_check(int val) { return val >= 0 && val < board_height; }
		private bool bounds_check(int a, int b) { return bounds_check(a) && bounds_check(b); }
	}
}
