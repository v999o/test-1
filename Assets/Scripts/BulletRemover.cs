using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRemover : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        print("bullet");
        // Проверьте, что объект, который входит в триггер, является пулей
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
