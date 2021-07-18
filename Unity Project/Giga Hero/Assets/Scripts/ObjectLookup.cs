using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLookup : MonoBehaviour
{

    public Transition EggToBabyTransition;

    public static Transition EggToBaby;

    public void Start()
    {
        EggToBaby = EggToBabyTransition;   
    }



}
