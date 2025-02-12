using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HospitalScript : MonoBehaviour
{


    public GameObject medic;
    public Transform medicSpawn;
    public List<GameObject> woundedHeroes = new List<GameObject>();
    public int availableMedics = 1;
    public GameObject heroToSave;
    public GameObject[] heroes;
    public GameObject hero;
    public GameObject PlayerInv;
    public GameObject returnMedic;
    public int j = 0;
    public int k = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameEventSystem.current.onSaveHeroes += AddWoundedHeroToList;
        heroes = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        HelpTest();
    }

    void HelpTest()
    {
        if (woundedHeroes.Count != 0 && availableMedics != 0)
        {
            SendMedic();
        }

    }

    public void SendMedic()
    {
        heroToSave = woundedHeroes[0];
        GameObject newMedic = Instantiate(medic, medicSpawn.position, Quaternion.identity);
        //medic.GetComponent<MedicScript>().heroToSave = heroToSave;
        newMedic.transform.parent = gameObject.transform;
        woundedHeroes.Remove(heroToSave);
        availableMedics -= 1;
    }

    private void AddWoundedHeroToList(GameObject woundedHero)
    {
        woundedHeroes.Add(woundedHero);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Unconscious" && !other.isTrigger) 
        {
            hero = other.gameObject;
            hero.transform.parent = null;
            heroes[j] = hero;
            Invoke("RestTime", 15);
            heroes[j].SetActive(false);
            PlayerInv.GetComponent<PlayerInventory>().gold += 10;
            if (j != 9)
            {
                j = j + 1;
            }
            else
            {
                j = 0;
            }
        }
        
        
        //if (other.gameObject.tag == "Medic" && !other.isTrigger)
       // {
           // returnMedic = other.gameObject;
           // Debug.Log("test1");
           // if (returnMedic.GetComponent<MedicScript>().gotHero == true && !other.isTrigger)
           // {
            //    Debug.Log("test2");
            //    returnMedic.GetComponent<MedicScript>().done = true;
           //     availableMedics += 1;
          //  }
       // }
    }


    void RestTime()
    {
        UndoBools();
        heroes[k].SetActive(true);

        if (k != 9)
        {
            k = k + 1;
        }
        else
        {
            k = 0;
        }


    }

    void UndoBools()
    {
        heroes[k]. transform.gameObject.tag = "Hero";
        heroes[k].transform.Rotate(0, 0, -90, Space.Self);
        heroes[k].GetComponent<CommonVariables>().waitingForSave = false;
        heroes[k].GetComponent<CommonVariables>().hp = 1;

        //agent.isStopped = true;
        heroes[k].GetComponent<Rigidbody>().isKinematic = false;
        heroes[k].GetComponent<NavMeshAgent>().enabled = true;
        heroes[k].GetComponent<CommonVariables>().mindMode = 3;
        heroes[k].GetComponent<CommonVariables>().healBoolReset = true;
        heroes[k].GetComponent<CommonVariables>().rest = true;

    }

}
