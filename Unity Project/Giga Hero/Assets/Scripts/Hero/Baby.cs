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
        public int Dex { get { return _dex; } }
        private int _str = 0;
        public int Str { get { return _str; } }
        private int _int = 0;
        public int Int {  get { return _int; } }
        private int _nutrition = 0;
        private int _saturation = 80;
        private int _maxSaturation = 100;
        private int _energy = 60;
        private int _sleep = 0;
        private int SLEEP_TICKS = 60;

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
            return new Teen(this);
        }

        public override ActionResult Tick()
        {
            base.Tick();
            if(_sleep > 0) //If the baby is sleeping
            {
                //TODO: If the baby is hungry, they shouldn't restore energy
                _energy++;
                _sleep--;
            } else if (_energy > 0) // If the baby is awake
            {
                _energy--;
                _saturation--;
                _saturation = Mathf.Max(0, _saturation);
                if (_saturation <= 0)
                {
                    _nutrition--;
                    _nutrition = Mathf.Max(0, _nutrition);
                }
            } else // Transition to sleeping
            {
                _sleep = SLEEP_TICKS;
            }
            checkState();
            return ActionResult.NOTHING;
        }

        private void checkState()
        {
            if (_saturation > _maxSaturation)
            {
                _state = BabyState.CRYING;
            }
            else if (_saturation <=  0)
            {
                _state = BabyState.CRYING;
            }
            else if (_saturation <= 25)
            {
                _state = BabyState.HUNGRY;
            } else if (_sleep > 0)
            {
                _state = BabyState.SLEEPING;
            }
            else
            {
                _state = isPacifier ? BabyState.PACIFIER : BabyState.HAPPY;
            }
            
        }


        public override Transition GetTransition()
        {
            return ObjectLookup.BabyToTeen;
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

        public override ActionResult Train(Training training)
        {
            //TODO: Make baby cry
            return ActionResult.NOTHING;
        }
    }
}