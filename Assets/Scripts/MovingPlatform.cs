using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Первая точка
    public Transform pointB; // Вторая точка
    public float speed = 2f; // Скорость движения

    private Rigidbody2D rb;
    private Vector3 target; // Текущая цель

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointB.position; // Начинаем движение ко второй точке
    }

    private void FixedUpdate()
    {
        // Двигаем платформу к цели
        rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlatformMovementBorder") && ((other.transform.position == pointA.transform.position)||(other.transform.position == pointB.transform.position)))
        {       
            target = (target == pointA.position) ? pointB.position : pointA.position;   // Если достигли точки A, двигаемся к B, и наоборот
        }
    }
}
