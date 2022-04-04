﻿using System.Drawing;
using WallpaperChanger.Models.Enums;

namespace WallpaperChanger.Models
{
    public class Temperature
    {
        public bool IsEnabled { get; set; }
        public Color Color { get; set; }
        public int Size { get; set; }
        public string AppId { get; set; }
        public string City { get; set; }
        public HorizontalPositions DynamicX { get; set; }
        public VerticalPositions DynamicY { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}