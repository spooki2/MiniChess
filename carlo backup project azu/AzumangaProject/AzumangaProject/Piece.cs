namespace AzumangaProject;

public static class Piece
{
    public const int None = 0;
    public const int Pawn = 1;
    public const int Bishop = 2;
    public const int Knight = 3;
    public const int Rook = 4;
    public const int King = 5;
    public const int White = 8;
    public const int Black = 16;

    /*
     if we image this as binary __|___
     left side will be white or black (01 or 10)
     and right side will be the piece
    */

    public static List<int> allPieces = new List<int> { Pawn, Bishop, Knight, Rook, King };
    public static List<int> allColoredPieces = new List<int> { Pawn | White, Bishop | White, Knight | White, Rook | White, King | White, Pawn | Black, Bishop | Black, Knight | Black, Rook | Black, King | Black };
    public static Dictionary<String, int> shortNames = new Dictionary<string, int>();
    public static Dictionary<int, String> pieceName = new Dictionary<int, string>();
    public static Dictionary<int, String> whiteShapes = new Dictionary<int, string>();
    public static Dictionary<int, String> blackShapes = new Dictionary<int, string>();

    static Piece()
    {
        pieceName[None] = "None";
        pieceName[Pawn] = "Pawn";
        pieceName[Bishop] = "Bishop";
        pieceName[Knight] = "Knight";
        pieceName[Rook] = "Rook";
        pieceName[King] = "King";

        whiteShapes[None] = " ";
        whiteShapes[Pawn] = "\u2659";
        whiteShapes[Bishop] = "\u2657";
        whiteShapes[Knight] = "\u2658";
        whiteShapes[Rook] = "\u2656";
        whiteShapes[King] = "\u2654";

        blackShapes[None] = " ";
        blackShapes[Pawn] = "\u265f";
        blackShapes[Bishop] = "\u265d";
        blackShapes[Knight] = "\u265e";
        blackShapes[Rook] = "\u265c";
        blackShapes[King] = "\u265a";

        shortNames["pa"] = Pawn;
        shortNames["bi"] = Bishop;
        shortNames["kn"] = Knight;
        shortNames["ro"] = Rook;
        shortNames["ki"] = King;

    }
}