using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour, IGameEntity, IMovable
{
    [SerializeField] private Rigidbody rb;

    public event Action OnInitialized;

    public int Value { get; private set; }

    public void Init(int value = 2, bool isKinematic = true)
    {
        rb.isKinematic = isKinematic;
        Value = value;

        OnInitialized?.Invoke();
    }

    public void AddForce(Vector3 direction, float impluse)
    {
        rb.isKinematic = false;
        rb.AddForce(direction * impluse, ForceMode.Impulse);
    }   

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public void SetPos(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void DestroyEntity()
    {
        Destroy(gameObject);
    }

    public void AddTorque(Vector3 torque)
    {
        rb.AddTorque(torque, ForceMode.Impulse);
    }
}
