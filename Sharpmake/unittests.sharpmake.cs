using System.IO;
using Sharpmake;
using Here.Sharpmake.Extern;

[module: Sharpmake.Include("common.sharpmake.cs")]
[module: Sharpmake.Include("externs.sharpmake.cs")]

namespace Here.Sharpmake
{
    // Unit Tests
    internal abstract class UnitTests : HereBaseProject
    {
        public UnitTests()
        {
        }

        [Configure]
        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.Output = Configuration.OutputType.DotNetClassLibrary;

            conf.TargetPath = Path.Combine(@"[project.HereRootPath]", "bin", "Tests", @"[target.Optimization]");

            conf.ReferencesByNuGetPackage.Add(Externs.NUnit, Externs.NUnitVersion);
            conf.ReferencesByNuGetPackage.Add(Externs.NUnitTestAdapter, Externs.NUnitTestAdapterVersion);
        }
    }

    // Unit Tests
    internal abstract class UnitTests<TProject> : UnitTests
        where TProject : HereBaseProject, new()
    {
        public UnitTests()
        {
        }

        [Configure]
        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.AddPrivateDependency<TProject>(target);
        }
    }
}