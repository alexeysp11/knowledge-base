using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace Debugger.ConsoleDebugger;

internal static class RunningHelper
{
    internal static void RunExternalProject(string csprojPath)
    {
        Console.WriteLine($"csproj: {csprojPath}");

        // 1. Compile csproj file at runtime.
        Assembly assembly = CompileProject(csprojPath);

        // 2. Load the compiled binary and start it at runtime.
        if (assembly != null)
        {
            ExecuteAssembly(assembly);
        }
        else
        {
            Console.WriteLine("Compilation failed. Cannot execute assembly.");
        }
    }

    private static Assembly CompileProject(string csprojPath)
    {
        try
        {
            // Find all C# files in the project directory.
            string projectDirectory = Path.GetDirectoryName(csprojPath);
            List<string> sourceFiles = Directory.GetFiles(projectDirectory, "*.cs", SearchOption.AllDirectories).ToList();

            if (!sourceFiles.Any())
            {
                Console.WriteLine("No C# source files found in the project.");
                return null;
            }

            // Read the source code from the files.
            List<SyntaxTree> syntaxTrees = new List<SyntaxTree>();
            foreach (string sourceFile in sourceFiles)
            {
                string sourceCode = File.ReadAllText(sourceFile);
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode, new CSharpParseOptions(), sourceFile); // Keep the source file path for diagnostics
                syntaxTrees.Add(syntaxTree);
            }

            // Create compilation options.  Important to set OutputKind to Exe if the project is a console application.
            CSharpCompilationOptions options = new CSharpCompilationOptions(
                OutputKind.ConsoleApplication,
                optimizationLevel: OptimizationLevel.Debug,
                assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default);

            // Extract assembly name from the csproj file (basic implementation).  This is a *VERY* simplified csproj parsing.  Consider using actual XML parsing for robustness.
            string assemblyName = Path.GetFileNameWithoutExtension(csprojPath);
            // try
            // {
            //     string csprojContent = File.ReadAllText(csprojPath);
            //     string assemblyNameTag = "<AssemblyName>";
            //     int assemblyNameStart = csprojContent.IndexOf(assemblyNameTag);
            //     if (assemblyNameStart > 0)
            //     {
            //         assemblyNameStart += assemblyNameTag.Length;
            //         int assemblyNameEnd = csprojContent.IndexOf("</AssemblyName>", assemblyNameStart);
            //         if (assemblyNameEnd > 0)
            //         {
            //             assemblyName = csprojContent.Substring(assemblyNameStart, assemblyNameEnd - assemblyNameStart).Trim();
            //         }
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Warning: Could not extract assembly name from csproj. Using default name: {assemblyName}. Error: {ex.Message}");
            // }

            // Create compilation
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: syntaxTrees,
                references: GetDefaultReferences(),
                options: options);

            // Define the output path
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{assemblyName}.dll");

            // Emit the assembly
            EmitResult result = compilation.Emit(outputPath);

            if (!result.Success)
            {
                Console.WriteLine("Compilation failed:");
                foreach (Diagnostic diagnostic in result.Diagnostics)
                {
                    Console.WriteLine(diagnostic); // Print the diagnostic information
                }
                return null;
            }

            Console.WriteLine($"Compilation successful. Assembly saved to: {outputPath}");
            return Assembly.LoadFrom(outputPath); // Load the compiled assembly

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during compilation: {ex}");
            return null;
        }
    }

    private static void ExecuteAssembly(Assembly assembly)
    {
        try
        {
            // Find the entry point (Main method)
            MethodInfo entryPoint = assembly.EntryPoint;

            if (entryPoint == null)
            {
                Console.WriteLine("Entry point (Main method) not found in the assembly.");
                return;
            }

            // Execute the Main method
            Console.WriteLine("Executing assembly...");
            entryPoint.Invoke(null, null); // Assuming Main method is static and takes no arguments
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during execution: {ex}");
        }
    }

    private static List<MetadataReference> GetDefaultReferences()
    {
        //Add references to commonly used assemblies
        var references = new List<MetadataReference>
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(System.Runtime.AssemblyTargetedPatchBandAttribute).Assembly.Location), // System.Runtime
            MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location) // System.Linq
        };

        //Add references for any other frequently used assemblies
        //e.g., System.Collections, System.IO, etc.

        return references;
    }
}
