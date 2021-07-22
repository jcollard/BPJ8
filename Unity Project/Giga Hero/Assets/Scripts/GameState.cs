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

        public ActionResult Tick()
        {
            return _hero.Tick();
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
                if (!t.gameObject.activeInHierarchy)
                {
                    t.StartTransition((_) => this._hero = nextHero);
                }
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
            return ButtonAction.OpenStatusScreen;
        }

        public GameObject GetGameObject()
        {
            return _playScreen.gameObject;
        }
    }

}