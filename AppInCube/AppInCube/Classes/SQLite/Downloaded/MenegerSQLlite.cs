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

        //// Получение всех дней программы по ID
        //public Task<List<SQLliteTableDopInfo>> GetDopInfoByProgramIdAsync(uint programId)
        //{
        //    // Прямой запрос к таблице с дополнительной информацией
        //    return _database.Table<SQLliteTableDopInfo>()
        //        .Where(d => d.IdProgram == programId)
        //        .ToListAsync();
        //}
        //// Получение доп. информации о программе по ID
        //public async Task<List<SQLliteTableDopInfo>> GetDopInfoByProgramIdAsync(uint programId)
        //{
        //    // Получаем базовую информацию о программе по ID
        //    var baseInfo = await _database.Table<SQLliteTableBaseInfo>()
        //                                   .FirstOrDefaultAsync(p => p.IdProgramInMySQL == programId);

        //    // Если базовая информация не найдена, возвращаем пустой список
        //    if (baseInfo == null)
        //    {
        //        return new List<SQLliteTableDopInfo>();
        //    }

        //    // Извлекаем программы из сериализованного JSON
        //    var programs = baseInfo.tablePrograms;

        //    // Преобразуем их в список SQLliteTableDopInfo
        //    var dopInfoList = new List<SQLliteTableDopInfo>();

        //    foreach (var program in programs)
        //    {
        //        // Здесь вы можете создать объект SQLliteTableDopInfo на основе данных из program
        //        // Предполагается, что у вас есть соответствующие поля в TableProgram
        //        dopInfoList.Add(new SQLliteTableDopInfo
        //        {
        //            IdProgram = baseInfo.IdProgramInMySQL, // Или другое поле, если нужно
        //            Day = program.Day, // Предполагается, что у вас есть поле Day в TableProgram
        //            MinTemperature = program.MinTemperature, // Предполагается, что у вас есть это поле
        //            MaxTemperature = program.MaxTemperature, // Предполагается, что у вас есть это поле
        //            MinHumidity = program.MinHumidity, // Предполагается, что у вас есть это поле
        //            MaxHumidity = program.MaxHumidity, // Предполагается, что у вас есть это поле
        //            MinАmountTurn = program.MinАmountTurn, // Предполагается, что у вас есть это поле
        //            MaxАmountTurn = program.MaxАmountTurn, // Предполагается, что у вас есть это поле
        //            АmountCooling = program.АmountCooling, // Предполагается, что у вас есть это поле
        //            MinTimeCooling = program.MinTimeCooling, // Предполагается, что у вас есть это поле
        //            MaxTimeCooling = program.MaxTimeCooling // Предполагается, что у вас есть это поле
        //        });
        //    }

        //    return dopInfoList;
        //}



        // Получение доп. информации о программе по ID
        public async Task<List<SQLliteTableDopInfo>> GetDopInfoByProgramIdAsync(uint programId)
        {
            try
            {
                // Получаем базовую информацию о программе по ID
                var baseInfo = await _database.Table<SQLliteTableBaseInfo>()
                                               .FirstOrDefaultAsync(p => p.IdProgramInMySQL == programId);

                // Если базовая информация не найдена, возвращаем пустой список
                if (baseInfo == null)
                {
                    Console.WriteLine($"Базовая программа с IdProgramInMySQL={programId} не найдена");
                    return new List<SQLliteTableDopInfo>();
                }

                // Извлекаем программы из сериализованного JSON
                var programs = baseInfo.tablePrograms;

                // Преобразуем их в список SQLliteTableDopInfo
                var dopInfoList = new List<SQLliteTableDopInfo>();

                foreach (var program in programs)
                {
                    dopInfoList.Add(new SQLliteTableDopInfo
                    {
                        IdProgram = baseInfo.IdProgramInMySQL,
                        Day = program.Day,
                        MinTemperature = program.MinTemperature,
                        MaxTemperature = program.MaxTemperature,
                        MinHumidity = program.MinHumidity,
                        MaxHumidity = program.MaxHumidity,
                        MinАmountTurn = program.MinАmountTurn,
                        MaxАmountTurn = program.MaxАmountTurn,
                        АmountCooling = program.АmountCooling,
                        MinTimeCooling = program.MinTimeCooling,
                        MaxTimeCooling = program.MaxTimeCooling
                    });
                }

                Console.WriteLine($"Получено {dopInfoList.Count} дней для программы {programId}");
                return dopInfoList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении программы {programId}: {ex.Message}");
                return new List<SQLliteTableDopInfo>();
            }
        }
        // Получение конкретного дня программы по ID программы и номеру дня
        public async Task<SQLliteTableDopInfo> GetDopInfoByProgramIdAndDayAsync(uint programId, byte day)
        {
            try
            {
                // Получаем все дни программы
                var allDays = await GetDopInfoByProgramIdAsync(programId);

                // Находим нужный день
                var specificDay = allDays?.FirstOrDefault(d => d.Day == day);

                Console.WriteLine($"Поиск дня: programId={programId}, day={day}, найдено дней={allDays?.Count}, результат={(specificDay != null ? "найден" : "не найден")}");

                return specificDay;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении дня {day} программы {programId}: {ex.Message}");
                return null;
            }
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
