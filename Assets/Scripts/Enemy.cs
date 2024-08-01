using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public CharacterController2D controller;
    public Sprite highlightedSprite;
    public Sprite originalSprite;
    public Sprite friendlySprite;
    private SpriteRenderer spriteRenderer;
    private GameObject player = null;
    private GameObject soul = null;
    float enemy_x = 0f;
    float enemy_y = 0f;
    bool is_enemy_highlighted = false;
    bool is_enemy_controlled = false;
    public SoulActions soulActions;
    public Transform enemyTransform;
    private GameObject cameraManager;

    public float speed = 40f;

    float[] enemy_coordinates;
    float horizontalMove = 0f;
    bool jump = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cameraManager = GameObject.FindGameObjectWithTag("CameraManager");
    }
    void SaveCurrentEnemyPosition() {
        enemy_x = gameObject.transform.position.x;
        enemy_y = gameObject.transform.position.y;
    }

    public void swapEnemy()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            SaveCurrentEnemyPosition();
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            player.GetComponent<PlayerMovement>().swap(enemy_x, enemy_y);

        } else
        {
            SaveCurrentEnemyPosition();
            gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            player.GetComponent<PlayerMovement>().swap(enemy_x, enemy_y);
        }
        //Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Soul") & !is_enemy_controlled)
        {
            spriteRenderer.sprite = highlightedSprite;
            is_enemy_highlighted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Soul") & !is_enemy_controlled)
        {
            spriteRenderer.sprite = originalSprite;
            is_enemy_highlighted = false;
        }
    }

    public void destroyEnemy()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (is_enemy_controlled)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) & is_enemy_highlighted) //если подсвечен и нажимаем на кнопку вселения - вселяемся
        {
            spriteRenderer.sprite = friendlySprite;
            is_enemy_highlighted = false;
            is_enemy_controlled = true;
            if (soul == null)
            {
                soul = GameObject.FindGameObjectWithTag("Soul");
                soul.GetComponent<SoulMovement>().destroy_soul();
                soul = null;
            }
            cameraManager.GetComponent<CameraTargetSwitcher>().SwitchTarget(enemyTransform);
        }
        else if (Input.GetKeyDown(KeyCode.F) & is_enemy_controlled) //выселение из врага
        {
            spriteRenderer.sprite = originalSprite;
            is_enemy_controlled = false;
            soulActions.spawn_soul(enemyTransform);

        }
        if (Input.GetKeyDown(KeyCode.G) & is_enemy_controlled)
        {
            spriteRenderer.sprite = originalSprite;
            is_enemy_controlled = false;
        }
    }
    private void FixedUpdate()
    {
        if (is_enemy_controlled)
        {
            //Time.fixedDeltaTime позволяет идти с одинаковой скорости вне зависимости от производительности компьютера
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }
}
