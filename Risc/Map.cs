using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Risc
{
	public class Map
	{
		private List<List<Tile>> map;

		public Map(int height, int width)
		{
			map = new List<List<Tile>>();
			for (int i = 0; i < height; ++i)
			{
				List<Tile> row = new List<Tile>();
				for (int j = 0; j < width; ++j) row.Add(new Tile());
				map.Add(row);
			}
		}



		public enum Color { empty, blocked, red, green, blue, yellow, orange, purple };

		private class Tile
		{
			private Color color = Color.empty;
			private int unit_count = 0;
			public Tile() { }

			public Color get_color() { return color; }
			public void set_color(Color set_color) { color = set_color; }

			public int get_unit_count() { return unit_count; }
			public void set_unit_count(int set_unit_count) { unit_count = set_unit_count; }
		}

		public void end_turn(Color color_moved)
		{
			int owned_tiles = 0;

			for (int i = 0; i < map.Count(); ++i)
				for (int j = 0; j < map[i].Count(); ++j)
					if (map[i][j].get_color() == color_moved) ++owned_tiles;

			// now add owned_tiles new units to color_moved's total units
		}
	}
}
