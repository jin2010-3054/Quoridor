using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriere : MonoBehaviour
{
    public GameObject GM;
    private GameManager GMSc;

    private Plane mDragPlane;
    private Vector3 initialPosition;
    private List<Collider> colliders = new List<Collider>();

    private void Start()
    {
        GMSc = GM.GetComponent<GameManager>();
    }

    private void Update()
    {

    }


    private void OnMouseDown()
    {
        int selectedPlayer = GMSc.selectedPlayer;

        // プレイヤー1のターンのときは barrier_p1 しか動かせない
        if ((selectedPlayer == 1 && tag != "b1") ||
            (selectedPlayer == 2 && tag != "b2"))
        {
            Debug.Log("このバリアは今のプレイヤーは動かせないよ！");
            transform.position = initialPosition;
            return;
        }

        initialPosition = transform.position;
        mDragPlane = new Plane(Vector3.up, transform.position);
    }

    private void OnMouseDrag()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float planDist;
        mDragPlane.Raycast(camRay, out planDist);
        transform.position = camRay.GetPoint(planDist);
    }

    private void OnMouseUp()
    {
        if (colliders.Count != 0)
        {
            Collider dropCollider = null;
            float dist = Mathf.Infinity;
            foreach (Collider collider in colliders)
            {
                if (Vector3.Distance(transform.position, collider.transform.position) < dist)
                {
                    dist = Vector3.Distance(transform.position, collider.transform.position);
                    dropCollider = collider;
                }
            }
            transform.position = dropCollider.transform.position;
            GMSc.SwitchPlayer();
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "barcen")
        {
            colliders.Add(other);
        }
        if (other.tag == "rotater")
        {
            transform.Rotate(0, 90, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (colliders.Count != 0 && colliders.Contains(other))
        {
            colliders.Remove(other);
        }
    }
}