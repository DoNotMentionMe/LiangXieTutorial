using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HYH
{
    public class SaveHideStateController : MonoBehaviour
    {
        private void Awake()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            var objName = name;

            var saveKey = sceneName + objName;

            var saveSystem = ApplePlatformer2D.Interface.GetSystem<ISaveSystem>();

            if(saveSystem.HasSavedKey(saveKey))
            {
                gameObject.SetActive(false);
            }
        }

        public void SaveHideState()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            var objName = name;

            var saveKey = sceneName + objName;
            ApplePlatformer2D.Interface.GetSystem<ISaveSystem>().AddSavedKey(saveKey);
        }
    }
    
}
