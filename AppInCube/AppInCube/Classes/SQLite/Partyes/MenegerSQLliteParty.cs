using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppInCube.Classes.SQLite.Partyes;

namespace AppInCube.Classes.SQLite.Partyes
{
    public class MenegerSQLliteParty
    {
        private readonly SQLiteAsyncConnection _database;

        public MenegerSQLliteParty(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<SQLliteTableParty>().Wait(); // Создание таблицы для партий, если она не существует
        }

        // Сохранение партии
        public Task<int> SavePartyAsync(SQLliteTableParty party)
        {
            return _database.InsertAsync(party);
        }   
        // ДОБАВЛЯЕМ МЕТОД ДЛЯ ОБНОВЛЕНИЯ
        public Task<int> UpdatePartyAsync(SQLliteTableParty party)
        {
            return _database.UpdateAsync(party);
        }
        // Получение всех партий
        public Task<List<SQLliteTableParty>> GetPartiesAsync()
        {
            return _database.Table<SQLliteTableParty>().ToListAsync();
        }

        // Получение партии по ID
        public Task<SQLliteTableParty> GetPartyByIdAsync(uint partyId)
        {
            return _database.Table<SQLliteTableParty>().FirstOrDefaultAsync(p => p.IdParty == partyId);
        }

        // Получение доп. информации о программертии по ID
        public Task<List<SQLliteTableDopInfoParty>> GetDopInfoByProgramIdAsync(uint IdParty)
        {
            return _database.Table<SQLliteTableDopInfoParty>().Where(d => d.IdParty == IdParty).ToListAsync();
        }
        // Удаление партии по ID
        public async Task<int> DeletePartyAsync(uint id)
        {
            // Получаем партию по ID
            var party = await _database.Table<SQLliteTableParty>().FirstOrDefaultAsync(p => p.IdParty == id);

            if (party != null)
            {
                return await _database.DeleteAsync(party); // Удаляем партию
            }

            return 0; // Возвращаем 0, если партия не найдена
        }
    }
}
