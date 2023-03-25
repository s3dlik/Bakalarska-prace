using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using BC.Models;

namespace BC.Helpers
{
    public class ReceiveGET : AbstractData
    {
        public override void listener()
        {
            throw new NotImplementedException();
        }

        public override void parseDataCSV(string CSV)
        {
            throw new NotImplementedException();
        }

        public override void parseDataJSON(string JSON)
        {
            throw new NotImplementedException();
        }

        public override void parseDataURL(string text)
        {
            throw new NotImplementedException();
        }

        public override void parseDataURL(string URL, string format)
        {
            if (!URL.EndsWith("/")) {
                URL += "/";
            }
            if (!format.StartsWith("?"))
            {
                format += "?";
            }
            var fullURL = URL + format;
            ParseQueryString(fullURL);
        }

        public override void ParseQueryString(string query) { 
            var data = query.Replace("?", "").Split('&').ToDictionary(pair => pair.Split('=').First(), pair => pair.Split('=').Last());

            URLtoDict uRLtoDict = new();

            data.Remove(data.ElementAt(0).Key);
            singleData.returnInstance().dataList.Clear();
            foreach (var item in data)
            {
                singleData.returnInstance().dataList.Add(new Parser(item.Key, item.Value));
            }

        }

        public override void parseDataXML(string text)
        {
            singleData.returnInstance().dataList.Clear();
            if (!string.IsNullOrEmpty(text))
            {
                singleData.returnInstance().dataList.Clear();
                XElement purchaseOrder = XElement.Parse(text);
                List<string> typeList = purchaseOrder.Descendants("sensor").Select(x => (string)x.Element("type")).ToList();
                List<string> nameList = purchaseOrder.Descendants("sensor").Select(x => (string)x.Element("name")).ToList();
                List<string> placeList = purchaseOrder.Descendants("sensor").Select(x => (string)x.Element("place")).ToList();
                List<string> valueList = purchaseOrder.Descendants("sensor").Select(x => (string)x.Element("value")).ToList();


                try
                {
                    XmlDocument doc = new();
                    doc.LoadXml(text);
                    var dayNight = doc.GetElementsByTagName("isday");
                    var bio = doc.GetElementsByTagName("bio");
                    var sunrise = doc.GetElementsByTagName("sunrise");
                    var sunset = doc.GetElementsByTagName("sunset");
                    var fog = doc.GetElementsByTagName("fog");
                    var astroStart = doc.GetElementsByTagName("astrostart");
                    var astroEnd = doc.GetElementsByTagName("astroend");


                }
                catch (Exception e)
                {

                    throw e.InnerException;
                }

                for (int i = 0; i < typeList.Count; i++)
                {
                    if (String.IsNullOrEmpty(placeList[i]))
                    {
                        placeList[i] = "unknown";
                    }
                }
                // singleData.returnInstance().dataList.Add(new Parser(serialNumber, model, gotDataParsed, dateParsed));
                for (int i = 0; i < nameList.Count; i++)
                {
                    if (valueList[i] != "INACTIVE" || nameList[i] != "PING0")
                    {
                        if (!nameList[i].StartsWith("OUT"))
                        {
                            foreach (var deviceSensorsItem in singleData.returnInstance().deviceSensors)
                            {
                                if (nameList[i] == deviceSensorsItem.Name)
                                {
                                    singleData.returnInstance().dataList.Add(new Parser(nameList[i], valueList[i]));
                                }
                            }
                            //Cacher.returnInstance().CacheData.Add(singleData.returnInstance().dataList);
                            
                        }

                    }
                }
            }
        }

        public string requestData(string url)
        {
            //var url = "https://pastebin.com/raw/jzG1f1g1";
            if (!string.IsNullOrEmpty(url)) { 
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                httpRequest.Accept = "application/xml";

                string tmp;
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                if ((int)httpResponse.StatusCode >= 200 && (int)httpResponse.StatusCode < 300)
                {

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        tmp = streamReader.ReadToEnd();
                    }
                    if ((int)httpResponse.StatusCode >= 200)
                    {
                        return tmp;
                    }
                }
            }
            return "";
        }
    }
}
