using System.IO;
using Sharpmake;
using Here.Sharpmake.Extern;

[module: Sharpmake.Include("externs.sharpmake.cs")]

namespace Here.Sharpmake
{
    // Target settings
    internal static class Settings
    {
        public static ITarget[] GetDefaultTargets()
        {
            return new ITarget[]
            {
                new Target(
                    // Target that build for both 32 and 64-bit
                    Platform.anycpu,
                    // Visual Studio 2017
                    DevEnv.vs2017,
                    // Debug & release configuration
                    Optimization.Debug | Optimization.Release,
                    // Target framework
                    framework: DotNetFramework.v4_5)
            };
        }
    }

    // Base class for C# projects
    internal class HereBaseProject : CSharpProject
    {
        public string HereRootPath = Path.Combine(@"[project.SharpmakeCsPath]", "..");

        public HereBaseProject()
        {
            Name = GetType().Name.Replace("_", ".");
            RootPath = Path.Combine(@"[project.HereRootPath]", "src");
            AddTargets(Settings.GetDefaultTargets());

            // Sources
            SourceRootPath = Path.Combine(@"[project.RootPath]", @"[project.Name]");
        }

        public virtual void ConfigureAll(Configuration conf, Target target)
        {
            // Project settings
            conf.ProjectFileName = "[project.Name]";
            conf.ProjectPath = Path.Combine(@"[project.RootPath]", @"[project.Name]");
            conf.TargetPath = Path.Combine(@"[project.HereRootPath]", "bin", @"[project.Name]", @"[target.Optimization]");

            conf.Options.Add(Options.CSharp.LanguageVersion.CSharp7);

            // References
            conf.ReferencesByName.Add("System");
            conf.ReferencesByName.Add("System.Core");

            conf.ReferencesByNuGetPackage.Add(Externs.JetBrainsAnnotations, Externs.JetBrainsAnnotationsVersion);
        }
    }
}