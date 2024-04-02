﻿using System.Reflection.Metadata;
using AzumangaProject;
using System.Text;

// ! ! ! ! ! !! ! ! !! !

//make methods bot by color

//new bot controller class with recursive functions

//add board saveCheckPoint loadCheckPoint (optional)

// ! ! ! ! ! !! ! ! !! !


Console.OutputEncoding = Encoding.UTF8;

static String vocal(int piece)
{
    int color = piece & 0b11000;
    String result = (color == 8) ? "White" : "Black";
    result += " ";
    int type = piece & 0b111;
    result += Piece.pieceName[type];
    return result;
}

static String toShape(int piece)
{
    int color = piece & 0b11000;
    int type = piece & 0b111;

    return (color == 8) ? Piece.whiteShapes[type] : Piece.blackShapes[type];
}

static String listToStr(List<int> list)
{
    String str = "";
    list.ForEach(p => str += Piece.blackShapes[p] + " ");
    return str;
}

//start
static void initGame()
{
    Board.clearBoard();
    Board.Square[0, 2] = Piece.Black | Piece.Bishop;
    Board.Square[0, 3] = Piece.Black | Piece.Knight;
    Board.Square[0, 4] = Piece.Black | Piece.Rook;
    Board.Square[0, 5] = Piece.Black | Piece.King;
    Board.Square[1, 5] = Piece.Black | Piece.Pawn;

    Board.Square[4, 0] = Piece.White | Piece.Pawn;
    Board.Square[5, 0] = Piece.White | Piece.King;
    Board.Square[5, 1] = Piece.White | Piece.Rook;
    Board.Square[5, 2] = Piece.White | Piece.Knight;
    Board.Square[5, 3] = Piece.White | Piece.Bishop;
}

void updateUI()
{
    int col = 6;
    Console.WriteLine("    a   b   c   d   e   f  ");
    for (int i = 0; i < 6; i++)
    {
        Console.WriteLine(asciiUI.row1);
        Console.WriteLine(col + String.Format(asciiUI.row2, toShape(Board.Square[i, 0]),
            toShape(Board.Square[i, 1]), toShape(Board.Square[i, 2]), toShape(Board.Square[i, 3]),
            toShape(Board.Square[i, 4]), toShape(Board.Square[i, 5])));
        col--;
    }

    Console.WriteLine(asciiUI.row1);

    Console.WriteLine("Black [" + PointSystem.BlackPoints + "]: " + listToStr(PieceManager.blackInv));
    Console.WriteLine("White [" + PointSystem.WhitePoints + "]: " + listToStr(PieceManager.whiteInv));
}


initGame();
int moves = 100;
while (true)
{
    if (moves != 0)
    {
        updateUI();
        string command = "";
        Console.Write("Enter move: ");
        command = Console.ReadLine();
        Boolean validMove = MovementManager.commandInterpreter(command);
        if (validMove)
        {
            Bot.updateDict();
            Bot.allLegalMoves();
            updateUI();
            Bot.pickLegalMove();
            moves--;
        }
    }
}