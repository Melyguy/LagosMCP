namespace LagosMcp.Services;

/// <summary>
/// Deterministic randomness. Every generator accepts an optional seed so the
/// output is reproducible in tests (and so we can claim "deterministic
/// orchestration" without lying).
/// </summary>
public static class Rng
{
    public static Random Create(int? seed) =>
        seed is null ? Random.Shared : new Random(seed.Value);

    public static T Pick<T>(this Random rng, IReadOnlyList<T> items) =>
        items[rng.Next(items.Count)];

    /// <summary>Picks a value different from <paramref name="avoid"/> when possible.</summary>
    public static T PickDistinct<T>(this Random rng, IReadOnlyList<T> items, T avoid)
    {
        if (items.Count < 2)
        {
            return items[0];
        }

        T candidate;
        do
        {
            candidate = items[rng.Next(items.Count)];
        }
        while (EqualityComparer<T>.Default.Equals(candidate, avoid));

        return candidate;
    }
}

public static class Text
{
    /// <summary>Trims whitespace and trailing punctuation so sentences compose cleanly.</summary>
    public static string Tidy(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        return value.Trim().TrimEnd('.', '!', '?', ',', ';', ':').Trim();
    }

    public static string Slugify(string value)
    {
        var chars = value.Trim().ToLowerInvariant()
            .Select(c => char.IsLetterOrDigit(c) ? c : '-')
            .ToArray();

        var slug = new string(chars);

        while (slug.Contains("--", StringComparison.Ordinal))
        {
            slug = slug.Replace("--", "-", StringComparison.Ordinal);
        }

        slug = slug.Trim('-');
        return slug.Length == 0 ? "thing" : slug;
    }

    /// <summary>Clamps text to a readable length without breaking mid-word where avoidable.</summary>
    public static string Shorten(string value, int maxLength)
    {
        var text = value.Trim();
        if (text.Length <= maxLength)
        {
            return text;
        }

        var cut = text.LastIndexOf(' ', Math.Min(maxLength, text.Length - 1));
        return cut > 20 ? text[..cut] : text[..maxLength];
    }
}
