# Управление переводами в ASP.NET Core

## Общие действия

- [x] Написать библиотеку для того, чтобы имитировать модуль, который возвращет переводы аналогично тому, как выполнено для UBP.
- [ ] Создать три ASP.NET Core приложений для тестирования переводов:
    - [x] **Первое**:
        - [x] С помощью логирования в консоль проверить последовательность, в которой выполняется рендеринг (для обычного файла `cshtml` и для `_Layout.csthml` - для условия `@if (User.Identity.IsAuthenticated)`).
        - [x] Cледует использовать `@if (User.Identity.IsAuthenticated)` в обычном файле `cshtml` для проверки того, что именно это условие виляет на последовательность рендеринга.
        - [x] Проверить работу с БД (EF Core, Dapper).
    - [ ] **Второе**: Выполнить переводы в `_Layout.csthml` с помощью `ViewComponent`.
    - [ ] **Третье**: Выполнить переводы в `_Layout.csthml` с помощью Base Controller.
- [ ] Научиться обрабатывать исключения.
    - [ ] Binding.
    - [ ] Фильтры: https://learn.microsoft.com/en-us/aspnet/core/mvc/controllers/filters
    - [ ] Обработка исключений: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling
