using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PokeButtonAction : ButtonAction
    {

        public override ActionResult PerformAction(GameState gameState)
        {
            return gameState.Poke();
        }
    }
}