using System.Text.RegularExpressions;

namespace advent_of_code_2015.Day08;
internal class StringAppraisal
{
    public long GetCodeSize(string line)
    {
        return line.Length;
    }

    public long GetInMemorySize(string line)
    {
        var firstQuoteRemoved = line.Substring(1);
        var secondQuoteRemoved = firstQuoteRemoved.Substring(0, firstQuoteRemoved.Length - 1);
        var hexStripped = Regex.Replace(secondQuoteRemoved, @"\\x[a-f0-9][a-f0-9]", "H   ");
        var escapeEscapeStripped = Regex.Replace(hexStripped, @"\\\\", "S ");
        var escapeQuoteStripped = Regex.Replace(escapeEscapeStripped, @"\\""", "Q ");

        var memoryLength = escapeQuoteStripped.Replace(" ", String.Empty).Length;

        return memoryLength;
    }

    public long GetEncodedSize(string line)
    {
        var escapesEscaped = line.Replace(@"\", @"\\");
        var quotesEscaped = escapesEscaped.Replace(@"""", @"\""");
        var quotesAdded = @"""" + quotesEscaped + @"""";

        var length = quotesAdded.Length;
        return length;
    }
}
