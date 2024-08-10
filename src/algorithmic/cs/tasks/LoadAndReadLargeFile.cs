using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Algorithmic.Tasks
{
    /// <summary>
    /// To load and process a large file in the background, you can use the Task class from the System.Threading.Tasks namespace. 
    /// In the Task.Run() method you can start an asynchronous operation of reading a file and processing it.
    /// </summary>
    public class LoadAndReadLargeFile : KnowledgeBase.Algorithmic.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            string directory = "data";
            string path = Path.Combine(directory, "KnowledgeBase.Algorithmic.Tasks.LoadAndReadLargeFile.txt");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Let's say, the file should be over 10 MB in size.
            // Calculations:
            // 1 megabyte = 1024 kilobytes = 1,048,576 bytes = 1,048,576 characters.
            // 10 megabytes = 10,485,760 characters.
            // Dummy string contains approximately 58 characters.
            // It's necessary to insert the dummy string around 180,790 times.
            var sb = new StringBuilder();
            string dummyString = "Default string to populate a large file with some data!!!\n";
            using (FileStream fs = File.Create(path))
            {
                for (int i = 0; i < 180_790; i++)
                {
                    sb.Append(i.ToString()).Append(" ").Append(dummyString);
                    AddText(fs, sb.ToString());
                    sb.Clear();
                }
            }

            // Download and process a large file in the background.
            /*
И в данный момент я решаю задачу, которая заключается в том, чтобы выполнить загрузку и обработку большого текстового файла 
в фоновом режиме (т.е. с помощью класса Task). Файл весит 10 МБ.
Ограничения: размер буффера для чтения составляет 4096 байт.
В процессе обработки файла должны быть получены следующие данные: количество строк в файле и количество повторений символа "а" 
во всём файле.

Исходя из ограничения и тех данных, которые необходимо получить при обработке файла, нужно подумать относительно стратегии 
чтения данных из файла. Мне на ум приходят следующие возможные стратегии: 
- Из основного потока запуситить одну асинхронную задачу с помощью класса Task. В запущенной задаче выполнить цикл, который 
будет производить чтение файла по кускам (цикл выполняется до тех пор, пока весь файл не будет прочитан; размер одного куска 
файла должен быть не больше размера буффера). Таким образом, мы должны прочитать сначала первые 4096 байт в файле, потом очистить 
буффер и прочитать вторые 4096 байт (и это должно повторяться до тех пор, пока весь файл не будет прочитан и обработан). Сама 
обработка файла производится в рамках цикла после чтения куска файла и перед очисткой буффера.
- Второй возможный подход в целом почти такой же, как первый. Но для обработки файла мы запускаем отдельную асинхронную задачу 
или поток.

Хотелось бы получить от тебя комментарий по моим идеям, которые я тебе озвучил. Также хотелось бы, чтобы ты предложил ещё 2-3 
возможные идеи для решения данной задачи.

Первый подход, который ты описал, похож на то, как обычно обрабатываются большие файлы в C#. Он должен работать нормально, 
если у тебя не будет проблем с памятью или производительностью. Однако, я бы рекомендовал использовать асинхронный метод чтения 
файла ReadAsync вместо цикла чтения по кускам. Это позволит уменьшить количество блокировок и улучшить производительность.

Второй подход, который ты описал, также может быть хорошим вариантом, но я бы рекомендовал использовать Task.Run вместо создания 
нового потока. Это позволит использовать пул потоков и уменьшить нагрузку на систему.

Ещё несколько возможных идей:
- Использование асинхронного метода File.ReadLinesAsync для чтения файла построчно. Это позволит не загружать весь файл в память 
и уменьшить нагрузку на систему.
- Разбиение файла на несколько частей и обработка каждой части в отдельной асинхронной задаче. Это может улучшить производительность, 
если у тебя есть несколько ядер процессора.
- Использование асинхронного метода Stream.CopyToAsync для чтения и обработки файла. Это позволит уменьшить количество кода и 
упростить реализацию.
            */
            // ReadFileBytesSyncFileStream(path);
            ReadFileSyncFileStream(path, true);
            // ReadFileSyncStreamReader(path);
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        #region Synchronous methods for reading a file
        /// <summary>
        /// 
        /// </summary>
        private void ReadFileSyncFileStream(string path, bool backward = false)
        {
            var totalReadLength = 0;
            var startDateTime = System.DateTime.Now;
            System.Console.WriteLine("START");
            using (FileStream fs = File.OpenRead(path))
            {
                int readLength = 0;
                byte[] buffer = new byte[1024];
                UTF8Encoding encoding = new UTF8Encoding(true);
                if (backward)
                {
                    long offset = buffer.Length;
                    while (true)
                    {
                        fs.Seek(-offset, SeekOrigin.End);
                        while ((readLength = fs.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            System.Console.WriteLine(encoding.GetString(buffer, 0, readLength));
                            totalReadLength += readLength;
                        }
                        offset += totalReadLength;
                        System.Console.WriteLine("offset : " + offset);
                    }
                }
                else
                {
                    while ((readLength = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        System.Console.WriteLine(encoding.GetString(buffer, 0, readLength));
                        totalReadLength += readLength;
                    }
                }
            }
            var endDateTime = System.DateTime.Now;
            System.Console.WriteLine("FINISH");
            System.Console.WriteLine("totalReadLength: " + totalReadLength);
            System.Console.WriteLine("Read start: " + startDateTime.ToString());
            System.Console.WriteLine("Read finished: " + endDateTime.ToString());
            System.Console.WriteLine("Time elapsed: " + (endDateTime - startDateTime).ToString());
        }

        public void ReadFileBytesSyncFileStream(string path)
        {
            long offset;
            int nextByte;
            var totalReadLength = 0;
            var startDateTime = System.DateTime.Now;
            System.Console.WriteLine("START");
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                while ((nextByte = fs.ReadByte()) > 0)
                {
                    System.Console.Write((char)nextByte);
                }
                System.Console.WriteLine();
                // for (offset = 1; offset <= fs.Length; offset++)
                // {
                //     fs.Seek(-offset, SeekOrigin.End);
                //     System.Console.Write((char)fs.ReadByte());
                // }
                // System.Console.WriteLine();
            }
            var endDateTime = System.DateTime.Now;
            System.Console.WriteLine("FINISH");
            System.Console.WriteLine("totalReadLength: " + totalReadLength);
            System.Console.WriteLine("Read start: " + startDateTime.ToString());
            System.Console.WriteLine("Read finished: " + endDateTime.ToString());
            System.Console.WriteLine("Time elapsed: " + (endDateTime - startDateTime).ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReadFileSyncStreamReader(string path)
        {
            var strNumber = 0;
            var startDateTime = System.DateTime.Now;
            System.Console.WriteLine("START");
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                do
                {
                    line = sr.ReadLine();
                    strNumber += 1;
                    System.Console.WriteLine("strNumber: " + strNumber);
                } while (line != null);
            }
            var endDateTime = System.DateTime.Now;
            System.Console.WriteLine("FINISH");
            System.Console.WriteLine("Total strNumber: " + strNumber);
            System.Console.WriteLine("Read start: " + startDateTime.ToString());
            System.Console.WriteLine("Read finished: " + endDateTime.ToString());
            System.Console.WriteLine("Time elapsed: " + (endDateTime - startDateTime).ToString());
        }
        #endregion  // Synchronous methods for reading a file
    }
}