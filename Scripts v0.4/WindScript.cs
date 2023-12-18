using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    public float windSpeed = 2f;         // �������� �����
    public float changeDirectionChance = 0.01f;  // ����������� ����� ����������� �����
    public float maxChangeAngle = 30f;   // ������������ ���� ����� �����������

    private Vector3 windDirection;

    void Start()
    {
        // �������������� ��������� ����������� �����
        windDirection = Random.onUnitSphere;
    }

    void Update()
    {
        // ���������� ������ ����� ����������� �����
        transform.Translate(windDirection * windSpeed * Time.deltaTime, Space.World);

        // �������� ����������� ����� � ��������� ������������
        if (Random.value < changeDirectionChance)
        {
            ChangeWindDirection();
        }
    }

    void ChangeWindDirection()
    {
        // ���������� ��������� ���� ��� ��������� ����������� �����
        float changeAngle = Random.Range(-maxChangeAngle, maxChangeAngle);

        // ������������ ������� ����������� �� ��������������� ����
        windDirection = Quaternion.Euler(0f, changeAngle, 0f) * windDirection;
    }

    public Vector3 GetWindDirection()
    {
        // ���������� ������� ����������� �����
        return windDirection;
    }
}
