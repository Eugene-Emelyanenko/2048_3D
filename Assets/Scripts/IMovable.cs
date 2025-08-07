using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    Vector3 GetPos();
    void SetPos(Vector3 newPos);
    void AddForce(Vector3 direction, float impluse);

    void AddTorque(Vector3 torque);
}
