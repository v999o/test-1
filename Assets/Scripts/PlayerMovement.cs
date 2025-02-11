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
    private Sprite previousSprite;
    private GameObject soul;
    public Transform player;
    public GameObject soulPrefab;
    private GameObject cameraManager;
    public Animator animator;

    public float speed = 10f;
    private bool is_soul_spawned = false;
    private bool is_player_highlighted = false;
    private bool is_player_controlled = true;
    private bool is_animator_state_updated = false;
    private bool isSoulActivationBlocked = false;

    float horizontalMove = 0f;
    bool jump = false;

    private void Start()
    {
        cameraManager = GameObject.FindGameObjectWithTag("CameraManager");
        soul = Instantiate(soulPrefab, transform.position + new Vector3(0, 1.5f), transform.rotation);
        soul.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Soul") & is_soul_spawned)
        {
            spriteRenderer.sprite = highlightedSprite;
            is_player_highlighted = true;
        }
        if (other.CompareTag("SoulActivationStopper"))
        {
            isSoulActivationBlocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Soul") && is_soul_spawned && !is_player_controlled)
        {
            spriteRenderer.sprite = disabledSprite;
            is_player_highlighted = false; 
        }
        if (other.CompareTag("SoulActivationStopper"))
        {
            isSoulActivationBlocked = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (is_player_controlled)
        {
            //GetAxisRaw выдает 1 если идем вправо, -1 если идем в лево, умножаем на скорость
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

            if (horizontalMove != 0 && !is_animator_state_updated) {
                animator.SetBool("isPlayerWalking", true);
                is_animator_state_updated = true; //переменная is_animator_state_updated нужна чтобы не передавать значение каждый кадр, а только тогда, когда оно меняется (для улучшения производительности)

            } else if (horizontalMove == 0 && is_animator_state_updated)
            {
                animator.SetBool("isPlayerWalking", false);
                is_animator_state_updated = false;
            }

            //прыжок; значение аргумента GetButtonDown берется из настроек инпута проекта, где можно создать новую переменную
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        } else if (is_animator_state_updated) {
            animator.SetBool("isPlayerWalking", false);
            is_animator_state_updated = false;
        }

        if (Input.GetButtonDown("Soul_activation") && is_player_controlled && !isSoulActivationBlocked)
        {
            activate_soul();
        }
        if ((Input.GetKeyDown(KeyCode.G) && !is_player_controlled) || (Input.GetButtonDown("Soul_activation") && is_player_highlighted & is_soul_spawned))
        {
            deactivate_soul();
        }
    }

    public void activate_soul()
    {
        change_sprite(disabledSprite);
        soul.transform.position = transform.position + new Vector3(0, 1.5f);
        soul.SetActive(true);
        is_soul_spawned = true;
        is_player_controlled = false;
    }

    public void deactivate_soul()
    {
        change_sprite(originalSprite);
        is_player_highlighted = false;
        is_player_controlled = true;
        jump = false;
        if (is_soul_spawned)
        {
            soul.SetActive(false);
            is_soul_spawned = false;
        }
        cameraManager.GetComponent<CameraTargetSwitcher>().SwitchTarget(player);
    }

    public void change_sprite(Sprite sprite_to_set)
    {
        spriteRenderer.sprite = sprite_to_set;
    }

    public void swap(float enemy_x, float enemy_y)
    {
        gameObject.transform.position = new Vector3(enemy_x, enemy_y, 0);
    }

    public void setIsSoulSpawned(bool isSoulSpawned)
    {
        is_soul_spawned = isSoulSpawned;
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
