﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Telepath : SuperHero
    {
        public override RuntimeAnimatorController GetAnimatorController()
        {
            return ObjectLookup.TelepathAnimatorController;
        }
    }
}