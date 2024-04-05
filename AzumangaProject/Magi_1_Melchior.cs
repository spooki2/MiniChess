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
    public static bool checkRange(int piece, Move move)
    {
        int moveDistance = move.to - move.from;
        int fromRow = move.from / 6;
        int toRow = move.to / 6;
        int fromCol = move.from % 6;
        int toCol = move.to % 6;

        // row and col delta
        int rowDiff = toRow - fromRow;
        int colDiff = toCol - fromCol;

        switch (moveDistance)
        {
            case Offsets.Left:
                if (fromCol == 0) return false; // wrapping from left edge
                break;
            case Offsets.Right:
                if (fromCol == 5) return false; // wrapping from right edge
                break;
            case Offsets.diagLU:
                if (fromCol == 0 || fromRow == 0) return false;
                break;
            case Offsets.diagLD:
                if (fromCol == 0 || fromRow == 5) return false;
                break;
            case Offsets.diagRU:
                if (fromCol == 5 || fromRow == 0) return false;
                break;
        }

        return true;
    }

    public static Boolean isLegal(int piece, Move move)
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
            //check that pieces stay in bounds
            if (checkRange(piece, move) == false)
            {
                return false;
            }

            if (pieceType == Piece.Rook || pieceType == Piece.Bishop)
            {
                return rbLegal(piece, move);
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