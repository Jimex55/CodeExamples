using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem current;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action<Vector3,float> onStartTouch;
    public event Action<Vector3,float> onEndTouch;
    public event Action<int> onBroadcastNumber;

    public event Action<Vector3,float> onGenerateNumber;

    public event Action<string> onSwitchMainPanel;

    public event Action< float> onAchievementCheck;
    public event Action<string, string, string> onAchievementBanner;

    public event Action<GameObject> ontechSeleced;

    public event Action onUpdateTechUI;

    public event Action onExitFromMenus;

    public void StartTouch(Vector3 pos, float time){
        if (!GameData.blockTapEvent) onStartTouch?.Invoke(pos, time);
    } 
    public void EndTouch(Vector3 pos, float time){
        if (!GameData.blockTapEvent) onEndTouch?.Invoke(pos, time);
    } 

    public void BroadcastNumber(int number) {  onBroadcastNumber?.Invoke(number); }

    public void AchievementCheck(float number) { onAchievementCheck?.Invoke(number); }

    public void AchievementBanner(string name, string description, string number)
    {
        onAchievementBanner?.Invoke(name, description, number);
    }

    public void GenerateNumber(Vector3 pos, float time) { onGenerateNumber?.Invoke(pos, time); }

    public void SwitchMainPanel(string panel) { onSwitchMainPanel?.Invoke(panel); }

    public void UpdateTechUI() { onUpdateTechUI?.Invoke(); }
    public void ExiFromMenus() { onExitFromMenus?.Invoke(); }

    public void OnTechSelected(GameObject btn) {  ontechSeleced?.Invoke(btn); }
}
