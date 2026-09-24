namespace TicTacToe
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
            for(int b=0;b<9;b++)
            {
                Console.WriteLine("X chance: Enter the data to change i(1,2,3): ");
                var iKey = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine("X chance : Enter the data to change j(1,2,3): ");
                var jKey = Console.ReadKey();
                Console.WriteLine();
                int i = int.Parse(iKey.KeyChar.ToString()) - 1;
                int j = int.Parse(jKey.KeyChar.ToString()) - 1;
                arr[i, j] = "X";
                if ((arr[0,0]== "X" && arr[1,1]=="X" && arr[2,2]=="X") || (arr[0, 0] == "X" && arr[0, 1] == "X" && arr[0, 2] == "X") || (arr[1, 0] == "X" && arr[1, 1] == "X" && arr[1, 2] == "X") || (arr[2, 0] == "X" && arr[2, 1] == "X" && arr[2, 2] == "X")|| (arr[2, 0] == "X" && arr[1, 1] == "X" && arr[0,2] == "X"))
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
                Console.WriteLine("O chance: Enter the data to change i(1,2,3): ");
                var iiKey = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine("O chance: Enter the data to change j(1,2,3): ");
                var jjKey = Console.ReadKey();
                Console.WriteLine();
                int ii = int.Parse(iiKey.KeyChar.ToString()) - 1;
                int jj = int.Parse(jjKey.KeyChar.ToString()) - 1;
                arr[ii, jj] = "O";
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
