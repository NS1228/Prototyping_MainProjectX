using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class Nil_Enemy_Sight : MonoBehaviour
    {

        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State
        {
            PATROL,
            CHASE,
            SEARCH,
            INVESTIGATE
            
            
        }

        public State state;
        private bool alive;

        //variables4Patrolling
        public GameObject[] waypoints;
        private int waypointsInd;
        public float patrolSpeed = 0.5f;

        //variable4Chasing
        public float chaseSpeed = 1f;
        public GameObject target;
        public float chaseTimer;
        public bool checkPlayer;
        public float reCheck;
        public float dist;
        
        public Transform Player;

        //variables4Searching
        // public float searchSpeed = 0;
        // public float lookSpeed = 0;

        //variables4 Investigating;
        private Vector3 investigateSpot;
        private float timer = 0;  
        public float investigateWait = 10;
       

        //variables for sight
        public float heightMultiplayer;
        public float sightDistance = 10f;



        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
            waypointsInd = Random.Range(0, waypoints.Length);

            state = Nil_Enemy_Sight.State.PATROL;

            alive = true;

            heightMultiplayer = 1.36f;

            StartCoroutine("FSM");

            Player.transform.position = target.gameObject.transform.position;
        }

        void Update()
        {
            //StartCoroutine("FSM");
            dist = Vector3.Distance(Player.position, transform.position);

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
                    case State.SEARCH:
                        Search();
                        break;
                    case State.INVESTIGATE:
                       Investigate();
                        break;
                }
                yield return null;

            }


        }

        void Patrol()
        {
            agent.speed = patrolSpeed;
            if (Vector3.Distance(this.transform.position, waypoints[waypointsInd].transform.position) >= 2)
            {
                agent.SetDestination(waypoints[waypointsInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointsInd].transform.position) <= 2)
            {
                waypointsInd = Random.Range(0, waypoints.Length);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }

        void Chase()
        {
            transform.LookAt(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
            agent.speed = 0;

            if(dist > sightDistance)
            {
               state = Nil_Enemy_Sight.State.PATROL;
            }

            if (checkPlayer && Time.timeSinceLevelLoad >= reCheck) 
            {
                chaseTimer = Time.timeSinceLevelLoad + 3f;
                
                
                checkPlayer = false;
                reCheck = Time.timeSinceLevelLoad + 3f;
                
            }
            if (Time.timeSinceLevelLoad >= chaseTimer)
            {
                agent.SetDestination(target.transform.position);
                agent.speed = chaseSpeed;

            }
            
        }

        void Search()
        {

            {
               // agent.SetDestination(target.transform.position);
               // agent.speed = searchSpeed;
                //character.Move(agent.desiredVelocity, false, false);
            }
        }

       public void Investigate ()
        {
            timer += Time.deltaTime;
            

            agent.SetDestination(this.transform.position);
            character.Move(Vector3.zero, false, false);
            transform.LookAt(investigateSpot);
            if(timer >= investigateWait)
            {
                state = Nil_Enemy_Sight.State.PATROL;
                timer = 0;
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                state = Nil_Enemy_Sight.State.INVESTIGATE;
                investigateSpot = coll.gameObject.transform.position;
            }
        }

        void FixedUpdate ()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplayer, transform.forward * sightDistance, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplayer, (transform.forward + transform.right).normalized * sightDistance, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplayer, (transform.forward - transform.right).normalized * sightDistance, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplayer, transform.forward, out hit, sightDistance))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = Nil_Enemy_Sight.State.CHASE;
                    target = hit.collider.gameObject;
                    checkPlayer = true;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplayer, (transform.forward + transform.right).normalized, out hit, sightDistance))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = Nil_Enemy_Sight.State.CHASE;
                    target = hit.collider.gameObject;
                    checkPlayer = true;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplayer, (transform.forward - transform.right).normalized, out hit, sightDistance))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    state = Nil_Enemy_Sight.State.CHASE;
                    target = hit.collider.gameObject;
                    checkPlayer = true;
                }
            }
        }

    }
}

