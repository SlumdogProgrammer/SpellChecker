using System;
using System.IO;
using System.Linq;

namespace SpellChecker
{
  class FileUtils
  {
    private string FileText;
    private readonly string FileName;

    public FileUtils(string fileName)
    {
      FileName = fileName;
    }

		// parse file on Dictionary and Text
		public Tuple<string, string> ParseFile()
		{
			string[] fileSplittedText;

			fileSplittedText = FileText.Split("===");

			string dic = fileSplittedText[0];
			string dictionary = (dic.Last() == '\n') ?
				dic.Remove(dic.Length - 1, 1) :
				dic;

			string txt = fileSplittedText[1];
			string text = (txt.Last() == '\n') ?
				fileSplittedText[1].Remove(fileSplittedText[1].Length - 1, 1) :
				fileSplittedText[1];

			if (text[0] == '\n')
				text = text.Remove(0, 1);

			return new Tuple<string, string>(text, dictionary);
		}

		// try open file
		public bool OpenFile()
		{
			if (File.Exists(FileName))
			{
				FileText = File.ReadAllText(FileName).Replace("\r", string.Empty);
				return true;
			}
			else
				return false;
		}
	}
}
