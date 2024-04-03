namespace AzumangaProject;

public static class MovementManager
{
    public static void placePiece(int piece, int pos)
    {
        Board.Square[pos / 6, pos % 6] = piece; //good transform?
    }

    public static Boolean isMegaLegal(int piece, Move move)
    {
        if (Magi_1_Melchior.isLegal(piece, move))
        {
            if (Magi_2_Balthasar.isLegal(piece, move))
            {
                return true;
            }
        }

        return false;
    }

    public static void movePiece(Move move)
    {
        int piece = Board.get1D(move.from);
        int from = move.from;
        int to = move.to;
        int targetPiece = Board.Square[to / 6, to % 6];
        if (isMegaLegal(piece, move))
        {
            if (targetPiece != Piece.None)
            {
                PieceManager.take(piece, targetPiece);
            }

            placePiece(piece, to);
            placePiece(Piece.None, from);
        }
    }

    public static Boolean commandInterpreter(String comm)
    {
        Boolean isValid = false;
        try
        {
            //proper command should be [CURRENT POS] [NEXT POS]
            comm = comm.ToLower();
            comm = comm.Replace(" ", "");
            string fromSTR = comm.Substring(0, 2);
            string toSTR = comm.Substring(2);
            int from = 0;
            int to = 0;
            string[] strArr = { fromSTR, toSTR };
            int[] dest = { from, to }
                ;
            for (int i = 0; i <= 1; i++)
            {
                foreach (char ch in strArr[i])
                {
                    if ('a' <= ch && ch <= 'f')
                    {
                        dest[i] += (ch - 'a');
                    }
                    else
                    {
                        dest[i] += (6 - (ch - '0')) * 6;
                    }
                }
            }

            isValid = isMegaLegal(Board.Square[dest[0] / 6, dest[0] % 6], new Move(dest[0], dest[1]));
            movePiece(new Move(dest[0], dest[1]));
        }
        catch (Exception e)
        {
            Console.WriteLine("ILLEGAL MOVE");
        }
        return isValid;
    }
}