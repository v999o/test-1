using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRemover : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        print("bullet");
        // ���������, ��� ������, ������� ������ � �������, �������� �����
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
