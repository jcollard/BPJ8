using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public enum BabyState
    {
        HAPPY, PACIFIER, HUNGRY, CRYING, SLEEPING
    }

    public class Baby : Hero
    {

        private BabyState _state = BabyState.PACIFIER;
        private bool isPacifier = false;
        private int _dex = 0;
        private int _str = 0;
        private int _int = 0;
        private int _nutrition = 0;
        private int _saturation = 80;
        private int _maxSaturation = 100;

        public Baby()
        {

        }

        public Baby(Egg previous) : base(previous)
        {
        }

        public override ActionResult Poke()
        {
            isPacifier = !isPacifier;
            return ActionResult.NOTHING;
        }

        public override Hero LevelUp(GameState state)
        {
            return this;
        }

        public override ActionResult Tick()
        {
            base.Tick();
            _saturation--;
            checkState();
            return ActionResult.NOTHING;
        }

        private void checkState()
        {
            if (_saturation > _maxSaturation)
            {
                _state = BabyState.CRYING;
            }
            else if (_saturation < 25)
            {
                _state = BabyState.CRYING;
            }
            else if (_saturation < 50)
            {
                _state = BabyState.HUNGRY;
            } else
            {
                _state = isPacifier ? BabyState.PACIFIER : BabyState.HAPPY;
            }
            
        }


        public override Transition GetTransition()
        {
            //TODO
            return null;
        }

        public override void HandleAnimator(Animator animator, GameState gameState)
        {
            animator.SetBool("isPacifier", _state == BabyState.PACIFIER);
            animator.SetBool("isHappy", _state == BabyState.HAPPY);
            animator.SetBool("isHungry", _state == BabyState.HUNGRY);
            animator.SetBool("isCrying", _state == BabyState.CRYING);
            animator.SetBool("isSleeping", _state == BabyState.SLEEPING);
        }

        public override RuntimeAnimatorController GetAnimatorController()
        {
            return ObjectLookup.BabyAnimatorController;
        }

        internal override ActionResult Feed(Food food)
        {
            _str += food.STR;
            _dex += food.DEX;
            _int += food.INT;
            _nutrition += food.NUTRITION;
            _saturation += food.SATURATION;
            checkState();
            return ActionResult.NOTHING;
        }


    }
}