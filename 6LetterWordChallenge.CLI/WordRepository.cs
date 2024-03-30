namespace _6LetterWordChallenge.CLI;

public interface IWordRepository
{
    HashSet<string> GetByLength(int length);
}

public class WordRepository : IWordRepository
{
    private readonly Dictionary<int, HashSet<string>> _wordsDict = new();

    public WordRepository(IEnumerable<string> words)
    {
        foreach (var word in words)
            Add(word);
    }

    public HashSet<string> GetByLength(int length)
    {
        _wordsDict.TryGetValue(length, out var value);
        return value ?? [];
    }

    private void Add(string word)
    {
        _wordsDict.TryGetValue(word.Length, out var value);

        if (value is null)
            _wordsDict[word.Length] = [word];
        else
            value.Add(word);
    }
}