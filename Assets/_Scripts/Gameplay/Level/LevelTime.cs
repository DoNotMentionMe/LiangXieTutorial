using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTime : MonoBehaviour
{
    private Text mText;
    private float duration;

    private void Start()
    {
        mText = GetComponent<Text>();
        duration = 0;
        mText.text = duration.ToString();
    }

    private void Update()
    {
        duration += Time.deltaTime;

        if(Time.frameCount % 30 != 0) return;

        mText.text = ((int)duration).ToString();
    }
}
