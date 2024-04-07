namespace AzumangaProject;

public class Magi_3_Casper
{
    public static Boolean isLegal(int piece, Move move, Board board)
    {
        //play 1 move ahead
        //if king dead return false
        BoardScore bestBoard = new BoardScore(board, 0);
        if (Board.Current == true)
        {
            Board.Current = false;
            MovementManager.movePiece(move, board, true);
            bestBoard = BotBrain.minimax(board, 1, true, true); //IGNORE MAGI 3
        }

        Dictionary<int, int> piecesPos = BotFunctions.getPieces(bestBoard.board);
        if (piecesPos.ContainsKey(Piece.King | Piece.White) && piecesPos.ContainsKey(Piece.King | Piece.Black))
        {
            return true;
        }
        else
        {
            Board.main = new Board(Board.lastBoard);
            return false;
        }
    }
}