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

    public float aggroDistance = 20f;
    private float distanceToPlayer;

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
        //Debug.Log(Vector3.Distance(player.transform.position, transform.position));
        while (true)
        {
            yield return new WaitForSecondsRealtime(shootDelay);
            shootBullet();
        }
    }

    private void shootBullet()
    {
        if (Vector3.Distance(player.transform.position, transform.position) > aggroDistance) return;
        GameObject spawnedObj = ammo[FindFirstDeactiveIndex()];
        spawnedObj.SetActive(true);

        Vector3 dirToPlayer = getDirToTarget(player.transform.position, transform.position);

        spawnedObj.transform.position = transform.position + (1.75f * dirToPlayer.normalized);           // spawns it a little in front of the drone so it doesnt hit the drone's collider and despawn
        
        //spawnedObj.GetComponent<Rigidbody>().velocity = dirToPlayer.normalized * bulletSpeed;

        // distance / bullet velocity = time

        Vector3 predictPos = (player.GetComponent<VelocityReporter>().velocity * 1) + player.transform.position;
        Vector3 dirToPredict = getDirToTarget(predictPos, transform.position);
        // dirToPredict.y = 0;     // could also set .y = 0 only if .y is < 0, which means bullets can shoot up, this fixed shooting into ground issue
        if (dirToPredict.y < 0)
        {
            dirToPredict.y = 0;
        }
        spawnedObj.GetComponent<Rigidbody>().velocity = dirToPredict * bulletSpeed;

        // need to fix angle at which bullet is shot, sometimes it is shot into the ground which hits the collider and despawns

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

    private void OnEnable()
    {
        // StartCoroutine(Shooting());
    }


}
