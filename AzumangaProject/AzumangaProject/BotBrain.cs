using System;
using System.Collections.Generic;

namespace AzumangaProject
{
    public static class BotBrain
    {
        public static Dictionary<Board, MoveScore> allChildBoards(Board board, Boolean botTurn,Boolean _ignoreMagi3_=false)
        {
            HashSet<Move> bothLegalMoves = BotFunctions.legalMoves(board,_ignoreMagi3_);
            HashSet<Move> legalMoves = new HashSet<Move>();
            foreach (Move move in bothLegalMoves)
            {
                if (botTurn)
                {
                    if (PieceManager.colorOnly(board.get1D(move.from)) == Piece.Black)
                    {
                        legalMoves.Add(move);
                    }
                }
                else
                {
                    if (PieceManager.colorOnly(board.get1D(move.from)) == Piece.White)
                    {
                        legalMoves.Add(move);
                    }
                }
            }


            Dictionary<Board, MoveScore> boardScores = new Dictionary<Board, MoveScore>();
            //make moves in child board
            foreach (Move move in legalMoves)
            {
                //copy board
                Board childBoard = new Board(board);
                MovementManager.movePiece(move, childBoard);
                boardScores[childBoard] = new MoveScore(move, BotFunctions.evaluate(childBoard));
            }

            return boardScores;
        }

        //minimax

        public static BoardScore minimax(Board currBoard, int depth, Boolean botTurn,Boolean _ignoreMagi3_=false)
        {

            Dictionary<Board, MoveScore> childBoardScores = allChildBoards(currBoard, botTurn,_ignoreMagi3_);
            if (depth == 0)
            {
                return new BoardScore(currBoard, BotFunctions.evaluate(currBoard));
            }


            if (botTurn)
            {
                BoardScore maxEval = new BoardScore(currBoard, int.MinValue);
                foreach (Board childBoard in childBoardScores.Keys)
                {
                    BoardScore eval = minimax(childBoard, depth - 1, false,_ignoreMagi3_);
                    if (eval.value > maxEval.value)
                    {
                        maxEval.value = eval.value;
                        maxEval.board = childBoard;
                    }
                }

                return maxEval;
            }
            else
            {
                BoardScore minEval = new BoardScore(currBoard, int.MaxValue);
                foreach (Board childBoard in childBoardScores.Keys)
                {
                    BoardScore eval = minimax(childBoard, depth - 1, true,_ignoreMagi3_);
                    if (eval.value < minEval.value)
                    {
                        minEval.value = eval.value;
                        minEval.board = childBoard;
                    }
                }

                return minEval;
            }
        }
    }
}