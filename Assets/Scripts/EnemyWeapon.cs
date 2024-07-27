using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform firePointEnemy;
    public GameObject bulletEnemyPrefab;
    private int defaultAttackSpeed = 2000;
    private int attackSpeed = 2000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attackSpeed == 0) {
            Shoot();
            attackSpeed = defaultAttackSpeed;
        } else
        {
            attackSpeed -= 1;
        }
    }

    void Shoot()
    {
        Instantiate(bulletEnemyPrefab, firePointEnemy.position, firePointEnemy.rotation);
    }
}
