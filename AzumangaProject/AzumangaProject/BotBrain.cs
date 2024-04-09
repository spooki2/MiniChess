using System;
using System.Collections.Generic;

namespace AzumangaProject
{
    public static class BotBrain
    {
        public static Dictionary<Board, double> allChildBoards(Board board, Boolean botTurn, Boolean _ignoreMagi3_ = false,Boolean _ignoreRespawns_ = false)
        {
            HashSet<Move> bothLegalMoves = BotFunctions.legalMoves(board, _ignoreMagi3_);
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


            Dictionary<Board, double> boardScores = new Dictionary<Board, double>();
            //make moves in child board

            foreach (Move move in legalMoves)
            {
                //copy board
                Board childBoard = new Board(board);
                MovementManager.movePiece(move, childBoard);
                double eval = BotFunctions.evaluate(childBoard);
                boardScores[childBoard] = eval;
            }

            if (!_ignoreRespawns_)
            {

                //SLOW PART
                //add respawn boards
                HashSet<respawnMove> legalRespawns = BotFunctions.legalRespawns(board);
                foreach (respawnMove move in legalRespawns)
                {
                    //copy board
                    Board childBoard = new Board(board);
                    MovementManager.placePiece(move.piece, move.to, childBoard);
                    double eval = BotFunctions.evaluate(childBoard);
                    boardScores[childBoard] = eval;
                }


            }

            return boardScores;
        }

        //minimax

        public static BoardScore minimax(Board currBoard, int depth, Boolean botTurn, Boolean _ignoreMagi3_ = false)
        {
            Dictionary<Board, double> childBoardScores = allChildBoards(currBoard, botTurn,_ignoreMagi3_,!(depth==3));
            if (depth == 0)
            {
                return new BoardScore(currBoard, BotFunctions.evaluate(currBoard));
            }


            if (botTurn)
            {
                BoardScore maxEval = new BoardScore(currBoard, int.MinValue);
                foreach (Board childBoard in childBoardScores.Keys)
                {
                    BoardScore eval = minimax(childBoard, depth - 1, false, _ignoreMagi3_);
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
                    BoardScore eval = minimax(childBoard, depth - 1, true, _ignoreMagi3_);
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