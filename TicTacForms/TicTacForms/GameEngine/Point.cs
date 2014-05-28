using System;

namespace System.Drawing
{
	public class Point
	{
		public Point ()
		{
		}

		public Point (int x, int y)
		{
			X = x; Y = y;
		}

		public int X {get;set;}
		public int Y {get;set;}
	}
}

