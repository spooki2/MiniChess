namespace AzumangaProject;

public static class PieceManager
{
    public static int colorOnly(int piece)
    {
        return piece & 0b11000;
    }

    public static int pieceOnly(int piece)
    {
        return piece & 0b111;
    }

    public static int switchPieceColor(int piece)
    {
        int blankpiece = pieceOnly(piece);
        if(colorOnly(piece)==Piece.Black)
        {
            return blankpiece|Piece.White;
        }
        else
        {
            return blankpiece|Piece.Black;
        }
    }

}