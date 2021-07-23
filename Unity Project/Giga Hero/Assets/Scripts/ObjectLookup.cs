using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLookup : MonoBehaviour
{

    public StatusScreen _StatusScreen;
    public static StatusScreen StatusScreen;

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

    public RuntimeAnimatorController _HulkAnimatorController;
    public static RuntimeAnimatorController HulkAnimatorController;

    public RuntimeAnimatorController _AcrobatAnimatorController;
    public static RuntimeAnimatorController AcrobatAnimatorController;

    public RuntimeAnimatorController _TelepathAnimatorController;
    public static RuntimeAnimatorController TelepathAnimatorController;

    public OptionsMenu _OptionsMenu;
    public CreditsScreen CreditsScreen;

    public void Start()
    {
        

        
        EggAnimatorController = _EggAnimatorController;
        EggToBaby = EggToBabyTransition;

        BabyAnimatorController = _BabyAnimatorController;
        BabyToTeen = BabyToTeenTransition;

        TeenAnimatorController = _TeenAnimatorController;

        TelepathAnimatorController = _TelepathAnimatorController;
        AcrobatAnimatorController = _AcrobatAnimatorController;
        HulkAnimatorController = _HulkAnimatorController;

        StatusScreen = _StatusScreen;
        OptionsMenu.INSTANCE = _OptionsMenu;
        CreditsScreen.INSTANCE = CreditsScreen;
    }



}
