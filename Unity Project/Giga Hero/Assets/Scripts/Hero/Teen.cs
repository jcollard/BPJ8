using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public enum TeenState
    {
        IDLE, EATING
    }

    public class Teen : Hero
    {

        private TeenState _state = TeenState.IDLE;
        private int _dex = 0;
        private int _str = 0;
        private int _int = 0;
        private int _nutrition = 0;
        private int _saturation = 80;
        private int _maxSaturation = 100;
        private int _energy = 60;
        private int _sleep = 0;
        private int SLEEP_TICKS = 60;

        public Teen()
        {

        }

        public Teen(Baby previous) : base(previous)
        {
        }

        public override ActionResult Poke()
        {
            //TODO: Scowl
            return ActionResult.NOTHING;
        }

        public override Hero LevelUp(GameState state)
        {
            return this;
        }

        public override ActionResult Tick()
        {
            base.Tick();
            
            return ActionResult.NOTHING;
        }

        private void checkState()
        {
           
            
        }


        public override Transition GetTransition()
        {
            //TODO
            return null;
        }

        public override void HandleAnimator(Animator animator, GameState gameState)
        {
            animator.SetBool("isEating", _state == TeenState.EATING);
        }

        public override RuntimeAnimatorController GetAnimatorController()
        {
            return ObjectLookup.TeenAnimatorController;
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