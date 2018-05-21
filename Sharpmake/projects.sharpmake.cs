using System.IO;
using Sharpmake;

[module: Sharpmake.Include("common.sharpmake.cs")]

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

    [Generate]
    internal class SampleMaybe : HereBaseProject
    {
        public SampleMaybe()
        {
        }

        [Configure]
        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.Output = Configuration.OutputType.DotNetConsoleApp;

            conf.AddPrivateDependency<Maybe>(target);
        }
    }
}