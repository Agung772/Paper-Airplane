using SimpleInputNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedPlayer;
    public float speedShoot, speedRotateShoot;

    public CharacterController characterController;
    public Joystick analogMove, analogShoot;

    public GameObject projectilePrefab;
    public Transform projectilePoint;

    private void Update()
    {
        MovePlayer();
        LimitY();
        PlayerShoot();
        RotateShoot();
    }

    void MovePlayer()
    {
        float horizontalInput = analogMove.m_value.x;
        float verticalInput = analogMove.m_value.y;


        Vector3 move = new Vector3(0, verticalInput, -horizontalInput);

        characterController.Move(move * speedPlayer * Time.deltaTime);
    }

    void LimitY()
    {
        if (transform.position.y >= 15) transform.position = new Vector3(transform.position.x, 15, transform.position.z);
        if (transform.position.y <= -5) transform.position = new Vector3(transform.position.x, -5, transform.position.z);
    }

    bool cooldownShoot = true;
    void PlayerShoot()
    {
        if (cooldownShoot)
        {
            if (Input.GetKey(KeyCode.Space) || analogShoot.joystickHeld)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectilePoint.position, transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(projectilePoint.transform.right * speedShoot, ForceMode.Impulse);
                Destroy(projectile, 5f);

                cooldownShoot = false;
                StartCoroutine(coroutine());
                IEnumerator coroutine()
                {
                    yield return new WaitForSeconds(0.2f);
                    cooldownShoot = true;
                }
            }
        }
    }

    float xDegree, yDegree;
    void RotateShoot()
    {
        float horizontalInput = 0;
        float verticalInput = 0;

        Vector2 v2 = new Vector2(analogShoot.m_value.y, analogShoot.m_value.x);
        float mag = v2.magnitude;
        if (mag >= 0.9)
        {
            horizontalInput = analogShoot.m_value.y;
            verticalInput = analogShoot.m_value.x;
        }

        xDegree += horizontalInput * speedRotateShoot * Time.deltaTime;
        yDegree += verticalInput * speedRotateShoot * Time.deltaTime;

        projectilePoint.rotation = Quaternion.Euler(0, yDegree, xDegree);

        xDegree = Mathf.Clamp(xDegree, -20, 20);
        yDegree = Mathf.Clamp(yDegree, -60, 60);



    }
}
