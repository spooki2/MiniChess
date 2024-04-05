namespace AzumangaProject;

public struct Move
{
    public int from;
    public int to;
    public Move(int from, int to)
    {
        this.from = from;
        this.to = to;
    }
}

public struct BoardScore
{
    public Board board;
    public int value;
    public BoardScore(Board board, int value)
    {
        this.board = board;
        this.value = value;
    }
}
public struct MoveScore
{
    public Move move;
    public int value;
    public MoveScore(Move move, int value)
    {
        this.move = move;
        this.value = value;
    }
}

public class Board
{
    public static Board main = new Board();

    public int[,] Square { get; set; } //shortcut get set

    public Board()
    {
        Square = new int[6, 6];
        clearBoard();
    }

    public void clearBoard()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Square[i, j] = Piece.None;
            }
        }
    }

    public int get1D(int square)
    {
        return Square[square / 6, square % 6];
    }

    public static int[] to2D(int pos)
    {
        return new int[] { pos / 6, pos % 6 };
    }

    public static int to1D(int[] pos)
    {
        return pos[0] * 6 + pos[1];
    }

    //copy constructor
    public Board(Board board)
    {
        Square = new int[6, 6];
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Square[i, j] = board.Square[i, j];
            }
        }
    }
}