using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.PagesApp
{
    public struct DataTask
    {
        public DataTask(int Id, string Title, string Description, string CreationData, int TimePeriod)
        {
            this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.CreationData = CreationData;
            this.TimePeriod = TimePeriod;
        }

        /// <summary>
        /// Id task in local database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title task in local database
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description task in local database
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Creation data task in local database
        /// </summary>
        public string CreationData { get; set; }

        /// <summary>
        /// Time period pushing notify task in local database
        /// </summary>
        public int TimePeriod { get; set; }
    }
}
