using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using BC.Models;
using BC.Helpers;
namespace BC.Helpers
{
    public class ListenerPost : AbstractData
    {
        public ListenerPost() { }
        public override void listener()
        {
            System.Console.WriteLine("jsem v listeneru");
            while (true)
            {

                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://+:80/MyUri/");

                listener.Start();
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                string text;
                if ((int)response.StatusCode != 500)
                {
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        text = reader.ReadToEnd();
                    }
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();

                    listener.Close();
                    if (singleData.returnInstance().DataType.ToLower() == "xml") {
                        parseDataXML(text);
                    }
                    else if(singleData.returnInstance().DataType.ToLower()=="url"){
                        parseDataURL(text);
                    }
                    else if(singleData.returnInstance().DataType.ToLower() == "csv")
                    {
                        parseDataCSV(text);
                    }
                    else
                    {
                        parseDataJSON(text);
                    }
                }
            }
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
            throw new NotImplementedException();
        }

        public override void parseDataXML(string text)
        {
            throw new NotImplementedException();
        }
    }
}
