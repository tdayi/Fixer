using Fixer.Core.Configuration;
using System.Collections.Generic;

namespace Fixer.Hosting.Extension
{
    class FixerConfiguration : IFixerConfiguration
    {
        public List<FixerTask> Tasks { get; set; }
    }
}
