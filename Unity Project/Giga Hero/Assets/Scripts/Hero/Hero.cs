using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Hero
    {

        private int _age = 0;
        public int Age {  get { return _age; } }
        public abstract ActionResult Poke();
        public abstract Hero LevelUp(GameState state);

        public Hero()
        {

        }

        public Hero(Hero previous)
        {
            _age = previous._age;
        }

        public virtual Transition GetTransition()
        {
            return null;
        }

        internal abstract ActionResult Feed(Food food);
        public abstract void HandleAnimator(Animator animator, GameState gameState);

        public abstract RuntimeAnimatorController GetAnimatorController();

        public abstract ActionResult Train(Training training);

        public virtual ActionResult Tick()
        {
            _age++;
            return ActionResult.NOTHING;
        }

        public abstract string Status();

    }

    
    public class StatusHelper
    {
        public static string Draw(string word, int value, int max, int length)
        {
            float percent = Mathf.Min(1.0F, ((float)value) / ((float)max)); ;
            int stars = Mathf.RoundToInt(percent * length);
            return word + ":" + "".PadLeft(stars, '*');
        }

        public static string Draw(string word, int value, int max)
        {
            return Draw(word, value, max, 6);
        }
    }
    
}