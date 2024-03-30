namespace AzumangaProject;

public class Layer2Enforcer
{
    public static Boolean isLegal(int piece, int from, int to)
    {
        //check that no self taking

        if ((Board.Square[from / 6, from % 6] & 0b11000) == (Board.Square[to / 6, to % 6] & 0b11000))
        {
            return false;
        }

        return true;
    }
}