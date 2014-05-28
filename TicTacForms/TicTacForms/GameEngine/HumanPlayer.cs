using System;

namespace TicTacToe
{
	/// <summary>
	/// This class represents a Human Player 
	/// </summary>
	public class HumanPlayer : Player
	{
		// HACK:       protected TicTacToeForm ticTacToeForm;

		protected bool alreadyMoved = false;

		public HumanPlayer(string name, Board.Pieces p)// HACK:       , TicTacToeForm tttf)
			: base(name, p)
		{

			// HACK:                   this.ticTacToeForm = tttf;

		}


		/// <summary>
		/// Make a move.  Waits for the player to double click a square 
		/// and then triggers the PlayerMoved Event
		/// </summary>
		/// <param name="gameBoard"></param>
		public override void Move(object gameBoard)
		{

			// start listening to clicks
			//HACK:            ticTacToeForm.SquareDoubleClicked += new SquareDoubleClickHandler(SquareDoubleClicked);

			// now wait until the user clicks
//            while (!alreadyMoved)
//              ;

			// reset the flag
			alreadyMoved = false;
			// raise the PlayerMovedEvent
			OnPlayerMoved();
		}


		// when a user double clicks a square on the TicTacToeForm this method receives the 
		// event message
		// the current move is set and the alreadyMoved flag is set to true so that the 
		// which breaks the while loop in the Move method
		void SquareClicked(object sender, TicTacToeBoardClickedEventArgs args)
		{
			// unregister the double clicked event
			//HACK:            ticTacToeForm.SquareDoubleClicked -= SquareDoubleClicked;

			currentMove = new TicTacToeMove(args.BoardPosition, this.PlayerPiece);
			alreadyMoved = true;
		}
	}
}