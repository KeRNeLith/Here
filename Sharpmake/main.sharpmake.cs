using System.IO;
using Sharpmake;

[module: Sharpmake.Include("projects.sharpmake.cs")]

namespace Here.Sharpmake
{
    internal static class SplashDiagramMain
    {
        [Main]
        public static void SharpmakeMain(Arguments arguments)
        {
            // Generate the solution
            arguments.Generate<HereSolution>();
        }
    }

    // Solution
    [Generate]
    internal class HereSolution : CSharpSolution
    {
        public HereSolution()
        {
            // The name of the solution.
            Name = "Here";
            AddTargets(Settings.GetDefaultTargets());
        }

        [Configure]
        public void ConfigureAll(Configuration conf, Target target)
        {
            conf.SolutionPath = Path.Combine(@"[solution.SharpmakeCsPath]", "..");
            conf.SolutionFileName = @"[solution.Name]";

            conf.AddProject<Maybe>(target);
            conf.AddProject<SampleMaybe>(target);
        }
    }
}