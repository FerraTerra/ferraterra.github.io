namespace Elartkh;

using System.Numerics;
using System.Text;
using System.Collections.Immutable;

class Generator
{
    public const int Base = 0xC0000;
    public const int PunctuationMarks = Base + 0x00;
    public const int Numerals = Base + 0x04;
    public const int Powers = Base + 0x0E;
    public const int PrimaryChars = Base + 0x12;
    public const int SecondaryChars = Base + 0x2A;
    public const int TertiaryChars = Base + 0x30;
    public const int ConsonantalChars = Base + 0x37;
    public const int PlaceholderMarks = Base + 0x4E;
    public const int Diacritics = Base + 0x50;
    public const int GridMark = Base + 0x7F;
    
    public static Rune Punctuation(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x03)
        {
            return new Rune(PunctuationMarks + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x03 range but equal {value}");
    }
    
    public static Rune Numeral(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x09)
        {
            return new Rune(Numerals + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x09 range but equal {value}");
    }
    
    public static Rune Power(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x03)
        {
            return new Rune(Powers + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x03 range but equal {value}");
    }

    public static Rune Primary(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x17)
        {
            return new Rune(PrimaryChars + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x17 range but equal {value}");
    }

    public static Rune Secondary(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x06)
        {
            return new Rune(SecondaryChars + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x06 range but equal {value}");
    }

    public static Rune Tertiary(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x07)
        {
            return new Rune(TertiaryChars + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x07 range but equal {value}");
    }

    public static Rune Consonantal(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x16)
        {
            return new Rune(TertiaryChars + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x16 range but equal {value}");
    }

    public static Rune Placeholder(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x01)
        {
            return new Rune(PlaceholderMarks + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x01 range but equal {value}");
    }

    public static Rune Modificator(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x20)
        {
            return new Rune(Diacritics + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x20 range but equal {value}");
    }
    
    public static Rune Grid(BigInteger value)
    {
        if (value >= 0x00 && value <= 0x00)
        {
            return new Rune(GridMark + (int)value);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 0x00..0x00 range but equal {value}");
    }

    public static IEnumerable<Rune> Digit(BigInteger value)
    {
        if (value >= 1 && value <= 99)
        {
            yield return Numeral((value - 1) % 10);
            yield return Modificator((value - 1) / 10);
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 1..99 range but equal {value}");
    }

    public static IEnumerable<Rune> DigitPair(BigInteger value)
    {
        if (value >= 1 && value <= 9999)
        {
            if (value > 100)
                foreach (var rune in Digit(value / 100))
                    yield return rune;
            if (value % 100 == 0)
                yield return Power(0);
            else
                foreach (var rune in Digit(value % 100))
                    yield return rune;
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 1..9999 range but equal {value}");
    }

    public static IEnumerable<Rune> DigitPairPair(BigInteger value)
    {
        if (value >= 1 && value <= 99999999)
        {
            if (value > 10000)
                foreach (var rune in DigitPair(value / 10000))
                    yield return rune;
            if (value % 10000 == 0)
                yield return Power(1);
            else
                foreach (var rune in DigitPair(value % 10000))
                    yield return rune;
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 1..99999999 range but equal {value}");
    }

    public static IEnumerable<Rune> DigitPairPairPair(BigInteger value)
    {
        if (value >= 1 && value <= 9999999999999999)
        {
            if (value > 100000000)
                foreach (var rune in DigitPairPair(value / 100000000))
                    yield return rune;
            if (value % 100000000 == 0)
                yield return Power(2);
            else
                foreach (var rune in DigitPairPair(value % 100000000))
                    yield return rune;
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 1..9999999999999999 range but equal {value}");
    }

    public static IEnumerable<Rune> DigitPairPairPairPair(BigInteger value)
    {
        if (value >= 1 && value <= BigInteger.Parse("99999999999999999999999999999999"))
        {
            if (value > 10000000000000000)
                foreach (var rune in DigitPairPairPair(value / 10000000000000000))
                    yield return rune;
            if (value % 10000000000000000 == 0)
                yield return Power(3);
            else
                foreach (var rune in DigitPairPairPair(value % 10000000000000000))
                    yield return rune;
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be in 1..99999999999999999999999999999999 range but equal {value}");
    }

    public static IEnumerable<(int, BigInteger)> DigitPairPairPairs(BigInteger value)
    {
        var pow3 =  10000000000000000;
        for (int i = 0; value > 0; i++, value /= pow3)
            if (value % pow3 != 0)
                yield return (i, value % pow3);
    }

    public static IEnumerable<Rune> Natural(BigInteger value)
    {
        if (value >= 1)
        {
            var xlist = ImmutableList<ImmutableList<Rune>>.Empty;
            int preI = -1;
            foreach (var (i, val) in DigitPairPairPairs(value)) {
                var list = ImmutableList<Rune>.Empty;
                foreach (var rune in DigitPairPairPair(val))
                    list = list.Add(rune);
                for (int j = 0; j < i; j++)
                    list = list.Add(Power(3));
                preI = i;
                xlist = xlist.Add(list);
            }
            foreach (var list in xlist.Reverse())
                foreach (var rune in list)
                    yield return rune;
        } else throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(value)} must be natural (better or equal 1) but equal {value}");
    }

    public static string StringNatural(string text)
    {
        var value = BigInteger.Parse(text);
        var builder = new StringBuilder();
        foreach (var rune in Natural(value))
            builder.Append(Char.ConvertFromUtf32(rune.Value));
        return builder.ToString();
    }
}