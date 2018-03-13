using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using SQLite;

using Sensus.Model_data;

namespace Sensus.DataStores
{
    public class RegistrationDatabase
    {

        readonly SQLiteAsyncConnection database;

        public RegistrationDatabase(string dpPath)
        {
            database = new SQLiteAsyncConnection(dpPath);
            database.CreateTableAsync<Registration>().Wait();
            
        }

        public Task<int> SaveItemAsync(Registration item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
    }
}
