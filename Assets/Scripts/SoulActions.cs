using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulActions : MonoBehaviour
{
    public GameObject soulPrefab;
    private GameObject currentSoul;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void spawn_soul(Transform transform)
    {
        //Instantiate(объект, копию которого хотим сделать, позиция объекта, ориентация объекта)
        //Vector3 чтобы дуща спавнилась со сдвигом, а не на самом игроке
        currentSoul = Instantiate(soulPrefab, transform.position + new Vector3(0, 1.5f), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
