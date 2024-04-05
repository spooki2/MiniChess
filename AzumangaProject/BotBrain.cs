using System;
using System.Collections.Generic;

namespace AzumangaProject
{
    public static class BotBrain
    {
        public static Dictionary<Board, MoveScore> allChildBoards(Board board)
        {
            HashSet<Move> legalMoves = BotFunctions.legalMoves(board);
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

        public static Move bestChildMove(Board board)
        {
            Dictionary<Board, MoveScore> boardScores = allChildBoards(board);
            //get max
            BoardScore max = new BoardScore(new Board(), int.MinValue);
            Move maxMove = new Move(0, 0);
            foreach (Board childBoard in boardScores.Keys)
            {
                if (boardScores[childBoard].value > max.value)
                {
                    max.board = childBoard;
                    max.value = boardScores[childBoard].value;
                    maxMove = boardScores[childBoard].move;
                }
            }

            Console.WriteLine("best move found: " + max.value);
            return maxMove;
        }


        //minimax

        public static BoardScore minimax(Board currBoard, int depth, Boolean botTurn)
        {
            if (depth == 0)
            {
                return new BoardScore(currBoard, BotFunctions.evaluate(currBoard));
            }

            Dictionary<Board, MoveScore> childBoardScores = allChildBoards(currBoard);

            if (botTurn)
            {
                BoardScore maxEval = new BoardScore(currBoard, int.MinValue);
                foreach (Board childBoard in childBoardScores.Keys)
                {
                    BoardScore eval = minimax(childBoard, depth - 1, false);
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
                    BoardScore eval = minimax(childBoard, depth - 1, true);
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