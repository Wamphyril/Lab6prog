using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab06
{
    class Task3
    {
        private string path = @"E:\Lab06\task3.txt";
        private string pathCopy = @"E:\Lab06\task3Copy.txt";

        public void TaskMain3()
        {
            StreamReader readFromFile = new StreamReader(File.Open(path, FileMode.Open));
            StreamWriter writeToSecondFile = new StreamWriter(File.Open(pathCopy, FileMode.OpenOrCreate));

            int countDeleting = 0;
            while(readFromFile.Peek() > -1)
            {
                char sym = Convert.ToChar(readFromFile.Read());
                if (!Char.IsDigit(sym))
                {
                    writeToSecondFile.Write(sym);
                }
                else
                {
                    countDeleting++;
                }
            }

            Console.WriteLine("Удалено - {0}", countDeleting);
            readFromFile.Close();
            writeToSecondFile.Close();
        }
    }
}
