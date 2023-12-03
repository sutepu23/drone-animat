using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovement : MonoBehaviour
{
    public float moveSpeed = 5f;    // �������� �������� �����
    public int minWaypoints = 3;    // ����������� ���������� ��������� ���������� �����
    public int maxWaypoints = 7;    // ������������ ���������� ��������� ���������� �����
    public float spawnRadius = 10f; // ������, � �������� �������� ������������ ������ �����

    private Transform[] waypoints;
    private int currentWaypoint = 0;

    void Start()
    {
        GenerateWaypoints();  // ���������� ��������� ����� ��� �������
    }

    void Update()
    {
        MoveToWaypoint();  // �������� ������� ��� �������� � ������
    }

    void GenerateWaypoints()
    {
        int numberOfWaypoints = Random.Range(minWaypoints, maxWaypoints + 1);
        waypoints = new Transform[numberOfWaypoints];

        NavMeshHit hit;

        // ���������� ������ ����� � �������� ������� �� ������� ������� �����
        Vector3 firstWaypointPosition = transform.position + Random.onUnitSphere * spawnRadius;
        firstWaypointPosition.y = 0f;

        if (NavMesh.SamplePosition(firstWaypointPosition, out hit, 10f, NavMesh.AllAreas))
        {
            waypoints[0] = new GameObject("Waypoint0").transform;
            waypoints[0].position = hit.position;
        }

        // ����� ���������� ��������� ����� � �������� NavMesh
        for (int i = 1; i < numberOfWaypoints; i++)
        {
            // ���������� ��������� ������� � �������� ������ NavMesh
            Vector3 randomPosition = new Vector3(
                Random.Range(-50f, 50f) + transform.position.x, // ������ ����������� �� X
                0f,
                Random.Range(-50f, 50f) + transform.position.z  // ������ ����������� �� Z
            );

            if (NavMesh.SamplePosition(randomPosition, out hit, 10f, NavMesh.AllAreas))
            {
                waypoints[i] = new GameObject("Waypoint" + i).transform;
                waypoints[i].position = hit.position;
            }
        }
    }

    void MoveToWaypoint()
    {
        if (currentWaypoint < waypoints.Length)
        {
            // ����������� � ������� �����
            Vector3 direction = waypoints[currentWaypoint].position - transform.position;
            direction.Normalize();

            // ��������� � ����������� �����
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            // ���� ����� ������ �����, ��������� � ���������
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
            {
                Destroy(waypoints[currentWaypoint].gameObject);  // ������� ���������� �����
                currentWaypoint++;
            }
        }
        else
        {
            // ����� ������ ��������� �����, ����� ��������� �����-�� �������������� ��������
            Debug.Log("Deer reached the last waypoint!");
            // ��������, ����� ������ �������� ������, ���� �����
            currentWaypoint = 0;
            GenerateWaypoints();  // ���������� ����� ���������� �����
        }
    }
}
