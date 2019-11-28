using System.Linq;
using System;
using System.Collections.Generic;

namespace minesweeper
{
    
    class Program
    {
        const int BOARD_SIZE = 14;
        const int MINE_COUNT = 1;
        const bool DEBUG = true;
        static void Main(string[] args)
        {
            // Grid with navigation
            // Set Flag
            // Reveal
            // 
            string difficulty = "";
            while(true)
            {
                Intro();

                string command = Console.ReadLine();
                if(command.ToLower() == "exit") {
                    break;
                }
                
                switch(command)
                {
                    case "play":
                        Board board = new Board(difficulty);
                        board.Play();
                        break;
                    case "difficulty":
                        Console.Clear();
                        Console.WriteLine("Choose a difficulty:");
                        Console.WriteLine("EASY, MEDIUM, HARD");

                        difficulty = Console.ReadLine().ToUpper();
                        break;
                }
            }



        }

        static void Intro() {
            Console.Clear();
            Console.WriteLine("MINESWEEPER");
            Console.WriteLine("Type 'play', 'difficulty', or 'exit'");
        }
        
    }
}
