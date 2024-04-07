namespace AzumangaProject;

public static class MovementManager
{
    public static void placePiece(int piece, int pos, Board board)
    {
        board.Square[pos / 6, pos % 6] = piece; //good transform?
    }

    public static Boolean isMegaLegal(int piece, Move move, Board board,Boolean _ignoreMagi3_=false)
    {
        if (Magi_1_Melchior.isLegal(piece, move, board))
        {
            if (Magi_2_Balthasar.isLegal(piece, move, board))
            {
                if (_ignoreMagi3_||Magi_3_Casper.isLegal(piece, move, Board.main))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static void movePiece(Move move, Board board,Boolean _ignoreMagi3_=false)
    {
        int piece = board.get1D(move.from);
        int from = move.from;
        int to = move.to;
        int targetPiece = board.get1D(to);
        if (isMegaLegal(piece, move, board,_ignoreMagi3_))
        {
            if (targetPiece != Piece.None)
            {
                PieceManager.take(piece, targetPiece);
            }

            placePiece(piece, to, board);
            placePiece(Piece.None, from, board);
        }
    }

    public static Boolean commandInterpreter(String comm, Board board)
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

            isValid = isMegaLegal(board.Square[dest[0] / 6, dest[0] % 6], new Move(dest[0], dest[1]), board);
            movePiece(new Move(dest[0], dest[1]), board);
        }
        catch (Exception e)
        {
            Console.WriteLine("ILLEGAL MOVE");
        }

        return isValid;
    }
}