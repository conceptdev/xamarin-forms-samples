using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TicTacToe
{
    /// <summary>
    /// This class represents a tic-tac-toe board
    /// It is Cloneable so that we can copy board configurations when searching for a move
    /// </summary>
	public class Board //: ICloneable
    {
        public enum Pieces { X, O, Empty };


        public const int COLUMNS = 3;
        public const int ROWS = 3;
        protected const int WINNING_LENGTH = 3;

        public bool haveWinner;  // do we have a winner?

        
        protected Pieces winningPiece;  

       

        protected int[,] board; // a two-dimensional array representing the game board


        /// <summary>
        /// Constructs a new board from a previous game state.
        /// The game state conventions are as follows:
        /// the first index indicates the board row, the second index represents 
        /// the column.  For example, gameState[1,2] represents the 2nd row and third column
        /// of the board.
        /// A value of 0 indicates an open square on the board
        /// A value of 1 indicates an 'X' on the board
        /// A value of 2 indicates an 'O' on the board
        /// 
        /// </summary>
        /// <param name="gameState">a two-dimensional array representing the game state</param>
        public Board(int[,] gameState) : this()
        {
            for (int i = 0; i <= gameState.GetUpperBound(0); i++)
                for (int j = 0; j <= gameState.GetUpperBound(1); j++)
                {
                    this.board[i, j] = gameState[i, j];
                }
        }


        /// <summary>
        /// Constucts an empty board
        /// </summary>
        public Board()
        {
            board = new int[ROWS, COLUMNS];
        }


        /// <summary>
        /// Returns the winner's piece (an 'X' or an 'O')
        /// </summary>
        public Pieces WinningPiece
        {
            get { return winningPiece; }
            set { winningPiece = value; }
        }


        /// <summary>
        /// Returns the number of rows in the game board
        /// </summary>
        public int Rows
        {
            get { return ROWS; }

        }


        /// <summary>
        /// Returns the number of columns in the game board
        /// </summary>
        public int Columns
        {
            get { return COLUMNS; }
        }




        /// <summary>
        /// Returns true if the position is on the board and currently not occupied by 
        /// an 'X' or an 'O'.  Position 0 is the upper-left most square and increases row-by-row
        /// so that first square in the second row is position 3 and position and position 8
        /// is the 3rd square in the 3rd row
        /// 
        /// 0 1 2 
        /// 3 4 5
        /// 6 7 8
        /// 
        /// </summary>
        /// <param name="position">The position to test</param>
        /// <returns></returns>
        public bool IsValidSquare(int position)
        {
            Point p = GetPoint(position);

            if (p.X >= 0 && p.X < ROWS && p.Y >= 0 && p.Y < COLUMNS && IsPositionOpen(p.X, p.Y))
                return true;

            return false;
        }



       

        public bool IsOnBoard(int position)
        {
            return IsOccupied(position) || IsValidSquare(position);
        }

        public void UndoMove(TicTacToeMove m)
        {

            
            if (!IsOnBoard(m.Position))
                throw new InvalidMoveException("Can't undo a move on an invalid square!");

            // just reset the position
            Point p = GetPoint(m.Position);
            board[p.X, p.Y] = 0;
        }


        /// <summary>
        /// Make a move on the board
        /// </summary>
        /// <param name="position">the board position to take</param>
        /// <param name="piece"></param>
        public void MakeMove(int position, Pieces piece)
        {
            if (!IsValidSquare(position))
                throw new InvalidMoveException();

            int pieceNumber = GetPieceNumber(piece);

            Point point = GetPoint(position);

            board[point.X, point.Y] = pieceNumber;
        }


        public int[] OpenPositions
        {
            get
            {
                List<int> positions = new List<int>();
                for (int i = 0; i < board.Length; i++)
                {
                    if (!IsOccupied(i))
                    {
                        positions.Add(i);
                    }
                }

                return positions.ToArray();
            }
        }



        /// <summary>
        /// Retries the Piece at the corresponding row and column on the board
        /// </summary>
        /// <param name="row">The row on the board (0-2)</param>
        /// <param name="column">The column (0-2)</param>
        /// <returns></returns>
        public Pieces GetPieceAtPoint(int row, int column)
        {
            return GetPieceAtPosition(GetPositionFromPoint(new Point(row, column)));
        }

        /// <summary>
        /// Returns the piece at the given board position
        /// </summary>
        /// <param name="position">A number representing the board position.
        /// 0 1 2
        /// 3 
        /// 4 5
        /// 6 7 9</param>
        /// <returns>The piece at the position</returns>
        public Pieces GetPieceAtPosition(int position)
        {
            if (!IsOccupied(position))
                return Pieces.Empty;

            if (GetBoardPiece(position) == 1)
                return Pieces.X;
            else
                return Pieces.O;
        }



        /// <summary>
        /// Checks the board to see if there is a winner
        /// </summary>
        /// <returns>True if there is a winner or false otherwise</returns>
        public bool HasWinner()
        {
            for (int i = 0; i < board.Length; i++)
                if (IsWinnerAt(i))
                {
                    
                    haveWinner = true;
                    SetWinnerAtPosition(i);
                    return true;
                }

            return false;
        }


        public static bool IsValidPosition (int position)
        {
            return position >= 0 && position < Board.COLUMNS * Board.ROWS;
        }

        private void InitBoard()
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    board[i, j] = 0;

        }

        // maps a board position number to a point containing 
        // the row in the x value and the column in the y value
        protected Point GetPoint(int position)
        {
            Point p = new Point();

            p.X = position / COLUMNS;
            p.Y = position % ROWS;

            return p;
        }


        // gets the internal representation of the
        // piece
        protected int GetPieceNumber(Pieces p)
        {
            if (p == Pieces.O)
                return 2;
            else
                return 1;
        }


        // returns the position number given the row and colum
        // p.X is the row
        // p.Y is the column
        protected int GetPositionFromPoint(Point p)
        {
            return p.X * Columns + p.Y;
        }


        private void SetWinnerAtPosition(int position)
        {
            // get the piece at i

            WinningPiece = GetPieceAtPosition(position);
        }



        private int GetBoardPiece(int position)
        {
            Point p = GetPoint(position);

            return board[p.X, p.Y];
        }


        // is the position available
        private bool IsPositionOpen(int row, int col)
        {

            return board[row, col] == 0;

        }


        private bool IsOccupied(int position)
        {
            Point p = GetPoint(position);
            return IsOccupied(p.X, p.Y);
        }


        private bool IsOccupied(int row, int col)
        {
            return !IsPositionOpen(row, col);
        }



        // helper method for checking for a winner
        private bool IsWinnerAt(int position)
        {

            // check each position for winner to the right
            if (IsWinnerToTheRight(position) || IsWinnerFromTopToBottom(position)
                || IsWinnerDiagonallyToRightUp(position) || IsWinnerDiagonallyToRightDown(position))
                return true;
            else
                return false;
        }


        // checks for a winner in the diagonal starting from 
        // the bottom-left corner of the board to the upper-right corner
        private bool IsWinnerDiagonallyToRightUp(int position)
        {
           
            if (!IsOccupied(position))
                return false;

            Pieces piece = GetPieceAtPosition(position);

            Point last = GetPoint(position);
            for (int i = 1; i < WINNING_LENGTH; i++)
            {
                last.X -= 1;
                last.Y += 1;

                if (!IsPointInBounds(last))
                    return false;

                if (piece != GetPieceAtPosition(GetPositionFromPoint(last)))
                    return false;

            }
            return true;

        }

        /// <summary>
        /// Returns true if there is a winner or there is a draw
        /// </summary>
        /// <returns></returns>
        public bool IsGameOver()
        {
            return HaveWinner() || IsDraw();
        }


        /// <summary>
        /// returns true if there is a winner
        /// </summary>
        /// <returns></returns>
        public bool HaveWinner()
        {
            return HasWinner();
        }


        /// <summary>
        /// Returns the piece representing the opponent
        /// </summary>
        /// <param name="yourPiece">The piece representing the player</param>
        /// <returns></returns>
        public static Board.Pieces GetOponentPiece(Board.Pieces yourPiece)
        {
            if (yourPiece == Board.Pieces.X)
                return Board.Pieces.O;
            else if (yourPiece == Board.Pieces.O)
                return Board.Pieces.X;
            else
                throw new Exception("Invalid Piece!");
        }

        /// <summary>
        /// Checks the board configuration to see if it is currently a draw.
        /// A draw occurs when all the positions are full and there isn't a winner.
        /// </summary>
        /// <returns>returns true if there is a draw and false otherwise</returns>
        public bool IsDraw()
        {
            if (HasWinner())
                return false;

            for (int i = 0; i < board.Length; i++)
            {
                if (!IsOccupied(i))
                    return false;
            }

            return true;
        }


        // checks to see if the row and column are on the board
        // p.X is the row, p.Y is the column
        private bool IsPointInBounds(Point p)
        {
            if (p.X < 0 || p.X >= Rows || p.Y < 0 || p.Y >= Columns)
                return false;

            return true;
        }


        // checks for a winner diagonally from the specified position
        // to the right
        private bool IsWinnerDiagonallyToRightDown(int position)
        {
            Point p = GetPoint(position);

            if (!IsOccupied(position))
                return false;

            Pieces piece = GetPieceAtPosition(position);

            // keep moving diagonally until we reach the winningLength
            // or we don't see the same piece
            Point last = GetPoint(position);
            for (int i = 1; i < WINNING_LENGTH; i++)
            {
                last.X += 1;
                last.Y += 1;
                if (!IsPointInBounds(last))
                    return false;

                if (piece != GetPieceAtPosition(GetPositionFromPoint(last)))
                    return false;

            }

            return true;
        }

        // checks for winner from top to bottom starting the the 
        // specified position
        private bool IsWinnerFromTopToBottom(int position)
        {
            Point p = GetPoint(position);

            // check if we even have a square here
            if (!IsOccupied(position))
                return false;

            // do we have the room to go down from here?
            if (p.X + WINNING_LENGTH - 1 >= ROWS)
                return false;


            Pieces piece = GetPieceAtPosition(position);

            // if we get here then we know we at least have
            // the potential for a winner from top to bottom
            for (int i = 1; i < WINNING_LENGTH; i++)
            {
                if (piece != GetPieceAtPosition(position + 3 * i))
                    return false;

            }

            return true;
        }


        // checks for a winner from the specified position to the right
        private bool IsWinnerToTheRight(int position)
        {
            Point p = GetPoint(position);

            // check if we even the square is occupied
            // if it's not then we don't have a winner 
            // starting here
            if (!IsOccupied(position))
                return false;

            // check if we have room to the right?
            if (p.Y + WINNING_LENGTH - 1 >= COLUMNS)
                return false;

            Pieces piece = GetPieceAtPosition(position);

            for (int i = 1; i < WINNING_LENGTH; i++)
            {
                if (GetPieceAtPosition(position + i) != piece)
                    return false;
            }
            return true;
        }


        
        #region ICloneable Members

        public object Clone()
        {
            Board b = new Board(this.board);
            return b;

        }

        #endregion
    }




}

