using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class SaveHideState : MonoBehaviour
    {
        public string SavedKey;

        public void Execute()
        {
            ApplePlatformer2D.Interface.GetSystem<ISaveSystem>().AddSavedKey(SavedKey);
        }
    }
}
