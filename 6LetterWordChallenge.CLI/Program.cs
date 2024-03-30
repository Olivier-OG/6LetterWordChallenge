using System.CommandLine;

namespace _6LetterWordChallenge.CLI;

internal static class Program
{
    private static int Main(string[] args)
    {
        var inputOption = new Option<FileInfo>("--file", "The input file to read.");
        var lengthOption = new Option<int>("--length", () => 6, "The combined length of the words.");
        var rootCommand = new RootCommand();
        rootCommand.AddOption(inputOption);
        rootCommand.AddOption(lengthOption);

        rootCommand.SetHandler((file, combinedLength) =>
        {
            try
            {
                var words = File.ReadLines(file.FullName);
                var wordRepo = new WordRepository(words);
                var combinationsFinder = new CombinationsFinder(wordRepo, combinedLength);
                var combinedWords = combinationsFinder.FindCombinations();
                combinedWords.ToList().ForEach(Console.WriteLine);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }, inputOption, lengthOption);

        return rootCommand.Invoke(args);
    }
}