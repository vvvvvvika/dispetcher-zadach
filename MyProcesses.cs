using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class MyProcesses
    {
        private static bool InTheMainMenu = true;
        private static Process Proc = new Process();
        public static void DrewProcesses(List<Process> list)
        {
            foreach (var element in list)
            {
                Console.WriteLine("  Название: " + element.ProcessName + " Приоритет: " + element.BasePriority + " Память: " + element.PagedMemorySize64 / 1048576 + "мб");
            }
        }
        public static void DrewProcesses(Process proc)
        {
            try
            {
                Console.WriteLine("  Название процесса: " + proc.ProcessName);
                Console.WriteLine("  Использование диска: " + proc.PagedMemorySize64);
                Console.WriteLine("  Приоритет: " + proc.BasePriority);
                Console.WriteLine("  Класс приоритета: " + proc.PriorityClass);
                Console.WriteLine("  Время использование процесса: " + proc.StartTime);
                Console.WriteLine("Завершить процесс - D");
                Console.WriteLine("Завершить все процессы с этим именем - Del");
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("  Ошибка: отказанно в доступе");
            }
            finally
            {
                Console.WriteLine("Выйти в меню процессов - Escape");
            }
            
        }
        public static int[] ChangeProcess(Process proc)
        {
            if (InTheMainMenu)
            {
                Console.Clear();
                Proc = proc;
                InTheMainMenu = false;
                DrewProcesses(proc);
                return new int[2] { 1, 0 };
            }
            return null;
        }
        public static int[] BackToMenu(List<Process> list)
        {
            if (!InTheMainMenu)
            {
                Console.Clear();
                InTheMainMenu = true;
                DrewProcesses(list);
                return new int[2] { list.Count, 0 };
            }
            return null;
        }
        public static int[] StopThisProcess()
        {
            if (!InTheMainMenu)
            {
                try
                {
                    Proc.Kill();
                    Console.Clear();
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("  Ошибка: Нету прав для закрытия данного процесса");
                    DrewProcesses(Proc);
                    return null;
                }
            }
            return BackToMenu(Process.GetProcesses().ToList());
        }
        public static int[] StopAllProcesses(List<Process> list)
        {
            if (!InTheMainMenu)
            {
                try
                {
                    Proc.Kill();
                    foreach(var element in list)
                    {
                        if(element.ProcessName == Proc.ProcessName)
                        {
                            element.Kill();
                        }
                    }
                    Console.Clear();
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("  Ошибка: Нету прав для закрытия данного процесса");
                    DrewProcesses(Proc);
                    return null;
                }
            }
            return BackToMenu(Process.GetProcesses().ToList());
        }
    }
}
