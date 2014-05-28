using System;

namespace TicTacToe
{
	/// <summary>
	/// This class represents a "comuter" player.  
	/// It determines moves using minmax decision rules
	/// </summary>
	public class ComputerPlayer : Player
	{
		public const int DEFAULT_SEARCH_DEPTH = 2;


		/// <summary>
		/// Constructs a new computer player.  The DEFAULT_SEARCH_DEPTH is used
		/// </summary>
		/// <param name="name">The name of the player</param>
		/// <param name="p">The piece this player is using in the came</param>
		public ComputerPlayer(string name, Board.Pieces p) : this(name,
			p, DEFAULT_SEARCH_DEPTH)
		{
		}


		/// <summary>
		/// Constructs a new computer player
		/// </summary>
		/// <param name="name">The name of the player</param>
		/// <param name="p">The piece the player is using</param>
		/// <param name="searchDepth">The depth to search for moves in the game tree.</param>
		public ComputerPlayer(string name, Board.Pieces p, int searchDepth) :base(name, p)
		{
			this.SearchDepth = searchDepth;
		}


		/// <summary>
		/// gets or sets the search depth which is the number of moves
		/// the computer player will look ahead to determine it's move
		/// Greater values yield better computer play
		/// </summary>
		public int SearchDepth { get; set; }



		/// <summary>
		/// Start the computer searching for a move
		/// Clients should listen to the OnPlayerMoved event to be notified 
		/// when the computer has found a move
		/// </summary>
		/// <param name="gameBoard">The current game board</param>
		public override void Move(object gameBoard)
		{
			Board b = (Board)gameBoard;

			//to make things interesting we move randomly if the board we
			//are going first (i.e. the board is empty)
			if (b.OpenPositions.Length == 9)
			{
				this.currentMove = GetRandomMove((Board)gameBoard);
				OnPlayerMoved();
				return;
			}

			Node root = new MaxNode(b, null, null);
			root.MyPiece = this.PlayerPiece;
			root.Evaluator = new EvaluationFunction();
			root.FindBestMove(DEFAULT_SEARCH_DEPTH);


			currentMove = root.BestMove;

			OnPlayerMoved();
		}


		// gets a random move.  this can be used to make game play interesting
		// particularly in the beginning of the game or if you wish to weaken the computer's
		// play by adding randomness.
		protected TicTacToeMove GetRandomMove(Board b)
		{
			int openPositions = b.OpenPositions.Length;
			Random rGen = new Random();

			int squareToMoveTo = rGen.Next(openPositions);

			TicTacToeMove move = new TicTacToeMove(squareToMoveTo, this.PlayerPiece);
			return move;
		}
	}
}