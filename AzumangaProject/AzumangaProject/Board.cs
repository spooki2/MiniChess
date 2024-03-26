namespace AzumangaProject;

public static class Board
{
    public static int[,] Square = new int[6, 6];


    public static void clearBoard()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Square[i,j] = 0;
            }
        }
    }
}