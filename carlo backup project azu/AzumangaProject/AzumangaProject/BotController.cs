namespace AzumangaProject;

public static class BotController
{
    public static void play(Board board,int depth,int times)
    {
        Board bestBoardNoVal = BotBrain.monteCarlo(board, depth, times,true).board;
        BoardScore bestBoard = new BoardScore(bestBoardNoVal, BotFunctions.evaluate(bestBoardNoVal));

        double eval = BotFunctions.evaluate(board);
        if (Math.Abs(eval) > 5)
        {
            //mid game

        }





        //update board + inventory

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


    }
}