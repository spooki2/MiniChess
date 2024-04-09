namespace AzumangaProject;

public static class BotController
{
    public static void play(Board board, int depth)
    {
        BoardScore bestBoard = BotBrain.minimax(board, depth, true);

        Console.WriteLine("Best board score: {0}" , bestBoard.value);

        //if bot deducts that forced checkmate, win


        int pieceCountNew = BotFunctions.getPieces(bestBoard.board).Count;
        int pieceCountOld = BotFunctions.getPieces(board).Count;
        Boolean pieceAdded = false;
        Boolean pieceTaken = false;
        if (pieceCountNew > pieceCountOld)
        {
            pieceAdded = true;
        }

        if (pieceCountNew < pieceCountOld)
        {
            pieceTaken = true;
        }

        for (int i = 0; i < 36; i++)
        {
            int pieceThen = board.get1D(i);
            int pieceNow = bestBoard.board.get1D(i);
            if (pieceTaken && PieceManager.colorOnly(pieceThen) == Piece.White &&
                PieceManager.colorOnly(pieceNow) == Piece.Black)
            {
                //took piece
                board.addToList(PieceManager.switchPieceColor(pieceThen));
            }

            if (pieceAdded && pieceThen == Piece.None && PieceManager.colorOnly(pieceNow) == Piece.Black)
            {
                //respawned piece
                board.removeFromList(pieceNow);
            }

            board.Square[i / 6, i % 6] = bestBoard.board.Square[i / 6, i % 6];
        }
        BoardScore nextBoard = BotBrain.minimax(Board.main, 1, true);
        if ((nextBoard.value >= 50|| nextBoard.value <= -50)&&(bestBoard.value >= 50 || bestBoard.value <= -50))
        {
            Board.checkMate = true;
            //GAME WON!!!!! 07/04/2024 3:14
        }
    }
}