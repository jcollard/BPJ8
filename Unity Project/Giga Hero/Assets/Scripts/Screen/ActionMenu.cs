using System.Collections;
using System;
using UnityEngine;
using Assets.Scripts.Screen;

namespace Assets.Scripts
{
    public class ActionMenu : MonoBehaviour, IScreen
    {

        public Selector[] Selectors;
        private Selector _selected;
        private int ix = 0;

        private readonly Func<GigaHero, ActionResult> _selectAction;
        private readonly Func<GigaHero, ActionResult> _exitAction;
        private readonly Func<GigaHero, ActionResult> _nextAction;

        private void Start()
        {
            GigaHero.Screens.Add(this.gameObject);
        }

        private ActionMenu()
        {
            _selectAction = (engine) => _selected.Select(engine);
            _exitAction = ActionResult.Nothing((engine) => engine.SetActiveScreen(engine.GameState));
            _nextAction = ActionResult.Nothing((engine) => this.Next());
        }
        
        public void Next()
        {
            ix = (ix + 1) % Selectors.Length;
            this._selected.gameObject.SetActive(false);
            this._selected = Selectors[ix];
            this._selected.gameObject.SetActive(true);
        }

        public void Activate(GigaHero engine)
        {
            engine.SetActiveScreen(this);
            this._selected = Selectors[ix];
            foreach(Selector s in Selectors)
            {
                s.gameObject.SetActive(false);
            }
            this._selected.gameObject.SetActive(true);
            
        }

        public Func<GigaHero, ActionResult> GetActionA()
        {
            return _selectAction;
        }

        public Func<GigaHero, ActionResult> GetActionB()
        {
            return _nextAction;
        }

        public Func<GigaHero, ActionResult> GetActionC()
        {
            return _exitAction;
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
    }
}