using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class SuperHero : Hero
    {

        public override void HandleAnimator(Animator animator, GameState gameState)
        {
        }

        public override Hero LevelUp(GameState state)
        {
            return this;
        }

        public override ActionResult Poke()
        {
            return ActionResult.NOTHING;
        }

        internal override ActionResult Feed(Food food)
        {
            return ActionResult.NOTHING;
        }

        public override ActionResult Train(Training training)
        {
            return ActionResult.NOTHING;
        }
    }
}