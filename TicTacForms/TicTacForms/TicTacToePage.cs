using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using TicTacToe;
using System.Collections.Generic;

namespace TicTacForms
{
    class TicTacToePage : ContentPage
    {
		static readonly int NUM = 3;

        Square[,] squares = new Square[NUM, NUM];
        
        StackLayout stackLayout;
        AbsoluteLayout absoluteLayout;
        double squareSize;
        bool isBusy;
		bool xturn = true; // x goes first
		bool Xturn { get { return xturn; } }
		bool Oturn { get { return !xturn; } }

        public TicTacToePage()
        {
            absoluteLayout = new AbsoluteLayout()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer
            {
                TappedCallback = OnSquareTapped
            };

			// create and layout the board
            for (int row = 0; row < NUM; row++)
            {
                for (int col = 0; col < NUM; col++)
                {
                    string text = " ";
                    Square square = new Square(text)
                    {
                        Row = row,
                        Col = col
                    };
                    square.GestureRecognizers.Add(tapGestureRecognizer);

                    squares[row, col] = square;
                    absoluteLayout.Children.Add(square);
                }
            }

            Button button = new Button 
            {
				Text = "Restart",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnRestartButtonClicked;

            stackLayout = new StackLayout
            {
                Children = 
                {
                    button,
                    absoluteLayout
                }
            };
            stackLayout.SizeChanged += OnStackSizeChanged;

            this.Padding = 
                new Thickness (0, Device.OnPlatform (20, 0, 0), 0, 0);
            this.Content = stackLayout;

			SetupGame ();
			LaunchGame ();
        }

        void OnStackSizeChanged(object sender, EventArgs args)
        {
            double width = stackLayout.Width;
            double height = stackLayout.Height;

            if (width <= 0 || height <= 0)
                return;

            stackLayout.Orientation = (width < height) ? StackOrientation.Vertical : StackOrientation.Horizontal;

            squareSize = Math.Min(width, height) / NUM;
            absoluteLayout.WidthRequest = NUM * squareSize;
            absoluteLayout.HeightRequest = NUM * squareSize;
            Font font = Font.BoldSystemFontOfSize(squareSize / 3);

            foreach (View view in absoluteLayout.Children)
            {
                Square square = (Square)view;
                square.Font = font;

                AbsoluteLayout.SetLayoutBounds(square,
                    new Rectangle(square.Col * squareSize, 
                                  square.Row * squareSize, 
                                  squareSize, 
                                  squareSize));
            }
        }

        async void OnSquareTapped(View view, object args)
        {
            if (isBusy)
                return;

            isBusy = true;
            Square tappedSquare = (Square)view;
			await DoTurn (tappedSquare.Row, tappedSquare.Col);
            isBusy = false;
        }

		#region TicTacToe
		protected TicTacToeGame game;
		protected List<Player> players;
		void SetupGame() {
			Player p1 = new HumanPlayer("Joe", Board.Pieces.X);

			// Create a computer player
			// You can create varying degrees of difficulty by creating computer
			// players that build bigger game trees
			// uncomment desired player and comment all other player 2s


			// create a computer player with the default game tree search depth
			Player p2 = new ComputerPlayer("HAL", Board.Pieces.O);

			// Create a computer player that only looks ahead 1 move
			// i.e only considers their immediate move and not any subsequent moves
			// by their opponent. 
			// this is a very poor player
			// Player p2 = new ComputerPlayer(Board.Pieces.X, f, 1);

			// Creates an advanced computer player that looks ahead 5 moves
			// Player p2 = new ComputerPlayer("Advanced HAL", Board.Pieces.X, 5);

			AddPlayer(p1);
			AddPlayer(p2);
		}
		/// <summary>
		/// Gets the game started.  Players must have already been added.
		/// </summary>
		public void LaunchGame()
		{
			if (players.Count != 2)
				throw new Exception ("There must be two players for this game!");

			game = new TicTacToeGame (players [0].PlayerPiece, players [1].PlayerPiece);
		}
		/// <summary>
		/// Add the player as a participant to the game
		/// </summary>
		/// <param name="p">The player to add</param>
		public void AddPlayer(Player p)
		{
			if (players == null)
				players = new List<Player>();

			if (this.players.Count > 2)
				throw new Exception("Must have only 2 players");

			if (players.Count == 1)
			if (players[0].PlayerPiece == p.PlayerPiece)
				throw new Exception("Players must have different board pieces");

			players.Add(p);
		}

		/// <summary>
		/// Remove all the players from the game.
		/// </summary>
		public void RemoveAllPlayers()
		{
			players.Clear();
		}

		TicTacToeMove lastMove;
		protected TicTacToeMove GetMoveForPlayer(Player p)
		{
			lastMove = null;

//			playerThread = new Thread(p.Move);
//			playerThread.Start(game.GameBoard);

//			if (!xturn) { // computer
				p.Move (game.GameBoard);
				// make the change on the board
				lastMove = p.CurrentMove;	
				var row = lastMove.Position / 3;
				var col = lastMove.Position - (row * 3);
				var square = squares [row, col];
				square.Text = xturn ? "X" : "O";
//			}

			// register a listener
//			p.PlayerMoved += new PlayerMovedHandler(player_PlayerMoved);
//
//
//			p.Move (game.GameBoard); // computer only
//
//
//			// lastMove is assigned in the player_PlayerMoved handler
//			while (lastMove == null)
//				;
//
//			// if we get here the player moved
//
//			// unregister the listenter
//			p.PlayerMoved -= player_PlayerMoved;

			// kill the thread
			//playerThread.Abort();

			return p.CurrentMove;

		}

		void player_PlayerMoved(object sender, PlayerMovedArgs args)
		{
			lastMove = args.Move;
		}
		#endregion

		async Task DoTurn(int tappedRow, int tappedCol, uint length = 100)
		{
			var square = squares [tappedRow, tappedCol];
			var position = (tappedRow * 3) + (tappedCol);

			if (game.IsGameOver ()) {
				// game already over, no clicks allowed
			} else {
				if (!String.IsNullOrWhiteSpace (square.Text)) {
					// position already used
				} else {
					square.Text = xturn ? "X" : "O";

					var piece = xturn ? Board.Pieces.X : Board.Pieces.O;

					TicTacToeMove playerMove;
					if (Oturn) {
						// O turn
						playerMove = GetMoveForPlayer(players[1]); // computer
					} else {
						// X turn
						playerMove = new TicTacToeMove (position, piece);
						game.MakeMove (playerMove);
						playerMove = GetMoveForPlayer(players[1]); // computer
					}
					game.MakeMove (playerMove);

					System.Diagnostics.Debug.WriteLine ("Over: " + game.IsGameOver ());

					if (game.IsGameOver ()) {
						if (game.GameBoard.IsDraw())
							await DisplayAlert ("Game Drawn", "There was no winner", "OK", null);
						else
							await DisplayAlert ("Game Over", square.Text + " won", "OK", null);
					}

					xturn = !xturn;
				}
			}
		}
      
		async void OnRestartButtonClicked (object sender, EventArgs args)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;

			foreach (View view in absoluteLayout.Children) {
				Square square = (Square)view;
				square.Text = " ";
			}

			xturn = true;
			LaunchGame (); // reset engine
            button.IsEnabled = true;
        }
    }
}
