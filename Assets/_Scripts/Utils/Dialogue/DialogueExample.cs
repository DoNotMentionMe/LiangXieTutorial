using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HYH
{
    public class DialogueExample : MonoBehaviour
    {
        private Queue<String> mSentenceQueue = new Queue<string>();

        private void Awake()
        {
            mSentenceQueue.Enqueue("你好~");
            mSentenceQueue.Enqueue("我是黄耀辉");
            mSentenceQueue.Enqueue("我贼帅");
            mSentenceQueue.Enqueue("你知道吗");
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && mSentenceQueue.Count > 0)
            {
                print(mSentenceQueue.Dequeue());
            }
        }
    }
}