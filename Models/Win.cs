using System.Runtime.InteropServices;

namespace WallpaperChanger.Models
{
    public static class Win
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    }
}