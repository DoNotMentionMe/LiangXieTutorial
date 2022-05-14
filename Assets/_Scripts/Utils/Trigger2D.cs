using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger2D : MonoBehaviour
{
    public bool Triggered = false;
    [SerializeField] LayerMask layers;
    public UnityEvent OnTriggerEnter = new UnityEvent();
    public UnityEvent OnTriggerExit = new UnityEvent();
    public UnityEvent<Collider2D> OnTriggerEnterWithCollider = new UnityEvent<Collider2D>();
    public UnityEvent<Collider2D> OnTriggerExitWithCollider = new UnityEvent<Collider2D>();

    private HashSet<Collider2D> mCollider2Ds = new HashSet<Collider2D>();

    public void Reset()
    {
        Triggered = false;
        mCollider2Ds.Clear();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!LayerMaskUtility.Contains(layers, col.gameObject.layer)) return;

        mCollider2Ds.Add(col);

        if(!Triggered && mCollider2Ds.Count > 0)
        {
            Triggered = true;
            OnTriggerEnter?.Invoke();
            OnTriggerEnterWithCollider?.Invoke(col);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(!LayerMaskUtility.Contains(layers, col.gameObject.layer)) return;

        mCollider2Ds.Remove(col);

        if(Triggered && mCollider2Ds.Count == 0)
        {
            Triggered = false;
            OnTriggerExit?.Invoke();
            OnTriggerExitWithCollider?.Invoke(col);
        }
    }
}
