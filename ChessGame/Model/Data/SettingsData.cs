using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Model
{
    public class SettingsData
    {
        public int Width { get; set; } = 1280;
        public int Height { get; set; } = 720;
        public bool IsFullScreen { get; set; } = false;
    }
}