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
        private int _dex = Random.Range(0, 100);
        public int Dex { get { return _dex; } }
        private int _str = Random.Range(0, 100);
        public int Str { get { return _str; } }
        private int _int = Random.Range(0, 100);
        public int Int {  get { return _int; } }
        private int _nutrition = 0;
        public int Nutrition { get { return _nutrition; } }
        private readonly int MAX_NUTRITION = 100;
        private int _saturation = 60;
        public int Saturation {  get { return _saturation;  } }
        private readonly int MAX_SATURATION = 100;
        private int _energy = 60;
        public int Energy {  get { return _energy; } }
        private readonly int MAX_ENERGY = 60;
        private int _sleep = 0;
        private Hero leveledUp;
        private float _lastNotified = 0;

        public Baby()
        {

        }

        public Baby(Egg previous) : base(previous)
        {
        }

        public override ActionResult Poke()
        {
            isPacifier = !isPacifier;
            if (_sleep > 0)
            {
                //TODO: If the baby is sleeping, wake it up and lose points instead of gaining them.
                //TODO: Baby Woke state. Pacifier will make baby "heal" faster. When energy below 0, hurt baby stats
                _sleep = 0;
            }
            return ActionResult.NOTHING;
        }

        public override Hero LevelUp(GameState state)
        {
            if (leveledUp == null)
            {
                leveledUp = new Teen(this);
            }
            return leveledUp;
        }

        public override ActionResult Tick()
        {
            base.Tick();
            if(_nutrition >= MAX_NUTRITION)
            {
                //When the baby has enough nutrients, level it up!
                return ActionResult.LEVEL_UP;
            }

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
                    // If your saturation is at 0, lose nutrition and lose stats proportional to their values
                    _nutrition--;
                    _str -= _str/MAX_ENERGY;
                    _int -= _int/MAX_ENERGY;
                    _dex -= _dex/MAX_ENERGY;
                    _nutrition = Mathf.Max(0, _nutrition);
                }
            } else // Transition to sleeping
            {
                _sleep = MAX_ENERGY;
            }



            checkState();

            if (_state == BabyState.CRYING)
            {
                if (Time.time > _lastNotified + 5f)
                {
                    SoundBoard.INSTANCE.Notification.Play();
                    _lastNotified = Time.time + 5f;
                }
            } else
            {
                _lastNotified = 0;
            }

            return ActionResult.NOTHING;
        }

        private void checkState()
        {
            if (_saturation > MAX_SATURATION)
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
            
            _saturation += food.SATURATION;
            if (_saturation > MAX_NUTRITION)
            {
                //TODO: If you over feed the baby, over saturate it and lose stats rather than gain them
                SoundBoard.INSTANCE.NoNo.Play();
                _saturation = 112;
                _sleep = 0;
                _str = Mathf.Max(0,_str - food.STR * 3);
                _dex = Mathf.Max(0,_dex - food.DEX * 3);
                _int = Mathf.Max(0,_int - food.INT * 3);
                _nutrition = Mathf.Max(0, _nutrition/4);
                checkState();
                return ActionResult.NOTHING;
            }
            if (_sleep > 0)
            {
                SoundBoard.INSTANCE.NoNo.Play();
                //TODO: If the baby is sleeping, wake it up and lose points instead of gaining them.
                //TODO: Baby Woke state. Requires pacifier to go back to sleep. When energy below 0, hurt baby stats
                //_sleep = 0;
                //_str -= food.STR;
                //_dex -= food.DEX;
                //_int -= food.INT;
                //_nutrition -= food.NUTRITION;
            }
            else
            {
                SoundBoard.INSTANCE.PowerUp.Play();
                _str += food.STR;
                _dex += food.DEX;
                _int += food.INT;
                _nutrition += food.NUTRITION;
            }
            
            checkState();
            return ActionResult.NOTHING;
        }

        public override ActionResult Train(Training training)
        {
            //TODO: Make baby cry
            SoundBoard.INSTANCE.NoNo.Play();
            return ActionResult.NOTHING;
        }

        public override string Status()
        {
            int maxStat = Mathf.Max(_str, _dex, _int, 200);
            return $@"
AGE:BABY
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