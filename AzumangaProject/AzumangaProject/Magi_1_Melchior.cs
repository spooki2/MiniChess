namespace AzumangaProject;

using static Offsets;

public static class Magi_1_Melchior

{
    public static Dictionary<int, int[]> ruleMoves = new Dictionary<int, int[]>();

    static Magi_1_Melchior()
    {
        ruleMoves[Piece.None] = null;
        ruleMoves[Piece.Pawn] = new int[] { Up, Down, Left + Up, Right + Up, Left + Down, Right + Down };
        ruleMoves[Piece.Bishop] = new int[] { diagRU, diagRD, diagLU, diagLD };
        ruleMoves[Piece.Rook] = new int[] { Up, Down, Left, Right };
        ruleMoves[Piece.King] = new int[] { Up, Down, Left, Right, diagRU, diagRD, diagLU, diagLD };
        ruleMoves[Piece.Knight] = new int[]
        {
            2 * Up + Left, 2 * Up + Right, 2 * Down + Left, 2 * Down + Right, 2 * Left + Up, 2 * Left + Down,
            2 * Right + Up, 2 * Right + Down
        };
    }

    static Boolean rbLegal(int piece, Move move)
    {
        int pieceType = piece & 0b111;
        int[] delta =
            [Board.to2D(move.from)[0] - Board.to2D(move.to)[0], Board.to2D(move.from)[1] - Board.to2D(move.to)[1]];
        if (pieceType == Piece.Rook && (delta[0] == 0 || delta[1] == 0))
        {
            return true;
        }

        if (pieceType == Piece.Bishop && (Math.Abs(delta[0]) == Math.Abs(delta[1])))
        {
            return true;
        }

        return false;
    }


    public static bool boardWrapCheck(int piece, Move move) //memory intensive funciton
    {
        /* pseueo
        delta vector = afterPosVector - beforePosVector
        if listOfPossibleMovesVector contains delta vector
        return true

        return false
        */
        int[] deltaVector = new int[] {Board.to2D(move.to)[0] - Board.to2D(move.from)[0], Board.to2D(move.to)[1] - Board.to2D(move.from)[1]};
        foreach (int[] offsetMove in Offsets.OffsetVectorList)
        {
            if (offsetMove[0] == deltaVector[0] && offsetMove[1] == deltaVector[1])
            {
                return true;
            }
        }
        return false;
    }

    public static Boolean isLegal(int piece, Move move, Board board)
    {
        try
        {
            int pieceType = piece & 0b111;
            int pieceColor = piece & 0b11000;

            //check that actually moved
            if (move.from == move.to)
            {
                return false;
            }


            if (pieceType == Piece.Rook || pieceType == Piece.Bishop)
            {
                return rbLegal(piece, move);
            }

            //check that pieces stay in bounds
            if (boardWrapCheck(piece, move) == false)
            {
                return false;
            }

            foreach (int offset in ruleMoves[pieceType])
            {
                if (move.from + offset == move.to)
                {
                    return true;
                }
            }

            return false;
        }
        catch (Exception e)
        {
            //Console.WriteLine("LAYER 1 ENFORCER EXCEPTION !!!");
            //Console.WriteLine(e);
            return false;
        }
    }
}