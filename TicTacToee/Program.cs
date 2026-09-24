namespace TicTacToee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] arr = new string[3, 3];
            int a = 1;
            bool x = true, o = false;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = (a++).ToString();
                    Console.Write(arr[i, j]);
                    Console.Write(" | ");
                }
                Console.WriteLine("\n --------- ");
            }
            for (int b = 0; b < 9; b++)
            {
                Console.WriteLine("X chance: Enter the number to be changed:");
                Console.WriteLine();
                string s= Console.ReadLine();
                for(int i = 0; i < 3; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        if (arr[i, j] == s)
                            arr[i, j] = "X";
                    }
                }
                if ((arr[0, 0] == "X" && arr[1, 1] == "X" && arr[2, 2] == "X") || (arr[0, 0] == "X" && arr[0, 1] == "X" && arr[0, 2] == "X") || (arr[1, 0] == "X" && arr[1, 1] == "X" && arr[1, 2] == "X") || (arr[2, 0] == "X" && arr[2, 1] == "X" && arr[2, 2] == "X") || (arr[2, 0] == "X" && arr[1, 1] == "X" && arr[0, 2] == "X"))
                {
                    Console.WriteLine("X won!");
                    return;
                }
                for (int p = 0; p < arr.GetLength(0); p++)
                {
                    for (int q = 0; q < arr.GetLength(1); q++)
                    {
                        Console.Write(arr[p, q]);
                        Console.Write(" | ");
                    }
                    Console.WriteLine("\n --------- ");
                }
                Console.WriteLine("O chance: Enter the number to be changed:");
                Console.WriteLine();
                string ss = Console.ReadLine();
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (arr[i, j] == ss)
                            arr[i, j] = "O";
                    }
                }
                if ((arr[0, 0] == "O" && arr[1, 1] == "O" && arr[2, 2] == "O") || (arr[0, 0] == "O" && arr[0, 1] == "O" && arr[0, 2] == "O") || (arr[1, 0] == "O" && arr[1, 1] == "O" && arr[1, 2] == "O") || (arr[2, 0] == "O" && arr[2, 1] == "O" && arr[2, 2] == "O") || (arr[2, 0] == "O" && arr[1, 1] == "O" && arr[0, 2] == "O"))
                {
                    Console.WriteLine("O won!");
                    return;
                }
                for (int p = 0; p < arr.GetLength(0); p++)
                {
                    for (int q = 0; q < arr.GetLength(1); q++)
                    {
                        Console.Write(arr[p, q]);
                        Console.Write(" | ");
                    }
                    Console.WriteLine("\n --------- ");
                }
            }
            Console.WriteLine("It's a draw!");
            Console.ReadKey();
        }
    }
}
