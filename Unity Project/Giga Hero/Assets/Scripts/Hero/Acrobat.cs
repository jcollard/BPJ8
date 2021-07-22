﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Acrobat : SuperHero
    {
        public override RuntimeAnimatorController GetAnimatorController()
        {
            return ObjectLookup.AcrobatAnimatorController;
        }
    }
}