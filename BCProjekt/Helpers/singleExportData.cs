using System;
using System.Collections.Generic;
using BC.Models;

namespace BC.Helpers
{
	public class singleExportData
	{
		private static singleExportData instance = null;
        public List<List<Values>> values { get; set; }
        public List<Dictionary<string, string>> valuesToGraph { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public singleExportData()
        {
            this.values = new List<List<Values>>();
            this.valuesToGraph = new List<Dictionary<string, string>>();
            this.startDate = string.Empty;
            this.endDate = string.Empty;
        }

        public static singleExportData returnInstance()
        {
            if (instance == null)
            {
                instance = new singleExportData();
            }
            return instance;
        }
    }
}
