using System.Collections;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    public float duration = 180f; // Duration in seconds (3 minutes)
    public float startRotationX = 0f; // Starting x rotation
    public float endRotationX = 90f; // Ending x rotation

    private Light lightSource;

    void Start()
    {
        lightSource = GetComponent<Light>();
        if (lightSource == null)
        {
            Debug.LogError("Light component not found on the GameObject.");
        }
        else
        {
            StartCoroutine(RotateLight());
        }
    }

    //Permet la rotation de la lumière pour le système jour/nuit
    IEnumerator RotateLight()
    {
        float elapsedTime = 0f;

        Quaternion startRotation = Quaternion.Euler(startRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Quaternion endRotation = Quaternion.Euler(endRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the light reaches the final rotation
        transform.rotation = endRotation;
        Debug.Log("Light has reached the end rotation.");
    }
}
