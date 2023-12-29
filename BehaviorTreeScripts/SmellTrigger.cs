using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellTrigger : MonoBehaviour
{
    public float detectionRadius = 10f;

    public float movementSpeed = 5f; // �������� �������� � ������ ����
    private bool isMoving = false;   // ���� ��� �����������, ��������� �� ������
    private float stopTime = 2f;     // ����� ��������� � ��������
    private float stopTimer = 0f;    // ������ ��� ������� ������� ���������

    private GameObject targetObject; // ������ �� ������, ������� �� ����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
            stopTimer = 0f; // ���������� ������ ��������� ��� ����� � ����
            targetObject = other.gameObject; // ��������� ������ �� ������, ������� �� ����
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = false;
            stopTimer = 0f; // ���������� ������ ��������� ��� ������ �� ����
            targetObject = null; // ������� ������ �� ������ ��� ������ �� ����
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

        if (targetObject != null && Vector3.Distance(transform.position, targetObject.transform.position) <= detectionRadius)
        {
            // ���������� ���������� ������� �� ����� (���������, �������� �� ���� ����������)
            Debug.Log("Object coordinates: " + targetObject.transform.position);
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