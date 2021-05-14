using System;
using System.Xml.Linq;

namespace XamarinAndroidAppWidgetDemo
{
	class BlogPost
	{
		public string Title { get; set; }
		public string Creator { get; set; }
		public string Link { get; set; }

		public BlogPost()
		{
			Title = string.Empty;
			Creator = string.Empty;
			Link = string.Empty;
		}

		public static BlogPost GetBlogPost()
		{
			var entry = new BlogPost();

			try
			{
				string url = "https://devblogs.microsoft.com/xamarin/feed";

				XNamespace dc = "http://purl.org/dc/elements/1.1/";

				XDocument doc = XDocument.Load(url);

				XElement latest = doc.Root.Element("channel").Element("item");

				entry.Title = latest.Element("title").Value;
				entry.Creator = latest.Element(dc + "creator").Value;
				entry.Link = latest.Element("link").Value;
			}
			catch (Exception ex)
			{
				entry.Title = "Error";
				entry.Creator = ex.Message;
			}

			return entry;
		}
	}
}