using System.Reflection;
using Build1.PostMVC.Core.MVCS.Injection;

namespace Build1.PostMVC.Unity.Services
{
    public sealed class ServicesModule : Module
    {
        [PostConstruct]
        public void PostConstruct()
        {
            // TODO: added for future
        }
    }
}