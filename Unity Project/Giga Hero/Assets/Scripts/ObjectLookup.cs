using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLookup : MonoBehaviour
{

    public Transition EggToBabyTransition;
    public RuntimeAnimatorController _BabyAnimatorController;
    public RuntimeAnimatorController _EggAnimatorController;

    public static Transition EggToBaby;
    public static RuntimeAnimatorController BabyAnimatorController;
    public static RuntimeAnimatorController EggAnimatorController;

    public void Start()
    {
        EggToBaby = EggToBabyTransition;
        BabyAnimatorController = _BabyAnimatorController;
        EggAnimatorController = _EggAnimatorController;
    }



}
