using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private GameObject player;
    private GameObject bulletHolder;

    public GameObject[] ammo;
    public GameObject bullet;

    public int maxAmmo;
    public int shootDelay;
    public int bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ammo = new GameObject[maxAmmo];
        bulletHolder = GameObject.FindGameObjectWithTag("SpawnHolder");         // this is so the hierarchy doesnt get clogged during gameplay

        for (int i = 0; i < maxAmmo; i++)
        {
            ammo[i] = Instantiate(bullet, bulletHolder.transform);
        }

        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(shootDelay);
            shootBullet();
        }
    }

    private void shootBullet()
    {
        GameObject spawnedObj = ammo[FindFirstDeactiveIndex()];
        spawnedObj.SetActive(true);

        Vector3 dirToPlayer = getDirToTarget(player.transform.position, transform.position);
        spawnedObj.transform.position = transform.position + (1.75f * dirToPlayer.normalized);           // spawns it a little in front of the drone so it doesnt hit the drone's collider and despawn
        
        //spawnedObj.GetComponent<Rigidbody>().velocity = dirToPlayer.normalized * bulletSpeed;

        // distance / bullet velocity = time

        Vector3 predictPos = (player.GetComponent<VelocityReporter>().velocity * 1) + player.transform.position;
        Vector3 dirToPredict = getDirToTarget(predictPos, transform.position);
        spawnedObj.GetComponent<Rigidbody>().velocity = dirToPredict * bulletSpeed;



    }

    private int FindFirstDeactiveIndex()
    {
        int index = -1;
        for (int i = 0; i < ammo.Length; i++)
        {
            if (ammo[i].activeSelf == false)
            {
                index = i;
                break;
            }
        }
        if (index < 0)
        {
            Debug.Log("no deactive " + bullet + " objs");
        }
        return index;
    }

    private Vector3 getDirToTarget(Vector3 target, Vector3 origin)
    {
        return (target - origin).normalized;
    }


}
