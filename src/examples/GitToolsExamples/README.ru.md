# LibGit2SharpExample

[English](README.md) | [Русский](README.ru.md)

## LibGit2SharpExample

`LibGit2SharpExample` является примером использования библиотеки [libgit2sharp](https://github.com/libgit2/libgit2sharp) для коммуникации с локальным и удаленным git-репозиторием.

Пример конфигурации:
```JSON
{
  "LibGit2SharpExampleSettings": {
    "RepositoryUrl": "https://github.com/alexeysp11/knowledge-base",
    "RepositoryPath": "C:\\path-to-repo\\knowledge-base",
    "OriginName": "origin",

    "CheckoutBranchName": "your-branch-name",
    "CompareBranchName": "main",
    "PullBranchName": "main",
    "PushRefSpec": "refs/heads/main",

    "CommitMessage": "COMMIT_MESSAGE",
    "FilePathsStage": [
      "FILE_PATH_1",
      "FILE_PATH_2"
    ],

    "GitUser": {
      "Username": "USERNAME",
      "Email": "EMAIL",
      "Password": "PASSWORD"
    }
  }
}
```
