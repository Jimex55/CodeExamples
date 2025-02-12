using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.TestTools;

[Serializable]
public class achievementData
{
    public int achID;
    public string achName;
    public string achSearchNum;
    public string achDescription;

    public bool soloNum;

    public bool completed;
    
    
}

public class AchievementCheck : MonoBehaviour
{
  public string numToString;
  public int tempNum = 0;
  public int amountInARow = 1;
  public int mostInRow = 1;
  public float previousNum;
  public bool paladromeAch, binaryAch, allOfAKind = false;
  public achievementData[] array;
    // Start is called before the first frame update
  void Start()
  {
    GameEventSystem.current.onAchievementCheck += CheckAchievement;
  }

    // Update is called once per frame
  void Update()
  {
        
  }

  public void CheckAchievement(float number)
  {
    Debug.Log(number);

    numToString = number.ToString();

    for (int i = 0; i < array.Length; i++)
    {
      if(array[i].completed != true)
      {
        if(array[i].soloNum == false)
        {
          if (numToString.Contains(array[i].achSearchNum))
          {
            Debug.Log("This is an achievemnt");
            Debug.Log(numToString + "" + array[i].achName);
            GameEventSystem.current.AchievementBanner(array[i].achName, array[i].achDescription, numToString);
            array[i].completed = true;
                
          }
        }
        else
        {
          if (numToString == array[i].achSearchNum)
          {
            Debug.Log("This is an achievemnt");
            Debug.Log(numToString + "" + array[i].achName);
            array[i].completed = true;
          }
        }
      }
    }

    if(paladromeAch == false)
    {
      char[] stringArray = numToString.ToCharArray();
      Array.Reverse(stringArray);
      string reverseNum = new string(stringArray);

      if(numToString == reverseNum)
      {
        paladromeAch = true;
        Debug.Log("Its a paladrome!");
      }

    }

    if( Mathf.Abs(previousNum - number) <= 1000 )
    {
      Debug.Log("Get within 1k");
    } else if(Mathf.Abs(previousNum - number) <= 10000 )
    {
      Debug.Log("Get within 10k");
    }

    if(allOfAKind == false)
    {
      for(int i = 0; i < numToString.Length; i++)
      {
        if (tempNum == numToString[i])
        {
          amountInARow += 1;
        }
        else{
          tempNum = numToString[i];

          if(amountInARow >= 3)
          {
            mostInRow = amountInARow;
          }

          amountInARow = 1;
        }

      }

        if (amountInARow >= 3)
        {
          mostInRow = amountInARow;
          amountInARow = 1;
        }

      switch (mostInRow)
      {
        case 3:
          SomeOfAKind();
          break;

        case 4:
          SomeOfAKind();
          break;

        case 5:
          SomeOfAKind();
          break;

        case 6:
          SomeOfAKind();
          break;

        default:
          mostInRow = 1;
          amountInARow = 1;
          tempNum = 0;
          break;
      }
    }

    previousNum = number;


  }
  void SomeOfAKind()
  {
    Debug.Log(mostInRow + " Of A kind!");
    Debug.Log("AChievement");
    mostInRow = 1;
    amountInARow = 1;
    tempNum = 0;
  }
}


