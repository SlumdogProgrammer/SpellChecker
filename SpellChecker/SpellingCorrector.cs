using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpellChecker
{
  class SpellingCorrector
  {
		// word exist in the dictionary
		private const short dist0 = 0;
		// one edit
		private const short dist1 = 1;
		// two edit
		private const short dist2 = 2;

		private string[] Dictionary;
		private string[] Words;

		public SpellingCorrector(string text, string dict)
		{
		  Words = text.Replace("\n", "\n ").Split(' ');
			Dictionary = dict.Replace('\n', ' ').Split(' ');
		}

		// finding words in dictinary 
		public string SpellCheker()
		{
			bool flag; // word exist in dictionary
			int distance = -1; // count different symbols 

			// corrected text
			StringBuilder resStr = new StringBuilder();

			foreach (var w in Words)
			{
				flag = false;

				Dictionary<int, List<string>> ListWordsByDistance =
					new Dictionary<int, List<string>>(dist0 + dist1 + dist2);
				for (int i = 0; i < dist0 + dist1 + dist2; i++)
					ListWordsByDistance.Add(i, new List<string>());

				StringCorrector strCor = null;
				foreach (var d in Dictionary)
				{
				 strCor = new StringCorrector(w.TrimEnd('\n'), d.TrimEnd('\n'));
					distance = strCor.LevenshteinDistance();

					// word in the dictionary
					if (distance == dist0)
					{
						flag = true;
						ListWordsByDistance[dist0].Add(w);
						break;
					}
					// one edit
					if (distance == dist1)
					{
						flag = true;
						ListWordsByDistance[dist1].
							Add(w.EndsWith('\n') ? d.TrimEnd('\n') + '\n' : d.TrimEnd('\n'));
					}
					// two edit and not adjacent
					if (distance == dist2 && !strCor.IsAdjacent())
					{
						flag = true;
						ListWordsByDistance[dist2].
							Add(w.EndsWith('\n') ? d.TrimEnd('\n') + '\n' : d.TrimEnd('\n'));
					}
				}

				if (ListWordsByDistance[dist0].Count > 0)
					resStr.Append($"{ListWordsByDistance[dist0][0]} ");
				else if (ListWordsByDistance[dist1].Count > 0)
				{
					if (ListWordsByDistance[dist1].Count == 1)
						resStr.Append($"{ListWordsByDistance[dist1][0]} ");
					else
						resStr.Append($"{{{string.Join(' ', ListWordsByDistance[1])}}} ");
				}
				else if (ListWordsByDistance[dist2].Count > 0)
				{
					if (ListWordsByDistance[dist2].Count == 1)
						resStr.Append($"{ListWordsByDistance[dist2][0]} ");
					else
						resStr.Append($"{{{string.Join(' ', ListWordsByDistance[dist2])}}} ");
				}
				// more then 2 edit
				if ((distance > 2 || strCor.IsAdjacent()) && !flag)
					resStr.Append($"{{{w}?}} ");

			}
			return resStr.Replace("\n ", "\n").ToString();
		}

	}
}
