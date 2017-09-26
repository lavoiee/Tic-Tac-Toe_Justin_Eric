using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public class GameController
    {
        #region FIELDS
        //
        // track game and round status
        //
        private bool _playingGame;
        private bool _playingRound;

        private int _roundNumber;

        //
        // track the results of multiple rounds
        //
        private int _playerXNumberOfWins;
        private int _playerONumberOfWins;
        private int _numberOfCatsGames;

        //
        // instantiate  a Gameboard object
        // instantiate a GameView object and give it access to the Gameboard object
        //
        private static Gameboard _gameboard = new Gameboard();
        private static ConsoleView _gameView = new ConsoleView(_gameboard);

        #endregion

        #region PROPERTIES



        #endregion

        #region CONSTRUCTORS
        
        
        public GameController()
        {
            DisplayPreSplash();
            DisplaySplashScreen();
            DisplayMenu();
            
        }

        private void DisplayPreSplash()
        {

            Console.SetCursorPosition(30, 20);

            ConsoleUtil.DisplayReset();
            string shifty = new String(' ', 24);
            string shifty2 = new String(' ', 18);
            string shifty3 = new String(' ', 28);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(shifty + @"    __ __             _" );
            Console.WriteLine(shifty+ @"   / //_/____  ____  / /______  ");
            Console.WriteLine(shifty+ @"  / ,<  / __ \/ __ \/ __/ ___/  ");
            Console.WriteLine(shifty+ @" / /| |/ / / / /_/ / /_(__  )   ");
            Console.WriteLine(shifty+ @"/_/ |_/_/ /_/\____/\__/____/   ");
            Console.WriteLine(shifty +@"          ___   ");
            Console.WriteLine(shifty3 + @"     ( _ )      ");
            Console.WriteLine(shifty3 + @"    / __ \/|   ");
            Console.WriteLine(shifty3 + @"   / /_/  <   ");
            Console.WriteLine(shifty3 + @"   \____/\/    ");
            Console.WriteLine(shifty2 +@"    ______ ");
            Console.WriteLine(shifty2 + @"   / ____/________  _____________  _____");
            Console.WriteLine(shifty2 + @"  / /   / ___/ __ \/ ___/ ___/ _ \/ ___/");
            Console.WriteLine(shifty2 + @" / /___/ /  / /_/ (__  |__  )  __(__  ) ");
            Console.WriteLine(shifty2 + @" \____/_/   \____/____/____/\___/____/  ");
            Console.SetCursorPosition(11, 30);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press Enter to continue.");

            Console.CursorVisible = false;
            Console.ReadKey();
            Console.WriteLine();
        }
        

        private void DisplaySplashScreen()
        {
            _gameView.DisplayWelcomeScreen();
        }

        private void DisplayMenu()
        {
            MenuOption menuChoice;

            menuChoice = _gameView.DisplayGetMenuChoice();

            switch (menuChoice)
            {
                case MenuOption.None:
                    break;
                case MenuOption.CreateAccount:
                    _gameView.CreateAccount();
                    DisplayMenu();
                    break;
                case MenuOption.PlayNewRound:
                    InitializeGame();
                    PlayGame();
                    break;
                case MenuOption.SignIn:
                    _gameView.SignIn();
                    DisplayMenu();
                    break;
                case MenuOption.GameRules:
                    _gameView.DisplayGameRules();
                    _gameView.DisplayContinuePrompt();
                    DisplayMenu();
                    break;
                case MenuOption.ViewCurrentGameResults:
                    _gameView.DisplayCurrentGameStatus(_roundNumber, _playerXNumberOfWins, _playerONumberOfWins, _numberOfCatsGames);
                    DisplayMenu();
                    break;
                case MenuOption.ViewPastGameResultsScores:
                    _gameView.DisplayPastGameStats();
                    DisplayMenu();
                    break;
                case MenuOption.SaveGameResults:
                    DisplayMenu();
                    break;
                case MenuOption.Quit:
                    _gameView.DisplayExitPrompt();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize the multi-round game.
        /// </summary>
        public void InitializeGame()
        {
            //
            // Initialize game variables
            //
            _playingGame = true;
            _playingRound = true;
            _roundNumber = 0;
            _playerONumberOfWins = 0;
            _playerXNumberOfWins = 0;
            _numberOfCatsGames = 0;

            //
            // Initialize game board status
            //
            _gameboard.InitializeGameboard();
        }


        /// <summary>
        /// Game Loop
        /// </summary>
        public void PlayGame()
        {
            
 
            while (_playingGame)
            {
                
                //
                // Round loop
                //
                while (_playingRound)
                {
                    
                    //
                    // Perform the task associated with the current game and round state
                    //
                    ManageGameStateTasks();


                    //
                    // Evaluate and update the current game board state
                    //
                    _gameboard.UpdateGameboardState();
                    
                }

                //
                // Round Complete: Display the results
                //
                _gameView.DisplayCurrentGameStatus(_roundNumber, _playerXNumberOfWins, _playerONumberOfWins, _numberOfCatsGames);
                
                //
                // Confirm no major user errors
                //
                if (_gameView.CurrentViewState != ConsoleView.ViewState.PlayerUsedMaxAttempts ||
                    _gameView.CurrentViewState != ConsoleView.ViewState.PlayerTimedOut)
                {
                    //
                    // Prompt user to play another round
                    //
                    if (_gameView.DisplayNewRoundPrompt())
                    {
                        _gameboard.InitializeGameboard();
                        _gameView.InitializeView();
                        _playingRound = true;
                        PlayGame();
                        //DisplayMenu();
                    }
                    else
                    {
                        _playingRound = false;
                        _gameView.DisplayClosingScreen();
                        DisplayMenu();
                    }
                }
                //
                // Major user error recorded, end game
                //
                else
                {
                    _playingGame = false;
                }
            }

            _gameView.DisplayClosingScreen();
        }

        /// <summary>
        /// manage each new task based on the current game state
        /// </summary>
        private void ManageGameStateTasks()
        {
            switch (_gameView.CurrentViewState)
            {
                case ConsoleView.ViewState.Active:

                    _gameView.DisplayGameArea();


                    switch (_gameboard.CurrentRoundState)
                    {
                        case Gameboard.GameboardState.NewRound:
                            _roundNumber++;
                            _gameboard.CurrentRoundState = Gameboard.GameboardState.PlayerXTurn;
                            break;

                        case Gameboard.GameboardState.PlayerXTurn:
                            ManagePlayerTurn(Gameboard.PlayerPiece.X);
                            break;

                        case Gameboard.GameboardState.PlayerOTurn:
                            ManagePlayerTurn(Gameboard.PlayerPiece.O);
                            break;

                        case Gameboard.GameboardState.PlayerXWin:
                            _playerXNumberOfWins++;
                            _playingRound = false;
                            break;

                        case Gameboard.GameboardState.PlayerOWin:
                            _playerONumberOfWins++;
                            _playingRound = false;
                            break;

                        case Gameboard.GameboardState.CatsGame:
                            _numberOfCatsGames++;
                            _playingRound = false;
                            break;


                        default:
                            break;
                    }
                    break;
                case ConsoleView.ViewState.PlayerTimedOut:
                    _gameView.DisplayTimedOutScreen();
                    _playingRound = false;
                    break;
                case ConsoleView.ViewState.PlayerUsedMaxAttempts:
                    _gameView.DisplayMaxAttemptsReachedScreen();
                    _playingRound = false;
                    //_playingGame = false;
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// Attempt to get a valid player move. 
        /// If the player chooses a location that is taken, the CurrentRoundState remains unchanged,
        /// the player is given a message indicating so, and the game loop is cycled to allow the player
        /// to make a new choice.
        /// </summary>
        /// <param name="currentPlayerPiece">identify as either the X or O player</param>
        private void ManagePlayerTurn(Gameboard.PlayerPiece currentPlayerPiece)
        {
            GameboardPosition gameboardPosition = _gameView.GetPlayerPositionChoice();

            if (_gameView.CurrentViewState != ConsoleView.ViewState.PlayerUsedMaxAttempts)
            {
                //
                // player chose an open position on the game board, add it to the game board
                //
                if (_gameboard.GameboardPositionAvailable(gameboardPosition))
                {
                    _gameboard.SetPlayerPiece(gameboardPosition, currentPlayerPiece);
                }
                //
                // player chose a taken position on the game board
                //
                else
                {
                    _gameView.DisplayGamePositionChoiceNotAvailableScreen();
                }
            }
        }

        #endregion
    }
}
