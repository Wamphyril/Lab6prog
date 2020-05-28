using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab06
{
    class Task2
    {
        private string pathDir = @"E:\Lab06";
        private string path = @"E:\Lab06\labaSec.dat";
        private string pathSec = @"E:\Lab06\labaSecBin.dat";

        public void BasicMain()
        {
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }

            using (BinaryWriter writeToFirstFile = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                for (int i = 4; i < 82; i++)
                {
                    double n = Math.Pow(i, 0.5);
                    writeToFirstFile.Write(i);
                    writeToFirstFile.Write(n);
                }
            }
        }
        public void WriteToSecondFile()
        {
            BinaryReader readFromFirstFile = new BinaryReader(new FileStream(path, FileMode.Open));
            BinaryWriter writeToSecondFile = new BinaryWriter(new FileStream(pathSec, FileMode.OpenOrCreate));
            while (readFromFirstFile.PeekChar() > -1)
            {
                int i = readFromFirstFile.ReadInt32();
                double n = readFromFirstFile.ReadDouble();
                writeToSecondFile.Write(n);
                Console.Write(i);
                Console.Write("-");
                Console.WriteLine(n);
            }
            readFromFirstFile.Close();
            writeToSecondFile.Close();
        }
    }
}
