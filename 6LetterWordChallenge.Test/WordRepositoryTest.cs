using System.Collections.Generic;
using System.Linq;
using _6LetterWordChallenge.CLI;
using JetBrains.Annotations;
using Xunit;

namespace _6LetterWordChallenge.Test;

[TestSubject(typeof(WordRepository))]
public class WordRepositoryFact
{
    [Fact]
    public void Constructor_WithNoWords_ShouldInitializeEmptyRepository()
    {
        // Arrange
        var repo = new WordRepository([]);

        // Act
        var result = repo.GetByLength(1);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Constructor_WithWords_ShouldAddWordsToRepository()
    {
        // Arrange
        var words = new List<string> { "test", "codes" };
        var repo = new WordRepository(words);

        // Act
        var result4 = repo.GetByLength(4);
        var result5 = repo.GetByLength(5);

        // Assert
        Assert.Single(result4);
        Assert.Single(result5);
        Assert.Equal("test", result4.First());
        Assert.Equal("codes", result5.First());
    }

    [Fact]
    public void GetByLength_WithNoMatchingWords_ShouldReturnEmptyList()
    {
        // Arrange
        var words = new List<string> { "hello" };
        var repo = new WordRepository(words);

        // Act
        var result = repo.GetByLength(4);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetByLength_WithMatchingWords_ShouldReturnCorrectWords()
    {
        // Arrange
        var words = new List<string> { "test", "demo", "hello" };
        var repo = new WordRepository(words);

        // Act
        var result4 = repo.GetByLength(4);
        var result5 = repo.GetByLength(5);

        // Assert
        Assert.Equal(2, result4.Count);
        Assert.Single(result5);
        Assert.Contains("test", result4);
        Assert.Contains("demo", result4);
        Assert.Contains("hello", result5);
    }
}