namespace AzumangaProject;

public static class BotController
{
    public static void play()
    {
        //update dict
        BotFunctions.updateDict();
        MoveScore defaultMove = new MoveScore(new Move(0, 0), 0);
        MoveScore bestMove = new MoveScore(new Move(0, 0), 0);
        //try except
        foreach (int piece in BotFunctions.pieceDictionary.Keys)
        {
            {
                MoveScore curr = BotFunctions.minimaxAlgorithm(new Move(BotFunctions.pieceDictionary[piece],0), 6, false);
                if (bestMove.value < curr.value)
                {
                    bestMove = curr;
                }
            }
        }

        if (bestMove.move.from == defaultMove.move.from&&bestMove.move.to == defaultMove.move.to)
        {
            Console.WriteLine("No legal moves found");
        }
        MovementManager.movePiece(bestMove.move);
        Console.WriteLine("-- value: " + bestMove.value);
    }
}