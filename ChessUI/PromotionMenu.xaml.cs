using ChessLogic;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChessUI.Views
{
    /// <summary>
    /// Interaction logic for PromotionMenu.xaml
    /// </summary>
    public partial class PromotionMenu : UserControl
    {
        public event Action<PieceType> PieceSelected;
        public PromotionMenu(Player player)
        {
            InitializeComponent();

            QueenImage.Source = Images.GetImage(player, PieceType.Queen);
            RookImage.Source = Images.GetImage(player, PieceType.Rook);
            BishopImage.Source = Images.GetImage(player, PieceType.Bishop);
            KnightImage.Source = Images.GetImage(player, PieceType.Knight);
        }

        private void QueenImg_MouseDown(object sender, MouseButtonEventArgs e) 
            => PieceSelected?.Invoke(PieceType.Queen);
        private void RookImg_MouseDown(object sender, MouseButtonEventArgs e)
            => PieceSelected?.Invoke(PieceType.Rook);
        private void BishopImg_MouseDown(object sender, MouseButtonEventArgs e)
            => PieceSelected?.Invoke(PieceType.Bishop);
        private void KnightImg_MouseDown(object sender, MouseButtonEventArgs e)
            => PieceSelected?.Invoke(PieceType.Knight);

    }
}
