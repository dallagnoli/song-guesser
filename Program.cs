using System;
using System.Collections.Generic;
using System.IO;

string filePath = "list.txt";
string secretWord = "";

// 1. Load Data
if (File.Exists(filePath))
{
    string[] words = File.ReadAllLines(filePath);
    if (words.Length == 0)
    {
        Console.WriteLine("Error: The list.txt file is empty.");
        return;
    }
    Random rng = new Random();
    secretWord = words[rng.Next(words.Length)].Trim();
}
else
{
    Console.WriteLine("Error: list.txt not found!");
    return;
}

int lives = 8;
char[] displayWord = new string('_', secretWord.Length).ToCharArray();
List<char> wrongGuesses = new List<char>();

Console.Clear();
Console.WriteLine("==============================");
Console.WriteLine("        WORD GUESSER          ");
Console.WriteLine("==============================");
Console.WriteLine("Press any key to start!");
Console.WriteLine("(Press Ctrl+Q at any time to quit)");

ConsoleKeyInfo startKey = Console.ReadKey(true);

if (startKey.Key == ConsoleKey.Q && (startKey.Modifiers & ConsoleModifiers.Control) != 0)
{
    Console.WriteLine("\nExited before starting.");
    return;
}

while (lives > 0 && new string(displayWord).Contains('_'))
{
    Console.WriteLine("\n------------------------------");
    Console.WriteLine($"Word:  {string.Join(" ", displayWord)}");
    Console.WriteLine($"Lives: {lives} | Misses: {string.Join(", ", wrongGuesses)}");
    Console.Write("Guess: ");

    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

    if (keyInfo.Key == ConsoleKey.Q && (keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
    {
        Console.WriteLine($"\n\nQuitting... The word was: {secretWord}");
        return;
    }

    if (keyInfo.Key == ConsoleKey.Enter)
    {
        Console.Write("\n[FULL WORD GUESS]: ");
        string wordGuess = Console.ReadLine()?.ToLower() ?? "";
        if (wordGuess == secretWord.ToLower())
        {
            displayWord = secretWord.ToCharArray();
            break;
        }
        else
        {
            Console.WriteLine(">> Incorrect word!");
            lives--;
            continue;
        }
    }

    char guess = char.ToLower(keyInfo.KeyChar);
    if (!char.IsLetter(guess)) continue;

    if (new string(displayWord).ToLower().Contains(guess) || wrongGuesses.Contains(guess))
    {
        Console.WriteLine($"\n>> You already tried '{guess}'!");
        continue;
    }

    if (secretWord.ToLower().Contains(guess))
    {
        Console.WriteLine($"\n>> Yes! '{guess}' is in the word.");
        for (int i = 0; i < secretWord.Length; i++)
        {
            if (char.ToLower(secretWord[i]) == guess)
                displayWord[i] = secretWord[i];
        }
    }
    else
    {
        Console.WriteLine($"\n>> No, '{guess}' is not there.");
        wrongGuesses.Add(guess);
        lives--;
    }
}

Console.WriteLine("\n==============================");
if (!new string(displayWord).Contains('_'))
{
    Console.WriteLine("WINNER!");
    Console.WriteLine($"The word was: {secretWord}");
}
else
{
    Console.WriteLine("GAME OVER");
    Console.WriteLine($"The word was: {secretWord}");
}
Console.WriteLine("==============================\n");
