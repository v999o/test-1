using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public SpriteRenderer spriteRenderer;
    public Sprite originalSprite;
    public Sprite disabledSprite;
    public Sprite highlightedSprite;
    private GameObject soul;
    public Transform player;
    public GameObject soulPrefab;
    private GameObject cameraManager;
    public SoulActions soulActions;

    public float speed = 10f;
    private bool is_soul_spawned = false;
    private bool is_player_highlighted = false;

    float horizontalMove = 0f;
    bool jump = false;

    private void Start()
    {
        cameraManager = GameObject.FindGameObjectWithTag("CameraManager");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Soul") & is_soul_spawned)
        {
            spriteRenderer.sprite = highlightedSprite;
            is_player_highlighted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Soul") & is_soul_spawned)
        {
            spriteRenderer.sprite = disabledSprite;
            is_player_highlighted = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!is_soul_spawned)
        {
            //GetAxisRaw выдает 1 если идем вправо, -1 если идем в лево, умножаем на скорость
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

            //прыжок; значение аргумента GetButtonDown берется из настроек инпута проекта, где можно создать новую переменную
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }

        if(Input.GetButtonDown("Soul_activation") & !is_soul_spawned)
        {
            activate_soul();

        } else if (Input.GetKeyDown(KeyCode.G) & is_soul_spawned)
        {
            deactivate_soul();
        }
        if (Input.GetButtonDown("Soul_activation") & is_player_highlighted & is_soul_spawned)
        {
            deactivate_soul();
        }
    }

    public void activate_soul()
    {
        change_sprite(disabledSprite);
        soulActions.spawn_soul(player); //soulActions - это скрипт-распределитель для взаимодействия с душойs
        //spawn_soul();
        is_soul_spawned = true;
    }

    public void deactivate_soul()
    {
        change_sprite(originalSprite);
        is_soul_spawned = false;
        is_player_highlighted = false;
        jump = false;
        if (soul == null)
        {
            soul = GameObject.FindGameObjectWithTag("Soul");
            soul.GetComponent<SoulMovement>().destroy_soul();
            soul = null;
        }
        cameraManager.GetComponent<CameraTargetSwitcher>().SwitchTarget(player);
    }

    
    /*public void spawn_soul()
    {
        //Instantiate(объект, копию которого хотим сделать, позиция объекта, ориентация объекта)
        //Vector3 чтобы дуща спавнилась со сдвигом, а не на самом игроке
        Instantiate(soulPrefab, player.position + new Vector3(0, 1.5f), player.rotation);
    }*/

    public void change_sprite(Sprite sprite_to_set)
    {
        spriteRenderer.sprite = sprite_to_set;
    }

    public void swap(float enemy_x, float enemy_y)
    {
        gameObject.transform.position = new Vector3(enemy_x, enemy_y, 0);
    }

    void FixedUpdate()
    {
        if (!is_soul_spawned)
        {
            //Time.fixedDeltaTime позволяет идти с одинаковой скорости вне зависимости от производительности компьютера
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }

    public void destroyPlayer()
    {
        Destroy(gameObject);
    }
}
