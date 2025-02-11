using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
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

    private void Update()
    {
        //двигаем платформу к цели. В данном случае update лучше, чтобы лазер не дрожал, потому что он все равно не сталкивается с физическими объектами
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LaserMovementBorder") && ((other.transform.position == pointA.transform.position) || (other.transform.position == pointB.transform.position)))
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;   // Если достигли точки A, двигаемся к B, и наоборот
        }
        if (other.CompareTag("Player"))
        {
            playerDeath(other.gameObject); // Вызываем метод смерти
        }
        else if (other.CompareTag("Enemy"))
        {
            enemyDeath(other.gameObject);
        }
    }

    void playerDeath(GameObject player)
    {
        // Например, перезапуск уровня
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        // Либо выключаем игрока:
        player.SetActive(false);

        // Либо отправляем его в начальную точку:
        // player.transform.position = startPosition;
    }

    void enemyDeath(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().destroyEnemy();
    }
}
