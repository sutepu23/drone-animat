using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellTrigger : MonoBehaviour
{
    public float detectionRadius = 10f;

    public float movementSpeed = 5f; // Скорость движения к центру зоны
    private bool isMoving = false;   // Флаг для определения, двигается ли объект
    private float stopTime = 2f;     // Время остановки в секундах
    private float stopTimer = 0f;    // Таймер для отсчета времени остановки

    private GameObject targetObject; // Ссылка на объект, который мы ищем

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
            stopTimer = 0f; // Сбрасываем таймер остановки при входе в зону
            targetObject = other.gameObject; // Сохраняем ссылку на объект, который мы ищем
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = false;
            stopTimer = 0f; // Сбрасываем таймер остановки при выходе из зоны
            targetObject = null; // Очищаем ссылку на объект при выходе из зоны
        }
    }

    void Update()
    {
        if (isMoving)
        {
            if (stopTimer < stopTime)
            {
                // Останавливаемся на несколько секунд
                stopTimer += Time.deltaTime;
            }
            else
            {
                // Продолжаем движение к центру зоны
                MoveToCenter();
            }
        }

        if (targetObject != null && Vector3.Distance(transform.position, targetObject.transform.position) <= detectionRadius)
        {
            // Отправляем координаты объекта на экран (псевдокод, замените на вашу реализацию)
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