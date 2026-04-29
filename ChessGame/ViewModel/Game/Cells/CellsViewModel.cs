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
    public class CellsViewModel : BaseViewModel
    {
        public ObservableCollection<CellViewModel> BoardCells { get; set; } = new ObservableCollection<CellViewModel>();
        public CellsViewModel()
        {

        }
        public void InitializeCells()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    BoardCells.Add(new CellViewModel
                    {
                        Position = new Position(r, c),
                        ImagePath = null
                    });
                }
            }
        }

        public void DrawBoard(IBoard board)
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Piece piece = board[r, c];
                    int index = r * 8 + c;
                    BoardCells[index].ImagePath = Images.GetImage(piece);
                }
            }
        }
    }
}
