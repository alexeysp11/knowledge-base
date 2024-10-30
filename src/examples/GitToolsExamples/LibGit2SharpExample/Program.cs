using LibGit2Sharp;
using LibGit2Sharp.Core;
using LibGit2Sharp.Handlers;

Console.WriteLine("LibGit2Sharp example");

Identity identity = new Identity("alexeysp11", "alexeyspiridon1111@gmail.com");
Signature signature = new Signature(identity, new DateTimeOffset(2024, 10, 30, 19, 54, 00, TimeSpan.FromHours(2)));

string pathToRepo = @"C:\Users\User\Documents\proj\delivery-service-csharp";
string logMessage = "";
using (var repo = new Repository(pathToRepo))
{
    System.Console.WriteLine($"Imitate git pull");

    var remote = repo.Network.Remotes["origin"];
    var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);

    System.Console.WriteLine($"git fetch");
    Commands.Fetch(repo, remote.Name, refSpecs, null, logMessage);

    System.Console.WriteLine($"git merge");
    MergeResult mergeResult = repo.Merge(repo.Branches["main"].Tip, signature);
    System.Console.WriteLine($"merge status: {mergeResult.Status}");
}
Console.WriteLine(logMessage);
