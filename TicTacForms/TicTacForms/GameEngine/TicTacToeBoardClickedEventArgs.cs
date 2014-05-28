using System;

namespace TicTacToe
{
	public class TicTacToeBoardClickedEventArgs : System.EventArgs
	{
		protected int boardPosition;

		public TicTacToeBoardClickedEventArgs(int position) :base()
		{
			boardPosition = position;
		}

		public int BoardPosition
		{
			get { return boardPosition; }
		}
	}
}

