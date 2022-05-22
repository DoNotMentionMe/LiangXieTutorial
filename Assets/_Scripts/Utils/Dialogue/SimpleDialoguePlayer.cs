using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HYH
{
    /// <summary>
    /// 简单对话播放（气泡对话）
    /// </summary>
    public class SimpleDialoguePlayer : MonoBehaviour
    {
        public enum DialogueStates
        {
            NotStart,
            Started,
            Finished,
        }

        public DialogueStates State = DialogueStates.NotStart;

        Coroutine PlayTextCoroutine;

        /// <summary>
        /// Inspector配置对话
        /// </summary>
        public List<string> Sentences = new List<string>();

        //提供三个状态的事件回调
        public UnityEvent OnNotStart = new UnityEvent();
        public UnityEvent OnDialogueStart = new UnityEvent();
        public UnityEvent OnDialogueFinish = new UnityEvent();

        /// <summary>
        /// 对外提供设置文本的方法
        /// </summary>
        /// <typeparam name="string">
        /// 要被设置的文本内容
        /// </typeparam>
        /// <returns></returns>
        public UnityEvent<string> OnPlayText = new UnityEvent<string>();

        /// <summary>
        /// 对外提供一个开始播放对话的方法
        /// </summary>
        public void StartDialogue()
        {
            if (State == DialogueStates.NotStart)
            {
                State = DialogueStates.Started;
                if (PlayTextCoroutine == null)
                    PlayTextCoroutine = StartCoroutine(StartPlayText(mSentenceQueue.Dequeue()));
                OnDialogueStart?.Invoke();
            }

        }

        /// <summary>
        /// 对外提供继续对话的方法
        /// </summary>
        public void ContinueDialogue()
        {
            if (State == DialogueStates.Started)
            {
                if (mSentenceQueue.Count > 0)
                {
                    if (PlayTextCoroutine == null)
                        PlayTextCoroutine = StartCoroutine(StartPlayText(mSentenceQueue.Dequeue()));
                }
                else
                {
                    if (PlayTextCoroutine == null)
                        PlayTextCoroutine = StartCoroutine(StartPlayText(string.Empty));
                    State = DialogueStates.Finished;
                    OnDialogueFinish?.Invoke();
                }
            }
        }

        /// <summary>
        /// 对外提供一个重置的方法
        /// </summary>
        public void Reset()
        {
            State = DialogueStates.NotStart;
            mSentenceQueue = new Queue<string>(Sentences);
            OnNotStart?.Invoke();
        }

        /// <summary>
        /// 对话数据结构
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        private Queue<string> mSentenceQueue = new Queue<string>();

        private void Awake()
        {
            //数组转换成队列
            mSentenceQueue = new Queue<string>(Sentences);
        }

        private void Start()
        {
            OnNotStart?.Invoke();
        }

        private string mCurrentSentence = string.Empty;

        IEnumerator StartPlayText(string sentence)
        {
            mCurrentSentence = sentence;

            var sentenceToPlay = string.Empty;

            var length = mCurrentSentence.Length;

            for (int i = 0; i <= length; i++)
            {
                yield return new WaitForSeconds(.1f);

                sentenceToPlay = mCurrentSentence.Substring(0, i);

                OnPlayText?.Invoke(sentenceToPlay);
            }

            PlayTextCoroutine = null;
        }
    }
}