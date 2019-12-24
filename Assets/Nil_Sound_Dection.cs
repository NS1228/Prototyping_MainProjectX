using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class Nil_Sound_Dection : MonoBehaviour
    {
        public GameObject AI;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player")
            {
                AI.GetComponent<Nil_BasicAI>().target = coll.gameObject;
                if (AI.GetComponent<Nil_BasicAI>().target.GetComponent<Nil_Sound_Maker>().isRunning == true)
                {
                    AI.GetComponent<Nil_BasicAI>().state = Nil_BasicAI.State.SEARCH;
                    
                }
                else
                {
                    AI.GetComponent<Nil_BasicAI>().state = Nil_BasicAI.State.PATROL;
                }
            }
        }
    }
}
