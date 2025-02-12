using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class MoveAgent : MonoBehaviour
{
    Renderer ren;
    public Transform home;
    public Transform shop;
    public Vector3 battleSpot;
    public Transform zombieSpawn;
    public bool battleSpotReached;
    public NavMeshAgent agent;
    public bool battleSpotAssigned = false;
    public Vector3 startingPosition;
    public bool enemySpawned = false;
    public int i;
    public int j;
    public bool waiting = false;
    public GameObject enemy;
    public bool fighting = false;
    public bool attacking = false;
    public bool attackCooldown = false;
    public bool battleTimer = false;
    public bool battleDone = false;
    public string myClass = "Warrior";
    public CommonVariables commonScript;
    public bool waitingForSave = false;
    public Rigidbody rb;
    public GameObject hospital;
    public bool retreating = false;
    public int hitDamage;

    [SerializeField]
    private GameObject zombie;


    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ren = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        commonScript = gameObject.GetComponent<CommonVariables>();
        startingPosition = home.position;
        i = 600;
        j = 800;

        
    
    }

    void OnTriggerStay(Collider other)
    {

        if(other.gameObject.tag == "enemy" && fighting == false && !other.isTrigger)
        {
            fighting = true;
            enemy = other.gameObject;
        }
    }

    void Update()
    {
        CheckHealth();
        CheckEquipment();

        if(commonScript.mindMode == 1)
        {
            Hunting();
        }
        else if (commonScript.mindMode == 2)
        {
            Battle();       
        }
        else if (commonScript.mindMode == 3)
        {
            TownTime();
        }
        else if (commonScript.mindMode == 4)
        {
            Dying();
        }
    }

    void CheckEquipment()
    {
        if(commonScript.equipmentUpdate)
        { 
            Destroy(commonScript.weaponSlot.GetChild(0).gameObject);
            commonScript.equipmentUpdate = false;
        }
    }

    void Hunting()
    {
        if(!battleSpotAssigned)
        {
            startingPosition = home.position;
            i = 600;
            j = 800;
            battleSpot = GetRoamingPosition();
            agent.destination = battleSpot;
            battleSpotAssigned = true;
        }


        if(!battleSpotReached)
        {

            if ((transform.position - battleSpot).sqrMagnitude <= 20)
            {
                battleSpotReached = true;
            }
        }

        if (battleSpotReached)
        {
            if (!waiting)
            {
                Invoke("Waiting", 3);
                waiting = true;
            }

        }


    }

    void Battle()
    {

        if(!battleTimer && !battleDone)
        {
            i = Random.Range(40, 100);
            Invoke("BattleTimer", i);
            battleTimer = true;
        }


        if(!enemySpawned)
        {  
            startingPosition = agent.transform.position; 
            i = 80;
            j = 120;
            GameObject newZombie = Instantiate(zombie, GetRoamingPosition(), Quaternion.identity);
            enemySpawned = true;
        }
        else if (fighting == true)
        {

            if((agent.transform.position - enemy.transform.position).sqrMagnitude > 150 && attacking == false)
            {
                agent.destination = enemy.transform.position;
            }

            if((agent.transform.position - enemy.transform.position).sqrMagnitude < 150 && attacking == false)
            {
                agent.isStopped = true;
                attacking = true;
            }

            if (attacking == true && attackCooldown == false)
            {
                if (enemy.activeInHierarchy)
                {
                    hitDamage = Random.Range(commonScript.minDamage, (commonScript.maxDamage + 1));
                    enemy.GetComponent<CommonEnemyScript>().hp -= hitDamage;
                    attackCooldown = true;
                    Invoke("Cooldown", commonScript.attackCooldown);

                } else {
                    battleSpotAssigned = false;
                    battleSpotReached = false;
                    attacking = false;
                    agent.isStopped = false;
                    fighting = false;
                    enemySpawned = false;
                    commonScript.gold += enemy.GetComponent<CommonEnemyScript>().goldReward;
                    commonScript.xp += enemy.GetComponent<CommonEnemyScript>().xpReward;


                    if(battleDone == true)
                    {
                        commonScript.mindMode = 3;
                        commonScript.rest = true;
                        //rest = true;
                        battleDone = false;

                    }
                }


            }



        }
    }

    void TownTime()
    {
        agent.destination = home.position;


        

        if (commonScript.rest == false)
        {
            VisitShops();

            if (commonScript.shopping == false)
            { 
                commonScript.mindMode = 1; 
            }
                
      
        }
    }

    void VisitShops()
    {
        agent.destination = shop.position;
        
    }



    void Dying()
    {
        if(!commonScript.waitingForSave)
        {
            transform.gameObject.tag = "Unconscious";
            gameObject.transform.Rotate(0, 0, 90, Space.Self);
            commonScript.waitingForSave = true;
            GameEventSystem.current.SaveHeroes(gameObject);
        }
    }

    void CheckHealth()
    {
        if(commonScript.hp <= 0 && commonScript.waitingForSave == false)
        {
            
            //agent.isStopped = true;
            agent.destination = transform.position;
            rb.isKinematic = true;
            agent.enabled = false;
            CancelInvoke("BattleTimer");
            
            commonScript.mindMode = 4;
        }

        //if(commonScript.hp <= (commonScript.maxhp * 0.2) && retreating == false)
        //{
        //    commonScript.mindMode = 3;
        //    commonScript.healBoolReset = true;
        //    commonScript.rest = true;
        //    retreating = true;
        //}

        if(commonScript.healBoolReset == true)
        {
            fighting = false;
            attacking = false;
            battleTimer = false;
            commonScript.healBoolReset = false;
            battleSpotReached = false;
            enemySpawned = false;
            battleSpotAssigned = false;
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(i, j);
    } 
    // gets a random  coordinate 

    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f,1f), 0, UnityEngine.Random.Range(-1f,1f)).normalized;

    }
    // gets a random direction 

    void Cooldown()
    {
        attackCooldown = false;
    }

    void Waiting()
    {
        commonScript.mindMode = 2;
        waiting = false;
    }

    void BattleTimer()
    {
        battleDone = true;
        battleTimer = false;
    }

}
