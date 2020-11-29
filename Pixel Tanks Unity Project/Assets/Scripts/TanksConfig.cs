using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TanksConfig", menuName = "Create Tank Config")]
public class TanksConfig : ScriptableObject
{
    public List<TankModel> Tanks;
}
