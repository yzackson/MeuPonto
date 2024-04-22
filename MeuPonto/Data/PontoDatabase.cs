using MeuPonto.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuPonto.Data
{
    public class PontoDatabase
    {
        SQLiteAsyncConnection Database;
        public PontoDatabase()
        {
        }
        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Ponto>();
        }

        public async Task<List<Ponto>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Ponto>().ToListAsync();
        }

        public async Task<int> SaveItemAsync(Ponto item)
        {
            await Init();
            if (item.ID != 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteItemAsync(Ponto item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
