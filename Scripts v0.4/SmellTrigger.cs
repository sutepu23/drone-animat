using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellTrigger : MonoBehaviour
{
    public float detectionRadius = 10f;
    public LayerMask playerLayer;

    public float movementSpeed = 5f; // �������� �������� � ������ ����
    private bool isMoving = false;   // ���� ��� �����������, ��������� �� ������
    private float stopTime = 2f;     // ����� ��������� � ��������
    private float stopTimer = 0f;    // ������ ��� ������� ������� ���������

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
            stopTimer = 0f; // ���������� ������ ��������� ��� ����� � ����
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = false;
            stopTimer = 0f; // ���������� ������ ��������� ��� ������ �� ����
        }
    }

    void Update()
    {
        if (isMoving)
        {
            if (stopTimer < stopTime)
            {
                // ��������������� �� ��������� ������
                stopTimer += Time.deltaTime;
            }
            else
            {
                // ���������� �������� � ������ ����
                MoveToCenter();
            }
        }
    }

    void MoveToCenter()
    {
        Vector3 directionToCenter = transform.position - transform.parent.position;
        transform.Translate(-directionToCenter.normalized * movementSpeed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}