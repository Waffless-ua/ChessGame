using ChessGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChessGame.ViewModel.Game
{
    public class HighlightsViewModel : BaseViewModel
    {
        public ObservableCollection<HighlightViewModel> HighlightCells { get; set; } = new ObservableCollection<HighlightViewModel>();
        private readonly Dictionary<Position, Move> _moveCache;
        public HighlightsViewModel(Dictionary<Position, Move> moveCache)
        {
            _moveCache = moveCache;
        }
        public void InitializeHighlights()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    HighlightCells.Add(new HighlightViewModel
                    {
                        Position = new Position(r, c),
                        Brush = Brushes.Transparent
                    });
                }
            }
        }
        public void ShowHighlights()
        {
            Color color = Color.FromArgb(150, 125, 255, 125);

            foreach (Position to in _moveCache.Keys)
            {
                HighlightCells.First(item => item.Position == to).Brush = new SolidColorBrush(color);
            }
        }

        public void HideHighlights()
        {
            foreach (Position to in _moveCache.Keys)
            {
                HighlightCells.First(item => item.Position == to).Brush = Brushes.Transparent;
            }
        }
    }
}
