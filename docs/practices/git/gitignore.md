# Использование gitignore

Бывает, что в файле `.gitignore` прописано игнорировать определенную папку (например, папку `packages`).
Однако в некоторых случаях нужно сделать исключение для определенной папки и добавить её в git репозиторий (к примеру, без папки `packages` не собирается проект).

Для того, чтобы сделать исключение для определенной папки в файл `.gitignore`, нужно указать путь до той папки, начиная со знака `!`, например:

```
!src/examples/WorkerServiceXplatform/packages/
!src/examples/WorkerServiceXplatform/packages/*
!src/examples/WorkerServiceXplatform/packages/*/*
```
