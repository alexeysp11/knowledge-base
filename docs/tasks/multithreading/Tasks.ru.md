# Task

Класс `Task` реализует интерфейс `IThreadPoolWorkItem` (вследствии чего может быть помещён в очередь пула потоков), а также имеет ссылку на планировщик заданий `TaskScheduler`.

Класс `TaskScheduler` является абстрактным классом и имеет internal метод `InternalQueueTask`, который вызывает метод `QueueTask`.

## Общая информация по вызову задачи

### Запуск задачи с помощью метода Run

В момент, когда вызывается метод `Run` (в методе `Run` передаётся делегат), следом вызывается метод `InternalStartNew`, в котором переданный делегат оборачивается в задачу `Task` (вызывается internal конструктор; в конструктор передаётся пул потоков по умолчанию, т.е. `ThreadPoolTaskScheduler`).
В конструкторе вызывается метод `TaskConstructorCore`, в котором присваивается переданный пул потоков.

Класс `ThreadPoolTaskScheduler` наследуется от класса `TaskScheduler` и реализует метод `QueueTask` следующим образом: 
- если было проставлено `CreationOptions.LongRunning` при инициализации задачи, то для её выполнения будет создан новый поток, который будет работать в фоновом режиме;
- в противном случае задача будет отправлена в очередь пула потоков.

### Запуск задачи с помощью метода Start

Если запуск производится с помощью метода без параметров, то берётся текущий пул потоков (пул потоков определяется по задаче `Task`, которая выполняется в данный момент); если нет задач `Task`, которые выполняются в данный момент, то возвращается `TaskScheduler` по умолчанию и вызывается метод `Start`, который принимает `TaskScheduler`.

В методе `Start`, который принимает `TaskScheduler`, вызывается метод `ScheduleAndStart`, который обращается к пулу потоков и ставит задачу в очередь (с помощью вызова метода `TaskScheduler.InternalQueueTask`, который в свою очередь вызывает `QueueTask`).

## Задачи 

Задачи для использования `Task` в C#:
- Загрузка и обработка большого файла в фоновом режиме.

Для загрузки и обработки большого файла в фоновом режиме можно использовать класс `Task` из пространства имен `System.Threading.Tasks`. В методе `Task.Run()` можно запустить асинхронную операцию чтения файла и его обработки. Например:

```C#
Task.Run(async () =>
{
    using (var stream = new FileStream("largefile.txt", FileMode.Open))
    {
        var buffer = new byte[4096];
        while (await stream.ReadAsync(buffer, 0, buffer.Length) > 0)
        {
            // Data processing.
        }
    }
});
```

- Отправка асинхронного запроса к API и обработка полученных данных.

Для отправки асинхронного запроса к API и обработки полученных данных можно использовать класс `HttpClient` из пространства имен System.Net.Http. Методы `SendAsync()` или `GetAsync()` могут быть вызваны внутри метода `Task.Run()`. Например:
```C#
Task.Run(async () =>
{
    using (var client = new HttpClient())
    {
        var response = await client.GetAsync("https://api.example.com/data");
        var content = await response.Content.ReadAsStringAsync();
        // Data processing.
    }
});
```

- Асинхронная обработка изображений (например, изменение размера или применение фильтров).

Для асинхронной обработки изображений можно использовать классы из пространства имен System.Drawing или библиотеки ImageSharp. Методы обработки изображений могут быть вызваны внутри метода `Task.Run()`. Например:
```C#
Task.Run(async () =>
{
    using (var image = Image.FromFile("image.jpg"))
    {
        // Изменение размера
        var resizedImage = new Bitmap(200, 200);
        using (var graphics = Graphics.FromImage(resizedImage))
        {
            graphics.DrawImage(image, 0, 0, 200, 200);
        }
        // Data processing.
    }
});
```

- Параллельное выполнение нескольких запросов к базе данных.

Для параллельного выполнения нескольких запросов к базе данных можно использовать классы из пространства имен System.Data.SqlClient или других библиотек для работы с базами данных. Методы выполнения запросов могут быть вызваны внутри метода `Task.Run()`. Например:
```C#
Task.Run(async () =>
{
    using (var connection = new SqlConnection(connectionString))
    {
        await connection.OpenAsync();
        var command1 = new SqlCommand("SELECT * FROM table1", connection);
        var command2 = new SqlCommand("SELECT * FROM table2", connection);
        var reader1 = await command1.ExecuteReaderAsync();
        var reader2 = await command2.ExecuteReaderAsync();
        // Data processing.
    }
});
```

- Асинхронная отправка email уведомлений.

Для асинхронной отправки email уведомлений можно использовать классы из пространства имен `System.Net.Mail`. Методы отправки email могут быть вызваны внутри метода `Task.Run()`. Например:
```C#
Task.Run(async () =>
{
    using (var message = new MailMessage("from@example.com", "to@example.com", "Subject", "Body"))
    {
        using (var client = new SmtpClient("smtp.example.com"))
        {
            client.Credentials = new NetworkCredential("username", "password");
            client.EnableSsl = true;
            await client.SendMailAsync(message);
        }
    }
});
```

- Асинхронный подсчет статистики по большому набору данных.

Для асинхронного подсчета статистики по большому набору данных можно использовать классы из пространства имен System.Threading.Tasks. Методы подсчета статистики могут быть вызваны внутри метода `Task.Run()`. Например:
```C#
Task.Run(async () =>
{
    var data = await LoadDataAsync();
    var statistics = CalculateStatistics(data);
    // Data processing.
});
```

- Параллельное выполнение нескольких задач на удаленных серверах.

Для параллельного выполнения нескольких задач на удаленных серверах можно использовать классы из пространства имен `System.Threading.Tasks` и соответствующие сетевые протоколы, например, TCP или UDP. Методы выполнения задач могут быть вызваны внутри метода `Task.Run()`. Например:
```C#
Task.Run(async () =>
{
    using (var client1 = new TcpClient())
    using (var client2
- = new TcpClient())
    {
        await Task.WhenAll(
            client1.ConnectAsync("server1.example.com", 1234),
            client2.ConnectAsync("server2.example.com", 1234)
        );
        // Data processing.
    }
});
```

- Асинхронная обработка и отправка большого количества файлов.

Для асинхронной обработки и отправки большого количества файлов можно использовать классы из пространства имен System.IO и `System.Net.Mail`. Методы обработки и отправки файлов могут быть вызваны внутри метода `Task.Run()`. Например:
```C#
Task.Run(async () =>
{
    var files = Directory.GetFiles("path/to/files");
    foreach (var file in files)
    {
        using (var stream = new FileStream(file, FileMode.Open))
        {
            // Data processing.
            var attachment = new Attachment(stream, Path.GetFileName(file));
            using (var message = new MailMessage("from@example.com", "to@example.com", "Subject", "Body"))
            {
                message.Attachments.Add(attachment);
                using (var client = new SmtpClient("smtp.example.com"))
                {
                    client.Credentials = new NetworkCredential("username", "password");
                    client.EnableSsl = true;
                    await client.SendMailAsync(message);
                }
            }
        }
    }
});
```

- Параллельное выполнение нескольких задач на многопроцессорной машине.

Для параллельного выполнения нескольких задач на многопроцессорной машине можно использовать классы из пространства имен `System.Threading.Tasks` и распределение задач между ядрами процессора. Для этого можно использовать класс Parallel из того же пространства имен. Например:
```C#
Task.Run(() =>
{
    var data = LoadData();
    Parallel.ForEach(data, item =>
    {
        // Data processing.
    });
});
```

- Асинхронный запуск и контроль выполнения других задач.

Для асинхронного запуска и контроля выполнения других задач можно использовать классы из пространства имен `System.Threading.Tasks` и методы `WaitAll()` или `WhenAll()`. Например:
```C#
var task1 = Task.Run(() => DoTask1());
var task2 = Task.Run(() => DoTask2());
await Task.WhenAll(task1, task2);
// Data processing.
```
