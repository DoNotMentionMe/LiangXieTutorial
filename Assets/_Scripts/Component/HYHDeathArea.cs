using UnityEngine;
using HYH;
using UnityEngine.Events;


public class HYHDeathArea : MonoBehaviour
{
    [SerializeField] UnityEvent OnHit = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();

            playerModel.HP--;

            if (playerModel.HP <= 0)
            {
                ApplePlatformer2D.IsGameOver = true;
            }
            else
            {
                OnHit?.Invoke();
                //送回临近检查点
                CheckPoint.SendPlayerToLastCheckPoint();
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
