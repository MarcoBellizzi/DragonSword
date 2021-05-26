using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int health { get; private set; }
    public int maxHealth { get; private set; }
    public int healthPackValue { get; private set; }
    public int barValueDamage { get; private set; }
    public void Startup()
    {
        Debug.Log("Player manager starting...");
        health = 100;
        healthPackValue = 20;
        status = ManagerStatus.Started;
    }
}