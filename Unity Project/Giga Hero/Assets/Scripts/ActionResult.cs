using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ActionResult
    {
        public static ActionResult LEVEL_UP = new LevelUpResult();
        public static ActionResult NOTHING = new ActionResult();

        public static Func<GigaHero, ActionResult> Nothing(Action<GigaHero> action)
        {
            return (engine) =>
            {
                action.Invoke(engine);
                return NOTHING;
            };
        }

        public virtual void PerformResult(GameState gameState)
        {

        }

    }


    public class LevelUpResult : ActionResult
    {
        public override void PerformResult(GameState gameState)
        {
            gameState.LevelUp();
        }
    }

}