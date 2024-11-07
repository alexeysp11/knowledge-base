using LibGit2Sharp;

namespace WorkflowLib.Examples.GitToolsExamples.LibGit2SharpExample.UseCases;

public class GeneralUseCasesConsole
{
    /// <summary>
    /// Get the list of remote repositories.
    /// </summary>
    /// <param name="pathToRepo">Path to the local repository</param>
    public static void GetRemotes(string pathToRepo)
    {
        using (var repo = new Repository(pathToRepo))
        {
            var remotes = GeneralGitOperations.GetRemotes(repo);
            foreach (var r in remotes)
            {
                System.Console.WriteLine($"remote: {r.Name} \t url: {r.Url}");
            }
        }
    }

    /// <summary>
    /// Get the list of branches.
    /// </summary>
    /// <param name="pathToRepo">Path to the local repository</param>
    public static void GetBranches(string pathToRepo)
    {
        using (var repo = new Repository(pathToRepo))
        {
            var branches = GeneralGitOperations.GetBranches(repo);
            foreach (var b in branches)
            {
                System.Console.WriteLine($"branch: {b.CanonicalName} \t FriendlyName: {b.FriendlyName} \t remote: {b.IsRemote} \t head: {b.IsCurrentRepositoryHead}");
            }
        }
    }

    /// <summary>
    /// Fetch changes from the remote repository.
    /// </summary>
    /// <param name="pathToRepo">Path to the local repository</param>
    /// <param name="remoteName">Name of the remote repository</param>
    public static void FetchChanges(string pathToRepo, string remoteName)
    {
        using (var repo = new Repository(pathToRepo))
        {
            GeneralGitOperations.FetchChanges(repo, remoteName);
        }
    }

    /// <summary>
    /// Compare the specified branch with head.
    /// </summary>
    /// <param name="pathToRepo">Path to the local repository</param>
    /// <param name="branchName">Name of the branch that is going to be compared with head</param>
    public static void CompareBranchWithHead(string pathToRepo, string branchName)
    {
        using (var repo = new Repository(pathToRepo))
        {
            var patch = GeneralGitOperations.CompareBranchWithHead(repo, branchName);
            foreach (var ptc in patch)
            {
                Console.WriteLine(ptc.Status + " : " + ptc.Path);
            }
        }
    }
}