using System;
using System.Collections.Generic;

namespace ButAllWordsImagesFromGoogle
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Read read = new Read();
			List<string> words = read.ReadWords(AppDomain.CurrentDomain.BaseDirectory + "Sample.txt");
			GetImage get = new GetImage();
			get.GetAllImage(words);
		}
	}
}
