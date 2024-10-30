using LibGit2Sharp;
using LibGit2Sharp.Core;
using LibGit2Sharp.Handlers;

Console.WriteLine("LibGit2Sharp example");

string pathToRepo = @"C:\Users\User\Documents\proj\delivery-service-csharp";
string logMessage = "";
using (var repo = new Repository(pathToRepo))
{
    // Start interacting with git.
    System.Console.WriteLine($"Imitate git pull");

    // Get the origin remote.
    var remote = repo.Network.Remotes["origin"];
    var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);

    // Fetch changes.
    System.Console.WriteLine($"1. git fetch");
    Commands.Fetch(repo, remote.Name, refSpecs, null, logMessage);

    // Get the last commit, its author and message.
    System.Console.WriteLine($"2. Getting the last commit");
    Commit branchTip = repo.Branches["main"].Tip;
    Signature branchTipAuthor = branchTip.Author;
    System.Console.WriteLine($"{branchTipAuthor.When} | {branchTip.MessageShort}");

    // Merge changes locally.
    System.Console.WriteLine($"3. git merge");
    MergeResult mergeResult = repo.Merge(branchTip, branchTipAuthor);
    System.Console.WriteLine($"merge status: {mergeResult.Status}");
}
Console.WriteLine(logMessage);
