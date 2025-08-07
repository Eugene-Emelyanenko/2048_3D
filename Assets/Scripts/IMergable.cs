using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMergable
{
    bool IsMerging { get; set; }
    event Action<int, Vector3> OnMerged;
    event Action OnDestroyed;
}
