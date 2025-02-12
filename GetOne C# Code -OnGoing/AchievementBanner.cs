using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BannerData
{
    public string achName;
    public string achDescription;
    public string achNumber;
}

public class AchievementBanner : MonoBehaviour
{
    public TMPro.TMP_Text achievementName;
    public TMPro.TMP_Text achievementDescription;
    public TMPro.TMP_Text numberGot;
    public Vector3 bannerSpawn;
    public Transform mainPanel;
    public bool bannerReady = true;
    public  BannerData[] array;
    string array2;
    public int j = 1;
    public int k = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        GameEventSystem.current.onAchievementBanner += MakeBanner;
        bannerSpawn = transform.position;
        array = new BannerData[10];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeBanner(string name, string description, string number)
    {
        // array[j].achName = name; // FIX!
        // array[j].achDescription = description;
        // array[j].achNumber = number;
        //MakingBanner();
        j += 1;
            

    }

    void MakingBanner()
    {
        while (j != k)
        {
            if (bannerReady == true)
            {
                achievementName.SetText(array[k].achName);
                achievementDescription.SetText(array[k].achDescription);
                numberGot.SetText(array[k].achNumber);
                StartCoroutine(RiseAnimationLowest(1500));
                //Invoke("ResetBanner", 4);
                bannerReady = false;
            }

        }
    }

    // void ResetBanner()
    // {
    //     StopCoroutine("RiseAnimationLowest");
    //     transform.position = bannerSpawn;
    //     bannerReady = true;
    // }

     IEnumerator RiseAnimationLowest(int timeDisapearMs)
    {
        for (int i = 0; i < timeDisapearMs; i+=5) {
            transform.Translate(transform.right*7);
            yield return new WaitForSeconds(0.01f); 
            
            if(i == timeDisapearMs-5)
            {
                transform.position = bannerSpawn;
                k += 1;
                bannerReady = true;
                
            }
        }
    }
}
