using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDeath(other.gameObject); // Вызываем метод смерти
        } else if (other.CompareTag("Enemy"))
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
