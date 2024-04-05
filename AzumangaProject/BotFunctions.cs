namespace AzumangaProject;

public static class BotFunctions
{
    public static int evaluate(Board board)
    {

        int evalScore = 0;
        Dictionary<int,int> piecesDict = getPieces(board);
        foreach (int piece in piecesDict.Keys)
        {

            if (PieceManager.colorOnly(piece) == Piece.Black)
            {
                evalScore += PointSystem.pieceValue[(PieceManager.pieceOnly(piece))];
            }
            else
            {
                evalScore -= PointSystem.pieceValue[(PieceManager.pieceOnly(piece))];
            }

        }

        return evalScore;
    }


    public static Dictionary<int, int> getPieces(Board board)
    {
        Dictionary<int, int> piecesPos = new Dictionary<int, int>();
        for (int i = 0; i < 36; i++)
        {
            int piece = board.get1D(i);
            if (piece != Piece.None)
            {
                piecesPos[piece] = i;
            }
        }

        return piecesPos;
    }


    public static HashSet<Move> legalMoves(Board board)
    {
        Dictionary<int, int> pieces = getPieces(board);
        HashSet<Move> legalMoves = new HashSet<Move>();
        foreach (int from in pieces.Values)
        {
            int piece = board.get1D(from);
            for (int i = 0; i < 36; i++)
            {
                Move move = new Move(from, i);
                if (MovementManager.isMegaLegal(piece, move,board))
                {
                    legalMoves.Add(move);
                }
            }
        }
        return legalMoves;
    }

}