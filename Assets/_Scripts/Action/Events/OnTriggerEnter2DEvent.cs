using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnter2DEvent : MonoBehaviour
{
    public enum TriggerType
    {
        Tags,
        Layers,
        TagsAndLayers,
        TagsOrLayers,
    }

    [SerializeField] TriggerType Type = TriggerType.Tags;

    [SerializeField] string[] Tags;
    [SerializeField] LayerMask Layers;

    public UnityEvent OnEnter = new UnityEvent();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Type == TriggerType.Tags || Type == TriggerType.TagsOrLayers)
        {
            if (Tags.Any(tag => col.CompareTag(tag)))
            {
                OnEnter?.Invoke();
            }
        }

        if (Type == TriggerType.Layers || Type == TriggerType.TagsOrLayers)
        {
            if (LayerMaskUtility.Contains(Layers, col.gameObject.layer))
            {
                OnEnter?.Invoke();
            }
        }

        if (Type == TriggerType.TagsAndLayers)
        {
            if (LayerMaskUtility.Contains(Layers, col.gameObject.layer)
                && Type == TriggerType.Tags || Type == TriggerType.TagsOrLayers)
            {
                OnEnter?.Invoke();
            }
        }
    }
}
