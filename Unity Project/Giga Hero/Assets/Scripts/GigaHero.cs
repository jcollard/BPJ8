using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Menu;
using System;

namespace Assets.Scripts
{
    public class GigaHero : MonoBehaviour
    {

        private GameState _gameState;
        public GameState GameState { get { return this._gameState; } }

        public ActionMenu ActionMenu;

        public GigaButton[] buttons;

        public GameObject FrontLayer;
        public GameObject BackLayer;
        private Animator _frontAnimator;
        private SpriteRenderer _backLayerRenderer;

        private float shakeUntil;

        // Use this for initialization
        void Start()
        {
            
            foreach (GigaButton button in buttons)
            {
                button.Engine = this;
            }
            _gameState = new GameState(this);

            _frontAnimator = FrontLayer.GetComponent<Animator>();
            _backLayerRenderer = BackLayer.GetComponent<SpriteRenderer>();
            ActivatePlayMode();
        }

        public void HandleResult(ActionResult result)
        {
            if(result == ActionResult.SHAKE && shakeUntil < Time.time)
            {
                shakeUntil = Time.time + 1F;
            }

            if(result == ActionResult.LEVEL_UP)
            {
                _frontAnimator.SetBool("isCracking", true);
                if(_gameState.LevelUp())
                {
                    Transition t = _gameState.Hero.GetTransition();
                    t.StartTransition();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            _frontAnimator.SetBool("isShaking", shakeUntil > Time.time);
        }

        internal void SetActions(Func<GigaHero, ActionResult> a, Func<GigaHero, ActionResult> b, Func<GigaHero, ActionResult> c)
        {
            if(a == null || b == null || c == null)
            {
                throw new Exception("Cannot register null action. " + a + ", " + b + ", " + c);
            }
            buttons[0].Action = a;
            buttons[1].Action = b;
            buttons[2].Action = c;
        }

        internal void ActivatePlayMode()
        {
            ActionMenu.gameObject.SetActive(false);
            FrontLayer.SetActive(true);
            BackLayer.SetActive(true);

            SetActions(ButtonAction.Poke, ButtonAction.OpenActionMenu, ButtonAction.Poke);
        }
    }
}