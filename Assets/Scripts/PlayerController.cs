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
    public Transform projectileRotate, projectileRotate1;
    public Transform projectilePoint, projectilePoint1;

    private void Update()
    {
        MovePlayer();
        LimitY();
        PlayerShoot();
        RotateShoot();
    }

    void MovePlayer()
    {
        float horizontalInput = 0;
        float verticalInput = 0;
        if (analogMove.joystickHeld)
        {
            horizontalInput = analogMove.m_value.x;
            verticalInput = analogMove.m_value.y;
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }



        Vector3 move = new Vector3(0.6f, verticalInput, -horizontalInput);

        characterController.Move(move * speedPlayer * Time.deltaTime);
    }

    void LimitY()
    {
        if (transform.position.y >= 25) transform.position = new Vector3(transform.position.x, 25, transform.position.z);
        if (transform.position.y <= 5) transform.position = new Vector3(transform.position.x, 5, transform.position.z);
    }

    bool cooldownShoot = true;
    int nameProjectile;
    void PlayerShoot()
    {
        if (cooldownShoot)
        {
            if (Input.GetKey(KeyCode.Space) || analogShoot.joystickHeld)
            {
                GameObject projectile = Instantiate(projectilePrefab, projectilePoint.position, projectilePoint.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(projectilePoint.transform.right * speedShoot, ForceMode.Impulse);
                Destroy(projectile, 5f);

                GameObject projectile1 = Instantiate(projectilePrefab, projectilePoint1.position, projectilePoint.rotation);
                projectile1.GetComponent<Rigidbody>().AddForce(projectilePoint1.transform.right * speedShoot, ForceMode.Impulse);
                Destroy(projectile1, 5f);

                projectile.name = projectile.name + nameProjectile.ToString();
                nameProjectile++;
                projectile1.name = projectile1.name + nameProjectile.ToString();
                nameProjectile++;

                cooldownShoot = false;
                StartCoroutine(coroutine());
                IEnumerator coroutine()
                {
                    yield return new WaitForSeconds(0.3f);
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

        projectileRotate.rotation = Quaternion.Euler(0, yDegree, xDegree);
        projectileRotate1.rotation = Quaternion.Euler(0, yDegree, xDegree);

        xDegree = Mathf.Clamp(xDegree, -20, 20);
        yDegree = Mathf.Clamp(yDegree, -60, 60);



    }
}
