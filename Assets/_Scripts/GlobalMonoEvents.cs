using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace HYH
{
    public class GlobalMonoEvents : MonoBehaviour
    {
        public static EasyEvent OnApplicationQuitEvent = new EasyEvent();

        private void OnApplicationQuit()
        {
            OnApplicationQuitEvent?.Trigger();
        }

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            var gameObj = new GameObject("GlabalMonoEvents");
            gameObj.AddComponent<GlobalMonoEvents>();
            DontDestroyOnLoad(gameObj);
        }
    }
    
}
