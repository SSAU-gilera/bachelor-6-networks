using System;
using CsQuery;
using HtmlAgilityPack; //это анализатор HTML, написанный на C # для чтения/записи DOM
using HtmlDoc = HtmlAgilityPack.HtmlDocument; //это то, что будет хранить наш Html документ
//Обеспечивает программный доступ верхнего уровня к HTML-документу, который размещается в элементе управления WebBrowser.
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace HttpParser
{
    class HtmlParse//обрабатывает страницу
    {

        public static string GetLinksHTML(string responseData, Uri path, out List<Uri> linksList)
        {
            linksList = new List<Uri>();
            HtmlDoc htmldoc = new HtmlDoc();
            htmldoc.LoadHtml(responseData);//подгружаем наш HTML файл
            if (htmldoc.DocumentNode?.SelectNodes(@"//a[@href]") == null)
            {
                return "Ссылки не найдены";
            }
            foreach(var link in htmldoc.DocumentNode.SelectNodes(@"//a[@href]"))
            {
                try
                {
                    HtmlAttribute attr = link.Attributes["href"];
                    if (attr == null) continue;
                    var href = attr.Value;
                    if (href.StartsWith("javascript", StringComparison.InvariantCultureIgnoreCase)) continue;
                    Uri uri = new Uri(href, UriKind.RelativeOrAbsolute);
                    uri = new Uri(path, uri);
                    if (!linksList.Contains(uri) && uri.Host == path.Host)//если не содержится строка и хост совпадает с хостом
                        linksList.Add(uri);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }         
            return "Ссылки на странице обработаны";
        }
        
    }
}
