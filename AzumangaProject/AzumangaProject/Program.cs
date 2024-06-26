﻿using System.Reflection.Metadata;
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
    Board.main.Square[1, 1] = Piece.Black | Piece.Bishop;
    //Board.main.Square[0, 3] = Piece.Black | Piece.Knight;
    Board.main.Square[0, 0] = Piece.Black | Piece.Rook;
    Board.main.Square[0, 4] = Piece.Black | Piece.King;
    Board.main.Square[1, 5] = Piece.Black | Piece.Pawn;

    Board.main.Square[3, 0] = Piece.White | Piece.Pawn;
    Board.main.Square[5, 0] = Piece.White | Piece.King;
    //Board.main.Square[5, 1] = Piece.White | Piece.Rook;
    Board.main.Square[5, 1] = Piece.White | Piece.Knight;
    Board.main.Square[3, 1] = Piece.White | Piece.Knight;
    Board.main.Square[5, 3] = Piece.White | Piece.Bishop;
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

initGame();
while (true) //magi 3 respawn evaluate respawn
{
    Console.WriteLine("BOARD VALUE: "+BotFunctions.evaluate(Board.main));
    updateUI();
    Board.lastBoard = new Board(Board.main);
    Board.Current = true;
    string command = "";
    Console.Write("Enter move: ");
    command = Console.ReadLine();
    //bot checks
    Boolean validMove = MovementManager.commandInterpreter(command, Board.main);
    Console.WriteLine("BOARD VALUE: "+BotFunctions.evaluate(Board.main));

    if (validMove)
    {
        updateUI();
        BotController.play(Board.main, 3); //2 IS LOW!!
    }

    if (Board.checkMate)
    {
        updateUI();
        Console.WriteLine(
            "   ____   _   _   _____    ____   _  __\n  / ___| | | | | | ____|  / ___| | |/ /\n | |     | |_| | |  _|   | |     | ' / \n | |___  |  _  | | |___  | |___  | . \\ \n  \\____| |_| |_| |_____|  \\____| |_|\\_\\\n                                       \n  __  __      _      _____   _____     \n |  \\/  |    / \\    |_   _| | ____|    \n | |\\/| |   / _ \\     | |   |  _|      \n | |  | |  / ___ \\    | |   | |___     \n |_|  |_| /_/   \\_\\   |_|   |_____|    \n                                       ");
        while (true)
        {
        }
    }
}