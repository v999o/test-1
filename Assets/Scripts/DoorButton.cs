using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public Door door; //Door - это скрипт двери
    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    public Sprite selectedSprite;
    public Sprite pressedSprite;
    private Rigidbody2D rb;
    private bool isSelected = false;
    private bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            isSelected = true;
            spriteRenderer.sprite = selectedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isSelected)
        {
            spriteRenderer.sprite = (isPressed) ? pressedSprite : originalSprite; //если нажата, заменяем на нажатый спрайт, если нет - на оригинальный
            isSelected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected && Input.GetKeyDown(KeyCode.Return)) //return - это enter
        {
            isSelected = false;
            isPressed = !isPressed; //меняем значение напротивоположное
            if (isPressed)  //это проверка ПОСЛЕ нажатия кнопки (если она была неактивна, то теперь стала активна и тд)
            {
                spriteRenderer.sprite = pressedSprite;
                door.DoorAction("open");
            } else
            {
                spriteRenderer.sprite = originalSprite;
                door.DoorAction("close");
            }
        }
    }
}
