using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ChessLogic;

namespace ChessUI
{
    public static class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            [PieceType.Pawn] = LoadImage("Assets/PawnW.png"),
            [PieceType.Bishop] = LoadImage("Assets/BishopW.png"),
            [PieceType.Rook] = LoadImage("Assets/RookW.png"),
            [PieceType.Knight] = LoadImage("Assets/KnightW.png"),
            [PieceType.Queen] = LoadImage("Assets/QueenW.png"),
            [PieceType.King] = LoadImage("Assets/KingW.png"),
        };

        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            [PieceType.Pawn] = LoadImage("Assets/PawnB.png"),
            [PieceType.Bishop] = LoadImage("Assets/BishopB.png"),
            [PieceType.Rook] = LoadImage("Assets/RookB.png"),
            [PieceType.Knight] = LoadImage("Assets/KnightB.png"),
            [PieceType.Queen] = LoadImage("Assets/QueenB.png"),
            [PieceType.King] = LoadImage("Assets/KingB.png"),
        };


        private static ImageSource LoadImage(string path) => 
            new BitmapImage(new Uri(path, UriKind.Relative));

        public static ImageSource GetImage(Player color, PieceType type)
        {
            return color switch
            {
                Player.White => whiteSources[type],
                Player.Black => blackSources[type],
                _ => null
            };
        }
        public static ImageSource GetImage(Piece piece) =>
            piece != null ?
            GetImage(piece.Color, piece.Type) :
            null;
    }
}
