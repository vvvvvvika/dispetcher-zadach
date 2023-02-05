using System.Diagnostics;
using System.Dynamic;

namespace ConsoleApp1
{
    internal enum HotKey
    {
        УдалитьОдинПроцесс = ConsoleKey.D,
        УдалитьОдинаковыеПроцессы = ConsoleKey.Delete,
        Выбрать = ConsoleKey.Enter,
        Вверх = ConsoleKey.UpArrow,
        Вниз = ConsoleKey.DownArrow,
        Назад = ConsoleKey.Escape
    }
    internal class Program
    {
        private static int y = 0;
        private static int lastY;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            MyProcesses.DrewProcesses(Process.GetProcesses().ToList());
            int MaxPositionArrow = Process.GetProcesses().Length;
            int MinPositionArrow = 0;
            while (true)
            {
                ConsoleKey key = ArrowMenu(MinPositionArrow, MaxPositionArrow-1);
                if (key == (ConsoleKey)HotKey.Выбрать)
                {
                    lastY = y;
                    int[] arr = MyProcesses.ChangeProcess(Process.GetProcesses()[y]);
                    if (arr != null)
                    {
                        MaxPositionArrow = arr[0];
                        MinPositionArrow = arr[1];
                    }
                    y = 0;
                }
                else if (key == (ConsoleKey)HotKey.УдалитьОдинПроцесс)
                {
                    int[] arr = MyProcesses.StopThisProcess();
                    if(arr != null)
                    {
                        MaxPositionArrow = arr[0];
                        MinPositionArrow = arr[1];
                        y = lastY;
                    }
                }
                else if (key == (ConsoleKey)HotKey.УдалитьОдинаковыеПроцессы)
                {
                    int[] arr = MyProcesses.StopAllProcesses(Process.GetProcesses().ToList());
                    if (arr != null)
                    {
                        MaxPositionArrow = arr[0];
                        MinPositionArrow = arr[1];
                        y = Process.GetProcesses().Length;
                    }
                }
                else if (key == (ConsoleKey)HotKey.Назад)
                {
                    int[] arr = MyProcesses.BackToMenu(Process.GetProcesses().ToList());
                    if (arr != null)
                    {
                        MaxPositionArrow = arr[0];
                        MinPositionArrow = arr[1];
                    }
                    else
                    {
                        Console.Clear();
                        break;
                    }
                    y = lastY;
                }
            }
        }
        private static ConsoleKey ArrowMenu(int MinPositionArrow ,int MaxPositionArrow)
        {
            int LastPosY = 0;
            Console.SetCursorPosition(0, y);
            Console.Write("->");
            ConsoleKey key = Console.ReadKey().Key;
            if (key == (ConsoleKey)HotKey.Вверх)
            {
                LastPosY = y--;
                if (y < MinPositionArrow)
                {
                    LastPosY = MinPositionArrow;
                    y = MaxPositionArrow;
                }
                ClearLatPositionArrow(LastPosY);
            }
            else if (key == (ConsoleKey)HotKey.Вниз)
            {
                LastPosY = y++;
                if (y >= MaxPositionArrow)
                {
                    y = MinPositionArrow;
                }
                ClearLatPositionArrow(LastPosY);
            }

            return key;
        }
        private static void ClearLatPositionArrow(int LastPosY)
        {
            Console.SetCursorPosition(0, LastPosY);
            Console.Write("  ");
        }
    }
}