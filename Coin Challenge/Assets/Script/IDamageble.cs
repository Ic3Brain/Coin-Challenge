using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void SetDamage(float damage);

    void OnKill();
}
