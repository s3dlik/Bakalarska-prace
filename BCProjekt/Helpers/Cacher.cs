using System;
using System.Collections.Generic;
using BC.Models;
namespace BC.Helpers
{
    public class Cacher
    {
        //key = sensor name, value = value from sensor
        public List<Tuple<int,int, string>> CacheData { get; set; }
        public int ListIndex { get; set; }
        public int selectedDevicesCount { get; set; }
        public List<int> selectedDevicesList { get; set; }
        private static Cacher instance = null;
        public Cacher()
        {
            //define list size
            this.CacheData = new List<Tuple<int,int, string>>(50);
            this.ListIndex = 0;
            this.selectedDevicesCount = 0;
            this.selectedDevicesList = new List<int>();
        }

        public static Cacher returnInstance()
        {
            if (instance == null)
            {
                instance = new Cacher();
            }
            return instance;
        }

        public bool CheckCount()
        {
            if(CacheData.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
