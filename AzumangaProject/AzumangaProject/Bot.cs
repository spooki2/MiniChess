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

    public static int evaluate(Move move)
    {
        return PointSystem.pieceValue[(Board.get1D(move.to)&0b111)];
    }

    public static Move bestChoice(int piece)
    {
        List<Move> colorLegalMoves = blackLegalMoves;
        int highestScore = 0;
        Move bestMove = colorLegalMoves[0]; //init value
        foreach (Move move in colorLegalMoves)
        {
            int moveScore = evaluate(move);
            if (moveScore > highestScore)
            {
                highestScore = moveScore;
                bestMove = move;
            }
        }
        return bestMove;
    }

    public static void allLegalMoves()
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
                    if ((piece & 0b11000) == Piece.White)
                    {
                        whiteLegalMoves.Add(move);
                    }
                    else if ((piece & 0b11000) == Piece.Black)
                    {
                        blackLegalMoves.Add(move);
                    }
                }
            }
        }
    }

    public static void pickLegalMove()
    {
        //List<Move> currMoves = moves % 2 == 0 ? whiteLegalMoves : blackLegalMoves;
        List<Move> colorLegalMoves = blackLegalMoves;
        Dictionary<int,Move> eval_Move = new Dictionary<int,Move>();
        foreach (Move move in colorLegalMoves)
        {
            eval_Move[evaluate(move)] = move;
        }
        Move bestestMove = eval_Move[eval_Move.Keys.Max()];
        if (colorLegalMoves.Count > 0)
        {
            MovementManager.movePiece(Board.get1D(bestestMove.from), bestestMove);
        }
        else
        {
            Console.WriteLine("LOST?? ERROR CASE");
        }
    }
}