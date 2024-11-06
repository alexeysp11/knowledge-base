using LibGit2Sharp;

namespace WorkflowLib.Examples.LibGit2SharpExample.UseCases;

public class GeneralUseCasesConsole
{
    /// <summary>
    /// 
    /// </summary>
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

    public static void FetchChanges(string pathToRepo, string remoteName)
    {
        using (var repo = new Repository(pathToRepo))
        {
            GeneralGitOperations.FetchChanges(repo, remoteName);
        }
    }

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