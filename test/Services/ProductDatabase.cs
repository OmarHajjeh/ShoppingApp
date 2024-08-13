using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using test.Models;

namespace test.Services
{
    internal class ProductDatabase
    {
        private SQLiteAsyncConnection _database;

        public ProductDatabase()
        {
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await _database.CreateTableAsync<Product>();
        }

        public async Task<List<Product>> GetItemsAsync()
        {
            return await _database.Table<Product>().ToListAsync();
        }

        public async Task<Product> GetItemAsync(int id)
        {
            return await _database.Table<Product>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<int> SaveItemAsync(Product item)
        {
                return await _database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Product item)
        {
            return await _database.DeleteAsync(item);
        }
    }
}
