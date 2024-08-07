using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable 
{
    void OnCollected();

    bool isCollectable
    {
        get;

        set;
    }
}
