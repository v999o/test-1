using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
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

    private void FixedUpdate()
    {
        // ������� ��������� � ����
        rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlatformMovementBorder") && ((other.transform.position == pointA.transform.position)||(other.transform.position == pointB.transform.position)))
        {       
            target = (target == pointA.position) ? pointB.position : pointA.position;   // ���� �������� ����� A, ��������� � B, � ��������
        }
    }
}
