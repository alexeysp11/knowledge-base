using LibGit2Sharp;
using WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample;
using WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample.Models;
using WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample.UseCases;

/// <summary>
/// The class that is responsible for initializing this example.
/// </summary>
public class StartupInstance : IStartupInstance
{
    private readonly LibGit2SharpExampleSettings _settings;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public StartupInstance(LibGit2SharpExampleSettings settings)
    {
        _settings = settings;
    }

    /// <summary>
    /// Entry method of the application.
    /// </summary>
    public void Run()
    {
        Console.WriteLine("LibGit2Sharp example");

        // Git user.
        var gitUser = _settings.GitUser;
        if (gitUser == null)
        {
            throw new System.Exception("Git user is not specified in the config file");
        }

        // Create the committer's signature.
        Signature author = new Signature(gitUser.Username, gitUser.Email, DateTime.Now);
        Signature committer = author;

        // Push options.
        var pushOptions = new PushOptions();
        pushOptions.CredentialsProvider = (url, user, cred) =>
            new UsernamePasswordCredentials { Username = gitUser.Username, Password = gitUser.Password };

        // Operations in the repository.
        GeneralUseCasesConsole.CloneRepo(_settings.RepositoryUrl, _settings.RepositoryPath);
        GeneralUseCasesConsole.StageChanges(_settings.RepositoryPath, _settings.FilePathsStage);
        GeneralUseCasesConsole.CommitChanges(_settings.RepositoryPath, _settings.CommitMessage, author, committer);
        GeneralUseCasesConsole.PushChanges(_settings.RepositoryPath, _settings.PushRefSpec, pushOptions);
        GeneralUseCasesConsole.Checkout(_settings.RepositoryPath, _settings.CheckoutBranchName);
        GeneralUseCasesConsole.FetchChanges(_settings.RepositoryPath, _settings.OriginName);
        GeneralUseCasesConsole.PullChanges(_settings.RepositoryPath, _settings.OriginName, _settings.PullBranchName);
        GeneralUseCasesConsole.GetLocalBranches(_settings.RepositoryPath);
        GeneralUseCasesConsole.CompareBranchWithHead(_settings.RepositoryPath, "origin/main");
    }
}