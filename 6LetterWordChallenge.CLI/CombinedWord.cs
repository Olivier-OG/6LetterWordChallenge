namespace _6LetterWordChallenge.CLI;

public class CombinedWord
{
    private CombinedWord(List<string> wordParts)
    {
        Parts = wordParts;
    }

    private List<string> Parts { get; }

    public string Value => string.Join("", Parts);

    public static CombinedWord Create(int combinedLength, List<string> wordParts)
    {
        if (wordParts.Sum(word => word.Length) != combinedLength)
            throw new ArgumentException($"The combined length of the wordParts must be {combinedLength}.");
        return new CombinedWord(wordParts);
    }

    public override string ToString()
    {
        return $"{string.Join("+", Parts)}={Value}";
    }

    private bool Equals(CombinedWord other)
    {
        return Parts.SequenceEqual(other.Parts);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((CombinedWord)obj);
    }

    public override int GetHashCode()
    {
        return Parts.GetHashCode();
    }
}