Console.WriteLine("docfx example");

var autoDocsStaticFiles = "C:\\Users\\User\\Documents\\auto-docs\\knowledge-base\\AutoDocsExamples\\docfx.json";

Console.WriteLine("docfx example: yml");
await Docfx.Dotnet.DotnetApiCatalog.GenerateManagedReferenceYamlFiles(autoDocsStaticFiles);

Console.WriteLine("docfx example: build");
await Docfx.Docset.Build(autoDocsStaticFiles);

Console.WriteLine("docfx example: pdf");
await Docfx.Docset.Pdf(autoDocsStaticFiles);
