using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IGameEntity), typeof(IMovable))]
public class MergeHandler : MonoBehaviour, IMergable
{
    [SerializeField] private float minImpulse = 1.5f;

    private IGameEntity selfEntity; 
    private IMovable movable;

    public event Action<int, Vector3> OnMerged;
    public event Action OnDestroyed;

    public bool IsMerging { get; set; }

    private void Awake()
    {
        selfEntity = GetComponent<IGameEntity>();
        movable = GetComponent<IMovable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out IGameEntity otherEntity)) return;
        if (!collision.gameObject.TryGetComponent(out IMergable otherMergeHandler)) return;
        if (!collision.gameObject.TryGetComponent(out IMovable otherMovable)) return;
        if (otherMergeHandler.IsMerging || IsMerging) return;

        float impactForce = collision.impulse.magnitude;
        if (impactForce < minImpulse) return;

        if (selfEntity.Value != otherEntity.Value) return;

        MergeWith(otherEntity, otherMergeHandler, otherMovable);
    }

    private void MergeWith(IGameEntity otherEntity, IMergable otherMergeHandler, IMovable otherMovable)
    {
        IsMerging = true;
        otherMergeHandler.IsMerging = true;

        otherEntity.DestroyEntity();

        int newValue = selfEntity.Value * 2;
        Vector3 mergePosition = (movable.GetPos() + otherMovable.GetPos()) / 2f;

        OnMerged?.Invoke(newValue, mergePosition);

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
