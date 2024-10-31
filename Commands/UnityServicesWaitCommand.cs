using System;
using Build1.PostMVC.Core.MVCS.Commands;
using Build1.PostMVC.Unity.App.Modules.Logging;
using Build1.PostMVC.Unity.Services.Impl;

namespace Build1.PostMVC.Unity.Services.Commands
{
    public sealed class UnityServicesWaitCommand : Command
    {
        [Log(LogLevel.Warning)] public ILog Log { get; set; }
        
        public override void Execute()
        {
            if (UnityServicesAdapter.Initialized)
                return;

            Retain();
            
            UnityServicesAdapter.OnInitialized += OnInitialized;
            UnityServicesAdapter.OnError += OnError;
        }
        
        private void OnInitialized()
        {
            UnityServicesAdapter.OnInitialized -= OnInitialized;
            UnityServicesAdapter.OnError -= OnError;
            
            Release();
        }

        private void OnError(Exception exception)
        {
            Log.Error(exception);
            
            UnityServicesAdapter.OnInitialized -= OnInitialized;
            UnityServicesAdapter.OnError -= OnError;
            
            Release();
        }
    }
}