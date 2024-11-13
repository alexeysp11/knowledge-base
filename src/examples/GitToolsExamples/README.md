# LibGit2SharpExample

[English](README.md) | [Русский](README.ru.md)

## LibGit2SharpExample

`LibGit2SharpExample` is an example of using the [libgit2sharp](https://github.com/libgit2/libgit2sharp) library to communicate with a local and remote git repository.

Configuration example:
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
