using System.Collections.Generic;
using System.Linq;

namespace BC.Helpers
{
    public class URLtoDict
    {
        //Helper for parsing params from URL, I will get key and value as I need. For example I from URL http://192.168.0.108/?&tepmerature=x&humidity=x I will get separately two keys and two values. 
        
        public Dictionary<string, string> ParseQueryString(string query)
        {
            return query.Replace("?", "").Split('&').ToDictionary(pair => pair.Split('=').First(), pair => pair.Split('=').Last());
        }
    }
}
