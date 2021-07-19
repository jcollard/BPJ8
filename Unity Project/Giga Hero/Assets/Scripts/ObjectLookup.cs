using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLookup : MonoBehaviour
{

    public Transition EggToBabyTransition;
    public RuntimeAnimatorController _EggAnimatorController;
    public static Transition EggToBaby;
    public static RuntimeAnimatorController EggAnimatorController;
    
    
    public Transition BabyToTeenTransition;
    public static Transition BabyToTeen;
    public RuntimeAnimatorController _BabyAnimatorController;
    public static RuntimeAnimatorController BabyAnimatorController;

    public RuntimeAnimatorController _TeenAnimatorController;
    public static RuntimeAnimatorController TeenAnimatorController;

    public void Start()
    {
        

        
        EggAnimatorController = _EggAnimatorController;
        EggToBaby = EggToBabyTransition;

        BabyAnimatorController = _BabyAnimatorController;
        BabyToTeen = BabyToTeenTransition;

        TeenAnimatorController = _TeenAnimatorController;
    }



}
