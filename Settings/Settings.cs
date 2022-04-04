using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WallpaperChanger.Models;
using WallpaperChanger.Models.Enums;

namespace WallpaperChanger.Settings
{
    public class Settings
    {
        public int WidthScreen { get; set; }
        public int HeightScreen { get; set; }
        public int IndentFromHorizontalEdge { get; set; }
        public int IndentFromVerticalEdge { get; set; }
        public Color BackgroundColor { get; set; }
        public string Font { get; set; }
        public DayTime DayTime { get; set; }
        public Date Date { get; set; }
        public Temperature Temperature { get; set; }
        public List<CustomString> CustomStrings { get; set; }

        public void Normalize()
        {
            if (DayTime.DynamicX != HorizontalPositions.Empty)
                DayTime.X = GetWidth(DayTime.DynamicX);
            if (DayTime.DynamicY != VerticalPositions.Empty)
                DayTime.Y = GetHeight(DayTime.DynamicY);
            
            if (Date.DynamicX != HorizontalPositions.Empty)
                Date.X = GetWidth(Date.DynamicX);
            if (Date.DynamicY != VerticalPositions.Empty)
                Date.Y = GetHeight(Date.DynamicY);
            
            if (Temperature.DynamicX != HorizontalPositions.Empty)
                Temperature.X = GetWidth(Temperature.DynamicX);
            if (Temperature.DynamicY != VerticalPositions.Empty)
                Temperature.Y = GetHeight(Temperature.DynamicY);

            foreach (var customString in CustomStrings)
            {
                if (customString.DynamicX != HorizontalPositions.Empty)
                    customString.X = GetWidth(customString.DynamicX);
                if (customString.DynamicY != VerticalPositions.Empty)
                    customString.Y = GetHeight(customString.DynamicY);
            }
        }

        private int GetWidth(HorizontalPositions position)
        {
            switch (position)
            {
                case HorizontalPositions.Left:
                    return IndentFromHorizontalEdge;
                case HorizontalPositions.Center:
                    return WidthScreen / 2;
                case HorizontalPositions.Right:
                    return WidthScreen - IndentFromHorizontalEdge;
            }

            return -1;
        }
        
        private int GetHeight(VerticalPositions position)
        {
            switch (position)
            {
                case VerticalPositions.Up:
                    return IndentFromVerticalEdge;
                case VerticalPositions.Center:
                    return HeightScreen / 2;
                case VerticalPositions.Down:
                    return HeightScreen - IndentFromVerticalEdge;
            }

            return -1;
        }
    }
}