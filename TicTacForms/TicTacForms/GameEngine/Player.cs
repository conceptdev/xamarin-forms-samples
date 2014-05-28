using System;

namespace TicTacToe
{
    // This delegate is used to respond to moves by a player
    public delegate void PlayerMovedHandler(object sender, PlayerMovedArgs args);


    /// <summary>
    /// This class abstracts the idea of a Player and includes some common functionality.
    /// It includes an event for clients to be notified when a move is made
    /// </summary>
    public abstract class Player
    {
		// Listen for a move made by a player
        public event PlayerMovedHandler PlayerMoved;

      
        protected TicTacToeMove currentMove;
        public Player(string name, Board.Pieces p)
        {
            this.Name = name;
            this.PlayerPiece = p;
        }


        public abstract void Move(object gameBoard);


        public TicTacToeMove CurrentMove
        {
            get { return currentMove; }
        }


        /// <summary>
        /// This is invoked by subclasses to indicate that the player decided on a move
        /// </summary>
        public virtual void OnPlayerMoved()
        {
            if (PlayerMoved != null)
                PlayerMoved(this, new PlayerMovedArgs(currentMove, this));
        }


        /// <summary>
        /// Get or Set the player's piece
        /// </summary>
        public Board.Pieces PlayerPiece { get; set; }

        /// <summary>
        /// Get or set the player's name
        /// </summary>
        public string Name { get; set; }

    }


    /// <summary>
    /// A class for encapuslating a player moved
    /// This is passed along with PlayerMoved events
    /// </summary>
    public class PlayerMovedArgs : System.EventArgs
    {
        protected TicTacToeMove move;
        protected Player player; 


        /// <summary>
        /// Constructs a new PlayerMovedArgs object with the specified Move and Player
        /// </summary>
        /// <param name="m">The move to make</param>
        /// <param name="player">The player making the move</param>
        public PlayerMovedArgs(TicTacToeMove m, Player player)
            : base()
        {
            this.player = player;
            move = m;
            
        }

        public TicTacToeMove Move
        {
            get { return move; }
        }

        public Player Player
        {
            get { return player; }
        }
    }



}