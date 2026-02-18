using System.IO;

string filePath = "list.txt";

if (File.Exists(filePath))
{
    string[] words = File.ReadAllLines(filePath);

    if (words.Length == 0)
    {
        Console.WriteLine("Error: The list.txt file is empty. Please add some words to it!");
        return;
    }

    Random rng = new Random();
    int index = rng.Next(words.Length);
    string secretWord = words[index];
    Console.WriteLine($"The secret word is: {secretWord}");
}
else
{
    Console.WriteLine("Error: list.txt not found!");
}
