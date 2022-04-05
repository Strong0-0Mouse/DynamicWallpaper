using System.Runtime.InteropServices;
using WallpaperChanger.Models.Enums;

namespace WallpaperChanger.Models
{
    public static class Win
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(int button);
        public static bool IsMouseButtonPressed(MouseButton button)
        {
            return GetAsyncKeyState((int)button);
        }
    }
}