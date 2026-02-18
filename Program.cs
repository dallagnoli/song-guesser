using System;
using System.Collections.Generic;
using System.IO;

string filePath = "list.txt";
string secretSong = "";

if (File.Exists(filePath))
{
    string[] lines = File.ReadAllLines(filePath);
    if (lines.Length == 0)
    {
        Console.WriteLine("Error: The list.txt file is empty.");
        return;
    }
    Random rng = new Random();
    secretSong = lines[rng.Next(lines.Length)].Trim();
}
else
{
    Console.WriteLine("Error: list.txt not found!");
    return;
}

int lives = 8;

char[] displaySong = new char[secretSong.Length];
for (int i = 0; i < secretSong.Length; i++)
{
    displaySong[i] = char.IsLetter(secretSong[i]) ? '_' : secretSong[i];
}

List<char> wrongGuesses = new List<char>();

// 2. Start Menu
Console.Clear();
Console.WriteLine("==============================");
Console.WriteLine("         SONG GUESSER         ");
Console.WriteLine("==============================");
Console.WriteLine("Press any key to start!");
Console.WriteLine("(Press Ctrl+Q at any time to quit)");

ConsoleKeyInfo startKey = Console.ReadKey(true);

if (startKey.Key == ConsoleKey.Q && (startKey.Modifiers & ConsoleModifiers.Control) != 0)
{
    Console.WriteLine("\nExited before starting.");
    return;
}

while (lives > 0 && new string(displaySong).Contains('_'))
{
    Console.WriteLine("\n------------------------------");
    Console.WriteLine($"SONG:  {string.Join(" ", displaySong)}");
    Console.WriteLine($"Lives: {lives} | Misses: {string.Join(", ", wrongGuesses)}");
    Console.Write("Guess a letter (or 'Enter' for full title): ");

    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

    if (keyInfo.Key == ConsoleKey.Q && (keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
    {
        Console.WriteLine($"\n\nQuitting... The song was: {secretSong}");
        return;
    }

    if (keyInfo.Key == ConsoleKey.Enter)
    {
        Console.Write("\n[FULL SONG GUESS]: ");
        string songGuess = Console.ReadLine()?.ToLower() ?? "";
        if (songGuess == secretSong.ToLower())
        {
            displaySong = secretSong.ToCharArray();
            break;
        }
        else
        {
            Console.WriteLine(">> Incorrect title!");
            lives--;
            continue;
        }
    }

    char guess = char.ToLower(keyInfo.KeyChar);
    if (!char.IsLetter(guess)) continue;

    if (new string(displaySong).ToLower().Contains(guess) || wrongGuesses.Contains(guess))
    {
        Console.WriteLine($"\n>> You already tried '{guess}'!");
        continue;
    }

    if (secretSong.ToLower().Contains(guess))
    {
        Console.WriteLine($"\n>> Yes! '{guess}' is in the song title.");
        for (int i = 0; i < secretSong.Length; i++)
        {
            if (char.ToLower(secretSong[i]) == guess)
                displaySong[i] = secretSong[i];
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
if (!new string(displaySong).Contains('_'))
{
    Console.WriteLine("    YOU'RE A ROCKSTAR!    ");
    Console.WriteLine($"Song: {secretSong}");
}
else
{
    Console.WriteLine("    OUT OF LIVES...       ");
    Console.WriteLine($"The song was: {secretSong}");
}
Console.WriteLine("==============================\n");
