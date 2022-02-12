using System;
using System.Collections.Generic;
using System.IO;

namespace ButAllWordsImagesFromGoogle
{
	internal class Read
	{
		private List<string> _Words = new List<string>();
		private string ToNormal(string input)
		{
			string[] symvols = {":","+", "=", "[", "]", ":", ";", "«", ",", ".", "/", "?", " –", " —","*", "»", "«"};
			string str = input;
			foreach(string symvol in symvols)
            {
				//here we replace all symbols who mustn't be present in path to file
				str = str.Replace(symvol, "");
            }
			return str;
		}

		private void Add(string[] NewWord)
		{
			foreach(string word in NewWord)
			{
				if (word != "")
				{
					_Words.Add(word);
				}
			}
		}
 
		public List<string> ReadWords(string path)
		{
			string line;
			StreamReader sr = new StreamReader(path);
			line = ToNormal(sr.ReadLine());
			Add(line.Split());
			while (line != null)
			{
				Console.WriteLine(line);
				line = sr.ReadLine();
				if (line != null)
				{
					line = ToNormal(line);
					Add(line.Split());
				}
			}
			sr.Close();
			return _Words;
		}
	}
}
