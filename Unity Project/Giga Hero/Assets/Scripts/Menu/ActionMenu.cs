using System.Collections;
using System;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class ActionMenu : MonoBehaviour
    {




        public GameObject SelectorsObject;
        public Selector[] Selectors;
        private Selector _selected;
        private int ix = 0;

        private readonly Func<GigaHero, ActionResult> _selectAction;
        private readonly Func<GigaHero, ActionResult> _exitAction;
        private readonly Func<GigaHero, ActionResult> _nextAction;

        private ActionMenu()
        {
            _selectAction = ActionResult.Nothing((engine) => _selected.Select());
            _exitAction = ActionResult.Nothing((engine) => this.Exit(engine));
            _nextAction = ActionResult.Nothing((engine) => this.Next());
        }



        
        public void Next()
        {
            ix = (ix + 1) % Selectors.Length;
            this._selected.gameObject.SetActive(false);
            this._selected = Selectors[ix];
            this._selected.gameObject.SetActive(true);
        }

        public void Select()
        {
            _selected.Select();
        }

        public virtual ActionResult PerformAction(GigaHero engine)
        {
            Next();
            return ActionResult.NOTHING;
        }

        public void Activate(GigaHero engine)
        {
            
            engine.FrontLayer.SetActive(false);
            engine.BackLayer.SetActive(false);
            this.gameObject.SetActive(true);
            
            this._selected = Selectors[ix];
            this._selected.gameObject.SetActive(true);
            engine.SetActions(_selectAction, _exitAction, _nextAction);
        }

        public void Exit(GigaHero engine)
        {
            this.gameObject.SetActive(false);
            engine.ActivatePlayMode();
        }
    }
}