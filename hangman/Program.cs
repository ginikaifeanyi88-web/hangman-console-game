// Random word generator external library
using CrypticWizard.RandomWordGenerator;
using System.Text.RegularExpressions;
using static CrypticWizard.RandomWordGenerator.WordGenerator; 

// Main program namespace
namespace hangman
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            WordGenerator myWordGenerator = new WordGenerator();
            string word = myWordGenerator.GetWord(PartOfSpeech.noun);
            int lives = 6;
            List<char> selectedLetters = new List<char>();
            string guessedLetters = "";
            playHangman(word, lives, selectedLetters, guessedLetters);
        }

        // Main game logic
        private static void playHangman(string word, int lives, List<char> selectedLetters, string guessedLetters)
        {
            while (true)
            {
                string hiddenWord = "";
                bool result = false;
                bool isAChar = false;
                bool onlyContainsLetters = false;
                char newLetterChar = '-';
                string updateMessage = "";

                // while loop that verifies if the input is of the char format and only contains alphabetical characters
                while (result == false)
                {
                    Console.WriteLine("enter a single letter and try and guess the word!");
                    string newLetterString = Console.ReadLine();
                    isAChar = Char.TryParse(newLetterString, out newLetterChar);
                    onlyContainsLetters = Regex.IsMatch(newLetterString, @"^[a-zA-Z]+$");
                    if (isAChar == false || onlyContainsLetters == false)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }

                // updates lives and updates message saying if player guessed correctly or not
                updateMessage = returnUpdateMessage(word, newLetterChar, updateMessage);
                lives = returnLives(lives, word, newLetterChar);
                // adds selected letter to guessed letters
                if (selectedLetters.Contains(newLetterChar))
                {
                    
                } else
                {
                    selectedLetters.Add(newLetterChar);
                }
                
                guessedLetters = createGuessedLettersString(selectedLetters);

                // foreach creates hidden word, adding correctly guessed letters
                foreach (char ch in word)
                {
                    if (selectedLetters.Contains(ch))
                    {
                        hiddenWord += ch;
                    }
                    else
                    {

                        hiddenWord += "_";

                    }

                }

                // If user guessed correctly and still has more than 0 lives
                if (hiddenWord == word && lives > 0)
                {
                    Console.WriteLine(updateMessage);
                    Console.WriteLine($"Lives: {lives}");
                    renderHangman(lives);
                    Console.WriteLine($"The word is {word}");
                    Console.WriteLine("YOU WIN!");
                    break;
                }
                // If user guessed wrongly and has no more lives
                else if (hiddenWord != word && lives == 0)
                {
                    Console.WriteLine(updateMessage);
                    Console.WriteLine($"Lives: {lives}");
                    renderHangman(lives);
                    Console.WriteLine($"The word is {word}");
                    Console.WriteLine("YOU LOSE!");
                    break;
                }

                // If user still has more letters to guess
                Console.WriteLine(updateMessage);
                Console.WriteLine($"Lives: {lives}");
                renderHangman(lives);
                Console.WriteLine(hiddenWord);
                Console.WriteLine($"guesses: {guessedLetters}");
                Console.WriteLine(" ");
            }
        }

        //Renders the stick figure
        private static void renderHangman(int lives)
        {
            if (lives == 6)
            {
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
            }
            if (lives == 5)
            {
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  o   |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
            }

            if (lives == 4)
            {
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  o   |");
                Console.WriteLine("  |   |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
            }

            if (lives == 3)
            {
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  o   |");
                Console.WriteLine(" /|   |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
            }

            if (lives == 2)
            {
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  o   |");
                Console.WriteLine(@" /|\  |");
                Console.WriteLine("      |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
            }

            if (lives == 1)
            {
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  o   |");
                Console.WriteLine(@" /|\  |");
                Console.WriteLine(" /    |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
            }
            if (lives == 0)
            {
                Console.WriteLine("  +---+");
                Console.WriteLine("  |   |");
                Console.WriteLine("  o   |");
                Console.WriteLine(@" /|\  |");
                Console.WriteLine(@" / \   |");
                Console.WriteLine("      |");
                Console.WriteLine("=========");
            }
        }

        // Updates the lives value
        private static int returnLives(int lives, string word, char newLetterChar)
        {
            if (word.Contains(newLetterChar))
            {

            }
            else
            {
                lives--;
            }
            return lives;
        }

        //Returns message indicating if the word contains a letter or not
        private static string returnUpdateMessage(string word, char newLetterChar, string updateMessage)
        {
            if (word.Contains(newLetterChar))
            {
                updateMessage = $"The word contains '{newLetterChar}'";
            }
            else
            {
                updateMessage = $"The word does NOT contain '{newLetterChar}'";
            }
            return updateMessage;
        }

        // Returns string consisiting of already guessed letters
        private static string createGuessedLettersString(List<char> selectedLetters)
        {
            string guessedLetters = "";
            for (int i = 0; i < selectedLetters.Count; i++)
            {
                guessedLetters += " " + selectedLetters[i];
            }
            return guessedLetters;
        }
    }
}
