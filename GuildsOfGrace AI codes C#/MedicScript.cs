using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MedicScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool heroLocation = false;
    public GameObject heroToSave;
    public GameObject hospital;
    public Transform hospitalDropOff;
    public bool gotHero = false;
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        hospital = transform.parent.gameObject;
        heroToSave = hospital.GetComponent<HospitalScript>().heroToSave;
        hospitalDropOff = hospital.GetComponent<HospitalScript>().medicSpawn;
        transform.parent = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (!gotHero)
        {


            if (!((agent.transform.position - heroToSave.transform.position).sqrMagnitude <= 150))
            {
                
                agent.destination = heroToSave.transform.position;
            }
            else
            {
                agent.destination = transform.position;
                
                PickUpHero();
                gotHero = true;
            }
        }
        else
        { 
            agent.destination = hospitalDropOff.transform.position;
            
        }

        if((agent.transform.position - hospitalDropOff.transform.position).sqrMagnitude <= 150 && gotHero == true)
        {
            Debug.Log("test1");
            hospital.GetComponent<HospitalScript>().availableMedics += 1;
            Destroy(gameObject);
        }







        //if (gotHero == false)
        //{

        //    if ((agent.transform.position - heroToSave.transform.position).sqrMagnitude <= 150)
        //    {
        //        gotHero = true;
        //        PickUpHero();
                
        //    }
        //    else
        //    {
        //        agent.destination = heroToSave.transform.position;
        //    }
        //}
        //else
        //{
        //    agent.destination = hospital.transform.position;
        //}
    }

    void PickUpHero()
    {
        //heroToSave.GetComponent<Collider>().enabled = false;
        heroToSave.transform.parent = gameObject.transform;
        

        //heroToSave.GetComponent<Rigidbody>().isKinematic = false;
    }


}
