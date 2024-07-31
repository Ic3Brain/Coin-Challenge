using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void OnDamage(float damage);

    void OnKill();

    bool IsAlive
    {
        get;
    }
}
