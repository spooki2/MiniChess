namespace AzumangaProject;

public static class BotFunctions
{
    public static int evaluate(Board board)
    {
        int evalScore = 0;
        List<int> pieces = getPieces(board);
        foreach (int piece in pieces)
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

        if(!pieces.Contains(Piece.King|Piece.Black))
        {
            evalScore =- 10000;
        }
        return evalScore;
    }


    public static List<int> getPieces(Board board)
    {
        List<int> piecesPos = new List<int>();
        for (int i = 0; i < 36; i++)
        {
            int piece = board.get1D(i);
            if (piece != Piece.None)
            {
                piecesPos.Add(piece);
            }
        }

        return piecesPos;
    }

    public static List<respawnMove> legalRespawns(Board board)
    {
        List<int> capturedPieces = PieceManager.getInv(board);
        List<respawnMove> legalRespawns = new List<respawnMove>();
        foreach (int piece in capturedPieces)
        {
            for (int i = 0; i < 36; i++)
            {
                if (MovementManager.isRespawnLegal(piece, i, board,Piece.Black))
                {
                    respawnMove move = new respawnMove(piece, i);
                    legalRespawns.Add(move);
                }
            }
        }

        return legalRespawns;
    }

    public static HashSet<Move> legalMoves(Board board, Boolean _ignoreMagi3_ = false)
    {
        List<int> pieces = getPieces(board);
        List<int> positions = new List<int>();
        for (int i = 0; i < 36; i++)
        {
            if (board.get1D(i) != Piece.None)
            {
                positions.Add(i);
            }
        }

        HashSet<Move> legalMoves = new HashSet<Move>();
        foreach (int from in positions)
        {
            int piece = board.get1D(from);
            for (int i = 0; i < 36; i++)
            {
                Move move = new Move(from, i);
                if (MovementManager.isMegaLegal(piece, move, board, _ignoreMagi3_))
                {
                    legalMoves.Add(move);
                }
            }
        }

        return legalMoves;
    }
}