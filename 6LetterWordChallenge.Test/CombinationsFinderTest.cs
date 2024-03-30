using System.Collections.Generic;
using _6LetterWordChallenge.CLI;
using JetBrains.Annotations;
using Moq;
using Xunit;

namespace _6LetterWordChallenge.Test;

[TestSubject(typeof(CombinationsFinder))]
public class CombinationsFinderTest
{
    private readonly CombinationsFinder _combinationsFinder;
    private readonly Mock<IWordRepository> _mockWordRepo;

    public CombinationsFinderTest()
    {
        _mockWordRepo = new Mock<IWordRepository>();
        _mockWordRepo.Setup(repo => repo.GetByLength(It.IsAny<int>())).Returns([]);

        var finalWords = new HashSet<string> { "flight", "sunset" };
        _mockWordRepo.Setup(repo => repo.GetByLength(6)).Returns(finalWords);

        _combinationsFinder = new CombinationsFinder(_mockWordRepo.Object, 6);
    }

    [Fact]
    public void FindCombinations_WithNoPossibleCombinations_ShouldReturnEmptyList()
    {
        // Arrange
        _mockWordRepo.Setup(repo => repo.GetByLength(4))
            .Returns(["wood", "food"]);
        _mockWordRepo.Setup(repo => repo.GetByLength(2))
            .Returns(["ie"]);

        // Act
        var result = _combinationsFinder.FindCombinations();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void FindCombinations_WithPossibleCombinations_ShouldReturnCorrectCombinations()
    {
        // Arrange
        _mockWordRepo.Setup(repo => repo.GetByLength(1))
            .Returns(["f", "s", "r"]);
        _mockWordRepo.Setup(repo => repo.GetByLength(5))
            .Returns(["light"]);

        _mockWordRepo.Setup(repo => repo.GetByLength(3))
            .Returns(["sun", "set", "lit"]);

        // Act
        var result = _combinationsFinder.FindCombinations();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, cw => cw.Value == "flight");
        Assert.Contains(result, cw => cw.Value == "sunset");
    }
}