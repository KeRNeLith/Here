using System.IO;
using Sharpmake;

[module: Sharpmake.Include("common.sharpmake.cs")]
[module: Sharpmake.Include("unittests.sharpmake.cs")]

namespace Here.Sharpmake
{
    [Generate]
    internal class Maybe : HereBaseProject
    {
        public Maybe()
        {
        }

        [Configure]
        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
        }
    }

    // Tests
    [Generate]
    internal class Maybe_Tests : UnitTests<Maybe>
    {
        public Maybe_Tests()
        {
        }
    }
}