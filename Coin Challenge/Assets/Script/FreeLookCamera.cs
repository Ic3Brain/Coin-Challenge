using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FreeLookCamera : MonoBehaviour
{   
    public CinemachineFreeLook freeLook;
   public void Lock()
    {
        freeLook.m_YAxis.m_MaxSpeed = 0;
        freeLook.m_XAxis.m_MaxSpeed = 0;
    }

    public void Unlock()
    {
        freeLook.m_YAxis.m_MaxSpeed = 5;
        freeLook.m_XAxis.m_MaxSpeed = 300;
    }
}
