using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    public float windSpeed = 2f;         // Скорость ветра
    public float changeDirectionChance = 0.01f;  // Вероятность смены направления ветра
    public float maxChangeAngle = 30f;   // Максимальный угол смены направления

    private Vector3 windDirection;

    void Start()
    {
        // Инициализируем начальное направление ветра
        windDirection = Random.onUnitSphere;
    }

    void Update()
    {
        // Перемещаем объект вдоль направления ветра
        transform.Translate(windDirection * windSpeed * Time.deltaTime, Space.World);

        // Изменяем направление ветра с небольшой вероятностью
        if (Random.value < changeDirectionChance)
        {
            ChangeWindDirection();
        }
    }

    void ChangeWindDirection()
    {
        // Генерируем случайный угол для изменения направления ветра
        float changeAngle = Random.Range(-maxChangeAngle, maxChangeAngle);

        // Поворачиваем текущее направление на сгенерированный угол
        windDirection = Quaternion.Euler(0f, changeAngle, 0f) * windDirection;
    }

    public Vector3 GetWindDirection()
    {
        // Возвращаем текущее направление ветра
        return windDirection;
    }
}
