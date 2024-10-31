using System;
using System.Threading.Tasks;
using Build1.PostMVC.Unity.App.Modules.Async;
using Unity.Services.Core;

namespace Build1.PostMVC.Unity.Services.Impl
{
    internal sealed class UnityServicesAdapter
    {
        public static bool Initialized  => UnityServices.State == ServicesInitializationState.Initialized;
        public static bool Initializing => UnityServices.State == ServicesInitializationState.Initializing;

        public static event Action            OnInitialized;
        public static event Action<Exception> OnError;

        public static void Initialize()
        {
            try
            {
                if (UnityServices.State == ServicesInitializationState.Uninitialized)
                    UnityServices.InitializeAsync().Resolve(OnInitializedHandler);
            }
            catch (Exception exception)
            {
                OnError?.Invoke(exception);
            }
        }

        private static void OnInitializedHandler(Task task)
        {
            if (task.IsCanceled)
            {
                OnError?.Invoke(new Exception("Game Services async initialization cancelled."));
                return;
            }

            if (task.Exception != null)
            {
                OnError?.Invoke(task.Exception);
                return;
            }
            
            OnInitialized?.Invoke();
        }
    }
}