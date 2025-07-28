using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppInCube.Classes.SQLite.Maked
{
    public class MenegerSQLliteMake
    {
        private readonly SQLiteAsyncConnection _database;

        public MenegerSQLliteMake(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<SQLliteTableBaseInfoMake>().Wait(); // Создаем таблицу, если она не существует
            _database.CreateTableAsync<SQLliteTableDopInfoMake>().Wait(); // Создаем таблицу, если она не существует
        }
 
        // Получение всех базовых программ
        public Task<List<SQLliteTableBaseInfoMake>> GetBaseInfoAsync()
        {
            return _database.Table<SQLliteTableBaseInfoMake>().ToListAsync();
        }

        // Сохранение базовой информации
        public async Task<uint> SaveBaseInfoAsync(SQLliteTableBaseInfoMake baseInfo)
        {
            await _database.InsertAsync(baseInfo);
            return baseInfo.IdMakeBird; // Предполагается, что IdMakeBird автоматически генерируется
        }

        // В классе MenegerSQLliteMake добавьте следующие методы:

        public async Task<uint> SaveProgramWithDetailsAsync(SQLliteTableBaseInfoMake program)
        {
            // Сохраняем основную информацию
            await _database.InsertAsync(program);

            // Сохраняем все дни программы
            foreach (var day in program.tablePrograms)
            {
                day.IdMakeProgram = program.IdMakeProgram;
                await _database.InsertAsync(day);
            }

            return program.IdMakeProgram;
        }

        // Сохранение дополнительной информации
        public Task<int> SaveDopInfoAsync(SQLliteTableDopInfoMake dopInfo)
        {
            return _database.InsertAsync(dopInfo);
        }

        // Получение всех программ
        public async Task<List<SQLliteTableBaseInfoMake>> GetProgramsAsync()
        {
            var programs = await _database.Table<SQLliteTableBaseInfoMake>().ToListAsync();

            foreach (var program in programs)
            {
                program.tablePrograms = await GetDopInfoByProgramIdAsync(program.IdMakeBird);
            }

            return programs;
        }

        // Метод для получения дополнительной информации по ID программы
        public Task<List<SQLliteTableDopInfoMake>> GetDopInfoByProgramIdAsync(uint programId)
        {
            return _database.Table<SQLliteTableDopInfoMake>().Where(d => d.IdMakeProgram == programId).ToListAsync();
        }


        // Удаление программы по ID
        public async Task<int> DeleteProgramAsync(uint id)
        {
            var program = await _database.Table<SQLliteTableBaseInfoMake>().FirstOrDefaultAsync(p => p.IdMakeBird == id);
            if (program != null)
            {
                return await _database.DeleteAsync(program);
            }
            return 0; // Возвращаем 0, если программа не найдена
        }
    }
}
