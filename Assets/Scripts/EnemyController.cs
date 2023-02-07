using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hpEnemy = 5;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            hpEnemy--;
            if (hpEnemy <= 0)
            {
                Destroy(gameObject);
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                for (int i = 0; i < transform.childCount; i++)
                {
                     transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                }

            }
            print("Peluru");
        }
    }
}
