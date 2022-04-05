using System;
using System.Globalization;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WallpaperChanger.Models;
using WallpaperChanger.Models.Enums;
using WallpaperChanger.Models.Workers;

namespace WallpaperChanger
{
    public partial class MainWindow
    {
        private int _lastMinute = -1;
        private readonly Settings.Settings _settings;

        private readonly FileWorker _fileWorker;
        private readonly TimeReceiver _timeReceiver;
        private readonly WeatherReceiver _weatherReceiver;

        private bool _isPressed;
        
        public MainWindow()
        {
            InitializeComponent();
            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
            _settings = JsonConvert.DeserializeObject<Settings.Settings>(File.ReadAllText("Settings/Settings.json"),
                jsonSettings);
            _settings?.Normalize();

            _fileWorker = new FileWorker();
            _timeReceiver = new TimeReceiver();
            _weatherReceiver = new WeatherReceiver(_settings);
            
            var timer = new Timer();
            timer.Interval = 16;
            timer.AutoReset = true;
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }
        
        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(Mess);
            if (_lastMinute == DateTime.Now.Minute) return;
            _lastMinute = DateTime.Now.Minute;
            Application.Current.Dispatcher.Invoke(RenderBackground);
            Application.Current.Dispatcher.Invoke(() =>
            {
                _fileWorker.SetWallpaper(@"Wallpaper/background.jpg");
            });
        }

        private void Mess()
        {
            if (Win.IsMouseButtonPressed(MouseButton.LeftMouseButton) && !_isPressed)
            {
                _isPressed = true;
            }
            else if (!Win.IsMouseButtonPressed(MouseButton.LeftMouseButton) && _isPressed)
            {
                _isPressed = false;
            }

        }

        private void RenderBackground()
        {
            var background = new Image();
            var drawingVisual = new DrawingVisual();
            var drawingContext = drawingVisual.RenderOpen();
            
            var dateInfo = _timeReceiver.GetInfoAboutTime();

            var temp = ParseText(
                "Температура: " +
                $"{Math.Round(double.Parse(JObject.Parse(_weatherReceiver.GetInfoAboutWeather().Result)["main"]!["temp"]!.ToString()))}°C",
                new SolidColorBrush(new Color
                {
                    A = _settings.Temperature.Color.A,
                    R = _settings.Temperature.Color.R,
                    G = _settings.Temperature.Color.G,
                    B = _settings.Temperature.Color.B
                }), _settings.Temperature.Size);
            var day = ParseText(dateInfo.Day.ToString(),
                new SolidColorBrush(new Color
                {
                    A = _settings.DayTime.Color.A,
                    R = _settings.DayTime.Color.R,
                    G = _settings.DayTime.Color.G,
                    B = _settings.DayTime.Color.B
                }), _settings.DayTime.Size);
            var time = ParseText(dateInfo.Time,                
                new SolidColorBrush(new Color
                {
                    A = _settings.DayTime.Color.A,
                    R = _settings.DayTime.Color.R,
                    G = _settings.DayTime.Color.G,
                    B = _settings.DayTime.Color.B
                }), _settings.DayTime.Size);
            var date = ParseText(dateInfo.Date,                
                new SolidColorBrush(new Color
                {
                    A = _settings.Date.Color.A,
                    R = _settings.Date.Color.R,
                    G = _settings.Date.Color.G,
                    B = _settings.Date.Color.B
                }), _settings.Date.Size);

            drawingContext.DrawRectangle(new SolidColorBrush(new Color
            {
                A = _settings.BackgroundColor.A,
                R = _settings.BackgroundColor.R,
                G = _settings.BackgroundColor.G,
                B = _settings.BackgroundColor.B
            }), null, new Rect(new Point(0, 0), new Point(_settings.WidthScreen, _settings.HeightScreen)));

            if (_settings.DayTime.IsEnabled)
            {
                drawingContext.DrawText(day,
                    new Point(_settings.DayTime.X - day.Width / 2, _settings.DayTime.Y - day.Height / 2));
            
                drawingContext.DrawLine(new Pen(new SolidColorBrush(new Color
                    {
                        A = _settings.Date.Color.A,
                        R = _settings.Date.Color.R,
                        G = _settings.Date.Color.G,
                        B = _settings.Date.Color.B
                    }), 2),
                    new Point(_settings.DayTime.X - day.Width / 2, _settings.DayTime.Y + day.Height / 2),
                    new Point(_settings.DayTime.X + day.Width / 2, _settings.DayTime.Y + day.Height / 2));
            
                drawingContext.DrawText(time,
                    new Point(_settings.DayTime.X - time.Width / 2, _settings.DayTime.Y + day.Height / 2));
            }

            if (_settings.Temperature.IsEnabled)
            {
                drawingContext.DrawText(temp,
                    new Point(_settings.Temperature.X - temp.Width / 2, _settings.Temperature.Y - temp.Height));
            }

            if (_settings.Date.IsEnabled)
            {
                drawingContext.DrawText(date,
                    new Point(_settings.Date.X - date.Width / 2, _settings.Date.Y));
            }

            foreach (var customString in _settings.CustomStrings)
            {
                if (!customString.IsEnabled) 
                    continue;
                var stringText = ParseText(customString.Text,                
                    new SolidColorBrush(new Color
                    {
                        A = customString.Color.A,
                        R = customString.Color.R,
                        G = customString.Color.G,
                        B = customString.Color.B
                    }), _settings.Date.Size);
                drawingContext.DrawText(stringText,
                    new Point(customString.X - stringText.Width / 2, customString.Y));
            }
            drawingContext.Close();
            
            var bmp = new RenderTargetBitmap(_settings.WidthScreen, _settings.HeightScreen, 0, 0, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            background.Source = bmp;
            _fileWorker.SaveAsImage("Wallpaper/background.jpg", (BitmapSource)background.Source);
        }
        
        private FormattedText ParseText(string text, Brush color, int size)
        {
            return new FormattedText(text, new CultureInfo("ru-RU"), FlowDirection.LeftToRight,
                new Typeface(new FontFamily(_settings.Font), FontStyles.Normal, FontWeights.Normal, new FontStretch()),
                size, color);
        }
    }
}