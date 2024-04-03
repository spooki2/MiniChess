namespace AzumangaProject;

public class SubBoards
{
    public int[,] Square = new int[6, 6];

    public static SubBoards cloneBoard()
    {
        SubBoards newBoard = new SubBoards();
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                newBoard.Square[i, j] = Board.Square[i, j];
            }
        }
        return newBoard;
    }

    public static void loadBoard(SubBoards subBoard)
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Board.Square[i, j] = subBoard.Square[i, j];
            }
        }
    }
}