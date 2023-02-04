using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStation : Station
{
    protected override void OnPlayerUse()
    {
        Debug.Log("AnimalStuff");
    }
}
