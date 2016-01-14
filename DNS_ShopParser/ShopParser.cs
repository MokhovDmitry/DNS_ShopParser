using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace DNS_ShopParser
{
    static class ShopParser
    {
        private  const string dnsSiteMapWebAddress = @"http://www.dns-shop.ru/sitemap.xml";
        private static string dnsSitemapFileAddress = Directory.GetCurrentDirectory() + @"\Temp\Sitemap.xml";


        private static void DownloadSitemap()
        {
            try
            {
                using (var client = new WebClient())
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dnsSitemapFileAddress));
                    client.DownloadFile(dnsSiteMapWebAddress, dnsSitemapFileAddress);
                }
            }
            catch (Exception ex)
            {
                Console.Write("Не удалось загрузить карту сайта по следующей причине:\n" + ex.ToString());
                //MessageBox.Show();
            }
        }

        private static void ParseIt(HtmlDocument thatHtmlDocument)
        {
            //var trNodes = thatHtmlDocument.GetElementbyId("job-items").ChildNodes.Where(x => x.Name == "tr");

            HtmlNodeCollection elements = thatHtmlDocument.DocumentNode.SelectNodes("//title");

            foreach (var v in elements)
            {
                //MessageBox.Show(v.InnerText);
            }
        }

        public static List<Sitemap> GetSitemap()
        {
            List<Sitemap> smList = new List<Sitemap>();

            if (!File.Exists(dnsSitemapFileAddress))
                DownloadSitemap();

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(dnsSitemapFileAddress);
                if (doc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    //READ PREPARING:

                    // Get the encoding declaration.
                    XmlDeclaration decl = (XmlDeclaration)doc.FirstChild;

                    // Set the encoding declaration.
                    decl.Encoding = "UTF-16";
                    string response = doc.InnerXml;

                    string filter = @"xmlns(:\w+)?=""([^""]+)""|xsi(:\w+)?=""([^""]+)""";
                    response = Regex.Replace(response, filter, "");

                    doc.LoadXml(response);

                    foreach (XmlNode parentNode in doc.SelectNodes("//url")) //sitemap
                    {
                        string parentNodeValue = parentNode.InnerText;

                        Sitemap sm = new Sitemap();

                        sm.ID = Guid.NewGuid();
                        sm.Url = parentNode.SelectSingleNode("./loc").InnerText;
                        sm.LastModification = DateTime.Parse(parentNode.SelectSingleNode("./lastmod").InnerText);
                        sm.ChangeFreq = parentNode.SelectSingleNode("./changefreq").InnerText;
                        string priority = parentNode.SelectSingleNode("./priority").InnerText.Replace(".",",");
                        sm.Priority = float.Parse(priority);

                        smList.Add(sm);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //MessageBox.Show(ex.ToString());
            }

            return smList;
        }
    }
}

/*
//check connection
HtmlWeb web = new HtmlWeb();
HtmlDocument nDoc = web.Load(@"http://www.dns-shop.ru/catalog/17aa72ab16404e77/noutbuki-i-planshety/");

nDoc.Save(@"D:\saved.html");
//nDoc.LoadHtml(@"https://www.google.ru");
//nDoc.Load(@Directory.GetCurrentDirectory() + @"\Temp\test.html");ыва а ваыва

ParseIt(nDoc);*/

