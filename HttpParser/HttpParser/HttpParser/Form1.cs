using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Linq;

namespace HttpParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            tbInfo.Clear();
            tbLinks.Clear();
           
            tbStatus.Clear();
            int deepCount = (int)nUDDeep.Value;
            int port = (int)nUDPort.Value;
            string url = tBLink.Text;
           
      
                if (!url.StartsWith("http"))
                {
                    url = "http://" + url;
                }
                if (Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                   uri.Scheme == Uri.UriSchemeHttp)//проверка что вначале http
                {
                    ParsingInfo info = ProcessPages(uri, port, deepCount);
                    tbInfo.Text +=
                        "Количество обработанных страниц: " + (info.PagesCount - 1) + "\n" +
                        "Общий размер: " + info.TotalSize + " байт\n" +
                         "Минимальный размер: " + info.MinURL + "\n(" + info.MinSize + " байт)\n" +
                          "Максимальный размер: " + info.MaxURL + "\n(" + info.MaxSize + " байт)\n";
                    tbStatus.Text += info.Status;

                }
                else tbStatus.Text = (" Не удаётся обработать сайт.Проверьте корректность ввода.");
                  
           
        }

        ParsingInfo ProcessPages(Uri startURL, int port, int maxLevel)//обработка страницы
        {
            List<Uri> urisList = new List<Uri>();// Общий промежуточный список
            List<Uri> totalUriList = new List<Uri>();// Общий список


            string minURL = "";
            string maxURL = "";
            int minSize = 0;
            int maxSize = 0;
            int pagesCount = 1;
            int totalSize = 0;

            urisList.Add(startURL);//добавляю стартовую ссылку
            totalUriList.Add(startURL);

            for (int i = 0; i < maxLevel; i++)
            {
                List<Uri> processingURLs = new List<Uri>(); //Список страниц на парсинг
                foreach (var uri in urisList)
                {
                    ResultStatus get_page;//Результат обработки страницы (получаю путь)
                    string curPageResponse = "";//Ответ сервера по странице (сюда будет записываться ответ сервера)
                    try
                    {
                        var path = uri.ToString().Split('/'); //выделяем путь
                        get_page = HttpRequest.GetPage(uri.Host, "/" + path[3], port);//передаём хост и путь

                        if (get_page.Length == -1 || get_page.Result == null)//проверка 
                        {
                            return new ParsingInfo(
                            minSize,
                            maxSize,
                            minURL,
                            maxURL,
                            pagesCount,
                            totalSize,
                            get_page.Status);
                        }

                        curPageResponse = get_page.Result;//ответ сервера

                        totalSize += get_page.Length;//прибавляем размер страницы
                        if (minSize > get_page.Length || minSize == 0)//нахождение минимальной и максимальной
                        {
                            minSize = get_page.Length;
                            minURL = uri.ToString();
                        }

                        if (maxSize < get_page.Length)
                        {
                            maxSize = get_page.Length;
                            maxURL = uri.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        return new ParsingInfo(
                            minSize,
                            maxSize,
                            minURL,
                            maxURL,
                            pagesCount,
                            totalSize,
                            ex.Message);
                    }
                    var PageProcessingResult = HtmlParse.GetLinksHTML(curPageResponse, uri, out List<Uri> curLinks);//передаю на обработку ответ сервера, адрес и выходной список ссылок
                    if (curLinks == null)
                    {
                        return new ParsingInfo(
                            minSize,
                            maxSize,
                            minURL,
                            maxURL,
                            pagesCount,
                            totalSize,
                            PageProcessingResult);
                    }
                    else
                    {
                        foreach (var curLink in curLinks)//проверка на совпадения и принадлежность ссылки к серверу
                        {
                            if (!processingURLs.Contains(curLink) && startURL.Host == curLink.Host && !totalUriList.Contains(curLink))
                            {
                                processingURLs.Add(curLink);//добавляем
                                tbLinks.Text += (pagesCount++) + ". " + curLink.ToString() + "\n";//выводим ссылку
                            }
                        }

                    }                    
                }
                urisList = processingURLs;//проходимся по новым ссылкам
                totalUriList.AddRange(processingURLs);//добавляем полученные ссылки в общий список
            }
            return new ParsingInfo(
                    minSize,
                    maxSize,
                    minURL,
                    maxURL,
                    pagesCount,
                    totalSize,
                    "Страница успешно обработана!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
