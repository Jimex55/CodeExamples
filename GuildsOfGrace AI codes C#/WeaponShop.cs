using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;




public class Item
{
    public int itemID;
    public string itemName;
    public int cost;
    public int damage;
    public GameObject weapon;
    public Item(int itemID, string itemName, int cost, int damage, GameObject weapon)
    {
        this.itemID = itemID;
        this.itemName = itemName;  
        this.cost = cost;
        this.damage = damage;
        this.weapon = weapon;
    }
}

public class HeroActivity
{
    public GameObject hero;
    public DateTime leaveTime;

}



public class WeaponShop : MonoBehaviour
{


    public Item[] dArray;

    public GameObject hero;
    public GameObject[] heroes;
    public GameObject PlayerInv;
    public bool resting = false;
    public int j = 0;
    public int k = 0;
    public int i = 0;
    public int l;
    public int maxCapacity = 9;
    public float heroGold;
    public int costToPurchase;
    public string weaponName;
    public GameObject[] weapons;
    public int arrayLength;
    public int placeHolderI;
    public string weaponNameToDestroy;
    
    // Start is called before the first frame update
    void Start()
    {
        heroes = new GameObject[10];
        dArray = new Item[2]
        
    {
        new Item(1,"Dagger",10,1,weapons[0]),
        new Item(2,"Sword",20,2,weapons[1])
    };



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider");
        if(!other.isTrigger)
        {
            hero = other.gameObject;

            if(RestTest())
            {
                //HeroActivity currentActivity = 
                //new HeroActivity
                //{
                //    hero = hero,
                //    leaveTime = DateTime.UtcNow.AddSeconds(5),
                //};
                //heroes.Where();

                
                heroes[j] = hero;
                Invoke("RestTime",5);
                l = dArray.Length;
                Shopping();
                heroes[j].SetActive(false);
                if(j != maxCapacity)
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
        if(!(hero.tag == "Hero" && hero.GetComponent<CommonVariables>().shopping == true))
        {
            return false;
        } 

        return true;

    }

    void Shopping()
    {
        

        arrayLength = dArray.Length - 1;

        for (int i = arrayLength; i >= 0; i --)
        {
            Debug.Log("Test0");
            if (dArray[i].itemID != heroes[j].GetComponent<CommonVariables>().weaponID)
            {

                Debug.Log("test1");
                if (heroes[j].GetComponent<CommonVariables>().gold >= dArray[i].cost)
                {
                    Debug.Log("Test2");
                    //if (dArray[i].weapon == heroes[j].GetComponent<CommonVariables>().weaponSlot.}
                    heroes[j].GetComponent<CommonVariables>().gold -= dArray[i].cost;
                    PlayerInv.GetComponent<PlayerInventory>().gold += dArray[i].cost;
                    Debug.Log(i);
                    if (heroes[j].GetComponent<CommonVariables>().weaponID != 0)
                    {
                        heroes[j].GetComponent<CommonVariables>().equipmentUpdate = true;
                        //placeHolderI = i;
                        //for(i = placeHolderI; i >= 0; i --)
                        //{
                        //    if (dArray[i].itemID == heroes[j].GetComponent<CommonVariables>().weaponID)
                        //    {
                        //        weaponNameToDestroy = dArray[i].itemName;
                        //        Debug.Log(weaponNameToDestroy);
                        //        break;
                        //    }
                        //}


                    }
                    //Destroy(heroes[j].child.find(weaponNameToDestroy); 


                    GameObject weaponName = Instantiate(dArray[i].weapon, heroes[j].GetComponent<CommonVariables>().weaponSlot.position, Quaternion.identity, heroes[j].transform);
                    weaponName.transform.parent = heroes[j].GetComponent<CommonVariables>().weaponSlot.transform;
                    heroes[j].GetComponent<CommonVariables>().weaponID = dArray[i].itemID;
                    weaponName.SetActive(true);
                    break;


                }
            }
            else
            {
                Debug.Log("Test3");
                break;
            }
                

        }

        

    }

    void RestTime()
    {
        heroes[k].GetComponent<CommonVariables>().shopping = false;
        Debug.Log(heroes[k].GetComponent<CommonVariables>().shopping);
        heroes[k].SetActive(true);

        if( k != maxCapacity)
        {
            k = k + 1;
        } else {
            k = 0;
        }
       

    }
}
