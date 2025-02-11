using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform limitPoint;
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private bool isDoorOpening;
    private bool isDoorClosing;
    public float openingSpeed = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    public void DoorAction(string whatToDo)
    {
        isDoorOpening = (whatToDo == "open") ? true : false;
        isDoorClosing = (whatToDo == "close") ? true : false;
    }

    private void FixedUpdate()
    {
        if (isDoorOpening)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, limitPoint.position, openingSpeed * Time.fixedDeltaTime));
        } else if (isDoorClosing)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, originalPosition, openingSpeed * Time.fixedDeltaTime));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlatformMovementBorder") && ((other.transform.position == limitPoint.transform.position) || (other.transform.position == originalPosition)))
        {
            isDoorOpening = false;
            isDoorClosing = false;
        }
    }
}
