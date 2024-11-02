using LibGit2Sharp;
using LibGit2Sharp.Core;
using LibGit2Sharp.Handlers;

namespace WorkflowLib.Examples.LibGit2SharpExample.UseCases;

/// <summary>
/// This class includes general and atomic usage scenarios for the LibGit2Sharp library.
/// </summary>
public class GeneralUseCases
{
    /// <summary>
    /// Gets the list of branches.
    /// </summary>
    public static void GetBranches(string pathToRepo)
    {
        using (var repo = new Repository(pathToRepo))
        {
            var allRemotes = repo.Network.Remotes.ToList();
            foreach (var r in allRemotes)
            {
                System.Console.WriteLine($"remote: {r.Name}    url: {r.Url}");
            }
            var allBranches = repo.Branches.ToList();
            foreach (var b in allBranches)
            {
                System.Console.WriteLine($"branch: {b.CanonicalName}    remote: {b.IsRemote}    head: {b.IsCurrentRepositoryHead}");
            }
        }
    }

    /// <summary>
    /// Pulls changes from the remote repository.
    /// </summary>
    public static void PullChanges(string pathToRepo, string remoteName, string branchName)
    {
        System.Console.WriteLine($"Imitate git pull");

        string logMessage = "";
        using (var repo = new Repository(pathToRepo))
        {
            // Get the origin remote.
            var remote = repo.Network.Remotes[remoteName];
            if (remote == null)
            {
                throw new System.Exception($"The specified romote '{remoteName}' is not found");
            }
            var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);

            return;

            // Fetch changes.
            System.Console.WriteLine($"1. git fetch");
            Commands.Fetch(repo, remote.Name, refSpecs, null, logMessage);

            // Get the last commit, its author and message.
            System.Console.WriteLine($"2. Getting the last commit");
            Branch branch = repo.Branches[branchName];
            if (branch == null)
            {
                throw new System.Exception($"Branch '{branchName}' could not be found");
            }
            Commit branchTip = branch.Tip;
            if (branchTip == null)
            {
                throw new System.Exception($"The last commit of the branch '{branchName}' could not be found");
            }
            Signature branchTipAuthor = branchTip.Author;
            if (branchTip == null)
            {
                throw new System.Exception($"The author of the last commit of the branch '{branchName}' could not be found");
            }
            System.Console.WriteLine($"{branchTipAuthor.When} | {branchTip.MessageShort}");

            // Merge changes locally.
            System.Console.WriteLine($"3. git merge");
            MergeResult mergeResult = repo.Merge(branchTip, branchTipAuthor);
            System.Console.WriteLine($"merge status: {mergeResult.Status}");
        }
        Console.WriteLine(logMessage);
    }
}