using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPass : MonoBehaviour
{
    [SerializeField] bool ResetPlayer2OriginPoint = false;
    [SerializeField] Text 通关提示;
    [SerializeField] UnityEvent OnLevelPass;
    [SerializeField] UnityEvent OnLevelPassDeleyFinish;

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

        通关提示.enabled = false;
        OnLevelPassDeleyFinish?.Invoke();
    }

    private void ResetPlayerPos()
    {
        var player = GameObject.FindWithTag("Player");
        player.transform.position = Vector2.zero;
        var Trigger2D = player.transform.Find("GroundCheck").GetComponent<Trigger2D>();
        Trigger2D.Reset();
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
