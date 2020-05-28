using System;

namespace Lab06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Выберите задание (6 - Выход)");
            string numTask = String.Empty;
            Task1 taskFirst = new Task1();
            Task2 taskSecond = new Task2();
            Task3 taskThird = new Task3();
            Task4 taskFourth = new Task4();
            Task5 taskFifth = new Task5();
            try
            {
                numTask = Console.ReadLine();
                switch (numTask)
                {
                    case "1":
                        Console.Clear();
                        taskFirst.Choose(ref taskFirst.data);
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        taskSecond.BasicMain();
                        taskSecond.WriteToSecondFile();
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        taskThird.TaskMain3();
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        taskFourth.Task4Main();
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Clear();
                        taskFifth.TaskMain5();
                        Console.ReadKey();
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                }
                Program.Main(args);
            }
            catch
            {
                Program.Main(args);
            }
        }
    }
}
