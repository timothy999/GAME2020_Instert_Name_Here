using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LeverData", menuName = "ScriptableObjects/LeverManager", order = 1)]
public class LeverManager : ScriptableObject
{
    public int totalAmountOfLevers;
    public int numberOfLeversOn;
}
