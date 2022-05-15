using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class AudioSystem : MonoBehaviour
    {
        public AudioSource UIFeedback;

        private static AudioSystem mAudioSystem;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            mAudioSystem = this;
        }

        private void OnDestroy()
        {
            mAudioSystem = null;
        }

        public static void PlayerUIFeedback()
        {
            mAudioSystem?.UIFeedback?.Play();
        }
    }
}
