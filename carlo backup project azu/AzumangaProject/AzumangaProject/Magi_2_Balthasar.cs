namespace AzumangaProject;

public class Magi_2_Balthasar
{
    public static Boolean pawnLegalDirection(int piece, Move move)
    {
        int pieceType = piece & 0b111;
        int pieceColor = piece & 0b11000;
        //pawn to empty space

        if (pieceColor == Piece.Black && (move.from < move.to))
        {
            return true;
        }

        if (pieceColor == Piece.White && (move.from > move.to))
        {
            return true;
        }

        return false;
    }


    public static Boolean pawnLegalMove(int piece, Move move,Board board)
    {
        int delta = move.to - move.from;
        if ((delta == Offsets.Up || delta == Offsets.Down))
        {
            if (board.get1D(move.to) == Piece.None)
            {
                return true;
            }
        }
        else
        {
            if (board.get1D(move.to) != Piece.None)
            {
                return true;
            }
        }

        return false;
    }

    public static Boolean bishopRookClearPathCheck(int piece, Move move,Board board)
    {
        int pieceType = piece & 0b111;
        int pieceColor = piece & 0b11000;
        int[] scanner = Board.to2D(move.from);

        int[] to2D = Board.to2D(move.to);
        int[] from2D = Board.to2D(move.from);
        int[] direction2D = new int[2];
        //messy implementation for divide by zero proofing:
        if (to2D[0] - from2D[0] == 0)
        {
            direction2D[0] = 0;
        }
        else
        {
            direction2D[0] = (to2D[0] - from2D[0]) / Math.Abs(to2D[0] - from2D[0]);
        }

        if (to2D[1] - from2D[1] == 0)
        {
            direction2D[1] = 0;
        }
        else
        {
            direction2D[1] = (to2D[1] - from2D[1]) / Math.Abs(to2D[1] - from2D[1]);
        }

        List<int> path = new List<int>();
        while (scanner[0] != to2D[0] || scanner[1] != to2D[1])
        {
            scanner[0] += direction2D[0];
            scanner[1] += direction2D[1];
            path.Add(Board.to1D(scanner));
        }
        path.RemoveAt(path.Count - 1); //no need for checking last piece

        foreach (int square in path)
        {
            if (board.get1D(square) != Piece.None)
            {
                return false;
            }
        }

        return true;
    }
    public static Boolean isLegal(int piece, Move move,Board board)
    {
        int pieceType = piece & 0b111;
        int pieceColor = piece & 0b11000;
        try
        {
            if ((board.get1D(move.to) & 0b11000) == pieceColor)
            {
                return false;
            }
            if (pieceType == Piece.Pawn)
            {
                return (pawnLegalDirection(piece, move) & pawnLegalMove(piece, move,board));
            }

            if (pieceType == Piece.Rook|| pieceType == Piece.Bishop)
            {
                return bishopRookClearPathCheck(piece, move,board);
                //return true;
            }

            return true;
        }

        catch (Exception e)
        {
            //Console.WriteLine("LAYER 2 ENFORCER EXCEPTION !!!");
            //Console.WriteLine(e);
            return false;
        }
    }
}