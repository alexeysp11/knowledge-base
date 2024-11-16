FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /knowledge-base

COPY . .

RUN dotnet restore "src/examples/WorkerServiceXplatform/WorkerServiceXplatform/KnowledgeBase.Examples.WorkerServiceXplatform.csproj" 
RUN dotnet restore "src/examples/AutoDocsExamples/DocfxExamples/KnowledgeBase.Examples.AutoDocsExamples.DocfxExamples.csproj"
RUN dotnet restore "src/examples/TrickyScheduler/TrickyScheduler.sln" 
RUN dotnet restore "src/examples/RabbitMqExamples/RabbitMqExamples.sln"

RUN dotnet publish -c Release -o /knowledge-base/publish/WorkerServiceXplatform "src/examples/WorkerServiceXplatform/WorkerServiceXplatform/KnowledgeBase.Examples.WorkerServiceXplatform.csproj" 
RUN dotnet publish -c Release -o /knowledge-base/publish/AutoDocsExamples "src/examples/AutoDocsExamples/DocfxExamples/KnowledgeBase.Examples.AutoDocsExamples.DocfxExamples.csproj"
RUN dotnet publish -c Release -o /knowledge-base/publish/TrickyScheduler "src/examples/TrickyScheduler/TrickyScheduler.sln" 
RUN dotnet publish -c Release -o /knowledge-base/publish/RabbitMqExamples "src/examples/RabbitMqExamples/RabbitMqExamples.sln"
