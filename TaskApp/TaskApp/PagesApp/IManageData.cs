using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.PagesApp
{
    internal interface IManageData
    {
        /// <summary>
        /// Getting data from local database SQLite
        /// </summary>
        /// <returns>Return data from local database SQLite</returns>
        DataTask GetData(int id);

        /// <summary>
        /// Saving data in local database SQLite
        /// </summary>
        /// <returns>Return true or false if data saved is succesfully</returns>
        bool SetData(string cm);
    }
}
