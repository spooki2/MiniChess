namespace AzumangaProject;

public static class BotFunctions
{
    public static double evaluate(Board board)
    {
        double evalScore = 0;
        HashSet<int> pieces = new HashSet<int>(getPieces(board));
        foreach (int invp in board.Inv)
        {
            pieces.Add(invp);
        }

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


        if (!pieces.Contains(Piece.King | Piece.Black))
        {
            evalScore += -10000;
        }

        if (!pieces.Contains(Piece.King | Piece.White))
        {
            evalScore += 1000;
        }


        return evalScore;
    }


    public static List<int> getPieces(Board board)
    {
        List<int> pieces = new List<int>();
        for (int i = 0; i < 36; i++)
        {
            int piece = board.get1D(i);
            if (piece != Piece.None)
            {
                pieces.Add(piece);
            }
        }

        return pieces;
    }

    public static HashSet<respawnMove> legalRespawns(Board board)
    {
        HashSet<respawnMove> legalRespawns = new HashSet<respawnMove>();


        List<int> capturedPieces = board.Inv;
        foreach (int piece in capturedPieces)
        {
            for (int i = 0; i < 36; i++)
            {
                if (MovementManager.isRespawnLegal(piece, i, board))
                {
                    respawnMove move = new respawnMove(piece, i);
                    legalRespawns.Add(move);
                }
            }
        }

        return legalRespawns;
    }

    public static HashSet<Move> legalMoves(Board board, int side,Boolean scanRespawns = false)
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
                if ((side == 0 || PieceManager.colorOnly(piece) == side) &&
                    MovementManager.isMegaLegal(piece, move, board))
                {
                    legalMoves.Add(move);
                }
            }
        }

        //add legalRespawns
        if (scanRespawns)
        {
            HashSet<respawnMove> legalRespawnList = legalRespawns(board);
            foreach (respawnMove move in legalRespawnList)
            {
                Move newMove = new Move(-1*move.piece, move.to);
                legalMoves.Add(newMove);
            }
        }
        return legalMoves;
    }
}