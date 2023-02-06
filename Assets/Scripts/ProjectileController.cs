using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public ParticleSystem efectSentuhan, projectile;
    public ParticleSystem efectSpawn;

    private void Start()
    {
        efectSpawn.Play();
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(coroutine());
        IEnumerator coroutine()
        {
            print("Object ini " + gameObject.name + " Tabarakan sama object ini " + collision.gameObject.name);
            efectSentuhan.Play();
            Destroy(gameObject, 2f);

            Rigidbody rb = GetComponent<Rigidbody>();
            gameObject.isStatic = true;

            yield return new WaitForSeconds(1f);
            projectile.Stop();

        }
    }
}
