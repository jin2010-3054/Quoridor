using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwicher : MonoBehaviour
{
    public Transform tr1;
    public Transform tr2;

    [HideInInspector] public bool isMoving = false; // ’Ç‰Á

    public void switchCamera(int camera)
    {
        Transform dest = (camera == 1) ? tr1 : tr2;
        StartCoroutine(MoveAndRotate(dest));
    }

    private IEnumerator MoveAndRotate(Transform dest)
    {
        isMoving = true;

        // ˆÚ“®
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float totalMoveTime = 1f;
        float t = 0f;

        while (t < totalMoveTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, dest.position, t / totalMoveTime);
            transform.rotation = Quaternion.Lerp(startRot, dest.rotation, t / totalMoveTime);
            yield return null;
        }

        transform.position = dest.position;
        transform.rotation = dest.rotation;
        isMoving = false;
    }
}