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
    public double value;

    public BoardScore(Board board, double value)
    {
        this.board = board;
        this.value = value;
    }
}

public struct MoveScore
{
    public Move move;
    public Double value;

    public MoveScore(Move move, Double value)
    {
        this.move = move;
        this.value = value;
    }
}

public struct respawnMove
{
    public int piece;
    public int to;

    public respawnMove(int piece, int to)
    {
        this.piece = piece;
        this.to = to;
    }
}

public class Board
{
    public static Boolean Current;
    public static Board main = new Board();
    public static Board lastBoard = new Board(main);

    public static Boolean checkMate = false;

    public int[,] Square { get; set; } //shortcut get set
    public List<int> Inv { get; set; }

    public Board()
    {
        Square = new int[6, 6];
        Inv = new List<int>();
    }

    public void addToList(int piece)
    {
        Inv.Add(piece);
    }

    public void removeFromList(int piece)
    {
        Inv.Remove(piece);
    }


    //update lastBoard

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
        this.Square = new int[6, 6];
        this.Inv = new List<int>(board.Inv);
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                this.Square[i, j] = board.Square[i, j];
            }
        }
    }

    //comparator
    public static bool operator ==(Board a, Board b)
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (a.Square[i, j] != b.Square[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool operator !=(Board a, Board b)
    {
        return !(a == b);
    }
}