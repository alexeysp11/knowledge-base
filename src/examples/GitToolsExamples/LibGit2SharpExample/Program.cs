using WorkflowLib.Examples.LibGit2SharpExample.UseCases;

Console.WriteLine("LibGit2Sharp example");

GeneralUseCases.Checkout(@"C:\Users\User\Documents\proj\workflow-lib", "main");
GeneralUseCases.GetLocalBranches(@"C:\Users\User\Documents\proj\workflow-lib");
// GeneralUseCases.PullChanges(@"C:\Users\User\Documents\proj\workflow-lib", "origin", "main");
