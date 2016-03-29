﻿using System;
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
			for (int i = 0; i < height; ++i)
			{
				List<Tile> row = new List<Tile>();
				for (int j = 0; j < width; ++j) row.Add(new Tile());
				map.Add(row);
			}
		}



		private enum Color { empty, blocked, red, green, blue, yellow, orange, purple };

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
	}
}