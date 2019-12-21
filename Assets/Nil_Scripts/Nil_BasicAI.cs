using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class Nil_BasicAI : MonoBehaviour
    {

        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State
        {
            PATROL,
            CHASE
        }

        public State state;
        private bool alive;

        //variables4Patrolling
        public GameObject[] waypoints;
        private int waypointsInd = 0;
        public float patrolSpeed = 0.5f;

        //variable4Chasing
        public float chaseSpeed = 1f;
        public GameObject target;


        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = Nil_BasicAI.State.PATROL;

            alive = true;

            //StartCoroutine("FSM");
        }

        void Update ()
        {
            StartCoroutine("FSM");
        }

        IEnumerator FSM()
        {
            while (alive)
            {
                switch (state)
                {
                    case State.PATROL:
                        Patrol();
                        break;
                    case State.CHASE:
                        Chase();
                        break;
                }
                yield return null; 
                  
            }
        }

        void Patrol()
        {
            agent.speed = patrolSpeed;
            if (Vector3.Distance (this.transform.position, waypoints[waypointsInd].transform.position) >= 2)
            {
                agent.SetDestination(waypoints[waypointsInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance (this.transform.position, waypoints[waypointsInd].transform.position) <=2)
            {
                waypointsInd += 1;
                if (waypointsInd > waypoints.Length)
                {
                    waypointsInd = 0;
                }
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }

        void Chase ()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }

        void OnTriggerEnter (Collider coll)
        {
            if (coll.tag == "Player")
            {
                state = Nil_BasicAI.State.CHASE;
                target = coll.gameObject;
                print("some");
            }
        }


    }
}

