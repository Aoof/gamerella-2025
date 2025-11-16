using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayTasks", menuName = "Gamerella/DayTasks")]
public class DayTasks : ScriptableObject
{
    public List<string> tasks = new();
    public List<PointsSystem.CorrectAction> correctActions = new();
}