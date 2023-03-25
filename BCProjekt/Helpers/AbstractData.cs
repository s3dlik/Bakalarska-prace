using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using BC.Models;

namespace BC.Helpers
{
    public abstract class AbstractData
    {
        public abstract void parseDataXML(string text);

        public abstract void parseDataURL(string text);
        public abstract void parseDataURL(string URL, string format);
        public abstract void parseDataCSV(string CSV);
        public abstract void parseDataJSON(string JSON);

        public abstract void listener();

        public abstract void ParseQueryString(string query);
    }
}
