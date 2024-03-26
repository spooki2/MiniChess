namespace AzumangaProject;

public static class MovementManager
{
    public static void placePiece(int piece, int pos)
    {
        Board.Square[pos / 6,pos % 6] = piece; //good transform?
    }

    public static void movePiece(int piece, int from, int to, Boolean bypass = false)
    {
        if (bypass || MoveEnforcer.isLegal(piece, to, from))
        {
            placePiece(piece, to);
            placePiece(Piece.None, from);
        }
    }

    public static void commandInterpreter(String comm)
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

        movePiece(Board.Square[dest[0] / 6,dest[0] % 6], dest[0], dest[1]);
    }
}