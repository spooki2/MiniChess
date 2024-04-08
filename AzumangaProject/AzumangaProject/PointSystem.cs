namespace AzumangaProject;

public static class PointSystem
{
    public static Dictionary<int, int> pieceValue = new Dictionary<int, int>();

    static PointSystem()
    {
        pieceValue[Piece.None] = 0;
        pieceValue[Piece.Pawn] = 1;
        pieceValue[Piece.Bishop] = 3;
        pieceValue[Piece.Knight] = 3;
        pieceValue[Piece.Rook] = 5;
        pieceValue[Piece.King] = 100;
    }
}