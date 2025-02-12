using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnScript : MonoBehaviour
{
    public GameObject hero;
    public GameObject[] heroes;
    public GameObject PlayerInv;
    public bool resting = false;
    public int j = 0;
    public int k = 0;
    // Start is called before the first frame update
    void Start()
    {
        heroes = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(!other.isTrigger)
        {
            hero = other.gameObject;

            if(RestTest())
            {
                heroes[j] = hero;
                Invoke("RestTime",15);
                heroes[j].SetActive(false);
                PlayerInv.GetComponent<PlayerInventory>().gold += 10;
                if(j != 9)
                {
                    j = j + 1;
                } else {
                    j = 0;
                }
                

            }
        }
    }

    bool RestTest()
    {

        if(hero.tag == "Hero" && hero.GetComponent<CommonVariables>().rest == true)

        //separate script for common variables.

        {
            return true;
        } else {
            return false;
        }

    }

    void RestTime()
    {
        heroes[k].SetActive(true);
        heroes[k].GetComponent<CommonVariables>().hp = heroes[k].GetComponent<CommonVariables>().maxhp;
        heroes[k].GetComponent<CommonVariables>().rest = false;
        heroes[k].GetComponent<CommonVariables>().shopping = true;
        if ( k != 9)
        {
            k = k + 1;
        } else {
            k = 0;
        }
       

    }
    
}
