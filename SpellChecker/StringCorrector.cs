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

      if (First.Contains(Second))
      {
        char c = Second[0];
        int posBeg = First.IndexOf(c, StringComparison.OrdinalIgnoreCase);
        c = Second[Second.Length - 1];
        int posFin = First.LastIndexOfAny(new char[] { c }) + 1;

        if (posBeg > 1 || First.Length - posFin > 1) /*||
					Second.Length + 1 > First.Length - First.Except(Second).Count())*/
          return true;
      }
      //find count different symbols in string
      else
      {
        for (int i = 0; i < First.Length - 1; i++)
				{					
					if (First.Remove(i, 2).Contains(Second))
						return true;
				}
			}
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
