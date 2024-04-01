namespace AzumangaProject;

public static class Bot
{
    static Dictionary<int, int> pieceDictionary = new Dictionary<int, int>();
    static List<Move> whiteLegalMoves = new List<Move>();
    static List<Move> blackLegalMoves = new List<Move>();
    static Random rand = new Random();

    public static void updateDict()
    {
        pieceDictionary.Clear();
        for (int i = 0; i < 36; i++)
        {
            int[] i2D = Board.to2D(i);
            int piece = Board.Square[i2D[0], i2D[1]];
            if (piece != Piece.None) // gcode
            {
                pieceDictionary[piece] = i; // mapping each piece type to its board index
            }
        }
    }

    public static void allLegalMoves(int moves)
    {
        whiteLegalMoves.Clear();
        blackLegalMoves.Clear();

        foreach (KeyValuePair<int, int> entry in pieceDictionary)
        {
            int piece = entry.Key;
            int position = entry.Value;
            for (int i = 0; i < 36; i++)
            {
                Move move = new Move(position, i);
                if (MovementManager.isMegaLegal(piece, move))
                {
                    if (moves % 2 == 0 && (piece & 0b11000) == Piece.White)
                    {
                        whiteLegalMoves.Add(move);
                    }
                    else if (moves % 2 == 1 && (piece & 0b11000) == Piece.Black)
                    {
                        blackLegalMoves.Add(move);
                    }
                }
            }
        }
    }

    public static void pickLegalMove(int moves)
    {
        List<Move> currMoves = moves % 2 == 0 ? whiteLegalMoves : blackLegalMoves;
        if (currMoves.Count > 0)
        {
            int index = rand.Next(currMoves.Count);
            Move move = currMoves[index];
            MovementManager.movePiece(Board.Square[move.from / 6, move.from % 6], move);
        }
    }
}