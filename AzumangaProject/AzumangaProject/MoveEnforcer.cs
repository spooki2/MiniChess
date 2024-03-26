namespace AzumangaProject;

using static Offsets;

public static class MoveEnforcer

{
    public static Dictionary<int, int[]> ruleMoves = new Dictionary<int, int[]>();

    static MoveEnforcer()
    {
        ruleMoves[Piece.None] = null;
        ruleMoves[Piece.Pawn] = new int[] { Up, Down };
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
            else if (possibleMove == delta)
            {
                return true;
            }
        }

        return false;
    }
}