namespace AzumangaProject;

public static class PieceManager
{
    public static List<int> blackInv = new List<int>();
    public static List<int> whiteInv = new List<int>();

    public static void take(int taker, int taken)
    {
        taken = taken & 0b111;
        if ((taker & 0b11000) == Piece.Black)
        {
            blackInv.Add(taken);
            PointSystem.BlackPoints += PointSystem.pieceValue[taken];
        }
        else
        {
            whiteInv.Add(taken);
            PointSystem.WhitePoints += PointSystem.pieceValue[taken];
        }
    }
}