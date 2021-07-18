using System.Collections;
using UnityEngine;
using System;

namespace Assets.Scripts.Screen
{
    public interface IScreen
    {

        public abstract void Activate(GigaHero engine);
        Func<GigaHero, ActionResult> GetActionA();
        Func<GigaHero, ActionResult> GetActionB();
        Func<GigaHero, ActionResult> GetActionC();
        GameObject GetGameObject();
    }

}