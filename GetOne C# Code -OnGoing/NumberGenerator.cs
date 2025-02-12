using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.UIElements;


public class NumberGenerator : MonoBehaviour
{
    public TMP_Text messageText;
    public TMP_Text lowestNumText;
    public bool isLowest = false;
    private float lowestNumber = 0;
    public int numOfTaps;

    private float animationTime = 1.5f;
    private float i;
    private float currentNumber = 0;
    private float tickChange;
    public bool createNumber = false;
    private int desiredNumber, initialNumber;
    public TMPro.TMP_Text numberText;
    public Vector3 ghostNumberSpawn;
    int clickTextAnimTimeMs = 5000;
    public Transform ghostNumPanel;
    int maxConcurrentGhostTexts = 10;
    [SerializeField]
    List<TMPro.TMP_Text> concurrentGhostTexts = new List<TMPro.TMP_Text>();
    [SerializeField]
    
    // Start is called before the first frame update
    private void Start()
    {
        GameEventSystem.current.onEndTouch += GenerateNum;
        ghostNumberSpawn = this.transform.position;
        initialNumber = 0;
        
    }

    // public void SetNumber(float value)
    // {
    //     initialNumber = currentNumber;
    //     desiredNumber = value;
    // }
    // public void AddToNumber(float value)
    // {
    //     initialNumber = currentNumber;
    //     desiredNumber += value;
    // }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            GenerateNumber(new Vector3(0, 0, 0), 1, 123321);
        }

        if (createNumber == true)
        {
            if(currentNumber != desiredNumber)
            {
                if (initialNumber < desiredNumber)
                {
                    currentNumber += (animationTime * Time.deltaTime) * (desiredNumber - initialNumber);
                    Debug.Log("Test1");
                    if(currentNumber >= desiredNumber)
                    {
                        currentNumber = desiredNumber;
                    }
                        
                }
                else if(initialNumber > desiredNumber)
                {
                    currentNumber -= (animationTime * Time.deltaTime) * (initialNumber - desiredNumber);
                    Debug.Log("Test2");
                    if(currentNumber <= desiredNumber)
                    {
                        currentNumber = desiredNumber;
                    }
                        
                }

                messageText.SetText(currentNumber.ToString("0"));

            }
            else
            {
                CheckLowest();
                GameEventSystem.current.AchievementCheck(desiredNumber);
                GameEventSystem.current.BroadcastNumber(desiredNumber);
                SpawnGhostText();
                initialNumber = desiredNumber;
                isLowest = false;
                Debug.Log("IsNotLowest");
                createNumber = false;
            }           
        }
    }

    public void SpawnGhostText()
    {
        if (concurrentGhostTexts.Count >= maxConcurrentGhostTexts)
        {
            Destroy(concurrentGhostTexts[0].gameObject);
            concurrentGhostTexts.RemoveAt(0);
        }

        TMPro.TMP_Text nText = Instantiate(numberText, ghostNumberSpawn, new Quaternion(), ghostNumPanel);
        concurrentGhostTexts.Add(nText);
        nText.GetComponent<GhostNumberMove>().StartRiseAnimation(desiredNumber.ToString("0"), clickTextAnimTimeMs, isLowest);
    }
    public void CheckLowest()
    {
        if (lowestNumber > desiredNumber || lowestNumber == 0)
        {
            lowestNumber = desiredNumber;
            isLowest = true;
            Debug.Log("Islowest");
            lowestNumText.SetText(lowestNumber.ToString("0"));
        }
       
    }

    void GenerateNum(Vector3 wpos, float time)
    {
        GenerateNumber(wpos, time);
    }

    // Update is called once per frame
  private void GenerateNumber(Vector3 wpos, float time, int num = 0)
  {

    if (!createNumber)

    {
        desiredNumber = num < 1 ? Random.Range(1, 1000002) : num;
        numOfTaps +=  1;
        createNumber = true;
    }
    // i = Random.Range(1f, 1000001f);

    // tickChange = (i - currentNumber); 
    // tickChange = Mathf.Abs(tickChange);


    // while(i != currentNumber)
    // {
    //     messageText.SetText(currentNumber.ToString());
    //     if (currentNumber > i)
    //     {
    //         currentNumber = currentNumber - tickChange;
    //     }
    //     else
    //     {
    //         currentNumber = currentNumber + tickChange;
    //     }
    // }

    // messageText.SetText(i.ToString("0"));
    // currentNumber = i;
  }
}
