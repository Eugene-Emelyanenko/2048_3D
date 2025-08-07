using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameEntity
{
    int Value { get; }
    void Init(int value, bool isKinematic = true);
    event Action OnInitialized;

    void DestroyEntity();
}
