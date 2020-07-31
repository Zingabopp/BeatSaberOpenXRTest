using IPA;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace UnityXRTest
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger log { get; set; }

        [Init]
        public Plugin(IPALogger logger)
        {
            Instance = this;
            log = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            var go = new GameObject().AddComponent<ControllerTestBehaviour>();
            GameObject.DontDestroyOnLoad(go);
        }

        [OnExit]
        public void OnApplicationQuit()
        {

        }

    }
}
