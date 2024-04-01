namespace AzumangaProject;

public class Layer2Enforcer
{
    public static Boolean isLegal(int piece, Move move)
    {
        try
        {
            int from = move.from;
            int to = move.to;
            int[] to2D = Board.to2D(to);
            int[] from2D = Board.to2D(from);

            if ((piece & 0b111) == Piece.Bishop)
            {
                int[] dir2D =
                [
                    (to2D[0] - from2D[0]) / Math.Abs(to2D[0] - from2D[0]),
                    (to2D[1] - from2D[1]) / Math.Abs(to2D[1] - from2D[1])
                ];
                int[] scanDiag = [from2D[0] + dir2D[0], from2D[1] + dir2D[1]];

                while (scanDiag[0] > to2D[0] && scanDiag[1] > to2D[1])
                {
                    if (Board.Square[scanDiag[0], scanDiag[1]] != Piece.None)
                    {
                        return false;
                    }

                    scanDiag[0] += dir2D[0];
                    scanDiag[1] += dir2D[1];
                }
            }

            if ((Board.Square[from / 6, from % 6] & 0b11000) == (Board.Square[to / 6, to % 6] & 0b11000))
            {
                return false;
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