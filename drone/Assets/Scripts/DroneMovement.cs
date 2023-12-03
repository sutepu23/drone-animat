using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovement : MonoBehaviour
{
    public float moveSpeed = 5f;    // Скорость движения оленя
    public int minWaypoints = 3;    // Минимальное количество случайных триггерных точек
    public int maxWaypoints = 7;    // Максимальное количество случайных триггерных точек
    public float spawnRadius = 10f; // Радиус, в пределах которого генерируется первая точка

    private Transform[] waypoints;
    private int currentWaypoint = 0;

    void Start()
    {
        GenerateWaypoints();  // Генерируем случайные точки при запуске
    }

    void Update()
    {
        MoveToWaypoint();  // Вызываем функцию для движения к точкам
    }

    void GenerateWaypoints()
    {
        int numberOfWaypoints = Random.Range(minWaypoints, maxWaypoints + 1);
        waypoints = new Transform[numberOfWaypoints];

        NavMeshHit hit;

        // Генерируем первую точку в пределах радиуса от текущей позиции оленя
        Vector3 firstWaypointPosition = transform.position + Random.onUnitSphere * spawnRadius;
        firstWaypointPosition.y = 0f;

        if (NavMesh.SamplePosition(firstWaypointPosition, out hit, 10f, NavMesh.AllAreas))
        {
            waypoints[0] = new GameObject("Waypoint0").transform;
            waypoints[0].position = hit.position;
        }

        // Затем генерируем остальные точки в пределах NavMesh
        for (int i = 1; i < numberOfWaypoints; i++)
        {
            // Генерируем случайную позицию в пределах границ NavMesh
            Vector3 randomPosition = new Vector3(
                Random.Range(-50f, 50f) + transform.position.x, // Пример ограничения по X
                0f,
                Random.Range(-50f, 50f) + transform.position.z  // Пример ограничения по Z
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
            // Направление к текущей точке
            Vector3 direction = waypoints[currentWaypoint].position - transform.position;
            direction.Normalize();

            // Двигаемся в направлении точки
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

            // Если олень достиг точки, переходим к следующей
            if (Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
            {
                Destroy(waypoints[currentWaypoint].gameObject);  // Удаляем триггерную точку
                currentWaypoint++;
            }
        }
        else
        {
            // Олень достиг последней точки, можно выполнить какие-то дополнительные действия
            Debug.Log("Deer reached the last waypoint!");
            // Например, можно начать движение заново, если нужно
            currentWaypoint = 0;
            GenerateWaypoints();  // Генерируем новые триггерные точки
        }
    }
}
