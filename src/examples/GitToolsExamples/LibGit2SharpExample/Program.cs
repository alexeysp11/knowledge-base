using LibGit2Sharp;
using WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample.UseCases;

Console.WriteLine("LibGit2Sharp example");

// Paths.
var knowledgeBaseUrl = "https://github.com/alexeysp11/knowledge-base/";
var knowledgeBasePath = @"C:\Users\User\Documents\proj\git-examples\knowledge-base";

// Create the committer's signature.
// Signature author = new Signature("", "", DateTime.Now);
// Signature committer = author;

// Push options.
// var pushOptions = new PushOptions();
// pushOptions.CredentialsProvider = (url, user, cred) => 
//     new UsernamePasswordCredentials { Username = "", Password = "" };

// Operations in the repository.
// GeneralUseCasesConsole.CloneRepo(knowledgeBaseUrl, knowledgeBasePath);
// GeneralUseCasesConsole.StageChanges(knowledgeBasePath, new string[] { "docs/git-test/csharp-git-test.txt" });
// GeneralUseCasesConsole.CommitChanges(knowledgeBasePath, "message for the test commit", author, committer);
// GeneralUseCasesConsole.PushChanges(knowledgeBasePath, @"refs/heads/csharp-git-test-1", pushOptions);
// GeneralUseCasesConsole.Checkout(knowledgeBasePath, "main");
GeneralUseCasesConsole.FetchChanges(knowledgeBasePath, "origin");
// GeneralUseCasesConsole.PullChanges(knowledgeBasePath, "origin", "main");
// GeneralUseCasesConsole.GetLocalBranches(knowledgeBasePath);
GeneralUseCasesConsole.CompareBranchWithHead(knowledgeBasePath, "origin/main");
