using System;
using System.Collections.Generic;
using System.Xml;

namespace RaceHouse.Test
{
    public class TestCaseProvider
    {
        //public static IEnumerable<object[]> ProvideInputHousesNum()
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(@"D:\my office\C#\RaceHouse\RaceHouse\Test\InputHousesNum.xml");
        //    XmlNode xn = xmlDocument.SelectSingleNode("testCaseSet");
        //    XmlNodeList xnl = xn.ChildNodes;
        //    foreach (var item in xnl)
        //    {
        //        XmlElement element = (XmlElement)item;
        //        string id = element.GetAttribute("id");
        //        string input = "";
        //        string result = "";
        //        XmlNodeList testData = element.ChildNodes;
        //        foreach (XmlNode t in testData)
        //        {
        //            if (t.Name == "input")
        //                input = t.InnerText;
        //            else
        //                if (t.Name == "result")
        //                result = t.InnerText;
        //        }
        //        yield return new object[] { id, input, result };
        //    }

        //}

        public static IEnumerable<object[]> ProvideHousePrintPos()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(@"D:\my office\C#\RaceHouse\RaceHouse\Test\HousePrintPos.xml");
            XmlNode xn = xmlDocument.SelectSingleNode("testCaseSet");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode item in xnl)
            {
                XmlElement element = (XmlElement)item;
                string id = element.GetAttribute("id");
                string input = "";
                string result = "";
                XmlNodeList testData = element.ChildNodes;
                foreach (XmlNode t in testData)
                {
                    if (t.Name == "input")
                        input = t.InnerText;
                    else if (t.Name == "result")
                        result = t.InnerText;
                }
                House house = new House(Convert.ToInt32(id));

                house.Position = (float)Convert.ToDouble(input);

                yield return new object[] { house, result };
            }
        }

        public static IEnumerable<object[]> ProvideHouseRun()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(@"D:\my office\C#\RaceHouse\RaceHouse\Test\HouseRun.xml");
            XmlNode xn = xmlDocument.SelectSingleNode("testCaseSet");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode item in xnl)
            {
                XmlElement element = (XmlElement)item;
                string id = element.GetAttribute("id");
                string Pos = "";
                string Speed = "";
                string result = "";
                XmlNodeList testData = element.ChildNodes;
                foreach (XmlNode t in testData)
                {
                    if (t.Name == "Pos")
                        Pos = t.InnerText;
                    else if (t.Name == "Speed")
                        Speed = t.InnerText;
                    else if (t.Name == "result")
                        result = t.InnerText;
                }
                House house = new House(Convert.ToInt32(id));

                house.Position = (float)Convert.ToDouble(Pos);

                yield return new object[] { house, Convert.ToInt32(Speed), Convert.ToInt32(result) };
            }
        }

        public static IEnumerable<object[]> ProvideGame()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(@"D:\my office\C#\RaceHouse\RaceHouse\Test\Game.xml");
            XmlNode xn = xmlDocument.SelectSingleNode("testCaseSet");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode item in xnl)
            {
                NotRandom notRandom = new NotRandom();
                XmlElement element = (XmlElement)item;
                string id = element.GetAttribute("id");
                XmlNodeList testData = element.ChildNodes;
                string Target = "";
                string Money = "";
                string Result = "";
                foreach (XmlNode t in testData)
                {
                    if (t.Name == "House")
                    {
                        string No = "";
                        XmlElement e = (XmlElement)t;
                        No = e.GetAttribute("No");
                        string Power = "";
                        string Race = "";
                        XmlNodeList house = e.ChildNodes;
                        foreach (XmlNode h in house)
                        {
                            if (h.Name == "Power")
                                Power = h.InnerText;
                            else if (h.Name == "Race")
                                Race = h.InnerText;
                        }
                        notRandom.AddHouse(Convert.ToInt32(No), (float)Convert.ToDouble(Power), (float)Convert.ToDouble(Race));
                    }
                    else if (t.Name == "Target")
                        Target = t.InnerText;
                    else if (t.Name == "Money")
                        Money = t.InnerText;
                    else if (t.Name == "Result")
                        Result = t.InnerText;
                }
                yield return new object[] { notRandom, Convert.ToInt32(Target), Convert.ToInt32(Money) , (float)Convert.ToDouble(Result) };
            }
        }
    }

}
