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

        public virtual ActionResult Tick()
        {
            _age++;
            return ActionResult.NOTHING;
        }

    }

    
    
}