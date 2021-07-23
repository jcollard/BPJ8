using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public enum TeenState
    {
        IDLE, EATING, JOG, STUDY, WEIGHTS
    }

    public class Teen : Hero
    {

        private TeenState _state = TeenState.IDLE;
        private int _dex = 0;
        private int _str = 0;
        private int _int = 0;
        private int _nutrition = 0;
        private readonly int MAX_NUTRITION = 100;
        private int _saturation = 80;
        private readonly int MAX_SATURATION = 100;
        private int _energy = 60;
        private readonly int MAX_ENERGY = 60;
        private readonly int EVOLVE_AT = 500; // 15 full workouts before food
        private Food _food = null;

        public Teen()
        {

        }

        public Teen(Baby previous) : base(previous)
        {
            _dex = previous.Dex;
            _str = previous.Str;
            _int = previous.Int;
            _nutrition = previous.Nutrition;
            _saturation = previous.Saturation;
            _energy = previous.Energy;
            EVOLVE_AT += _dex + _str + _int;
        }

        public override ActionResult Poke()
        {
            //TODO: Scowl
            _state = TeenState.IDLE;
            return ActionResult.NOTHING;
        }

        public override Hero LevelUp(GameState state)
        {
            if (_str >= _dex && _str >= _int)
            {
                return new Hulk();
            } if (_dex >= _str && _dex >= _int)
            {
                return new Acrobat();
            } else // _int is greatest?
            {
                return new Telepath();
            }
        }

        public override ActionResult Tick()
        {
            base.Tick();

            if(_str + _dex + _int >= EVOLVE_AT)
            {
                return ActionResult.LEVEL_UP;
            }

            if(_state == TeenState.JOG)
            {
                Jog();
            }

            if(_state == TeenState.STUDY)
            {
                Study();
            }

            if(_state == TeenState.WEIGHTS)
            {
                Weights();
            }

            if(_state == TeenState.EATING)
            {
                Eat();
            }

            if(_state == TeenState.IDLE)
            {
                Idle();
            }

            if(_saturation > _nutrition)
            {
                _saturation--;
            }

            if(_saturation <= 0)
            {
                _nutrition--;
            }

            _saturation = Mathf.Clamp(_saturation, 0, MAX_SATURATION);
            _nutrition = Mathf.Clamp(_nutrition, 0, MAX_NUTRITION);
            _energy = Mathf.Clamp(_energy, 0, MAX_ENERGY);

            return ActionResult.NOTHING;
        }

        private void Idle()
        {
            if (_saturation > 0 && _energy < MAX_ENERGY) // Convert saturation to energy
            {
                _saturation--;
                _energy++;
            }
        }

        private void Eat()
        {
            if(Age % 3 != 0) // Only eat every 3 ticks
            {
                return;
            }
            _saturation += _food.SATURATION;
            if (_saturation >= MAX_SATURATION)
            {
                _saturation = MAX_SATURATION;
                _state = TeenState.IDLE;
                return;
            }
            if (Age % 6 == 0)
            {
                //Only increase stats every other saturation tick.
                _str += _food.STR;
                _dex += _food.DEX;
                _int += _food.INT;
            }
            _nutrition += _food.NUTRITION;
            
        }

        private void Jog()
        {
            // Uses energy and nutrition and increases dex
            if (_energy <= 0 || _nutrition <= 0)
            {
                _state = TeenState.IDLE;
                return;
            }
            _energy--;
            _nutrition--;
            _dex++;
        }

        private void Study()
        {
            // Uses saturation and nutrition and increases int
            if (_saturation <= 0 || _nutrition <= 0)
            {
                _state = TeenState.IDLE;
                return;
            }
            _saturation--;
            _nutrition--;
            _int++;
        }

        private void Weights()
        {
            // Uses saturation and energy and increases strength
            if (_saturation <= 0 || _energy <= 0)
            {
                _state = TeenState.IDLE;
                return;
            }
            _saturation--;
            _energy--;
            _str++;
        }


        public override Transition GetTransition()
        {
            //TODO
            return ObjectLookup.BabyToTeen;
        }

        public override void HandleAnimator(Animator animator, GameState gameState)
        {
            animator.SetBool("isEating", _state == TeenState.EATING);
            animator.SetBool("isRunning", _state == TeenState.JOG);
            animator.SetBool("isReading", _state == TeenState.STUDY);
            animator.SetBool("isLifting", _state == TeenState.WEIGHTS);
        }

        public override RuntimeAnimatorController GetAnimatorController()
        {
            return ObjectLookup.TeenAnimatorController;
        }

        internal override ActionResult Feed(Food food)
        {
            _state = TeenState.EATING;
            this._food = food;
            return ActionResult.NOTHING;
        }

        public override ActionResult Train(Training training)
        {
            _state = training.TeenState;
            return ActionResult.NOTHING;
        }

        public override string Status()
        {
            int maxStat = Mathf.Max(_str, _dex, _int, 200);
            return $@"
AGE:TEEN
{StatusHelper.Draw("ENG", _energy, MAX_ENERGY, 6)}
{StatusHelper.Draw("NTR", _nutrition, MAX_NUTRITION, 6)}
{StatusHelper.Draw("SAT", _saturation, MAX_SATURATION, 6)}
{StatusHelper.Draw("STR", _str, maxStat, 6)}
{StatusHelper.Draw("SPD", _dex, maxStat, 6)}
{StatusHelper.Draw("INT", _int, maxStat, 6)}
                ".Trim();
        }
    }
}