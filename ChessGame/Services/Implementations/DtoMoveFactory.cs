using ChessGame.Model;
using ChessGame.Model.Moves;
using ChessGame.Services.Interfaces;

namespace ChessGame.Services.Implementations
{
    public class DtoMoveFactory : IDtoMoveFactory
    {
        public Move GetMoveFromDTO(DtoMove dtoMove)
        {
            Position fromPos = new Position(dtoMove.FromPosRow, dtoMove.FromPosColumn);
            Position toPos = new Position(dtoMove.ToPosRow, dtoMove.ToPosColumn);

            return dtoMove.Type switch
            {
                MoveType.NormalMove => new NormalMove(fromPos, toPos),
                _ => null,
            };
        }

        public DtoMove GetMoveToDTO(Move Move)
        {
            return new DtoMove(Move);
        }
    }
}
