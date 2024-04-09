namespace AzumangaProject;

public static class BotFunctions
{
    public static double evaluate(Board board)
    {
        double evalScore = 0;
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

        foreach (int piece in board.Inv)
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
            evalScore = -10000;
        }


        //adjusted mate bonus should be between -3 and 3
        double adjustedMateBonus = Math.Min(3, Math.Max(-3, mateBonus(board) / 10));
        if (adjustedMateBonus < 0)
        {
            Console.WriteLine("adjusted mate bonus: {0}", adjustedMateBonus);
        }

        return evalScore + adjustedMateBonus;
    }


    public static double mateBonus(Board board)
    {
        //encourage opponent king to be furthest from center
        double mateBonus = 0;
        int kingPos1D = 0;
        int[] whiteSumPos = [0, 0];
        int counterWhite = 0;
        int[] centerPos = [3, 3];
        for (int i = 0; i < 36; i++)
        {
            if (board.get1D(i) == (Piece.King | Piece.Black))
            {
                kingPos1D = i;
            }

            if (PieceManager.colorOnly(board.get1D(i)) == Piece.White)
            {
                whiteSumPos[0] += i % 6;
                whiteSumPos[1] += i / 6;
                counterWhite++;
            }
        }

        double[] whiteAveragePos = [whiteSumPos[0] / counterWhite, whiteSumPos[1] / counterWhite];


        int[] kingPos = [kingPos1D % 6, kingPos1D / 6];
        double kingCenterDistance = Math.Abs(centerPos[0] - kingPos[0]) + Math.Abs(centerPos[1] - kingPos[1]);
        double whiteKingDistance =
            Math.Abs(whiteAveragePos[0] - kingPos[0]) + Math.Abs(whiteAveragePos[1] - kingPos[1]);
        return kingCenterDistance + whiteKingDistance;
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
                if (MovementManager.isRespawnLegal(piece, i, board, Piece.Black))
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