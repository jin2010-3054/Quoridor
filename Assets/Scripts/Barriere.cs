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

    private bool isPlaced = false; // 置かれたかどうか
    private void OnMouseDown()
    {
        int selectedPlayer = GMSc.selectedPlayer;

        // 自分のバリアじゃない or すでに置かれている場合は無効
        if ((selectedPlayer == 1 && tag != "b1") ||
            (selectedPlayer == 2 && tag != "b2") ||
            isPlaced)
        {
            return;
        }
        initialPosition = transform.position;
        mDragPlane = new Plane(Vector3.up, transform.position);
    }

    private void OnMouseDrag()
    {

        int selectedPlayer = GMSc.selectedPlayer;

        // 自分のターンじゃないバリアなら無効
        if ((selectedPlayer == 1 && tag != "b1") ||
            (selectedPlayer == 2 && tag != "b2") ||
            isPlaced)
        {
            return;
        }

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float planDist;
        mDragPlane.Raycast(camRay, out planDist);
        transform.position = camRay.GetPoint(planDist);
    }

    private void OnMouseUp()
    {
        int selectedPlayer = GMSc.selectedPlayer;

        // 自分のターンじゃないバリアなら無効
        if ((selectedPlayer == 1 && tag != "b1") ||
            (selectedPlayer == 2 && tag != "b2") ||
            isPlaced)
        {
            return;
        }

        bool overlap = false;

        // このバリアのColliderを取得
        Collider myCol = GetComponent<Collider>();

        // 自分の位置とサイズからOverlapBoxを作成
        Collider[] hits = Physics.OverlapBox(
            myCol.bounds.center,
            myCol.bounds.extents * 0.9f, // 少し小さめにすることで誤判定を防ぐ
            transform.rotation
        );

        foreach (Collider hit in hits)
        {
            if (hit != myCol && (hit.CompareTag("b1") || hit.CompareTag("b2")))
            {
                // 他のバリアにぶつかった
                overlap = true;
                break;
            }
        }

        if (colliders.Count != 0 && !overlap)
        {
            Collider dropCollider = null;
            float dist = Mathf.Infinity;
            foreach (Collider collider in colliders)
            {
                float d = Vector3.Distance(transform.position, collider.transform.position);
                if (d < dist)
                {
                    dist = d;
                    dropCollider = collider;
                }
            }
            transform.position = dropCollider.transform.position;

            isPlaced = true; // 一度置かれたら動かせない

            GMSc.SwitchPlayer();
        }
        else
        {
            // 置けない（重なっている or 有効な場所じゃない）
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