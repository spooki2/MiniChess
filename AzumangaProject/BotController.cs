namespace AzumangaProject;

public static class BotController
{
    public static void play(Board board)
    {
        BoardScore bestBoard = BotBrain.minimax(board, 3, true);
        //make main board into bestboard
        Console.WriteLine("move value: "+ bestBoard.value);
        for (int i = 0; i < 36; i++)
        {
            board.Square[i / 6, i % 6] = bestBoard.board.Square[i / 6, i % 6];
        }
    }
}