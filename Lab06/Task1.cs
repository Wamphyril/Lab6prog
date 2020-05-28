using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab06
{
    class Task1
    {
        public struct tvShow
        {
            public string name;
            public string announcer;
            public byte raiting;
            public typeOfShow type;
        }
        public struct log
        {
            public DateTime time;
            public string detailInfo;
            public string oldDetailInfo;
            public typeEvent tEvent;
        }
        public enum typeEvent
        {
            ADD,
            DELETE,
            UPDATE
        }
        public enum typeOfShow
        {
            А = 'А',
            И = 'И',
            Т = 'Т'
        }

        private log[] dataLog = new log[50];
        private byte index = 0;

        private string pathDir = @"E:\Lab06";
        private string path = @"E:\Lab06\laba.dat";

        private string[] choices = {"1 – Просмотр таблицы",
                                    "2 – Добавить запись",
                                    "3 – Удалить запись",
                                    "4 – Обновить запись",
                                    "5 – Поиск записей",
                                    "6 – Просмотреть лог",
                                    "7 - Выход"};

        public tvShow[] data = new tvShow[0];
        private DateTime dt = new DateTime();

        //interval
        private TimeSpan intervalFirst = new TimeSpan();
        private TimeSpan intervalSecond = new TimeSpan();
        private DateTime start = new DateTime();
        private DateTime end = new DateTime();

        public void Choose(ref tvShow[] data)
        {
            if (!Directory.Exists(pathDir))
            {
                Directory.CreateDirectory(pathDir);
            }
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int numElem = 0;
                    for (string line = String.Empty; (line = sr.ReadLine()) != null;)
                    {
                        numElem++;
                    }
                    Array.Resize(ref data, numElem);

                }
                using (StreamReader sr = new StreamReader(path))
                {
                    string[] dataLine = new string[4];
                    int i = 0;
                    for (string line; (line = sr.ReadLine()) != null;)
                    {
                        dataLine = line.Split(" ");
                        data[i].name = dataLine[0];
                        data[i].announcer = dataLine[1];
                        data[i].raiting = Convert.ToByte(dataLine[2]);
                        data[i].type = (typeOfShow)Enum.Parse(typeof(typeOfShow), dataLine[3]);
                        i += 1;
                    }
                }
            }

            while (true)
            {
                if (index == 50)
                {
                    index = 0;
                }
                Console.Clear();
                foreach (string str in choices)
                {
                    Console.WriteLine(str);
                }
                start = DateTime.Now;
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        end = DateTime.Now;
                        intervalSecond = end.Subtract(start);
                        ShowTable(data);
                        break;
                    case "2":
                        end = DateTime.Now;
                        intervalSecond = end.Subtract(start);
                        AddNote(ref data, ref index);
                        break;
                    case "3":
                        end = DateTime.Now;
                        intervalSecond = end.Subtract(start);
                        DeleteNote(ref data);
                        break;
                    case "4":
                        end = DateTime.Now;
                        intervalSecond = end.Subtract(start);
                        ChangeNote(data);
                        break;
                    case "5":
                        end = DateTime.Now;
                        intervalSecond = end.Subtract(start);
                        FindNote(data);
                        break;
                    case "6":
                        LogList(dataLog);
                        break;
                    case "7":
                        using (StreamWriter sw = new StreamWriter(path, false))
                        {
                            string line = String.Empty;
                            foreach (var element in data)
                            {
                                line = $"{element.name} {element.announcer} {element.raiting} {element.type}";
                                sw.WriteLine(line);
                            }
                        }
                        Environment.Exit(0);
                        break;
                }
                index++;
                if (intervalFirst < intervalSecond)
                {
                    intervalFirst = intervalSecond;
                }
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        private void AddNote(ref tvShow[] data, ref byte index)
        {
            log log_add = new log();

            // add
            tvShow show = new tvShow();
            #region Ввод данных
            bool errInput = false;
            do
            {
                try
                {
                    errInput = false;

                    Console.WriteLine("Введите название тв-шоу");
                    show.name = Console.ReadLine();
                    Console.WriteLine("Введите Имя и фамилию ведущего");
                    show.announcer = Console.ReadLine();
                    Console.WriteLine("Введите рейтинг (0-5)");
                    show.raiting = Byte.Parse(Console.ReadLine());
                    Console.WriteLine("Введите рейтинг (И, А, Т)");
                    char type = Char.Parse(Console.ReadLine());
                    if (type == 'И')
                    {
                        show.type = typeOfShow.И;
                    }
                    else if (type == 'А')
                    {
                        show.type = typeOfShow.А;
                    }
                    else if (type == 'Т')
                    {
                        show.type = typeOfShow.Т;
                    }
                }
                catch
                {
                    Console.WriteLine("Введены неправильные данные");
                    errInput = true;
                }
            }
            while (errInput);

            #endregion

            int size = data.Length + 1;
            Array.Resize(ref data, size);
            data[size - 1] = show;

            // log
            dt = DateTime.Now;
            log_add.time = dt;
            log_add.detailInfo = $"{show.name} | {show.announcer} | {show.raiting} | {show.type}";
            log_add.tEvent = typeEvent.ADD;

            dataLog[index].time = dt;
            dataLog[index].tEvent = log_add.tEvent;
            dataLog[index].detailInfo = log_add.detailInfo;
        }
        private void ShowTable(tvShow[] data)
        {
            Console.WriteLine(" ___________________________________________________________________________________");
            Console.WriteLine("|Телепередачи_______________________________________________________________________|");
            Console.WriteLine("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|", "Передача____________", "Ведущий_____________", "Рейтинг_____________", "Тип_________________");
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("|{0,-20}|{1,-20}|{2,-20}|{3,-20}|", $"{i + 1}. " + data[i].name, data[i].announcer, data[i].raiting, data[i].type);
            }
            Console.WriteLine("|И-игровая; А-аналитическая; Т-токшоу_______________________________________________|");
        }
        private void DeleteNote(ref tvShow[] data)
        {
            log log_add = new log();

            // delete
            bool errNumDel = false;

            Console.WriteLine("Введите номер записи, которую хотите удалить");
            int numDel = 0;
            do
            {
                try
                {
                    errNumDel = false;
                    numDel = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Введите корректно номер записи");
                    errNumDel = true;
                }
            }
            while (errNumDel);
            numDel--;
            log_add.detailInfo = $"{data[numDel].name} | {data[numDel].announcer} | {data[numDel].raiting} | {data[numDel].type}";

            Array.Clear(data, numDel, 1);
            if (numDel != data.Length - 1)
            {
                for (int i = numDel; i < data.Length - 1; i++)
                {
                    data[i] = data[i + 1];
                }
            }
            Array.Resize(ref data, data.Length - 1);

            // log
            dt = DateTime.Now;
            log_add.time = dt;
            log_add.tEvent = typeEvent.DELETE;

            dataLog[index].time = dt;
            dataLog[index].tEvent = log_add.tEvent;
            dataLog[index].detailInfo = log_add.detailInfo;
        }
        private void ChangeNote(tvShow[] data)
        {
            log log_add = new log();

            bool errNumChange = false;

            Console.WriteLine("Введите номер записи, которую хотите изменить");
            int numChange = 0;
            do
            {
                try
                {
                    errNumChange = false;
                    numChange = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Введите корректно номер в списке");
                    errNumChange = true;
                }
            }
            while (errNumChange);
            numChange--;

            log_add.oldDetailInfo = $"{numChange++}. {data[numChange].name} | {data[numChange].announcer} | {data[numChange].raiting} | {data[numChange].type}";
            dataLog[index].oldDetailInfo = log_add.oldDetailInfo;

            Array.Clear(data, numChange, 1);
            tvShow show = new tvShow();
            #region Ввод данных
            bool errInput = false;
            do
            {
                try
                {
                    errInput = false;

                    Console.WriteLine("Введите название тв-шоу");
                    show.name = Console.ReadLine();
                    Console.WriteLine("Введите Имя и фамилию ведущего");
                    show.announcer = Console.ReadLine();
                    Console.WriteLine("Введите рейтинг (0-5)");
                    show.raiting = Byte.Parse(Console.ReadLine());
                    Console.WriteLine("Введите рейтинг (И, А, Т)");
                    char type = Char.Parse(Console.ReadLine());
                    if (type == 'И')
                    {
                        show.type = typeOfShow.И;
                    }
                    else if (type == 'А')
                    {
                        show.type = typeOfShow.А;
                    }
                    else if (type == 'Т')
                    {
                        show.type = typeOfShow.Т;
                    }
                }
                catch
                {
                    Console.WriteLine("Введены неправильные данные");
                    errInput = true;
                }
            }
            while (errInput);
            #endregion
            data[numChange] = show;

            // log
            dt = DateTime.Now;
            log_add.time = dt;
            log_add.detailInfo = $"{show.name} | {show.announcer} | {show.raiting} | {show.type}";
            log_add.tEvent = typeEvent.UPDATE;

            dataLog[index].time = dt;
            dataLog[index].tEvent = log_add.tEvent;
            dataLog[index].detailInfo = log_add.detailInfo;
        }
        private void FindNote(tvShow[] data)
        {
            Console.WriteLine("Искать по:\n1. Названию\n2. Имени ведущего\n" +
                                "3. Рейтингу\n4. Типу передачи");
            string mdSearch = Console.ReadLine();
            int index = 1;
            switch (mdSearch)
            {
                case "1":
                    Console.WriteLine("Введите часть или полное название");
                    string strN = Console.ReadLine();
                    index = 1;
                    foreach (tvShow item in data)
                    {
                        if (item.name.StartsWith(strN))
                        {
                            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", $"{index}. " + item.name, item.announcer, item.raiting, item.type);
                        }
                        index++;
                    }
                    break;
                case "2":
                    Console.WriteLine("Введите часть или полное название");
                    string strA = Console.ReadLine();
                    index = 1;
                    foreach (tvShow item in data)
                    {
                        if (item.announcer.StartsWith(strA))
                        {
                            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", $"{index}. " + item.name, item.announcer, item.raiting, item.type);
                        }
                        index++;
                    }
                    break;
                case "3":
                    Console.WriteLine("Введите рейтинг передачи");
                    byte bRaiting = Byte.Parse(Console.ReadLine());
                    index = 1;
                    foreach (tvShow item in data)
                    {
                        if (item.raiting == bRaiting)
                        {
                            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", $"{index}. " + item.name, item.announcer, item.raiting, item.type);
                        }
                        index++;
                    }
                    break;
                case "4":
                    Console.WriteLine("Введите тип передачи [И, А, Т]");
                    char cType = Char.Parse(Console.ReadLine());
                    index = 1;
                    foreach (tvShow item in data)
                    {
                        if ((char)item.type == cType)
                        {
                            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-20}", $"{index}. " + item.name, item.announcer, item.raiting, item.type);
                        }
                        index++;
                    }
                    break;
            }
        }
        private void LogList(log[] dataLog)
        {
            for (int i = 0; i < dataLog.Length; i++)
            {
                if (dataLog[i].time != new DateTime())
                {
                    if (dataLog[i].tEvent == typeEvent.ADD)
                    {
                        Console.WriteLine($"{dataLog[i].time.ToString("hh:mm:ss")} - Добавлена запись ({dataLog[i].detailInfo})");
                    }
                    else if (dataLog[i].tEvent == typeEvent.DELETE)
                    {
                        Console.WriteLine($"{dataLog[i].time.ToString("hh:mm:ss")} - Удалена запись ({dataLog[i].detailInfo})");
                    }
                    else if (dataLog[i].tEvent == typeEvent.UPDATE)
                    {
                        Console.WriteLine($"{dataLog[i].time.ToString("hh:mm:ss")} - Обновлена запись с ({dataLog[i].oldDetailInfo}) на ({dataLog[i].detailInfo})");
                    }
                }
            }
            Console.WriteLine("{0:hh\\:mm\\:ss} - максимальное время бездействия", intervalFirst);
        }
    }
}