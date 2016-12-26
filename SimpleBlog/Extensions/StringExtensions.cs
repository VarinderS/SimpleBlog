using System.Text.RegularExpressions;

namespace SimpleBlog.Extensions
{
	public static class StringExtensions
	{
		public static string Sluggify(this string input) 
		{
			input = Regex.Replace(input: input, pattern: @"[^a-zA-Z0-9\s]", replacement: "");

			input = input.ToLower();

			input = Regex.Replace(input: input, pattern: @"\s", replacement: "-");

			input = input.TrimEnd(trimChars: '-');

			return input;
		}
	}
}