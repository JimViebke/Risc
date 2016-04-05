using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
	//Game class handles storage of all player objects, and tile objects
	public class Game
	{
		public List<TileModel> Board { get; set; }
		private int board_height = 9;
		private int board_width = 9;

		private List<string> colors = new List<string>() { BLUE, RED, PURPLE, ORANGE };
		private List<string> used_colors = new List<string>();
		public List<Player> players = new List<Player>();

		public const string BLUE = "Blue";
		public const string RED = "Red";
		public const string PURPLE = "Purple";
		public const string ORANGE = "Orange";
		public const string GREEN = "Green"; // reserved for background
		public const string WHITE = "White"; // reserved for highlights
		public const string TRANSPARENT = "Transparent";
		public const string BACKGROUND = GREEN; // background

		public Game()
		{
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
		}

		public Player new_player()
		{
			// for each color that a player can have
			foreach (string color in colors)
			{
				// if the color is not already used
				if (!used_colors.Contains(color))
				{
					used_colors.Add(color);
					// add the player
					Player new_player = new Player(color);
					players.Add(new_player);
					return new_player;
				}
			}

			// the game cannot handle more players
			return null;
		}

		public void PaintAllTiles()
		{
			foreach (var tile in Board)
			{
				if (tile.Background != BLUE)
					tile.Background = GREEN;
			}
		}
		public void PaintSurroundingTiles(int column, int row, string color)
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

		public void do_combat(int attack_column, int attack_row, int defend_column, int defend_row)
		{
			// return if the coordinates are invalid or not adjacent
			if (!bounds_check(attack_column, attack_row) || !bounds_check(defend_column, defend_row) ||
			Math.Abs(attack_column - defend_column) > 1 || Math.Abs(attack_row - defend_row) > 1) return;

			// extract both tiles
			TileModel attack_tile = tile_at(attack_column, attack_row);
			TileModel defend_tile = tile_at(defend_column, defend_row);

			// return if the defending tile is friendly or not another player
			if (defend_tile.Background.Equals(attack_tile.Background) ||
			!colors.Contains(defend_tile.Background)) return;

			// find and subtract the lesser value from both tiles
			int reduce = Math.Min(attack_tile.Value, defend_tile.Value);
			attack_tile.Value -= reduce;
			defend_tile.Value -= reduce;

			// set tiles to the background color if no units remain
			if (attack_tile.Value < 1) attack_tile.Background = BACKGROUND;
			if (defend_tile.Value < 1) defend_tile.Background = BACKGROUND;
		}

		private TileModel tile_at(int row, int column) { return Board[(row * board_height) + column]; }
		private bool bounds_check(int val) { return val >= 0 && val < board_height; }
		private bool bounds_check(int a, int b) { return bounds_check(a) && bounds_check(b); }
	}
}
