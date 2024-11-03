using LibGit2Sharp;
using WorkflowLib.Examples.LibGit2SharpExample.UseCases;

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
// GeneralUseCases.CloneRepo(knowledgeBaseUrl, knowledgeBasePath);
// GeneralUseCases.StageChanges(knowledgeBasePath, new string[] { "docs/git-test/csharp-git-test.txt" });
// GeneralUseCases.CommitChanges(knowledgeBasePath, "message for the test commit", author, committer);
// GeneralUseCases.PushChanges(knowledgeBasePath, @"refs/heads/csharp-git-test-1", pushOptions);
// GeneralUseCases.Checkout(knowledgeBasePath, "main");
// GeneralUseCases.PullChanges(knowledgeBasePath, "origin", "main");
// GeneralUseCases.GetLocalBranches(knowledgeBasePath);
GeneralUseCases.CompareBranchWithHead(knowledgeBasePath, "origin/main");
