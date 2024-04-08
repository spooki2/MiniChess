namespace AzumangaProject;

public static class BotController
{
    public static void play(Board board,int depth)
    {
        BoardScore bestBoard = BotBrain.minimax(board, depth, true);


        if (BotFunctions.evaluate(board) >= 50)
        {
            Board.checkMate = true;
            //GAME WON!!!!! 07/04/2024 3:14
        }
        for (int i = 0; i < 36; i++)
        {
            board.Square[i / 6, i % 6] = bestBoard.board.Square[i / 6, i % 6];
        }
    }
}