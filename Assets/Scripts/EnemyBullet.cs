using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
        if (enemy != null)
        {
            enemy.destroyEnemy();
            Destroy(gameObject);
        }
        if (player != null)
        {
            player.destroyPlayer();
            Destroy(gameObject);
        }
        
    }
}
