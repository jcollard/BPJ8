using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Egg : Hero
    {

        private bool isCracking;
        private int poked = 0;
        private float shakeUntil;

        public override ActionResult Poke()
        {
            int chance = UnityEngine.Random.Range(0, 100) + poked;
            if (poked > 3 && chance > 75 && !this.isCracking)
            {
                return ActionResult.LEVEL_UP;
            }
            poked++;
            if (Time.time > shakeUntil)
            {
                shakeUntil = Time.time + 1.0F;
            }
            return ActionResult.NOTHING;
        }

        public override ActionResult Tick()
        {
            base.Tick();
            if (Age % 5 == 0)
            {
                return Poke();
            }
            return ActionResult.NOTHING;
        }

        public override Hero LevelUp(GameState state)
        {
            this.isCracking = true;
            return new Baby(this);
        }

        public override void HandleAnimator(Animator animator, GameState gameState)
        {
            animator.SetBool("isCracking", this.isCracking);
            animator.SetBool("isShaking", shakeUntil > Time.time);
        }

        public override Transition GetTransition()
        {
            return ObjectLookup.EggToBaby;
        }

        public override RuntimeAnimatorController GetAnimatorController()
        {
            return ObjectLookup.EggAnimatorController;
        }

        internal override ActionResult Feed(Food food)
        {
            //TODO: Show food being thrown? 
            return Poke();
        }

        public override ActionResult Train(Training training)
        {
            return Poke();
        }
    }
}