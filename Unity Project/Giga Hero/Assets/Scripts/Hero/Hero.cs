using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Hero
    {

        public abstract ActionResult Poke();
        public abstract Hero LevelUp(GameState state);

        public virtual Transition GetTransition()
        {
            return null;
        }

        public abstract void HandleAnimator(Animator animator, GameState gameState);
    }

    public enum BabyState
    {
        HAPPY, PACIFIER, HUNGRY, CRYING, SLEEPING
    }

    public class Baby : Hero
    {

        private BabyState _state = BabyState.PACIFIER;

        public override ActionResult Poke()
        {
            return ActionResult.NOTHING;
        }

        public override Hero LevelUp(GameState state)
        {
            return this;
        }

        public override Transition GetTransition()
        {
            //TODO
            return null;
        }

        public override void HandleAnimator(Animator animator, GameState gameState)
        {
            animator.SetBool("isPacifier", true);
        }
    }
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
                this.isCracking = true;
                return ActionResult.LEVEL_UP;
            }
            poked++;
            if (Time.time > shakeUntil)
            {
                shakeUntil = Time.time + 1.0F;
            }
            return ActionResult.NOTHING;
        }

        public override Hero LevelUp(GameState state)
        {
            return new Baby();
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
    }
}