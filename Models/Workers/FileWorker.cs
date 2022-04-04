using System;
using System.IO;
using System.Windows.Media.Imaging;
using static WallpaperChanger.Models.Win;

namespace WallpaperChanger.Models.Workers
{
    public class FileWorker
    {
        private const int SetDesktopBackground = 20;
        private const int UpdateIniFile = 1;
        private const int SendWindowsIniChange = 2;
        
        public void SaveAsImage(string path, BitmapSource source)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(source));
            using var stream = new FileStream(path, FileMode.Create);
            encoder.Save(stream);
        }
        
        public void SetWallpaper(string path)
        {
            SystemParametersInfo(SetDesktopBackground, 0, $"{Environment.CurrentDirectory}/{path}",
                UpdateIniFile | SendWindowsIniChange);
        }
    }
}