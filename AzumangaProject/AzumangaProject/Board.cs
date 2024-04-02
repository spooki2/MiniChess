namespace AzumangaProject;
public struct Move
{
    public readonly int from; //readonly = can only be read
    public readonly int to;
    public Move(int from, int to)
    {
        this.from = from;
        this.to = to;
    }
}

public static class Board
{
    public static int get1D(int square)
    {
        return Board.Square[square / 6, square % 6];
    }
    public static int[] to2D(int pos)
    {
        return [pos / 6, pos % 6];
    }

    public static int to1D(int[] pos)
    {
        return pos[0] * 6 + pos[1];
    }
    public static int[,] Square = new int[6, 6];


    public static void clearBoard()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Square[i,j] = Piece.None;
            }
        }
    }
}