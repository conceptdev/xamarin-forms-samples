using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TicTacToe
{

    /// <summary>
    /// This class represents a Tic-Tac-Toe game board.  It includes logic
    /// to keep track of player turns and assign board squares to a player
    /// </summary>
    public class TicTacToeGame
    {

        public enum Players { Player1, Player2 };

        protected bool isDraw = false;
        protected bool haveWinner;


        protected Board board;

        protected Stack<TicTacToeMove> moves;
        protected Players currentTurn = Players.Player1; // Player 1 goes first 
        protected Board.Pieces player1Piece = Board.Pieces.X; // player 1 uses X  and player 2 uses O by default
        protected Board.Pieces player2Piece = Board.Pieces.O;

        protected Board.Pieces winningPiece = Board.Pieces.Empty;
        protected Players winningPlayer;

        protected bool gameOver = false;


        /// <summary>
        /// Constructs a new TicTacToeGame using the default board pieces for player one and two
        /// </summary>
        public TicTacToeGame() : this(Board.Pieces.X, Board.Pieces.O)
        {

        }


        /// <summary>
        /// Constructs a new TicTacToe game using the specified player's pieces.
        /// 
        /// </summary>
        /// <param name="player1Piece">Player one's piece</param>
        /// <param name="player2Piece">Player two's piece</param>
        public TicTacToeGame(Board.Pieces player1Piece, Board.Pieces player2Piece)
        {
            this.player1Piece = player1Piece;
            this.player2Piece = player2Piece;
            board = new Board();
            moves = new Stack<TicTacToeMove>();
        }

        /// <summary>
        /// Gets the Board associated with this game
        /// </summary>
        public Board GameBoard
        {
            get { return board; }
        }


        /// <summary>
        /// gets number of columns on the board
        /// </summary>
        public int Columns
        {
            get { return board.Columns; }
        }


        /// <summary>
        /// gets the number of rows on the game board
        /// </summary>
        public int Rows
        {
            get { return board.Rows; }
        }


        /// <summary>
        /// If there currently is a winner, this returns the the piece that has
        /// won. Otherwise it returns Pieces.Empty if there is no winner.
        /// </summary>
        public Board.Pieces WinningPiece
        {
            get { return winningPiece; }
        }


        /// <summary>
        /// Returns true if the game is over (if there is a winner or there is a draw)
        /// </summary>
        /// <returns>true if the game is over or false otherwise</returns>
        public bool IsGameOver()
        {
            return board.IsGameOver();
        }

        /// <summary>
        /// Undoes the last move 
        /// </summary>
        public void UndoLastMove()
        {
            TicTacToeMove lastMove = moves.Pop();

            board.UndoMove(lastMove);

            SwapTurns();

        }


        /// <summary>
        /// gets or sets Player 1's game piece
        /// </summary>
        public Board.Pieces Player1Piece
        {
            get { return player1Piece; }
            set { player1Piece = value; }
        }


        /// <summary>
        /// Gets or sets Player 2's game piece
        /// </summary>
        public Board.Pieces Player2Piece
        {
            get { return player2Piece; }
            set { player2Piece = value; }
        }



        /// <summary>
        /// Returns the player for whose turn it is
        /// </summary>
        public Players CurrentPlayerTurn
        {
            get { return this.currentTurn; }
        }


        /// <summary>
        /// Makes the specified move
        /// </summary>
        /// <param name="m">The TicTacToe move to be made</param>
        /// 
        public void MakeMove(TicTacToeMove m)
        {
            MakeMove(m, GetPlayerWhoHasPiece(m.Piece));
        }


        /// <summary>
        /// Makes the move for the specified player
        /// </summary>
        /// <param name="m">The move to make</param>
        /// <param name="p">The player making the move</param>
        public void MakeMove(TicTacToeMove m, Players p)
        {

            if (currentTurn != p)
            {
                throw new InvalidMoveException("You went out of turn!");
            }

            if (!board.IsValidSquare(m.Position))
                throw new InvalidMoveException("Pick a square on the board!");

            board.MakeMove(m.Position, m.Piece);

            moves.Push(m);

            SwapTurns();

        }


        /// <summary>
        /// This should not be called by clients.  This is only for unit testing
        /// </summary>
        /// <param name="position">The position to take</param>
        /// <param name="p">The player who is taking the piece</param>
        public void TakeSquare(int position, Players p)
        {
            if (currentTurn != p)
                throw new InvalidMoveException("You tried to move out of turn!");

            if (!board.IsValidSquare(position))
                throw new InvalidMoveException();

            board.MakeMove(position, GetPlayersPiece(p));

            if (board.HasWinner())
                winningPlayer = currentTurn;


            SwapTurns();



        }


        
        // Returns the game piece for the specified player
        protected Board.Pieces GetPlayersPiece(Players p)
        {

            if (p == Players.Player1)
                return player1Piece;
            else
                return player2Piece;
        }


        // returns the Player who has the specified piece
        protected TicTacToeGame.Players GetPlayerWhoHasPiece(Board.Pieces piece)
        {
            if (piece == player1Piece)
                return Players.Player1;
            else
                return Players.Player2;
        }



 

        


        // Swap whose turn it is.
        // If X just moved we make it O's turn and
        // vice versa
        private void SwapTurns()
        {
            if (currentTurn == Players.Player1)
                currentTurn = Players.Player2;

            else
                currentTurn = Players.Player1;
        }


    }

    /// <summary>
    /// Represents a tic-tac-toe move
    /// </summary>
    public class TicTacToeMove
    {
        
        /// <summary>
        /// Constructs a TicTacToeMove
        /// </summary>
        /// <param name="position">The position to move to</param>
        /// <param name="piece">The piece that is moving</param>
        public TicTacToeMove(int position, Board.Pieces piece)
        {
            this.Position = position;
            this.Piece = piece;
        }


        /// <summary>
        /// gets or sets the position on the board
        /// </summary>
        public int Position { get; set; }


        /// <summary>
        /// Gets or sets the piece making this move
        /// </summary>
        public Board.Pieces Piece {get; set;}
        
    }

    /// <summary>
    /// An Exception representing an invalid move
    /// </summary>
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException()
            : base()
        {
        }

        public InvalidMoveException(string msg)
            : base(msg)
        {
        }
    }
}
    

