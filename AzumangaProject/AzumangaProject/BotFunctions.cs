namespace AzumangaProject;

public static class BotFunctions
{
    public static Dictionary<int, int> pieceDictionary = new Dictionary<int, int>();
    //static List<Move> whiteLegalMoves = new List<Move>();
    //static List<Move> blackLegalMoves = new List<Move>();

    public static void updateDict()
    {
        pieceDictionary.Clear();
        for (int i = 0; i < 36; i++)
        {
            int[] i2D = Board.to2D(i);
            int piece = Board.Square[i2D[0], i2D[1]];
            if (piece != Piece.None)
            {
                pieceDictionary[piece] = i; // mapping each piece type to its board index
            }
        }
    }

    public static int evaluate(Move move)
    {
        //calculate a fair score for black where nothing = 0 and pieces taken = -x
        //consider the fact that it needs to calculate for the move black is ABOUT to make...

    }

    public static List<Move> legalMoves(int from)
    {
        List<Move> legalMoves = new List<Move>();
        int piece = Board.get1D(from);
        for (int i = 0; i < 36; i++)
        {
            Move move = new Move(from, i);
            if (MovementManager.isMegaLegal(piece, move))
            {
                legalMoves.Add(move);
            }
        }

        return legalMoves;
    }

    public static MoveScore minimaxAlgorithm(Move move, int depth, Boolean botTurn)
    {
        //temp

        if (depth == 0) //or game over!
        {
            int evalScore = evaluate(move);
            return new MoveScore(move, evalScore);
        }

        List<Move> currLegalMoves = legalMoves(move.from);
        if (botTurn == true)
        {
            MoveScore maxEval = new MoveScore(move, -10000);
            //black Leagl moves using legalMoves func
            foreach (Move currMove in currLegalMoves)
            {
                MoveScore eval = minimaxAlgorithm(currMove, depth - 1, false);
                if (eval.value > maxEval.value)
                {
                    maxEval = eval;
                }
            }

            return maxEval;
        }
        else
        {
            MoveScore minEval = new MoveScore(move, 10000);
            foreach (Move currMove in currLegalMoves)
            {
                MoveScore eval = minimaxAlgorithm(move, depth - 1, true);
                if (eval.value < minEval.value)
                {
                    minEval = eval;
                }
            }

            return minEval;
        }
    }
}