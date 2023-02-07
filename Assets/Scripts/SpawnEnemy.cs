using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    public GameObject enemyPrefab;
    private void Start()
    {
        InvokeRepeating("Spawns", 1, 3);
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + offset, 100 * Time.deltaTime);
    }

    void Spawns()
    {
        GameObject prefab = Instantiate(enemyPrefab, transform.position, transform.rotation);
        Destroy(prefab, 30);
        prefab.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(-10, 10));
        prefab.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * -5000, ForceMode.Impulse);
    }
}
