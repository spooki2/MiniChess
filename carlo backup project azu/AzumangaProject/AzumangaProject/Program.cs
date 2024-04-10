using System.Reflection.Metadata;
using AzumangaProject;
using System.Text;


Console.OutputEncoding = Encoding.UTF8;

static String toShape(int piece)
{
    int color = piece & 0b11000;
    int type = piece & 0b111;

    return (color == 8) ? Piece.whiteShapes[type] : Piece.blackShapes[type];
}

static String listToStr(List<int> list)
{
    String str = "";
    foreach (int piece in list)
    {
        if (PieceManager.colorOnly(piece) == Piece.Black)
        {
            str += Piece.blackShapes[PieceManager.pieceOnly(piece)] + " ";
        }
        else
        {
            str += Piece.whiteShapes[PieceManager.pieceOnly(piece)] + " ";
        }
    }

    return str;
}

//start
void initGame()
{
    Board.main.Square[0, 2] = Piece.Black | Piece.Bishop;
    Board.main.Square[0, 3] = Piece.Black | Piece.Knight;
    Board.main.Square[0, 4] = Piece.Black | Piece.Rook;
    Board.main.Square[0, 5] = Piece.Black | Piece.King;
    Board.main.Square[1, 5] = Piece.Black | Piece.Pawn;

    Board.main.Square[4, 0] = Piece.White | Piece.Pawn;
    Board.main.Square[5, 0] = Piece.White | Piece.King;
    Board.main.Square[5, 1] = Piece.White | Piece.Rook;
    Board.main.Square[5, 2] = Piece.White | Piece.Knight;
    Board.main.Square[5, 3] = Piece.White | Piece.Bishop;
}

void initGameTest()
{
}

void updateUI()
{
    List<int> blackInv = new List<int>();
    List<int> whiteInv = new List<int>();
    List<int> capturedPieces = Board.main.Inv;

    foreach (int piece in capturedPieces)
    {
        if (PieceManager.colorOnly(piece) == Piece.Black)
        {
            blackInv.Add(piece);
        }
        else
        {
            whiteInv.Add(piece);
        }
    }

    int col = 6;
    Console.WriteLine("    a   b   c   d   e   f  ");
    for (int i = 0; i < 6; i++)
    {
        Console.WriteLine(asciiUI.row1);
        Console.WriteLine(col + String.Format(asciiUI.row2, toShape(Board.main.Square[i, 0]),
            toShape(Board.main.Square[i, 1]), toShape(Board.main.Square[i, 2]), toShape(Board.main.Square[i, 3]),
            toShape(Board.main.Square[i, 4]), toShape(Board.main.Square[i, 5])));
        col--;
    }

    Console.WriteLine(asciiUI.row1);

    Console.WriteLine("Black: " + listToStr(blackInv));
    Console.WriteLine("White: " + listToStr(whiteInv));
}

void checkMate()
{
    Console.WriteLine(
        "   ____   _   _   _____    ____   _  __\n  / ___| | | | | | ____|  / ___| | |/ /\n | |     | |_| | |  _|   | |     | ' / \n | |___  |  _  | | |___  | |___  | . \\ \n  \\____| |_| |_| |_____|  \\____| |_|\\_\\\n                                       \n  __  __      _      _____   _____     \n |  \\/  |    / \\    |_   _| | ____|    \n | |\\/| |   / _ \\     | |   |  _|      \n | |  | |  / ___ \\    | |   | |___     \n |_|  |_| /_/   \\_\\   |_|   |_____|    \n                                       ");
    while (true) ;
    {
    }
}

initGame();
while (true)
{

    updateUI();
    Board.lastBoard = new Board(Board.main);
    Board.Current = true;
    string command = "";
    Console.Write("Enter move: ");
    command = Console.ReadLine();
    Boolean validMove = MovementManager.commandInterpreter(command, Board.main);

    if (Math.Abs(BotFunctions.evaluate(Board.main)) > 50)
    {
        checkMate();
    }
    if (validMove)
    {
        updateUI();
        BotController.play(Board.main, depth: 3, times: 100);
    }

    if (Math.Abs(BotFunctions.evaluate(Board.main)) > 50)
    {
        checkMate();
    }
}