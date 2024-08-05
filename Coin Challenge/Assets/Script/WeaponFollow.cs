using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFollow : MonoBehaviour
{
    public Transform target;

    //Prend la position et la rotation de l'arme 
    void Update()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
