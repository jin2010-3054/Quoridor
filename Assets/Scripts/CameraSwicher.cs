using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwicher : MonoBehaviour
{
    public Transform tr1;
    public Transform tr2;


    public void switchCamera(int camera)
    {
        Transform dest = (camera == 1) ? tr1 : tr2;
        StartCoroutine(move(transform.position, dest.position));
        StartCoroutine(rotate(transform.rotation, dest.rotation));
    }

    IEnumerator move(Vector3 start, Vector3 end)
    {
        float totalMoveTime = 1f;
        float currentMoveTime = 0f;
        while (Vector3.Distance(transform.position, end) > 0)
        {
            currentMoveTime += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, currentMoveTime / totalMoveTime);
            yield return null;
        }
    }

    IEnumerator rotate(Quaternion start, Quaternion end)
    {
        float totalMoveTime = 1f;
        float currentMoveTime = 0f;
        while (Quaternion.Angle(transform.rotation, end) != 0)
        {
            currentMoveTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(start, end, currentMoveTime / totalMoveTime);
            yield return null;
        }
    }
}