using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab06
{
    class Task5
    {
        private string nameBMP;
        private string pathToDirectory = @"E:\Lab06";

        public void TaskMain5()
        {
            string[] allFilesDir = Directory.GetFiles(pathToDirectory);
            foreach (string fileName in allFilesDir)
            {
                Console.WriteLine(fileName);
            }
            Console.WriteLine("Введите название файла файла");
            nameBMP = Console.ReadLine();
            string pathToFileBMP = $@"{pathToDirectory}\{nameBMP}.bmp";
            Console.WriteLine(pathToFileBMP);
            Console.Clear();

            BinaryReader readInfo = new BinaryReader(File.Open(pathToFileBMP, FileMode.Open));
            
            readInfo.ReadChars(2);
            int sizeBMPInfo = readInfo.ReadInt32();
            readInfo.ReadBytes(12);
            int widthOfFile = readInfo.ReadInt32();
            int heightOfFile = readInfo.ReadInt32();
            readInfo.ReadBytes(2);
            short amountOfPixels = readInfo.ReadInt16();
            int typeOfCompressInt = readInfo.ReadInt32();
            string typeOfCompress = String.Empty;
            switch (typeOfCompressInt)
            {
                case 0:
                    typeOfCompress = "без сжатия";
                    break;
                case 1:
                    typeOfCompress = "8 бит RLE";
                    break;
                case 2:
                    typeOfCompress = "4 бит RLE";
                    break;
            }
            readInfo.ReadInt32();
            int horizontalResolution = readInfo.ReadInt32();
            int verticalResolution = readInfo.ReadInt32();

            Console.WriteLine($"Размер файла: {sizeBMPInfo} байт\n" +
                $"Ширина файла: {widthOfFile} px\n" +
                $"Высота файла: {heightOfFile} px\n" +
                $"Количество бит на пиксель: {amountOfPixels} бит\n" +
                $"Тип сжатия: {typeOfCompress}\n" +
                $"Горизонтальное разрешение: {horizontalResolution} пиксел/м\n" +
                $"Вертикальное разрешение: {verticalResolution} пиксел/м");

            readInfo.Close();
        }
    }
}
