﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game_Escape_From_Lab
{
    public class Game
    {
        private int[,] matrix;
        private int x;
        private int y;
        public Player Player1;
        public bool playing;
        public int timer;


        // constructor
        public Game(LabirinthSize S)
        {
            switch (S)
            {
                case LabirinthSize.Small:
                    x = 5;
                    y = 7;
                    break;
                case LabirinthSize.Medium:
                    x = 7;
                    y = 11;
                    break;
                case LabirinthSize.Large:
                    x = 9;
                    y = 21;
                    break;
                default:
                    Console.WriteLine("Default size !!!");
                    x = 10;
                    y = 21;
                    break;
            }

            matrix = new int[x, y];
            Player1 = new Player(3, 1);
            matrix[3, 1] = 5;
            playing = true;
            timer = x * x * 150;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0 || i == x - 1)
                    {
                        matrix[i, j] = 2;
                    }
                    else
                    {
                        if (j % 2 == 0) matrix[i, j] = 1;
                    }
                }
            }
        }

        // Methods for gaming
        public void DrawLabirinth()
        {
            while (playing && timer > 0)
            {
                Random rd = new Random();
                int rdx = rd.Next(1, x - 1);
                int rdy0 = rd.Next(1, y - 1);
                int rdy = (rdy0 % 2 == 0) ? rdy0 : (rdy0 + 1 != y ? (rdy0 + 1) : (rdy0 - 1));
                matrix[rdx, rdy] = 0;
                Draw();
                Thread.Sleep(x * 150);
                timer -= 150;
                Timer();

                while (matrix[rdx, rdy] == 5 && playing)
                {
                    Thread.Sleep(x * 150);
                    timer -= 150;
                    Timer();
                }
                matrix[rdx, rdy] = playing?1:5;
                Draw();
            }
        }

        public void DrawPlayer()
        {
            while (playing && timer > 0)
            {
                Console.SetCursorPosition(20, 10);
                ConsoleKey move = Console.ReadKey().Key;
                switch (move)
                {
                    case ConsoleKey.LeftArrow:
                        if (matrix[Player1.LocationX, Player1.LocationY - 1] == 0)
                        {
                            matrix[Player1.LocationX, Player1.LocationY--] = 0;
                            matrix[Player1.LocationX, Player1.LocationY] = 5;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if ((matrix[Player1.LocationX, Player1.LocationY + 1] == 0))
                        {
                            matrix[Player1.LocationX, Player1.LocationY++] = 0;
                            matrix[Player1.LocationX, Player1.LocationY] = 5;

                            if(Player1.LocationY == (y - 1))
                            {
                                playing = false;
                                break;
                            }          
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (matrix[Player1.LocationX - 1, Player1.LocationY] == 0)
                        {
                            matrix[Player1.LocationX--, Player1.LocationY] = 0;
                            matrix[Player1.LocationX, Player1.LocationY] = 5;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (matrix[Player1.LocationX + 1, Player1.LocationY] == 0)
                        {
                            matrix[Player1.LocationX++, Player1.LocationY] = 0;
                            matrix[Player1.LocationX, Player1.LocationY] = 5;
                        }
                        break;
                    default:
                        break;
                }
                Draw();
                Timer();
            }
        }


        // Plotter
        private void Draw()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 2)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("\u22A0");
                    }
                    if (matrix[i, j] == 1)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("|");
                    }
                    if (matrix[i, j] == 0)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(' ');
                    }
                    if (matrix[i, j] == 5)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write("\u263B");
                    }
                }
            }

        }

        //Timer
        private void Timer()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(22, 3);
            Console.WriteLine(new string('|', x * x - timer /150));
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Life Drawer
        public void DrawLife()
        {
            Console.SetCursorPosition(22, 2);
            Console.WriteLine("REMAINING LIFE");
            Console.SetCursorPosition(22, 3);
            Console.WriteLine(new string('|', x * x));
        }
    }
}
