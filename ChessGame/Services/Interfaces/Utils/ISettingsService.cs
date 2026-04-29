using ChessGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Services
{
    public interface ISettingsService
    {
        SettingsData Load();
        void Save(SettingsData settings);
    }
}
