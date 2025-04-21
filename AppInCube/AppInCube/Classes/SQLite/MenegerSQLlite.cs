using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppInCube.Classes.SQLite
{
    public class MenegerSQLlite
    {
        private readonly SQLiteAsyncConnection _database;

        public MenegerSQLlite(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<SQLliteTableBaseInfo>().Wait();
        }

        // Получение всех программ
        public Task<List<SQLliteTableBaseInfo>> GetProgramsAsync()
        {
            return _database.Table<SQLliteTableBaseInfo>().ToListAsync();
        }

        // Сохранение программы
        public Task<int> SaveProgramAsync(SQLliteTableBaseInfo program)
        {
            return _database.InsertAsync(program);
        }

        // Получение программы по ProgramId
        public Task<SQLliteTableBaseInfo> GetProgramByIdAsync(uint programId)
        {
            return _database.Table<SQLliteTableBaseInfo>().FirstOrDefaultAsync(p => p.IdBirdInMySQL == programId);
        }

        // Удаление программы по ID
        public async Task<int> DeleteProgramAsync(uint id)
        {
            // Получаем программу по ID
            var program = await _database.Table<SQLliteTableBaseInfo>().FirstOrDefaultAsync(p => p.IdBirdInMySQL == id);

            if (program != null)
            {
                return await _database.DeleteAsync(program); // Удаляем программу
            }

            return 0; // Возвращаем 0, если программа не найдена
        }
    }
}