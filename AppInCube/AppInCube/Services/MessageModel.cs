using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInCube.Services
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public DateTime ScheduleTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string TimeLeft
        {
            get
            {
                var timeLeft = ScheduleTime - DateTime.Now;
                if (timeLeft.TotalSeconds <= 0)
                    return "Выполнено";
                else if (timeLeft.TotalMinutes < 60)
                    return $"Через {(int)timeLeft.TotalMinutes} мин";
                else if (timeLeft.TotalHours < 24)
                    return $"Через {(int)timeLeft.TotalHours} ч";
                else
                    return $"Через {(int)timeLeft.TotalDays} дн";
            }
        }
    }
}