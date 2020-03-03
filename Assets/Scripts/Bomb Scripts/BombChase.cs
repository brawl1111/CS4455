﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BombChase : MonoBehaviour
{

    private NavMeshAgent navAgent;
    private Animator anim;

    public GameObject player;
    public GameObject[] waypoints;
    public int curWaypoint = -1;
    public int fuseTime = 1;
    public float aggroRange = 4.0f;

    public bool isExploded = false;

    public Material[] mats;
    private Renderer render;

    public enum AIState
    {
        Idle,
        Patrol,
        Chase
    };

    public AIState aIState;
    private AIState prevState;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        aIState = AIState.Patrol;

        render = GetComponent<Renderer>();
        anim = GetComponent<Animator>();

        player = GameObject.Find("Princess");      // need to set up player/waypoints b/c of prefab
        waypoints = new GameObject[2];
        waypoints[0] = GameObject.Find("Waypoint1");
        waypoints[1] = GameObject.Find("Waypoint2");

        //player = GameObject.Find("BombTestPlayer");

    }

    // Update is called once per frame
    void Update()
    {




        float distance = Vector3.Distance(transform.position, player.transform.position);

        prevState = aIState;

        if (distance < aggroRange)
        {
            aIState = AIState.Chase;
        }
        else
        {
            aIState = AIState.Patrol;
        }



        switch (aIState)
        {
            case AIState.Patrol:

                //render.sharedMaterial = mats[0];

                if (navAgent.remainingDistance == 0)
                {
                    if (navAgent.pathPending == false)
                    {
                        setNextWaypoint();
                    }
                }
                break;

            case AIState.Chase:

                //render.sharedMaterial = mats[1];

                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 newPos = transform.position - dirToPlayer;
                navAgent.SetDestination(newPos);

                float explodeDist = Vector3.Distance(transform.position, player.transform.position);

                /*
                if (explodeDist < 1.0f)
                {
                    anim.Play("mon00_attack01");
                    print("boom");
                }
                */

                if (prevState != aIState)
                {
                    print("in explosion part");
                    StartCoroutine(DelayedExplosion());
                }

                break;
        }

        /*
        // run towards player
        if (distance < aggroRange)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            navAgent.SetDestination(newPos);

        }
        */

        //anim.SetFloat("vely", navAgent.velocity.magnitude / navAgent.speed);

    } // Update

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "some_tag")
        {
            render.sharedMaterial = mats[1];
        }
    }

    IEnumerator DelayedExplosion()
    {
        speedUp();

        Debug.Log("started coroutine at timestamp: " + Time.time);
        yield return new WaitForSecondsRealtime(fuseTime);
        Debug.Log("finish coroutine at timestamp: " + Time.time);



        //aIState = AIState.Idle;
        navAgent.ResetPath();       // prob not needed
        navAgent.isStopped = true;
        //Destroy(gameObject);

        //navAgent.isStopped = true;
        anim.Play("attack01");

        //isExploded = true;

        //gameObject.SetActive(false);        // can't destroy for pool spawning
        //Destroy(gameObject);      // can use destroy for spawner(1)

    }

    private void speedUp()
    {
        navAgent.speed *= 2.5f;
        //navAgent.angularSpeed *= 3;
    }

    private void OnEnable()
    {
        //anim.Play("mon00_walk");
        //Debug.Log("enabled");
    } // does stuff when you enable the thing

    private void OnDisable()
    {

    }

    private void setNextWaypoint()
    {

        if (waypoints.Length == 0)
        {
            print("waypoints is empty");
        }
        else
        {
            curWaypoint++;
            if (curWaypoint >= waypoints.Length)
            {
                curWaypoint = 0;
            }

            navAgent.SetDestination(waypoints[curWaypoint].transform.position);
        }

    } // setNextWaypoint

    private int getClosestWaypointIndex()
    {
        float minDist = Mathf.Infinity;
        int closestIndex = curWaypoint;

        for (int i = 0; i < waypoints.Length; i++)
        {
            GameObject cur = waypoints[i];
            float dist = Vector3.Distance(transform.position, cur.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestIndex = i;
            }

        }

        return closestIndex;

    } // getClosestWaypoint

    public bool getExploded()
    {
        return isExploded;
    }

}
