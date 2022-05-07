using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPass : MonoBehaviour
{
    [SerializeField] Text 通关提示;
    [SerializeField] UnityEvent OnFinish;
    private void OnTriggerEnter2D(Collider2D col)
    {
        通关提示.enabled = true;
        OnFinish?.Invoke();
        Invoke(nameof(LoadCurrentScene), 2f);
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
