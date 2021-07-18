using Assets.Scripts.Screen;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{
    public class GameState : IScreen
    {

        private Hero _hero;
        private readonly GigaHero _engine;
        private readonly PlayScreen _playScreen;

        public Hero Hero { get { return _hero; } }

        public GameState(GigaHero engine, PlayScreen playScreen)
        {
            if(playScreen == null)
            {
                throw new NullReferenceException("PlayScreen is null");
            }
            this._engine = engine;
            this._hero = new Egg();
            this._playScreen = playScreen;
        }

        public ActionResult Poke()
        {
            return _hero.Poke();
        }

        public void LevelUp()
        {
            Hero nextHero = _hero.LevelUp(this);
            if (nextHero != _hero)
            {
                Transition t = _hero.GetTransition();
                t.StartTransition((_) => this._hero = nextHero);
            }

        }

        public void Activate(GigaHero engine)
        {
            engine.SetActiveScreen(this);
        }

        public Func<GigaHero, ActionResult> GetActionA()
        {
            return ButtonAction.Poke;
        }

        public Func<GigaHero, ActionResult> GetActionB()
        {
            return ButtonAction.OpenActionMenu;
        }

        public Func<GigaHero, ActionResult> GetActionC()
        {
            return ButtonAction.Poke;
        }

        public GameObject GetGameObject()
        {
            return _playScreen.gameObject;
        }
    }

    public class ActionResult
    {
        public static ActionResult LEVEL_UP = new LevelUpResult();
        public static ActionResult NOTHING = new ActionResult();

        public static Func<GigaHero, ActionResult> Nothing(Action<GigaHero> action) {
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

    public abstract class Hero
    {

        public abstract ActionResult Poke();
        public abstract Hero LevelUp(GameState state);

        public virtual Transition GetTransition()
        {
            return null;
        }

        public abstract void HandleAnimator(Animator animator, GameState gameState);
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

        public override Hero LevelUp(GameState state)
        {
            return this;
        }

        public override Transition GetTransition()
        {
            //TODO
            return null;
        }

        public override void HandleAnimator(Animator animator, GameState gameState)
        {
            animator.SetBool("isPacifier", true);
        }
    }
    public class Egg : Hero
    {

        private bool isCracking;
        private int poked = 0;
        private float shakeUntil;

        public override ActionResult Poke()
        {
            int chance = UnityEngine.Random.Range(0, 100) + poked;
            if(poked > 3 && chance > 75 && !this.isCracking)
            {
                this.isCracking = true;
                return ActionResult.LEVEL_UP;
            }
            poked++;
            if (Time.time > shakeUntil)
            {
                shakeUntil = Time.time + 1.0F;
            }
            return ActionResult.NOTHING;
        }

        public override Hero LevelUp(GameState state)
        {
            return new Baby();
        }

        public override void HandleAnimator(Animator animator, GameState gameState)
        {
            animator.SetBool("isCracking", this.isCracking);
            animator.SetBool("isShaking", shakeUntil > Time.time);
        }

        public override Transition GetTransition()
        {
            return ObjectLookup.EggToBaby;
        }
    }

}