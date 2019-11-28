using System;
using System.Collections.Generic;
using System.Linq;
using static minesweeper.Constants;

namespace minesweeper
{
    public class Board
    {
        public Tuple<int, int> cursor { get; set; } = new Tuple<int, int>(0, 0);
        public List<List<Dirt>> matrix { get; set; }
        public Difficulty difficulty = new Difficulty("EASY");
        public Board(string diff)
        {
            try
            {
                difficulty = new Difficulty(diff);

                matrix = new List<List<Dirt>>();
                List<Tuple<int, int>> minesPlaced = new List<Tuple<int, int>>();
                for (int r = 0; r <= difficulty.GetSettings().BoardSize; r++)
                {
                    matrix.Add(new List<Dirt>());
                    for (int c = 0; c < difficulty.GetSettings().BoardSize; c++)
                    {
                        matrix[r].Add(new Dirt());
                    }
                }
                Random rnd = new Random();

                while (minesPlaced.Count < difficulty.GetSettings().MineCount)
                {
                    int rndRow = rnd.Next(0, difficulty.GetSettings().BoardSize);
                    int rndCol = rnd.Next(0, difficulty.GetSettings().BoardSize);

                    if (!minesPlaced.Any(m => (m.Item1 == rndRow || m.Item2 == rndCol)))
                    {
                        Tuple<int, int> mineCoords = new Tuple<int, int>(rndRow, rndCol);
                        PlantMine(mineCoords, matrix);
                        minesPlaced.Add(mineCoords);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
            }

        }
        public void Play()
        {


            Tuple<int, int> cursor = new Tuple<int, int>(0, 0);

            PrintAfterChange(cursor, matrix);

            bool isGameOver = false;

            while (!isGameOver)
            {
                var ch = Console.ReadKey(false).Key;
                switch (ch)
                {
                    case ConsoleKey.UpArrow:
                        if (cursor.Item1 != 0)
                        {
                            cursor = new Tuple<int, int>(cursor.Item1 - 1, cursor.Item2);
                            PrintAfterChange(cursor, matrix);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (cursor.Item1 < matrix.Count - 1)
                        {
                            cursor = new Tuple<int, int>(cursor.Item1 + 1, cursor.Item2);
                            PrintAfterChange(cursor, matrix);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (cursor.Item2 != 0)
                        {
                            cursor = new Tuple<int, int>(cursor.Item1, cursor.Item2 - 1);
                            PrintAfterChange(cursor, matrix);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (cursor.Item2 < matrix[cursor.Item1].Count - 1)
                        {
                            cursor = new Tuple<int, int>(cursor.Item1, cursor.Item2 + 1);
                            PrintAfterChange(cursor, matrix);
                        }
                        break;
                    case ConsoleKey.D:
                        Dig(cursor, matrix);
                        PrintAfterChange(cursor, matrix);
                        break;
                    case ConsoleKey.F:
                        matrix[cursor.Item1][cursor.Item2].TileState =
                        matrix[cursor.Item1][cursor.Item2].TileState == Dirt.TileStateEnum.FLAGGED
                        ? Dirt.TileStateEnum.HIDDEN
                        : Dirt.TileStateEnum.FLAGGED;

                        PrintAfterChange(cursor, matrix);
                        break;
                    default:
                        string command = Console.ReadLine();

                        if (command.ToLower() == "exit")
                        {
                            break;
                        }
                        return;
                }
                if(CheckBoard()) {
                    isGameOver = true;
                }

            }
        }
        void PrintAfterChange(Tuple<int, int> cursor, List<List<Dirt>> matrix)
        {
            int row = cursor.Item1;
            int col = cursor.Item2;
            Dirt dirt = matrix[row][col];
            Console.Clear();

            if (true)
            {
                Console.WriteLine("\nDEBUGGING INFO:");
                Console.WriteLine("row: {0}, col: {1}", row, col);
                Console.WriteLine("IsMine: {0}, MineCount: {1}, TileState: {2}",
                    dirt.IsMine, dirt.MineCount, dirt.TileState);
            }

            Print("\n");
            for (int r = 0; r < matrix.Count; r++)
            {
                Print(" ");
                for (int c = 0; c < matrix[r].Count; c++)
                {
                    if (r == cursor.Item1 && c == cursor.Item2)
                        PrintWhite(matrix[r][c].GetConsoleElement());
                    else
                        Print(matrix[r][c].GetConsoleElement().ToString());
                    Print(" ");

                }
                Print("\n");
            }
        }

        static void PrintWhite(string character)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(character);
            Console.ResetColor();
        }
        static void Print(string character)
        {
            Console.Write(character);
        }

        static void GameOver()
        {
            Console.Clear();
            Console.WriteLine("Game Over");
            Console.ReadLine();
        }
        static void Win()
        {
            Console.Clear();
            Console.WriteLine("You Win!");
            Console.ReadLine();
        }
        bool CheckBoard() {
            int FlaggedMines = 0;
            for (int r = 0; r < matrix.Count; r++)
            {
                for (int c = 0; c < matrix[r].Count; c++)
                {
                    if (matrix[r][c].IsMine && matrix[r][c].TileState == Dirt.TileStateEnum.FLAGGED)
                        FlaggedMines++;
                    else if(matrix[r][c].IsMine && matrix[r][c].TileState == Dirt.TileStateEnum.REVEALED) {
                        GameOver();
                        return true;
                    }

                }
            }
            if(FlaggedMines == difficulty.GetSettings().MineCount) {
                RevealAll();
                Win();
                return true;
            }
            return false;
        }
        List<List<Dirt>> PlantMine(Tuple<int, int> ptr, List<List<Dirt>> matrix)
        {

            //Plant mine
            matrix[ptr.Item1][ptr.Item2].IsMine = true;
            List<Tuple<int, int>> adjacent = GetAdjacent(ptr, matrix);
            foreach (Tuple<int, int> adj in adjacent)
            {
                matrix[adj.Item1][adj.Item2].MineCount++;
            }

            return matrix;
        }
        void Dig(Tuple<int, int> ptr, List<List<Dirt>> matrix)
        {
            matrix[ptr.Item1][ptr.Item2].TileState = Dirt.TileStateEnum.REVEALED;

            if (matrix[ptr.Item1][ptr.Item2].MineCount == 0)
            {

                List<Tuple<int, int>> adjacent = GetAdjacent(ptr, matrix);

                foreach (Tuple<int, int> adj in adjacent)
                {
                    if (matrix[adj.Item1][adj.Item2].TileState == Dirt.TileStateEnum.HIDDEN)
                    {
                        Dig(adj, matrix);
                    }
                }

            }
        }

        List<Tuple<int, int>> GetAdjacent(Tuple<int, int> ptr, List<List<Dirt>> matrix)
        {
            List<Tuple<int, int>> output = new List<Tuple<int, int>>();

            for (int r = 0; r < matrix.Count; r++)
            {
                if (r == ptr.Item1 + 1 || r == ptr.Item1 - 1 || r == ptr.Item1)
                {
                    for (int c = 0; c < matrix[r].Count; c++)
                    {
                        if ((c == ptr.Item2 + 1 || c == ptr.Item2 - 1 || c == ptr.Item2))
                        {
                            output.Add(new Tuple<int, int>(r, c));
                        }

                    }
                }
            }
            return output;
        }
        List<List<Dirt>> RevealAll() {
            foreach (var row in matrix)
            {
                foreach (var col in row)
                {
                    col.TileState = Dirt.TileStateEnum.REVEALED;
                }
            }
            return matrix;
        }
    }



}
