using System.Collections.Generic;

namespace Fixer.Core.Configuration
{
    public interface IFixerConfiguration
    {
        List<FixerTask> Tasks { get; set; }
    }

    public class FixerTask
    {
        public string TaskName { get; set; }
        public string TimePattern { get; set; }
        public bool Status { get; set; }
        public bool IsRunImmediately { get; set; }
    }
}
