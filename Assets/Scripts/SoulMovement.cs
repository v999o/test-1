using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMovement : MonoBehaviour
{
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;

    public new Rigidbody2D rigidbody2D;
    private GameObject cameraManager;
    private Vector3 m_Velocity = Vector3.zero;
    public Transform soulTransform;
    public float speed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f;

    private void Start()
    {

        cameraManager = GameObject.FindGameObjectWithTag("CameraManager");
        cameraManager.GetComponent<CameraTargetSwitcher>().SwitchTarget(soulTransform);
    }
    void Update()
    {
        //GetAxisRaw выдает 1 если идем вправо, -1 если идем в лево, умножаем на скорость
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;
        if (Input.GetKeyDown(KeyCode.G))
        {
            destroy_soul();
        }
    }

    void FixedUpdate()
    {
        //Time.fixedDeltaTime позволяет идти с одинаковой скорости вне зависимости от производительности компьютера
        move_soul(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);

        
    }

    void move_soul(float move_x, float move_y)
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move_x * 10f, move_y * 10f);
        // And then smoothing it out and applying it to the character
        rigidbody2D.velocity = Vector3.SmoothDamp(rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    public void destroy_soul()
    {
        Destroy(gameObject);
    }
}
