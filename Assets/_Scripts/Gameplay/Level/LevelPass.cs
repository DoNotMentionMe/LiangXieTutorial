using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPass : MonoBehaviour
{
    [SerializeField] bool ResetPlayer2OriginPoint = false;
    [SerializeField] UnityEvent OnLevelPass;
    [SerializeField] UnityEvent OnLevelPassDeleyFinish;
    [SerializeField] Text 通关提示;

    private void OnTriggerEnter2D(Collider2D col)
    {
        通关提示.enabled = true;
        OnLevelPass?.Invoke();
        Invoke(nameof(OnFinishExecute), 0.469f);
    }

    private void OnFinishExecute()
    {
        if(ResetPlayer2OriginPoint)
        {
            ResetPlayerPos();
        }
        else
        {
            LoadCurrentScene();
        }

        通关提示.enabled = false;
        OnLevelPassDeleyFinish?.Invoke();
    }

    private void ResetPlayerPos()
    {
        GameObject.FindWithTag("Player").transform.position = Vector3.zero;
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
