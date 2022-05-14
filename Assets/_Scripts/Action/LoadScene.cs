using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string SceneName;

    public void Execute()
    {
        SceneManager.LoadScene(SceneName);
    }
}
