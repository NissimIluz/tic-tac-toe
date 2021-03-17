using System;
using System.Runtime.CompilerServices;
/**
* @author  Nissim Iluz
* XO Board 
* This program is free software; you can redistribute it and / or
* modify it.
*/

namespace XOBoard
{
    public class XOBoard
    {
        private char[,] _board;
        const char emptyChar= '\0';
        /**
         * constructor of the class. 
         * creates a new X or O bored in size 3x3
         */
        public XOBoard()
        {
            _board = new char[3, 3];
        }
        /**
      * constructor of the class. 
      * creates a new X or O bored in size: sizexsize
      * @param size the bored size
      */
        public XOBoard(int size)
        {
            _board = new char[size, size];  //[column,row]
        }

        /**
         * Put X or O on the bored.
         * @param toPut 'X' or 'O' only.
         * @param indexRow the desired row
         * @param the desired column
         */
        public void Put(char toPut, int indexRow, int indexColumn)
        {
            if (indexRow >= _board.GetLength(0) || indexColumn >= _board.GetLength(1) || indexRow < 0 || indexColumn < 0)
                throw new ArgumentException("Indexs out of the board");
            else
             if (toPut == 'X' || toPut == 'O') {

                if (_board[indexRow, indexColumn] == emptyChar)
                    _board[indexRow, indexColumn] = toPut;
                else
                    throw new ArgumentException("This spot is already taken");
            }
            else
                throw new ArgumentException("Not valid input");
        }

        /**
         * Checking the status of the board
         * @return "X" when X won
         *         "O" when O won
         *         "None" when the game end and no one won
         *         "Draw" no one won and the game doesn't end
         */         
        public string Status()
        {
            Boolean theGameEnd = true; // =varies to flase when an empty cell was found 
            Boolean rowWon, columnWon, domwSlantWon, upSlantWon;
            int lastIndex = _board.GetLength(0) - 1;
            if (_board[0, 0] == emptyChar)
            {
                domwSlantWon = false;
                theGameEnd = false;
            }
            else
                domwSlantWon = true;
            if (_board[lastIndex, 0] == emptyChar)
            {
                upSlantWon = false;
                theGameEnd = false;
            }
            else
                upSlantWon = true;
            for (int index = 0; index < lastIndex; index++)  //Checking only the slants
            {
                if (!domwSlantWon && !upSlantWon)
                    break;
                if (domwSlantWon && _board[index, index] != _board[index + 1, index + 1])
                {
                    domwSlantWon = false;
                    if (_board[index + 1, index + 1] == emptyChar)
                        theGameEnd = false;
                }
                int temp = lastIndex - index;
                if (upSlantWon && _board[temp, index] != _board[temp - 1, index + 1])
                {
                    upSlantWon = false;
                    if (_board[index + 1, index + 1] == emptyChar)
                        theGameEnd = false;
                }

            }
            if (domwSlantWon)
                return _board[0, 0].ToString();
            if (upSlantWon)
                return _board[lastIndex, 0].ToString();

            for (int j = 0; j < _board.GetLength(0); j++) //Checking the rows and the columns
            {
                if (_board[0, j] == emptyChar)
                {
                    theGameEnd = false; columnWon = false;
                }
                else
                    columnWon = true;
                if (_board[j, 0] == emptyChar)
                {
                    theGameEnd = false; rowWon = false;
                }
                else
                    rowWon = true;
                if (!(columnWon || rowWon))
                    continue;
                for (int i = 0; i < lastIndex; i++)
                {
                    if (columnWon && _board[i, j] != _board[i + 1, j])
                    {
                        columnWon = false;
                        if (_board[i + 1, j] == emptyChar)
                            theGameEnd = false;

                    }
                    if (rowWon && _board[j, i] != _board[j, i + 1])
                    {
                        rowWon = false;
                        if (_board[j, i + 1] == emptyChar)
                            theGameEnd = false;
                    }
                    if (!(columnWon || rowWon))
                        break;
                }
                if (rowWon)
                    return _board[j, 0].ToString();
                if (columnWon)
                    return _board[0, j].ToString();
            }
            if (theGameEnd)
                return "None";
            return "Draw";
        }
        /**
         * Print the board in the console.
         */
        public void Display()
        {
            string line = new string('-', _board.GetLength(0)*4+1);
            const string row = "|";
            Console.Clear();
            Console.WriteLine(line);
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                Console.Write(row);
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    if (_board[i, j] == emptyChar)
                        PrintSpace(3);
                    else
                    {
                        PrintSpace(1);
                        Console.Write(_board[i, j]);
                        PrintSpace(1);
                    }
                    Console.Write(row);
                }

                    Console.WriteLine("\n"+line);



            }
        }
        private static void PrintSpace(int num)
        {
            for (int u = 0; u < num; u++)
                Console.Write(" ");
        }

    }

    public class Class1
    {
        static void Main(string[] args)
        {
            XOBoard game;
            string input;
            int size;
            char player = 'O';
            string status;
            Console.Write("Please enter desired board size:");
            input = Console.ReadLine().Trim();
            int.TryParse(input, out size);
            while (size <= 0)
            {
                Console.Write("\ntry again:");
                input = Console.ReadLine().Trim();
                int.TryParse(input, out size);
            }
            game = new XOBoard(size);
            do
            {
                int row = -1;
                int column = -1;
                game.Display();
                switch (player)
                {
                    case 'X':
                        player = 'O';
                        break;
                    case 'O':
                        player = 'X';
                        break;
                }

                Console.Write(player + " turn. Please enter your selection, according to a row , column: ");
                input = Console.ReadLine() + " ";
                while (column == -1)
                {
                    Boolean aNumberIsRead = false;
                    int counter1 = 0;
                    int counter2 = 0;
                    foreach (char a in input)
                    {

                        if (!char.IsDigit(a))
                        {
                            if (aNumberIsRead)
                                if (row == -1)
                                {
                                    int.TryParse(input.Substring(counter1, counter2), out row);
                                    aNumberIsRead = false;
                                    counter1 = counter1 + counter2;
                                    counter2 = 0;
                                }
                                else
                                {
                                    int.TryParse(input.Substring(counter1, counter2), out column);
                                    break;
                                }
                            counter1++;
                        }
                        else
                        {
                            counter2++; aNumberIsRead = true;
                        }
                    }
                    try
                    {
                        game.Put(player, row, column);
                    }
                    catch (ArgumentException e)
                    {
                        Console.Write(e.Message+ ". Try again: ");
                        input = Console.ReadLine() + " ";
                        column = -1;       
                        row = -1;
                        column = -1;
                    }
                }

              status = game.Status();
            } while (status.Equals("Draw"));
            game.Display();
            if (status.Equals("None"))
                Console.WriteLine("\nThe game ended without a winner");
            else
                Console.WriteLine("\n" + player + " won the game");
        }
    }
}
