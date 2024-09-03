# Worker Service

## Сравнительный анализ отличий шаблоны Worker service на .NET 6 и .NET 8

- Основные различия минимальны. В .NET 8, как и в .NET 6, шаблон Worker Service предоставляет базовую структуру для создания фоновых задач, работающих в отдельном процессе.
- Ключевые изменения: .NET 8 включает новые возможности для улучшения производительности, такие как поддержка JIT-компиляции и улучшенный сборщик мусора.
- Рекомендации: Изучите возможности .NET 8, такие как улучшенная поддержка облачных сервисов и новые API, которые могут быть полезны для вашего сервиса.

## Возможность использования WCF в рамках Worker service на .NET 6 и .NET 8

- [dotnet/wcf](https://github.com/dotnet/wcf): Этот репозиторий на GitHub действительно является портом WCF для .NET Core/8. Он предоставляет основные возможности WCF, но не все функции доступны.
- [CoreWCF](https://github.com/CoreWCF/CoreWCF): Это популярный open-source проект, который предлагает более полную поддержку WCF для .NET Core/8. 

## Адаптация Worker service для Windows и Linux:

`RuntimeInformation.IsOSPlatform()` - это правильный подход для определения платформы в runtime.

Пример кода:
```C#
using System;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting;

namespace MyWorkerServiceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHostedService<MyWorkerService>(); 

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                builder.Services.AddWindowsService();
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // Configuration for Linux.
                // Example: 
                // builder.Services.AddHostedService<MyWorkerService>()
                //     .Configure<HostOptions>(options => options.ShutdownTimeout = TimeSpan.FromSeconds(10));
            }

            var app = builder.Build();

            app.Run();
        }
    }
}
```

Тестирование Worker service на Linux:
- WSL (Windows Subsystem for Linux): Это наиболее простой способ протестировать Worker service на Linux. Установите WSL и запустите ваш сервис в Linux-среде внутри Windows.
- Виртуальная машина (VM): Создайте виртуальную машину с Linux-системой и разверните ваш сервис на ней. 

## Разделение базы данных и сборок на test и production

Аргументация для разделения:
- Повышение стабильности: Тестовая среда должна быть изолирована от production, чтобы изменения, которые ещё не протестированы, не повлияли на работу сервиса в продакшене.
- Упрощение тестирования: Тестовая среда позволяет имитировать условия продакшена и проводить более полное тестирование сервиса.
- Управление версиями: Разделение тестовой и продакционной среды позволяет использовать разные версии сервиса и базы данных, что упрощает управление выпусками и откат на предыдущие версии.

### Один компьютер

Способы разделения на одном компьютере:

- Виртуальные машины (VM): Используйте виртуальные машины для создания изолированных тестовых и продакционных сред.
- Docker: Используйте контейнеры Docker для изоляции тестовой и продакционной среды.
- Разные конфигурации: Используйте разные конфигурационные файлы (appsettings.json, connection strings) для тестовой и продакционной среды.

### Два и более компьютеров

В случае двух и более компьютеров, разделение на тест и прод производится следующим образом: `x` компьютеров используется для тестовой среды, а `y` компьютеров - для продакционной среды.
