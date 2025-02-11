using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
{
    public Transform pointA; // ������ �����
    public Transform pointB; // ������ �����
    public float speed = 2f; // �������� ��������

    private Rigidbody2D rb;
    private Vector3 target; // ������� ����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = pointB.position; // �������� �������� �� ������ �����
    }

    private void Update()
    {
        //������� ��������� � ����. � ������ ������ update �����, ����� ����� �� ������, ������ ��� �� ��� ����� �� ������������ � ����������� ���������
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LaserMovementBorder") && ((other.transform.position == pointA.transform.position) || (other.transform.position == pointB.transform.position)))
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;   // ���� �������� ����� A, ��������� � B, � ��������
        }
        if (other.CompareTag("Player"))
        {
            playerDeath(other.gameObject); // �������� ����� ������
        }
        else if (other.CompareTag("Enemy"))
        {
            enemyDeath(other.gameObject);
        }
    }

    void playerDeath(GameObject player)
    {
        // ��������, ���������� ������
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        // ���� ��������� ������:
        player.SetActive(false);

        // ���� ���������� ��� � ��������� �����:
        // player.transform.position = startPosition;
    }

    void enemyDeath(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().destroyEnemy();
    }
}
