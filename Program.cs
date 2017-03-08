using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManipulation
{
  class Program
  {
    private const string Input = @"Publication Date,Title,Authors
28/11/2008,Learning C# 3.0,Jesse Liberty & Brian MacDonald
16/09/2013,Head First C#,Jennifer Greene & Andrew Stellman
27/10/2015,Learn C# in One Day and Learn It Well: C# for Beginners with Hands-on Project: Volume 3,Jamie Chan";

    private const string ExpectedOutput =
      @"| Pub Date    |                       Title | Authors                         |
|=============================================================================|
| 28 Nov 2008 |             Learning C# 3.0 | Jesse Liberty & Brian MacDonald |
| 16 Sep 2013 |               Head First C# | Jennifer Greene & Andrew Ste... |
| 27 Oct 2015 | Learn C# in One Day and ... | Jamie Chan                      |
";

    static void Main()
    {
      var output = ConvertToTable(Input);

      Console.WriteLine("Expected:");
      Console.WriteLine(ExpectedOutput);
      Console.WriteLine();

      Console.WriteLine("Actual:");
      Console.WriteLine(output);
      Console.WriteLine();

      Console.WriteLine($"The result {(output == ExpectedOutput ? "DID" : "DID NOT")} match the expected result");
      Console.ReadLine();
    }

    private static string ConvertToTable(string input)
    {
      IEnumerable<string[]> rows =
        input.Split(new[] {Environment.NewLine}, StringSplitOptions.None).Select(row => row.Split(',')).Skip(1);

      StringBuilder builder = new StringBuilder();
      builder.AppendLine("| Pub Date    |                       Title | Authors                         |");
      builder.AppendLine("|=============================================================================|");
      
      foreach (var row in rows)
      {
        builder.AppendLine(FormatLine(DateTime.Parse(row[0]), row[1], row[2]));
      }
      
      return builder.ToString();
    }

    private static string FormatLine(DateTime pubDate, string title, string authors)
    {
      return $"| {pubDate:dd MMM yyyy} | {LimitLength(title, 27).PadLeft(27)} | {LimitLength(authors, 31).PadRight(31)} |";
    }

    private static string LimitLength(string text, int length)
    {
      if (text.Length > length)
      {
        return text.Substring(0, length - 3) + "...";
      }

      return text;
    }
  }
}
