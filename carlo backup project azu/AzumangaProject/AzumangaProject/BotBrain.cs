using System;
using System.Collections.Generic;

namespace AzumangaProject
{
    public static class BotBrain
    {
        public static Dictionary<Board, double> allChildBoards(Board board)
        {
            HashSet<Move> bothLegalMoves = BotFunctions.legalMoves(board, 0);
            HashSet<Move> legalMoves = new HashSet<Move>();
            foreach (Move move in bothLegalMoves)
            {
                if (PieceManager.colorOnly(board.get1D(move.from)) == Piece.Black)
                {
                    legalMoves.Add(move);
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

            /*
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

            */
            return boardScores;
        }


        public static void randomMove(Board currBoard, int side, HashSet<Move> legalMoves = null)
        {
            if (side == Piece.Black)
            {
                if (legalMoves == null)
                {
                    legalMoves = BotFunctions.legalMoves(currBoard, side);
                }

                Random rand = new Random();
                int index = rand.Next(legalMoves.Count);
                int i = 0;
                foreach (Move move in legalMoves)
                {
                    if (i == index)
                    {
                        MovementManager.movePiece(move, currBoard);
                        break;
                    }

                    i++;
                }
            }
            else
            {
                if (legalMoves == null)
                {
                    legalMoves = BotFunctions.legalMoves(currBoard, side);
                }

                Random rand = new Random();
                int index = rand.Next(legalMoves.Count);
                int i = 0;
                foreach (Move move in legalMoves)
                {
                    if (i == index)
                    {
                        MovementManager.movePiece(move, currBoard);
                        break;
                    }

                    i++;
                }
            }
        }

        public static BoardScore monteCarlo(Board currBoard, int depth, int times, Boolean botTurn)
        {
            int firstDepth = depth;
            return monteCarlo(currBoard, depth, times, botTurn, (depth==firstDepth));
        }
        public static BoardScore monteCarlo(Board currBoard, int depth, int times,Boolean botTurn,Boolean respawnDepths)
        {
            if (depth == 0)
            {
                return new BoardScore(currBoard, BotFunctions.evaluate(currBoard));
            }

            int side = Piece.Black;
            if (botTurn)
            {
                BoardScore maxEval = new BoardScore(currBoard, int.MinValue);
                HashSet<Move> legalMoves = BotFunctions.legalMoves(currBoard, side,respawnDepths);
                for (int i = 0; i < times; i++)
                {
                    {
                        Board childBoard = new Board(currBoard);
                        randomMove(childBoard, Piece.Black, legalMoves);
                        BoardScore eval = monteCarlo(childBoard, depth - 1, times, false,respawnDepths);
                        if (eval.value > maxEval.value)
                        {
                            maxEval.value = eval.value;
                            maxEval.board = childBoard;
                        }
                    }
                }

                return maxEval;
            }
            else
            {
                side = Piece.White;
                BoardScore minEval = new BoardScore(currBoard, int.MaxValue);
                HashSet<Move> legalMoves = BotFunctions.legalMoves(currBoard, side,respawnDepths);
                for (int i = 0; i < times; i++)
                {
                    Board childBoard = new Board(currBoard);
                    randomMove(childBoard, Piece.White, legalMoves);
                    BoardScore eval = monteCarlo(childBoard, depth - 1, times, true,respawnDepths);
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