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

    private bool isPlaced = false; // �u���ꂽ���ǂ���
    private void OnMouseDown()
    {
        int selectedPlayer = GMSc.selectedPlayer;

        // �����̃o���A����Ȃ� or ���łɒu����Ă���ꍇ�͖���
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

        // �����̃^�[������Ȃ��o���A�Ȃ疳��
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

        // �����̃^�[������Ȃ��o���A�Ȃ疳��
        if ((selectedPlayer == 1 && tag != "b1") ||
            (selectedPlayer == 2 && tag != "b2") ||
            isPlaced)
        {
            return;
        }

        bool overlap = false;

        // ���̃o���A��Collider���擾
        Collider myCol = GetComponent<Collider>();

        // �����̈ʒu�ƃT�C�Y����OverlapBox���쐬
        Collider[] hits = Physics.OverlapBox(
            myCol.bounds.center,
            myCol.bounds.extents * 0.9f, // ���������߂ɂ��邱�ƂŌ딻���h��
            transform.rotation
        );

        foreach (Collider hit in hits)
        {
            if (hit != myCol && (hit.CompareTag("b1") || hit.CompareTag("b2")))
            {
                // ���̃o���A�ɂԂ�����
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

            isPlaced = true; // ��x�u���ꂽ�瓮�����Ȃ�

            GMSc.SwitchPlayer();
        }
        else
        {
            // �u���Ȃ��i�d�Ȃ��Ă��� or �L���ȏꏊ����Ȃ��j
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