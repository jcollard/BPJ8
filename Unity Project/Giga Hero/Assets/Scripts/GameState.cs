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

    }

    public class ActionResult
    {
        public static ActionResult SHAKE = new ActionResult();
        public static ActionResult CRACK = new ActionResult();
        public static ActionResult NOTHING = new ActionResult();

    }

    public abstract class Hero
    {

        public abstract ActionResult Poke();

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
                return ActionResult.CRACK;
            }
            poked++;
            return ActionResult.SHAKE;
        }

    }

}