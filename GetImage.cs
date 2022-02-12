using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ButAllWordsImagesFromGoogle
{
	internal class GetImage
	{
		private const string _StartUrl = "https://www.google.com/search?q=";
		private const string _EndUrl = "&client=opera-gx&hs=4lq&source=lnms&tbm=isch&sa=X&ved=2ahUKEwiKlJ-WpNn1AhXsg_0HHeJOAOUQ_AUoAnoECAEQBA&biw=1879&bih=939&dpr=1";
		IWebDriver driver = new ChromeDriver();
		Random rnd = new Random();

		public void GetOneImage(string ImageName)
		{
			const string XpathToIMage = "/html/body/div[2]/c-wiz/div[3]/div[1]/div/div/div/div[1]/div[1]/span/div[1]/div[1]/div[1]/a[1]/div[1]/img";
			driver.Url = _StartUrl + ImageName + _EndUrl;
			try
			{
				IWebElement element = driver.FindElement(By.XPath(XpathToIMage));
				string scr = element.GetAttribute("src");

				SaveByteArrayAsImage(AppDomain.CurrentDomain.BaseDirectory + "img/" + ImageName + ".png", ToBase64(scr));
			}
			catch (Exception ex)
			{
				MessageBox.Show("Pass a capcha. Click ok only after passing");
				GetOneImage(ImageName);
			}
		}
		
		private void SaveByteArrayAsImage(string fullOutputPath, string base64String)
		{
			byte[] bytes = Convert.FromBase64String(base64String);

			Image image;

			MemoryStream ms = new MemoryStream(bytes);
			image = Image.FromStream(ms);
			
			image.Save(fullOutputPath, System.Drawing.Imaging.ImageFormat.Png);
		}
		
		private string ToBase64(string input)
		{
			return input.Replace("data:image/jpeg;base64,", "").Replace("data:image/png;base64,", "");
		}
		
		public void GetAllImage(List<string> words)
		{
			const int BaseTime = 33000, MinRandom = -3000, MaxRandom = 3000;
			foreach(string word in words)
			{
				GetOneImage(word);
				Task.Delay(BaseTime + rnd.Next(MinRandom, MaxRandom));
			}
			driver.Quit();
		}
	}
}
