using System;
using System.Linq;
using System.Text;

namespace SpellChecker
{
  class StringCorrector
  {
		private string First;
		private string Second;
		public StringCorrector(string first, string second)
		{
			First = first;
			Second = second;
		}

		// swap 2 string
		private void Swap()
		{
			string tmp = new string(First);
			First = Second;
			Second = tmp;
		}

		// cheking characters on adjacent 
		public bool IsAdjacent()
		{ 
			if (First.Length < Second.Length)
				Swap();

			StringBuilder strBuilder = new StringBuilder();
			foreach (var t in First.Except(Second))
				strBuilder.Append(t);

			if (strBuilder.Length > 1)
				return true;

			return false;
		}

		// calculation LevenshteinDistance with operation Insert and Delete
		public int LevenshteinDistance()
		{
			var opt = new int[First.Length + 1, Second.Length + 1];
			for (var i = 0; i <= First.Length; ++i)
				opt[i, 0] = i;
			for (var i = 0; i <= Second.Length; ++i)
				opt[0, i] = i;
			for (var i = 1; i <= First.Length; ++i)
				for (var j = 1; j <= Second.Length; ++j)
				{
					if (char.ToLower(First[i - 1]) == char.ToLower(Second[j - 1]))
						opt[i, j] = opt[i - 1, j - 1];
					else
						opt[i, j] = 1 + Math.Min(opt[i - 1, j], opt[i, j - 1]);
				}

			return opt[First.Length, Second.Length];
		}
	
	}
}
