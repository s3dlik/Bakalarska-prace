using System.Collections.Generic;
using BC.Models;

namespace BC.Helpers
{
    public class singleMeteo
    {

        public List<Device> devicesList;
        private static singleMeteo instance = null;

        public singleMeteo()
        {
            this.devicesList = new List<Device>();
        }
        public static singleMeteo returnInstance()
        {
            if (instance == null)
            {
                instance = new singleMeteo();
            }
            return instance;
        }
    }
}
