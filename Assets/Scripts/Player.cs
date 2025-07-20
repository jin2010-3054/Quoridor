using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public List<Box> possibleBoxes;
    [HideInInspector] public List<Box> previeusPossibleBoxes;

    public Player opponent;

    public void MoveToPosition(Transform dest)
    {
        transform.position = dest.position + transform.up;
    }
    public List<Box> GetPrevieusPossibleBoxes()
    {
        return previeusPossibleBoxes;
    }

    public List<Box> GetPossibleBoxes()
    {

        previeusPossibleBoxes = possibleBoxes;
        possibleBoxes.Clear();

        RaycastHit hitFront;
        RaycastHit hitBack;
        RaycastHit hitRight;
        RaycastHit hitLeft;

        if (Physics.Raycast(transform.position - transform.up + transform.forward * 0.06f, transform.TransformDirection(Vector3.forward), out hitFront, 1f))
        {
            if ((hitFront.transform.tag == "b1")||(hitFront.transform.tag =="b2"))
            {

            }
            else if ((hitFront.transform.tag == "box" || hitFront.transform.tag == "finalBox") &&
                Vector3.Distance(hitFront.transform.position, opponent.transform.position) > 1f)
            {
                possibleBoxes.Add(hitFront.transform.GetComponent<Box>());
            }
        }

        if (Physics.Raycast(transform.position - transform.up - transform.forward * 0.06f, transform.TransformDirection(-Vector3.forward), out hitBack, 1f))
        {
            if ((hitFront.transform.tag == "b1") || (hitFront.transform.tag == "b2"))
            {

            }
            else if (hitBack.transform.tag == "box" &&
                Vector3.Distance(hitBack.transform.position, opponent.transform.position) > 1f)
            {
                possibleBoxes.Add(hitBack.transform.GetComponent<Box>());
            }
        }

        if (Physics.Raycast(transform.position - transform.up + transform.right * 0.06f, transform.TransformDirection(Vector3.right), out hitRight, 1f))
        {
            if ((hitFront.transform.tag == "b1") || (hitFront.transform.tag == "b2"))
            {

            }
            else if (hitRight.transform.tag == "box" &&
                Vector3.Distance(hitRight.transform.position, opponent.transform.position) > 1f)
            {
                possibleBoxes.Add(hitRight.transform.GetComponent<Box>());
            }
        }

        if (Physics.Raycast(transform.position - transform.up - transform.right * 0.06f, transform.TransformDirection(-Vector3.right), out hitLeft, 1f))
        {
            if ((hitFront.transform.tag == "b1") || (hitFront.transform.tag == "b2"))
            {

            }
            else if (hitLeft.transform.tag == "box" &&
                Vector3.Distance(hitLeft.transform.position, opponent.transform.position) > 1f)
            {
                possibleBoxes.Add(hitLeft.transform.GetComponent<Box>());
            }
        }

        return possibleBoxes;
    }
}
