using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab06
{
    class Task4
    {
        private string path = @"E:\Lab6_Temp";
        private string pathToOriginalFile = @"E:\Lab06\laba.dat";
        private string pathToCopyFile = @"E:\Lab6_Temp\lab_copy.dat";
        private string pathToBackupFile = @"E:\Lab6_Temp\lab_backup.dat";

        public void Task4Main()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.Copy(pathToOriginalFile, pathToCopyFile, true);

            BinaryReader readFromOrigin = new BinaryReader(new FileStream(pathToCopyFile, FileMode.Open));
            BinaryWriter writeToBackup = new BinaryWriter(new FileStream(pathToBackupFile, FileMode.OpenOrCreate));

            while (readFromOrigin.PeekChar() > -1)
            {
                var sym = readFromOrigin.Read();
                writeToBackup.Write(sym);
            }

            FileInfo fileLabaDat = new FileInfo(pathToOriginalFile);
            Console.WriteLine("Информация о файле\nРазмер: {0} байт\nВремя последнего использования: {1}\nВремя последнего доступа: {2}", fileLabaDat.Length, fileLabaDat.LastWriteTime, fileLabaDat.LastAccessTime);

            readFromOrigin.Close();
            writeToBackup.Close();
        }
    }
}
