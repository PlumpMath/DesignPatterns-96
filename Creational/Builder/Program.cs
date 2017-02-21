using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{


    #region Participants
    // separate the data from the logic and reuse that logic
    //Builder : specifies an abstract interface for creating parts of a Product object
    //ConcreteBuilder : constructs and assembles parts of the product by implementing the Builder interface
    //Director : constructs an object using the Builder interface
    //Product  :
    #endregion

    /// <summary>
    /// BUilder
    /// </summary>

    public interface SiteMapBuilder
    {
        void Header();
        void Footer();
        void Page(string url);
        string GetContent();
    }


    /// <summary>
    /// ConcreteBuilder
    /// </summary>
    public class HtmlSiteMapBuilder : SiteMapBuilder
    {
        private string header;
        private string footer;
        private List<string> urls;

        public void Header()
        {
            header = "<ul>\n";
        }
        public void Footer()
        {
            footer = "</ul>\n";
        }
        public void Page(string url)
        {
            if (urls == null)
                urls = new List<string>();

            urls.Add(url);
        }
        public string GetContent()
        {
            StringBuilder content = new StringBuilder();
            foreach (var url in urls)
            {

                content.Append("<li>" + url+ "</li>\n");
            }
            return string.Format("{0}{1}{2}", header, content.ToString(), footer);
        }
    }

    public class GoogleSiteMapBuilder : SiteMapBuilder
    {
        private string header;
        private string footer;
        private List<string> urls;


        public void Header()
        {
            header = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n"
                + "<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n";
        }
        public void Footer()
        {
            footer = "</urlset>\n</xml>\n";
        }
        public void Page(string url)
        {
            if (urls == null)
                urls = new List<string>();

            urls.Add(url);
        }
        public string GetContent()
        {
            StringBuilder content = new StringBuilder();
            foreach (var url in urls)
            {

                content.Append("<url>\n<loc>" + url + "</loc>\n</url>\n");
            }
            return string.Format("{0}{1}{2}", header, content.ToString(), footer);
        }
    }


    /// <summary>
    /// Director
    /// </summary>
    public class SiteMapMaker
    {

        //Product
        List<string> _data = new List<string>();
        string siteMap;

        public SiteMapMaker()
        {
            _data.Add("www.test.com");
            _data.Add("www.test.com/page1");
            _data.Add("www.test.com/page2");
            _data.Add("www.test.com/page3");
        }
        public string Maker(SiteMapBuilder siteMapBuilder)
        {
            siteMapBuilder.Header();
            siteMapBuilder.Footer();
            foreach (var item in _data)
            {
                siteMapBuilder.Page(item);
            }
            siteMap = siteMapBuilder.GetContent();

            return siteMap;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {

            SiteMapMaker _mapMaker = new SiteMapMaker();
            var siteMap = _mapMaker.Maker(new GoogleSiteMapBuilder());

            Console.WriteLine(siteMap);

            Console.Read();


        }
    }
}
