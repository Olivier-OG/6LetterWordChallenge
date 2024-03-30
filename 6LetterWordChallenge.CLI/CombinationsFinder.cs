namespace _6LetterWordChallenge.CLI;

public class CombinationsFinder(IWordRepository wordRepo, int combinedLength)
{
    public HashSet<CombinedWord> FindCombinations()
    {
        var combinedWords = new HashSet<CombinedWord>();

        var finalWords = wordRepo.GetByLength(combinedLength);

        for (var i = 1; i < combinedLength; i++)
        {
            var firstParts = wordRepo.GetByLength(i);
            if (firstParts.Count == 0) continue;

            var secondParts = wordRepo.GetByLength(combinedLength - i);
            if (secondParts.Count == 0) continue;

            foreach (
                var combinedWord
                in firstParts
                    .SelectMany(firstPart => secondParts
                        .Select(secondPart => CombinedWord.Create(combinedLength, [firstPart, secondPart]))
                        .Where(combinedWord => finalWords.Contains(combinedWord.Value)))
            )
                combinedWords.Add(combinedWord);
        }

        return combinedWords;
    }
}