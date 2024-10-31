using Build1.PostMVC.Core.MVCS.Commands;
using Build1.PostMVC.Unity.Services.Impl;

namespace Build1.PostMVC.Unity.Services.Commands
{
    public sealed class UnityServicesInitializeAsyncCommand : Command
    {
        public override void Execute()
        {
            if (!UnityServicesAdapter.Initialized && !UnityServicesAdapter.Initializing)
                UnityServicesAdapter.Initialize();
        }
    }
}