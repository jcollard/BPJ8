using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameState
    {

        private Hero _hero;
        private readonly GigaHero _engine;

        public Hero Hero { get { return _hero; } }

        public GameState(GigaHero engine)
        {
            this._engine = engine;
            this._hero = new Egg();
        }

        public ActionResult Poke()
        {
            return _hero.Poke();
        }

        public bool LevelUp()
        {
            Hero nextHero = _hero.LevelUp();
            if(nextHero != _hero)
            {
                this._hero = nextHero;
                return true;
            }
            return false;
        }

    }

    public class ActionResult
    {
        public static ActionResult SHAKE = new ActionResult();
        public static ActionResult LEVEL_UP = new ActionResult();
        public static ActionResult NOTHING = new ActionResult();

    }

    public abstract class Hero
    {

        public abstract ActionResult Poke();
        public abstract Hero LevelUp();

        public virtual Transition GetTransition()
        {
            return null;
        }
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

        public override Hero LevelUp()
        {
            return this;
        }

        public override Transition GetTransition()
        {
            Debug.Log("Getting Transition Sprite");
            return ObjectLookup.EggToBaby;
        }
    }
    public class Egg : Hero
    {

        private bool isCracking;
        private int poked = 0;

        public override ActionResult Poke()
        {
            int chance = Random.Range(0, 100) + poked;
            if(poked > 3 && chance > 75 && !this.isCracking)
            {
                this.isCracking = true;
                return ActionResult.LEVEL_UP;
            }
            poked++;
            return ActionResult.SHAKE;
        }

        public override Hero LevelUp()
        {
            return new Baby();
        }

    }

}