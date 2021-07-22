using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Hulk : Hero
    {
        public override RuntimeAnimatorController GetAnimatorController()
        {
            return ObjectLookup.HulkAnimatorController;
        }

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
    }
}