using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuess {
    class Program {
        static int MaxPlayers = 2;

        /// <summary>
        /// Method to have user input their name
        /// </summary>
        /// <param name="playerNumber">Which player is inputting their name?</param>
        /// <returns>Player's name as a string</returns>
        public static string GetPlayerName(int playerNumber) {
            while (true) {
                Console.WriteLine();
                Console.Write("Player {0}: Please enter your name. ", playerNumber);
                string nameInput = Console.ReadLine();
                if (string.IsNullOrEmpty(nameInput)) {
                    Console.WriteLine("Incorrect Input. Please try again.");
                    continue;
                }
                else {
                    return nameInput;
                }
            }
        }

        /// <summary>
        /// Method to have user input their number
        /// </summary>
        /// <param name="playerName">Which player is inputting their number?</param>
        /// <param name="message">Message to display on console</param>
        /// <returns>The integer input by the player</returns>
        public static int GetNumber(string playerName, string message) {
            const int minimumValue = 0;
            const int maximumValue = 100;

            //loop makes sure no bogey values are input
            while (true) {
                Console.WriteLine("{0}: {1}", playerName, message);
                string tempInput = Console.ReadLine();
                int input = 0 ;
                int.TryParse(tempInput,out input);
                if (!(input > minimumValue && input <= maximumValue)) {
                    Console.WriteLine("Incorrect input. Please try again.");
                    continue;
                }
                else {
                    Console.Clear();
                    return input;
                }
            }
        }

        /// <summary>
        /// Handles the game logic
        /// </summary>
        /// <param name="playerNames">All the players that are part of the game</param>
        public static void PlayGame (string[] playerNames) {
            int[] playerGuesses = new int[playerNames.Length];
            const int maxGuesses = 4;
            int incorrectGuesses = 0;

            for (int i=0; i<playerNames.Length; i++) {
                playerGuesses[i] = GetNumber(playerNames[i], "Please guess a number between 0 and 100");
            }

            //Checks if the player's input is too high or too low. Also checks how many tries they have left. Handles the game logic
            while (true) {
                if (maxGuesses - incorrectGuesses < 1) {
                    Console.WriteLine("You ran out of tries. {0} wins. The number was {1}", playerNames[0], playerGuesses[0]);
                    break;
                }

                if (playerGuesses[1] > playerGuesses[0]) {
                    incorrectGuesses += 1;
                    Console.WriteLine("Too High. Please try again. {0} tries left.", maxGuesses - incorrectGuesses);
                    playerGuesses[1] = GetNumber(playerNames[1], "What is your guess? ");
                }

                if (playerGuesses[1] < playerGuesses[0]) {
                    incorrectGuesses += 1;
                    Console.WriteLine("Too Low. Please try again. {0} tries left.", maxGuesses - incorrectGuesses);
                    playerGuesses[1] = GetNumber(playerNames[1], "What is your guess? ");
                }

                if (playerGuesses[1] == playerGuesses[0]) {
                    Console.WriteLine("You guessed correctly, Good Job! Player 2 wins");
                    break;
                }
            }
        }

        static void Main(string[] args) {
            bool play = true;
            while (play) {
                Console.WriteLine("Welcome to the Number Guess app!");
                Console.ReadKey();

                int answer = 0;

                //loop makes sure no bogey values are input
                while (true) {
                    Console.Write("How many players are there? (2) ");
                    ConsoleKeyInfo selectedOption = Console.ReadKey();
                    Console.WriteLine();
                    answer = selectedOption.KeyChar - '0';
                    if (!(answer > 1 && answer <= MaxPlayers)) {
                        Console.WriteLine("Incorrect input. Please try again.");
                        continue;
                    }
                    else {
                        break;
                    }
                }
                string[] playerNames = new string[answer];
                for (int i = 1; i <= answer; i++) {
                    playerNames[i - 1] = GetPlayerName(i);
                }

                PlayGame(playerNames);

                Console.WriteLine();

                Console.Write("Would you like to play again? Type 'y' for yes:  ");
                ConsoleKeyInfo selectedReplay = Console.ReadKey();
                char replay = selectedReplay.KeyChar;
                if (replay == 'y') {
                    Console.Clear();
                    continue;
                }
                else {
                    play = false;
                }
            }
        }
    }
}
