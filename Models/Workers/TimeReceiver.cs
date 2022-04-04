using System;
using WallpaperChanger.Models.Enums;

namespace WallpaperChanger.Models.Workers
{
    public class TimeReceiver
    {
        public TimeInfo GetInfoAboutTime() => new()
        {
            Date = DateTime.Now.ToString("dd.MM.yy"),
            Day = (Days) DateTime.Now.DayOfWeek,
            Time = DateTime.Now.ToString("HH:mm")
        };
    }
}