using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class ButtonAction
    {

        public abstract ActionResult PerformAction(GameState state);
    }
}