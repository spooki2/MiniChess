namespace AzumangaProject;

public class Magi_3_Casper
{
    public static Boolean isLegal(int piece, Move move, Board board)
    {
        //play 1 move ahead
        //if king dead return false
        BoardScore bestBoard = new BoardScore(board, 0);
        Board copyBoard = new Board(board);
        if (Board.Current == true)
        {
            Board.Current = false;
            if (!(move.to == 0 && move.from == 0))
            {
                MovementManager.movePiece(move, copyBoard, true);
            }

            bestBoard = BotBrain.minimax(copyBoard, 1, true); //IGNORE MAGI 3
        }

        List<int> pieces = BotFunctions.getPieces(bestBoard.board);
        if (pieces.Contains(Piece.King | Piece.White) && pieces.Contains(Piece.King | Piece.Black))
        {
            return true;
        }
        else
        {
            //Board.main = new Board(Board.lastBoard);
            return false;
        }
    }
}