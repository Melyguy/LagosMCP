using LagosMcp.Services;
using Xunit;

namespace LagosMcp.Tests;

public class BuzzwordTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void GeneratesNonEmptyOutputForEveryIntensity(int intensity)
    {
        var result = BuzzwordGenerator.Generate("going to the gym", intensity, seed: 1);
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Fact]
    public void MaxIntensityMentionsMcp()
    {
        var result = BuzzwordGenerator.Generate("ordering pizza", 5, seed: 42);
        Assert.Contains("MCP", result);
    }

    [Fact]
    public void IntensityOneStaysRestrained()
    {
        var result = BuzzwordGenerator.Generate("ordering pizza", 1, seed: 42);
        Assert.DoesNotContain("MCP", result);
        Assert.Equal("Maybe we could automate ordering pizza.", result);
    }

    [Fact]
    public void SameSeedProducesSameOutput()
    {
        var a = BuzzwordGenerator.Generate("making a website", 5, seed: 7);
        var b = BuzzwordGenerator.Generate("making a website", 5, seed: 7);
        Assert.Equal(a, b);
    }

    [Fact]
    public void DifferentSeedsProduceDifferentOutput()
    {
        var a = BuzzwordGenerator.Generate("making a website", 5, seed: 1);
        var b = BuzzwordGenerator.Generate("making a website", 5, seed: 2);
        Assert.NotEqual(a, b);
    }

    [Fact]
    public void IntensityIsClamped()
    {
        Assert.Equal(BuzzwordGenerator.MaxIntensity, BuzzwordGenerator.Clamp(99));
        Assert.Equal(BuzzwordGenerator.MinIntensity, BuzzwordGenerator.Clamp(-4));
    }

    [Fact]
    public void ClampedHighIntensityStillGenerates()
    {
        var result = BuzzwordGenerator.Generate("sleeping", 99, seed: 3);
        Assert.Contains("MCP", result);
    }

    [Fact]
    public void TrailingPunctuationIsTidied()
    {
        var result = BuzzwordGenerator.Generate("doing the dishes.", 1, seed: 1);
        Assert.Equal("Maybe we could automate doing the dishes.", result);
    }

    [Fact]
    public void BlankTopicIsRejected()
    {
        Assert.ThrowsAny<ArgumentException>(() => BuzzwordGenerator.Generate("   "));
    }
}

public class TextTests
{
    [Theory]
    [InlineData("ordering pizza", "ordering-pizza")]
    [InlineData("  Making a Website!  ", "making-a-website")]
    [InlineData("ae o ae", "ae-o-ae")]
    public void SlugifyProducesLowercaseDashedSlugs(string input, string expected)
    {
        Assert.Equal(expected, Text.Slugify(input));
    }

    [Fact]
    public void SlugifyFallsBackForPunctuationOnlyInput()
    {
        Assert.Equal("thing", Text.Slugify("!!!"));
    }

    [Fact]
    public void ShortenKeepsShortTextIntact()
    {
        Assert.Equal("short", Text.Shorten("short", 20));
    }

    [Fact]
    public void ShortenCutsOnWordBoundary()
    {
        var result = Text.Shorten("the quick brown fox jumps over the lazy dog", 20);
        Assert.True(result.Length <= 20);
        Assert.DoesNotContain("jumps", result);
    }
}
