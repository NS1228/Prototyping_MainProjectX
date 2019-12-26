using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class Nil_Sound_Dection : MonoBehaviour
    {
        public Transform Player;
        public bool soundMeter;

       float dist;
        public float range;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
          dist = Vector3.Distance(Player.position, transform.position);

            if (soundMeter && dist <= range)
            {
                this.GetComponent<Nil_Enemy_Sight>().state = Nil_Enemy_Sight.State.INVESTIGATE;
                //this.GetComponent<Nil_Enemy_Sight>().Investigate();
            }


            if(Input.GetKey(KeyCode.LeftShift))
            {
                soundMeter = true;
            }
            else
            {
                soundMeter = false;
            }
        }

      
        }
    
}
