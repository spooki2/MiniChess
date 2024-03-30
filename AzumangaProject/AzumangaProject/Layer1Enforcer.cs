namespace AzumangaProject;

using static Offsets;

public static class Layer1Enforcer

{
    public static Dictionary<int, int[]> ruleMoves = new Dictionary<int, int[]>();

    static Layer1Enforcer()
    {
        ruleMoves[Piece.None] = null;
        ruleMoves[Piece.Pawn] = new int[] { Up, Down, Left + Up, Right + Up, Left + Down, Right + Down };
        List<int> bishopMoves = new List<int>();
        List<int> rookMoves = new List<int>();
        for (int i = 1; i <= 1; i++)
        {
            bishopMoves.Add(i * diagRU);
            bishopMoves.Add(i * diagRD);
            bishopMoves.Add(i * diagLU);
            bishopMoves.Add(i * diagLD);
            rookMoves.Add(i * Up);
            rookMoves.Add(i * Down);
            rookMoves.Add(i * Left);
            rookMoves.Add(i * Right);
        }

        ruleMoves[Piece.Bishop] = bishopMoves.ToArray();
        ruleMoves[Piece.Rook] = rookMoves.ToArray();

        ruleMoves[Piece.Knight] = new int[]
            { knightUUL, knightUUR, knightRRU, knightRRD, knightDDR, knightDDL, knightLLD, knightLLU };
        ruleMoves[Piece.King] = new int[] { Up, Down, Left, Right, diagRU, diagRD, diagLU, diagLD };
    }

    public static Boolean isLegal(int piece, int from, int to)
    {
        int color = piece & 0b11000;
        piece = piece & 0b111;
        int rowDelta = Math.Abs((to / 6) - (from / 6));
        int colDelta = Math.Abs((to % 6) - (from % 6));
        int delta = Math.Abs(to - from);

        foreach (int possibleMove in ruleMoves[piece])
        {
            if (piece == Piece.Bishop)
            {
                if (rowDelta == colDelta)
                {
                    return true;
                }
            }
            else if (piece == Piece.Rook)
            {
                if (rowDelta == 0 || colDelta == 0)
                {
                    return true;
                }
            }
            else if (piece == Piece.Pawn)
            {
                if (possibleMove == delta)
                {
                    if (possibleMove == Up || possibleMove == Down)
                    {
                        if (color == Piece.Black && (from > to))
                        {
                            return true;
                        }

                        if (color == Piece.White && (from < to))
                        {
                            return true;
                        }
                    }
                    //flipped vals, should be to. why works?
                    else if (Board.Square[from / 6, from % 6] != Piece.None)
                    {
                        if (color == Piece.Black && (from > to))
                        {
                            return true;
                        }

                        if (color == Piece.White && (from < to))
                        {
                            return true;
                        }

                        return false;
                    }
                }
            }
            else if (possibleMove == delta)
            {
                return true;
            }
        }

        return false;
    }
}