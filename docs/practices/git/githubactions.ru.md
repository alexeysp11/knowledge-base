# GitHub Actions

Использование GitHub Actions может быть как выгодным, так и рискованным с точки зрения затрат. GitHub предоставляет бесплатные минуты для публичных репозиториев и для частных репозиториев (в зависимости от вашего плана). Если ваш проект активно развивается и вы часто выполняете сборки, то количество использованных минут может превысить бесплатный лимит. В этом случае стоит внимательно следить за использованием ресурсов и оценивать, насколько это необходимо.

Пример использования GitHub Actions для автоматической сборки решения `src/Shared/WorkflowLib.Shared.sln`:
```yml
name: Build Solution

on:
  push:
    branches:
      - main
  pull_request:
    branches:
      - main

jobs:
  build:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout code
        uses: actions/checkout@v2

      - name: Set up .NET
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: '8.0'

      - name: Restore dependencies
        run: dotnet restore src/Shared/WorkflowLib.Shared.sln

      - name: Build the solution
        run: dotnet build src/Shared/WorkflowLib.Shared.sln --configuration Release --no-restore

      - name: Run tests
        run: dotnet test src/Shared/WorkflowLib.Shared.sln --configuration Release --no-build --verbosity normal
```
