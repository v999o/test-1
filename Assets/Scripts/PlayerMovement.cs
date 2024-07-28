using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public SpriteRenderer spriteRenderer;
    public Sprite originalSprite;
    public Sprite newSprite;
    private GameObject soul;
    public Transform player;
    public GameObject soulPrefab;
    public SoulActions soulActions;

    public float speed = 10f;
    private bool is_soul_spawned = false;

    float horizontalMove = 0f;
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        //GetAxisRaw ������ 1 ���� ���� ������, -1 ���� ���� � ����, �������� �� ��������
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        //������; �������� ��������� GetButtonDown ������� �� �������� ������ �������, ��� ����� ������� ����� ����������
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if(Input.GetButtonDown("Soul_activation") & !is_soul_spawned)
        {
            activate_soul();

        } else if (Input.GetKeyDown(KeyCode.G) & is_soul_spawned)
        {
            deactivate_soul();
        }
    }

    public void activate_soul()
    {
        change_sprite(newSprite);
        soulActions.spawn_soul(player); //soulActions - ��� ������-�������������� ��� �������������� � �����s
        //spawn_soul();
        is_soul_spawned = true;
    }

    public void deactivate_soul()
    {
        change_sprite(originalSprite);
        is_soul_spawned = false;
        jump = false;
    }

    /*public void spawn_soul()
    {
        //Instantiate(������, ����� �������� ����� �������, ������� �������, ���������� �������)
        //Vector3 ����� ���� ���������� �� �������, � �� �� ����� ������
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
            //Time.fixedDeltaTime ��������� ���� � ���������� �������� ��� ����������� �� ������������������ ����������
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }

    public void destroyPlayer()
    {
        Destroy(gameObject);
    }
}
