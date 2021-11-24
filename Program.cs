using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CalendarForXML
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var _db = new ApplicationDbContext())
            {
                int page = 0;
                while (true)
                {
                    //抓取檔案URL
                    string file = $"https://data.ntpc.gov.tw/api/datasets/308DCD75-6434-45BC-A95F-584DA4FED251/xml?page={page}&size=100000";
                    //新建一個XML Document
                    XmlDocument xmlDocument = new XmlDocument();
                    //載入檔案
                    xmlDocument.Load(file);

                    if (xmlDocument.InnerText == "") break;
                    //抓取root節點
                    XmlNode root = xmlDocument.DocumentElement;

                    XmlNodeList list = root.ChildNodes;
                    DateTime lastTime = _db.Calendars.OrderByDescending(x => x.date).Select(t => t.date).FirstOrDefault();
                    foreach (XmlNode row in list)
                    {
                        DateTime date;
                        if (!DateTime.TryParse(row.ChildNodes[0].InnerText, out date)) continue;
                        Calendar calendar = new Calendar();
                        if (lastTime!=null?Convert.ToDateTime(row.ChildNodes[0].InnerText) > lastTime:true)
                        {
                            calendar.date = Convert.ToDateTime(row.ChildNodes[0].InnerText);
                            calendar.name = row.ChildNodes[1].InnerText;
                            calendar.isHoliday = row.ChildNodes[2].InnerText == "是" ? true : false;
                            calendar.holidayCategory = row.ChildNodes[3].InnerText;
                            calendar.description = row.ChildNodes[4].InnerText;
                            _db.Calendars.Add(calendar);
                        }
                        _db.SaveChanges();
                    }
                    Console.WriteLine($"page={page}結束,每頁100000筆");
                    page++;
                }
                Console.WriteLine($"上傳結束");
                Console.ReadKey();
            }
        }
    }
}
