using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hulk : SuperHero
    {
        public override RuntimeAnimatorController GetAnimatorController()
        {
            return ObjectLookup.HulkAnimatorController;
        }

    }
}