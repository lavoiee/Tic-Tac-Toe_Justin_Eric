using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public class ConsoleView
    {
        #region ENUMS

        public enum ViewState
        {
            Active,
            PlayerTimedOut, // TODO Track player time on task
            PlayerUsedMaxAttempts
        }

        #endregion

        #region FIELDS
        

        private const int GAMEBOARD_VERTICAL_LOCATION = 13; //3

        private const int POSITIONPROMPT_VERTICAL_LOCATION = 11;
        private const int POSITIONPROMPT_HORIZONTAL_LOCATION = 23;

        private const int MESSAGEBOX_VERTICAL_LOCATION = 3;

        private const int TOP_LEFT_ROW = 3;
        private const int TOP_LEFT_COLUMN = 6;

        private Gameboard _gameboard;
        private ViewState _currentViewStat;

        #endregion

        #region PROPERTIES
        public ViewState CurrentViewState
        {
            get { return _currentViewStat; }
            set { _currentViewStat = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public ConsoleView(Gameboard gameboard)
        {
            _gameboard = gameboard;

            InitializeView();

        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize the console view
        /// </summary>
        public void InitializeView()
        {
            _currentViewStat = ViewState.Active;

            InitializeConsole();
        }

        /// <summary>
        /// configure the console window
        /// </summary>
        public void InitializeConsole()
        {
            ConsoleUtil.WindowWidth = ConsoleConfig.windowWidth;
            ConsoleUtil.WindowHeight = ConsoleConfig.windowHeight;

            Console.BackgroundColor = ConsoleConfig.bodyBackgroundColor;
            Console.ForegroundColor = ConsoleConfig.bodyBackgroundColor;

            ConsoleUtil.WindowTitle = "Knots and Crosses 2.0";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        internal void DisplayPastGameStats()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Thank you for playing the game. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the session timed out screen
        /// </summary>
        public void DisplayTimedOutScreen()
        {
            ConsoleUtil.HeaderText = "Session Timed Out!";
            ConsoleUtil.DisplayReset();

            DisplayMessageBox("It appears your session has timed out.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the maximum attempts reached screen
        /// </summary>
        public void DisplayMaxAttemptsReachedScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Maximum Attempts Reached!";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you are having difficulty entering your");
            sb.Append(" choice. Please refer to the instructions and play again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Displays the Main Menu
        /// </summary>
        /// <returns>enum</returns>
        public MenuOption DisplayGetMenuChoice()
        {
            MenuOption menuChoice = MenuOption.None;

            Console.WriteLine();

            ConsoleUtil.DisplayReset();
            ConsoleUtil.HeaderText = "Main Menu"; ;

            Console.CursorVisible = true;

            //Display the Menu//


            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("*******************************************************************************");
            ConsoleUtil.DisplayMessage(ConsoleUtil.Center(" Main Menu "));
            Console.WriteLine("*******************************************************************************");
            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(

            "\t" + "1. Create Account" + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(
            "\t" + "2. Sign In " + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(
            "\t" + "3. Rules" + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(
            "\t" + "4. Play Round" + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(
            "\t" + "5. View Current Game Results" + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(
            "\t" + "6. View Past Game Results " + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(
            "\t" + "7. Quit" + Environment.NewLine);

            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            ConsoleUtil.DisplayPromptMessage("Select a menu option by number:");

            ConsoleKeyInfo userChoice = Console.ReadKey(true);

            switch (userChoice.KeyChar)
            {
                case '1':
                    menuChoice = MenuOption.CreateAccount;
                    break;
                case '2':
                    menuChoice = MenuOption.SignIn;
                    break;
                case '3':
                    menuChoice = MenuOption.GameRules;
                    break;
                case '4':
                    menuChoice = MenuOption.PlayNewRound;
                    break;
                case '5':
                    menuChoice = MenuOption.ViewCurrentGameResults;
                    break;
                case '6':
                    menuChoice = MenuOption.ViewCurrentGameResults;
                    break;
                case '7':
                    menuChoice = MenuOption.Quit;
                    break;
                default:
                    break;
            }
        
            return menuChoice;
        }

        /// <summary>
        /// Displays the rules of the game
        /// </summary>
        public void DisplayGameRules()
        {
            ConsoleUtil.DisplayReset();
            ConsoleUtil.HeaderText = "Rules for Tic-Tac-Toe";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("*******************************************************************************");
            ConsoleUtil.DisplayMessage(ConsoleUtil.Center("          Rules of the Game"));
            Console.WriteLine("*******************************************************************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            ConsoleUtil.DisplayMessage(
                "\t" + "Two players take turns placing game pieces on the board." + Environment.NewLine +
                "\t" + "The first player to get three of their pieces in a row wins the round." + Environment.NewLine +
                "\t" + "The three pieces can be in a horizontal, vertical, or diagonal line." + Environment.NewLine +
                "\t" + "If neither player is able to get three pieces in a row before the board is full," + Environment.NewLine +
                "\t" + "then a cat's game or a draw occurs." + Environment.NewLine);
        }

        //
        // Consider using a user defined exception to catch when a user attempts to sign in when they are 
        // already signed in.
        //
        private void SignIn()
        {
            
        }

        //
        // Consider using a user defined exception to catch when a user attempts to create an account that 
        // already exists.
        //
        public void CreateAccount()
        {
            try
            {

            }
            catch (NotImplementedException)
            {

            }
            finally
            {
                ConsoleUtil.DisplayReset();
                Console.WriteLine();
                ConsoleUtil.DisplayMessage("This part of the application is still under construction.");
                DisplayContinuePrompt();
            }
        }

        /// <summary>
        /// Inform the player that their position choice is not available
        /// </summary>
        public void DisplayGamePositionChoiceNotAvailableScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Position Choice Unavailable";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you have chosen a position that is already");
            sb.Append(" taken. Please try again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();
            
            ConsoleUtil.HeaderText = "Knots and Crosses V2";
            ConsoleUtil.DisplayReset();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            ConsoleUtil.DisplayMessage("Programing by Eric LaVoie and Justin Smith");
            ConsoleUtil.DisplayMessage("");
            Console.WriteLine();



            sb.Clear();

            sb.AppendFormat("This application is designed to allow two players to play ");
            sb.AppendFormat("a game of Knots & Crosses. The rules can be found in the documentation ");
            sb.AppendFormat("included with the product.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            Console.WriteLine();

            sb.Clear();
            sb.AppendFormat("");
            ConsoleUtil.DisplayMessage(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display a closing screen when the user quits the application.
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.HeaderText = "Knots & Crosses";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using Knots & Crosses.");

            DisplayContinuePrompt();
        }

        public void DisplayGameArea()
        {
            ConsoleUtil.HeaderText = "Current Game Board";
            ConsoleUtil.DisplayReset();

            DisplayGameboard();
            DisplayGameStatus();
        }

        public void DisplayCurrentGameStatus(int roundsPlayed, int playerXWins, int playerOWins, int catsGames)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleUtil.HeaderText = "Current Game Status";
            ConsoleUtil.DisplayReset();

            double playerXPercentageWins = (double)playerXWins / roundsPlayed;
            double playerOPercentageWins = (double)playerOWins / roundsPlayed;
            double percentageOfCatsGames = (double)catsGames / roundsPlayed;

            ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
            ConsoleUtil.DisplayMessage("Rounds for Player X: " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
            ConsoleUtil.DisplayMessage("Rounds for Player O: " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
            ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

            DisplayContinuePrompt();
        }

        public bool DisplayNewRoundPrompt()
        {
            ConsoleUtil.HeaderText = "Continue or Quit";
            ConsoleUtil.DisplayReset();

            return DisplayGetYesNoPrompt("Would you like to play another round?");
        }

        public void DisplayGameStatus()
        {
            StringBuilder sb = new StringBuilder();

            switch (_gameboard.CurrentRoundState)
            {
                case Gameboard.GameboardState.NewRound:
                    //
                    // The new game status should not be an necessary option here
                    //
                    break;
                case Gameboard.GameboardState.PlayerXTurn:
                    DisplayMessageBox("It is currently Player X's turn.");
                    break;
                case Gameboard.GameboardState.PlayerOTurn:
                    DisplayMessageBox("It is currently Player O's turn.");
                    break;
                case Gameboard.GameboardState.PlayerXWin:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    DisplayMessageBox("Player X Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.PlayerOWin:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    DisplayMessageBox("Player O Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.CatsGame:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    DisplayMessageBox("Cat's Game! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                default:
                    break;
            }
        }

        public void DisplayMessageBox(string message)
        {
            string leftMargin = new String(' ', ConsoleConfig.displayHorizontalMargin);
            string topBottom = new String('*', ConsoleConfig.windowWidth - 2 * ConsoleConfig.displayHorizontalMargin);

            StringBuilder sb = new StringBuilder();

            Console.SetCursorPosition(0, MESSAGEBOX_VERTICAL_LOCATION);
            Console.WriteLine(leftMargin + topBottom);

            Console.WriteLine(ConsoleUtil.Center("Game Status"));

            ConsoleUtil.DisplayMessage(message);

            Console.WriteLine(Environment.NewLine + leftMargin + topBottom);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        /// <summary>
        /// display the current game board
        /// </summary>
        /// 

        private void DisplayGameboard()
        {
            //
            // move cursor below header
            //
            
            Console.SetCursorPosition(0, GAMEBOARD_VERTICAL_LOCATION);

            
            Console.Write("\t\t\t        |---+---+---+---|\n");

            for (int i = 0; i < 4; i++)
            {
                Console.Write("\t\t\t        | ");

                for (int j = 0; j < 4; j++)
                {
                    if (_gameboard.PositionState[i, j] == Gameboard.PlayerPiece.None)
                    {
                        Console.Write(" " + " | ");
                    }
                    else
                    {
                        Console.Write(_gameboard.PositionState[i, j] + " | ");
                    }

                }
                
                Console.Write("\n\t\t\t        |---+---+---+---|\n");
                

            }

        }

        /// <summary>
        /// display prompt for a player's next move
        /// </summary>
        /// <param name="coordinateType"></param>
        private void DisplayPositionPrompt(string coordinateType)
        {
            //
            // Clear line by overwriting with spaces
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write(new String(' ', ConsoleConfig.windowWidth));
            //
            // Write new prompt
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write("Enter " + coordinateType + " number: ");
        }

        /// <summary>
        /// Display a Yes or No prompt with a message
        /// </summary>
        /// <param name="promptMessage">prompt message</param>
        /// <returns>bool where true = yes</returns>
        private bool DisplayGetYesNoPrompt(string promptMessage)
        {
            bool yesNoChoice = false;
            bool validResponse = false;
            string userResponse;

            while (!validResponse)
            {
                ConsoleUtil.DisplayReset();

                ConsoleUtil.DisplayPromptMessage(promptMessage + "(y/n)");
                userResponse = Console.ReadLine();

                if (userResponse.ToUpper() == "Y")
                {
                    validResponse = true;
                    yesNoChoice = true;
                }
                else if (userResponse.ToUpper() == "N")
                {
                    validResponse = true;
                    yesNoChoice = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    ConsoleUtil.DisplayMessage(
                        "It appears that you have entered an incorrect response." +
                        " Please enter either \"y\" or \"n\"."
                        );
                    DisplayContinuePrompt();
                }
            }

            return yesNoChoice;
        }

        /// <summary>
        /// Get a player's position choice within the correct range of the array
        /// Note: The ConsoleView is allowed access to the GameboardPosition struct.
        /// </summary>
        /// <returns>GameboardPosition</returns>
        public GameboardPosition GetPlayerPositionChoice()
        {
            //
            // Initialize gameboardPosition with -1 values
            //
            GameboardPosition gameboardPosition = new GameboardPosition(-1, -1);

            //
            // Get row number from player.
            //
            gameboardPosition.Row = PlayerCoordinateChoice("Row");

            //
            // Get column number.
            //
            if (CurrentViewState != ViewState.PlayerUsedMaxAttempts)
            {
                gameboardPosition.Column = PlayerCoordinateChoice("Column");
            }

            return gameboardPosition;

        }

        /// <summary>
        /// Validate the player's coordinate response for integer and range
        /// </summary>
        /// <param name="coordinateType">an integer value within proper range or -1</param>
        /// <returns></returns>
        private int PlayerCoordinateChoice(string coordinateType)
        {
            int tempCoordinate = -1;
            int numOfPlayerAttempts = 1;
            int maxNumOfPlayerAttempts = 4;

            while ((numOfPlayerAttempts <= maxNumOfPlayerAttempts))
            {
                DisplayPositionPrompt(coordinateType);

                if (int.TryParse(Console.ReadLine(), out tempCoordinate))
                {
                    //
                    // Player response within range
                    //
                    if (tempCoordinate >= 1 && tempCoordinate <= _gameboard.MaxNumOfRowsColumns)
                    {
                        return tempCoordinate;
                    }
                    //
                    // Player response out of range
                    //
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3, or 4)");
                    }
                }
                //
                // Player response cannot be parsed as integer
                //
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3, or 4)");
                }

                //
                // Increment the number of player attempts
                //
                numOfPlayerAttempts++;
            }

            //
            // Player used maximum number of attempts, set view state and return
            //
            CurrentViewState = ViewState.PlayerUsedMaxAttempts;
            return tempCoordinate;
        }

        #endregion
    }
}
