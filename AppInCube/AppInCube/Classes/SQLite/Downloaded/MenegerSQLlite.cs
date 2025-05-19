using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppInCube.Classes.SQLite.Downloaded
{
    public class MenegerSQLlite
    {
        private readonly SQLiteAsyncConnection _database;

        public MenegerSQLlite(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<SQLliteTableBaseInfo>().Wait(); // Создание таблицы для базовой информации о программах
            _database.CreateTableAsync<SQLliteTableDopInfo>().Wait(); // Создание таблицы для доп. информации, если она не существует
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

        // Получение программы по ID
        public Task<SQLliteTableBaseInfo> GetProgramByIdAsync(uint programId)
        {
            return _database.Table<SQLliteTableBaseInfo>().FirstOrDefaultAsync(p => p.IdBirdInMySQL == programId);
        }

        // Получение доп. информации о программе по ID
        public async Task<List<SQLliteTableDopInfo>> GetDopInfoByProgramIdAsync(uint programId)
        {
            // Получаем базовую информацию о программе по ID
            var baseInfo = await _database.Table<SQLliteTableBaseInfo>()
                                           .FirstOrDefaultAsync(p => p.IdProgramInMySQL == programId);

            // Если базовая информация не найдена, возвращаем пустой список
            if (baseInfo == null)
            {
                return new List<SQLliteTableDopInfo>();
            }

            // Извлекаем программы из сериализованного JSON
            var programs = baseInfo.tablePrograms;

            // Преобразуем их в список SQLliteTableDopInfo
            var dopInfoList = new List<SQLliteTableDopInfo>();

            foreach (var program in programs)
            {
                // Здесь вы можете создать объект SQLliteTableDopInfo на основе данных из program
                // Предполагается, что у вас есть соответствующие поля в TableProgram
                dopInfoList.Add(new SQLliteTableDopInfo
                {
                    IdProgram = baseInfo.IdProgramInMySQL, // Или другое поле, если нужно
                    Day = program.Day, // Предполагается, что у вас есть поле Day в TableProgram
                    MinTemperature = program.MinTemperature, // Предполагается, что у вас есть это поле
                    MaxTemperature = program.MaxTemperature, // Предполагается, что у вас есть это поле
                    MinHumidity = program.MinHumidity, // Предполагается, что у вас есть это поле
                    MaxHumidity = program.MaxHumidity, // Предполагается, что у вас есть это поле
                    MinАmountTurn = program.MinАmountTurn, // Предполагается, что у вас есть это поле
                    MaxАmountTurn = program.MaxАmountTurn, // Предполагается, что у вас есть это поле
                    АmountCooling = program.АmountCooling, // Предполагается, что у вас есть это поле
                    MinTimeCooling = program.MinTimeCooling, // Предполагается, что у вас есть это поле
                    MaxTimeCooling = program.MaxTimeCooling // Предполагается, что у вас есть это поле
                });
            }

            return dopInfoList;
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
