using UnityEngine;
using System;

namespace Assets.Scripts.Screen
{
    public class ItemOption : MonoBehaviour
    {

        public virtual ActionResult Select(GigaHero engine)
        {
            engine.SetActiveScreen(engine.GameState);
            return ActionResult.NOTHING;
        }

    }
}