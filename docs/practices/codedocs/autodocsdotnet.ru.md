# Обзор автоматической документации к проектам в .NET

В современных ИТ-компаниях, особенно при работе над коммерческим программным обеспечением, часто встречается проблема устаревшей и неполной документации. Это приводит к значительным потерям времени и ресурсов.
Использование средств автоматической генерации документации позволяет существенно упростить и ускорить процесс создания и обновления документации, что приведет к повышению эффективности разработки и снижению затрат на сопровождение проектов.

## Оценка возможности использования

Плюсы использования инструментов автоматической генерации документации:

1. **Экономия времени**: Автоматизация процесса создания документации позволит разработчикам сосредоточиться на написании кода, а не на ручном документировании.
2. **Актуальность информации**: Документация будет автоматически обновляться при изменениях в коде, что поможет избежать устаревшей информации.
3. **Единообразие**: Использование стандартных инструментов поможет создать унифицированный стиль и формат документации, что облегчит ее восприятие.
4. **Улучшение качества**: Автоматическая генерация документации может помочь в выявлении недостатков в коде, таких как отсутствие комментариев или описаний методов.
5. **Легкость в обучении**: Новые члены команды смогут быстрее ориентироваться в проекте благодаря хорошо структурированной документации.

Проблемы, которые будут решены:

- Снижение нагрузки на технического писателя.
- Устранение разрыва между кодом и документацией.
- Повышение качества и доступности информации для команды.
- Улучшение onboarding-процесса для новых сотрудников.

## Инструменты для автоматической документации кода

### docfx

Если требуется простота использования и интеграция с HTML/Markdown, то DocFX будет хорошим выбором. Также можно генерировать PDF файл, который будет будет содержать документацию, доступную оффлайн.

**Ссылки**:
- [GitHub](https://github.com/dotnet/docfx)
- [документация](https://dotnet.github.io/docfx/)
- [вводная информация от Microsoft](https://learn.microsoft.com/en-us/shows/on-dotnet/intro-to-docfx)

**Поддержка .NET Framework**: Поддерживает .NET Framework, и ты можешь генерировать документацию для проектов на .NET Framework 4.8, даже если сам инструмент работает в окружении .NET 6+.

**Формат генерации документации**: Генерирует документацию в формате Markdown и HTML.

Используется в таких проектах, как Microsoft Docs. Он активно используется в экосистеме Microsoft для генерации документации для различных продуктов.

Вы можете использовать команду `docfx build path-to-proj/docfx.json`, которая также сгенерирует только статические файлы.

#### Генерация PDF

Гайд по генерации PDF представлен по [ссылке](https://dotnet.github.io/docfx/docs/pdf.html).

Для того, чтобы docfx сгенерировал PDF файл с документацией из командной строки, необходима установка chromium, который будет установлен в момент выполнения команды `docfx pdf` (без этого не будет произведена генерация PDF).

#### Кастомизация

1. Выставить дополнительные ссылки в панели навигации можно в файле `toc.yml` следующим образом:

```yml
- name: Документы
  href: docs/
- name: API
  href: api/
- name: Основное приложение
  href: /
```

2. Для кастомизации лендинговой страницы поменять файл `index.md`, например:

```md
# Документация по коду

<a href="/">Стартовая страница</a>: вернуться в основное приложение
```

3. Изменить шаблон можно следующим образом:

- Выгрузить стандартный шаблон docfx, например, `docfx template export modern`.
- В `_exported_templates/modern/layout/_master.tmpl` можно менять разметку или отображаемые значения для визуальных элементов (например, `<meta name="loc:downloadPdf" content="Загрузить PDF">` позволит отображать `"Загрузить PDF"` вместо `"Download PDF"`).
- Перенесем `_exported_templates/modern` в `templates/modern-new` и выполним команду `docfx -t default,templates/modern-new` (команда позволяет сделать merge для шаблонов `default` и `modern-new`).

#### Примеры использования

Пример генерации документации из кода (yml, html, pdf):
```C#
var autoDocsStaticFiles = "path-to-docs/docfx.json";

Console.WriteLine("docfx example: yml");
await Docfx.Dotnet.DotnetApiCatalog.GenerateManagedReferenceYamlFiles(autoDocsStaticFiles);

Console.WriteLine("docfx example: build");
await Docfx.Docset.Build(autoDocsStaticFiles);

Console.WriteLine("docfx example: pdf");
await Docfx.Docset.Pdf(autoDocsStaticFiles);
```

### Sandcastle Help File Builder

Если нужна более традиционная документация в HTML, то SHFB может быть предпочтительным вариантом.

**Ссылки**: [GitHub](https://github.com/EWSoftware/SHFB)

**Поддержка .NET Framework**: Также поддерживает .NET Framework и предназначен для генерации документации для проектов на этой платформе.

**Формат генерации документации**: Генерирует документацию в формате HTML, а также может создавать справочные файлы в формате CHM.

Используется в некоторых проектах, связанных с библиотеками .NET, но информация о крупных компаниях может быть менее доступной.

### Fody

**Ссылки**: [GitHub](https://github.com/Fody/Fody)

**Поддержка .NET Framework**: Это инструмент для манипуляции с IL-кодом и не предназначен непосредственно для генерации документации. Однако, если ты используешь Fody в проекте, который генерирует документацию, то это не должно вызывать проблем.

**Формат генерации документации**: Не генерирует документацию, поэтому этот вопрос к нему не применим.

## Способы интеграции инструментов для автоматической документации в существующую ERP систему

### Confluence

- Плюсы: Позволяет централизовать документацию и сделать ее доступной для команды.
- Минусы: Может потребовать дополнительной настройки и использования API Confluence.

#### Пример использования

1. Сервис генерирует HTML-код документации.
2. Сервис использует API Confluence для поиска или создания страницы документации.
3. Сервис загружает сгенерированный HTML-код в страницу документации.
4. Сервис обновляет страницу документации с новым контентом.

#### Опасения по поводу использования Confluence для больших проектов

Хранение большого объема документации в Confluence может быть дорогостоящим. Бесплатный тарифный план ограничен 2 ГБ, и вам может потребоваться перейти на платный план, если ваш проект будет генерировать больше документации.

Важно отметить:
- Ограничения бесплатного плана: Бесплатный план Confluence ограничен 2 ГБ хранилища, и это может быть недостаточно для вашего проекта.
- Стоимость платных планов: Укажите стоимость платных планов Confluence, чтобы продемонстрировать потенциальные расходы.
- Альтернативные варианты: Предложите альтернативные варианты хранения документации, например, собственное хранилище, GitHub Pages, Azure Blob Storage, статический веб-сайт и т.д.

### Использование из C#

- Плюсы: Гибкость в автоматизации процессов генерации документации в рамках приложения.
- Минусы: Может потребоваться больше усилий для интеграции и управления зависимостями.

### Проблемы при изучении и интеграции

При изучении:
- Могут возникнуть сложности с пониманием специфики каждого инструмента, особенно если нет опыта работы с ними.
- Документация может быть недостаточно понятной или устаревшей.

При интеграции в ERP:
- Возможные конфликты с существующими процессами разработки.
- Необходимость адаптации существующей структуры проекта под новый инструмент.

## Автоматизация генерации документации

### Создание worker-сервиса

- Плюсы: Автоматизация процесса, возможность планирования задач.
- Минусы: Дополнительные ресурсы на поддержку сервиса.

### Скрипты

Написание скриптов на PowerShell или Bash для запуска генерации документации.

- Плюсы: Легко настраивается и адаптируется под нужды проекта.
- Минусы: Меньшая гибкость по сравнению с worker-сервисом.

## Примерный план тестирования инструментов

### Шаг 1: Установка и настройка

DocFX:
- Установи DocFX через Chocolatey или скачай архив с GitHub.
- Ознакомься с документацией по установке и настройке.

SHFB:
- Установи Sandcastle Help File Builder через NuGet или скачай с официального сайта.

### Шаг 2: Тестирование на маленьком проекте

1. Создай простой проект "Hello World" на .NET (например, .NET Core или .NET Framework).

2. Добавь комментарии к коду, используя XML-теги.

Для DocFX:
- Создай конфигурационный файл docfx.json.
- Запусти команду docfx для генерации документации.

Для SHFB:
- Создай новый проект SHFB и добавь ссылку на свой проект.
- Сгенерируй документацию и посмотри на результат.

### Шаг 3: Генерация документации для разных проектов

- Повтори процесс для проектов на .NET Framework 4.8, .NET 6+ и .NET Standard 2.0.
- Сравни результаты генерации документации для каждого проекта.

### Критерии для сравнения docfx и SHFB

- **Поддержка платформы**: Совместимость с различными версиями .NET.
- **Формат выходной документации**: Поддерживаемые форматы (HTML, Markdown и т.д.).
- **Простота использования**: Легкость в установке и настройке.
- **Гибкость настройки**: Возможности кастомизации стилей и шаблонов.
- **Поддержка интеграции**: Наличие библиотек или API для интеграции с другими системами (например, Confluence).
- **Сообщество и поддержка**: Активность сообщества, наличие документации, примеров и т.д.

## Библиотеки API для интеграции с Confluence

Для интеграции с Confluence можно использовать библиотеки, такие как:

- **Atlassian.SDK**: Это библиотека для работы с API Atlassian, включая Confluence. Она доступна через [NuGet](https://www.nuget.org/packages/Atlassian.SDK) и есть в open-source [репозитории на bitbucket](https://bitbucket.org/farmas/atlassian.net-sdk/src/master/).
- **Confluence REST API**: Можно использовать стандартные HTTP-запросы для работы с API Confluence. Документация доступна на [официальном сайте Atlassian](https://developer.atlassian.com/cloud/confluence/rest/v2/intro/)

## Архитектура

### Отдельный сервис для генерации документации:

Плюсы:

- **Разделение ответственности**: Ответственность за документацию ложится на отдельный сервис, что улучшает модульность и структуру системы.
- **Легкость поддержки**: Отдельный сервис проще поддерживать и развивать.
- **Масштабируемость**: Сервис можно легко масштабировать, добавляя мощности и ресурсов для обработки больших объемов документации.
- **Гибкость**: Можно легко менять настройки и конфигурацию сервиса без влияния на клиентское приложение.
- **Независимость от платформы**: Сервис может работать на любой платформе, не завися от платформы клиентского приложения.

Минусы:

- **Дополнительная сложность**: Требуется разработка и поддержка отдельного сервиса, что может быть сложнее, чем интеграция в клиентское приложение.
- **Дополнительная инфраструктура**: Требуется настройка и поддержка дополнительной инфраструктуры для запуска сервиса, например, сервер, база данных.
- **Проблемы с синхронизацией**: Необходимо обеспечить синхронизацию изменений в коде и документации между сервисом и клиентским приложением.

В каких ситуациях данный подход может быть более логичным, чем использование GitLab CI/CD:

- **Простые процессы сборки и развертывания**: Если процессы сборки и развертывания достаточно простые, то использование отдельного сервиса может быть более эффективным, чем настройка CI/CD системы.
- **Контроль над процессом**: Отдельный сервис дает вам больше контроля над процессом сборки и развертывания.
- **Необходимость специальной логики**: Если вам нужна специальная логика в процессе сборки и развертывания, которую сложно реализовать в CI/CD системе, то отдельный сервис может быть лучшим решением.

### Подходы к генерации документации

#### Автоматическая генерация раз в некоторое время

Плюсы:

- **Простота**: Простой подход для реализации.
- **Не требует дополнительной настройки**: Сервис запускается по расписанию, что минимизирует необходимость для дополнительных конфигураций.
- **Эффективность для статичной документации**: Подходит для документации, которая редко меняется.

Минусы:

- **Риск неактуальной документации**: Документация может быть неактуальной между генерациями.
- **Длительное время ожидания**: Пользователь может столкнуться с неактуальной документацией, пока не произойдет следующая генерация.
- **Не подходит для динамичной документации или для больших проектов**: Не подходит для документации, которая постоянно обновляется, особенно в крупных системах.
- **Не гибкий**: Невозможно получить обновленную документацию по требованию.

#### Генерация по запросу

Плюсы:

- **Актуальная документация**: Документация может быть сгенерирована по триггеру, например, при публикации задачи на прод.
- **Гибкость**: Можно получить обновленную документацию в любое время.
- **Подходит для динамичной документации**: Подходит для документации, которая постоянно обновляется.
- **Выбор модулей, по которым нужно обновить документацию**: Можно гибко настроить процесс публикации документации по запросу, чтобы по запросу не тратились ресурсы на обновление документации по модулям, где документация не менялась.

Минусы:

- **Сложность реализации**: Более сложная реализация, чем автоматическая генерация.
- **Необходимость синхронизации ответов от сервиса**: Сервис вынужден будет работать по запросу асинхронно, т.е. выполнение задачи может затянуться на несколько минут, соответственно, столько времени держать соединение для ожидания ответа по задаче нерационально. Из этого следует, что лучше использовать либо брокер сообщений, либо реализовывать свой механизм асинхронного взаимодействия (например, через некую общую таблицу в БД).
- **Необходимость отображения логов**: В клиентском приложении, из которого производится запрос на обновление документации, может потребоваться отображать логи для понимания статуса запроса на обновление.

### Определение оптимального времени обновления

Статическое время:

- Плюсы: Простота реализации, предсказуемость.
- Минусы: Негибкость, может быть неэффективным для часто меняющегося кода или редких изменений.

Вычисление оптимального периода:

- Плюсы: Гибкость, адаптивность к изменениям в коде.
- Минусы: Сложнее реализовать, требует дополнительных ресурсов для мониторинга и анализа.

Рекомендации:

- Начните с фиксированного периода (например, 1 час). 
- Отслеживайте частоту изменений кода. Если код меняется часто, уменьшите период обновления. 
- Проанализируйте время генерации документации. Если генерация занимает много времени, увеличьте период обновления. 
- Используйте гибкий подход, который можно изменять в зависимости от потребностей.

### Логирование сервиса автоматической документации

Сервис должен логировать:

- Время запуска и завершения генерации документации.
- Успех/неудачу генерации документации.
- Ошибки, возникшие во время генерации документации.
- Изменения в коде, которые привели к генерации новой документации.
