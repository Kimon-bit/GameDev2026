using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public static RunManager Instance;

    public int currentLevel = 1;

    public List<UpgradeData> acquiredUpgrades = new();

    void Awake()
    {
        Instance = this;
    }

    public void AddUpgrade(UpgradeData upgrade)
    {
        acquiredUpgrades.Add(upgrade);
    }

    public void NextLevel()
    {
        currentLevel++;
    }
}