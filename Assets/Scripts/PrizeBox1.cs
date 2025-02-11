using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeBox1 : MonoBehaviour
{
    public enum StarBoxTypes {
        PlayerBox,
        SoulBox
    }
    public StarBoxTypes starBoxType;
    private Rigidbody2D rb;
    public GameObject star;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        star.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((starBoxType == StarBoxTypes.PlayerBox) && other.CompareTag("Player")) || ((starBoxType == StarBoxTypes.SoulBox) && other.CompareTag("Soul")))
        {
            star.SetActive(true);
            Destroy(gameObject);
        }
    }
}
