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

    public static List<int> getInv(Board board)
    {
        Dictionary<int, int> pieceTimes = new Dictionary<int, int>();
        List<int> boardPieces = BotFunctions.getPieces(board);
        List<int> pieces = new List<int>();
        foreach (int piece in Piece.allPieces)
        {
            pieceTimes[piece | Piece.Black] = 0;
        }

        foreach (int piece in Piece.allPieces)
        {
            pieceTimes[piece | Piece.White] = 0;
        }

        foreach (int piece in boardPieces)
        {
            pieceTimes[piece]++;
        }


        foreach (int piece in pieceTimes.Keys)
        {
            if (pieceTimes[piece] == 0)
            {

                if(pieceTimes[PieceManager.pieceOnly(piece)|Piece.Black]!=2&&pieceTimes[PieceManager.pieceOnly(piece)|Piece.White]!=2)
                {
                    pieces.Add(piece);
                }
            }
        }

        return pieces;
    }
}