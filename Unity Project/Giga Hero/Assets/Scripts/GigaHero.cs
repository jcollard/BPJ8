using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Screen;

namespace Assets.Scripts
{
    public class GigaHero : MonoBehaviour
    {

        private GameState _gameState;
        public GameState GameState { get { return this._gameState; } }

        internal void SetActiveScreen(IScreen screen)
        {
            DeactivateAllScreens();
            screen.GetGameObject().SetActive(true);
            SetActions(screen);
        }

        public ActionMenu ActionMenu;

        public GigaButton[] buttons;

        public static readonly ISet<GameObject> Screens = new HashSet<GameObject>();

        public GameObject FrontLayer;
        public GameObject BackLayer;
        public GameObject PlayScreen;
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

            _gameState = new GameState(this, PlayScreen);
            SetActions(_gameState);

            _frontAnimator = FrontLayer.GetComponent<Animator>();
            _backLayerRenderer = BackLayer.GetComponent<SpriteRenderer>();
            

        }

        internal void DeactivateAllScreens()
        {
            foreach(GameObject screen in Screens)
            {
                screen.SetActive(false);
            }
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

        internal void SetActions(IScreen actionMenu)
        {
            buttons[0].Action = actionMenu.GetActionA();
            buttons[1].Action = actionMenu.GetActionB();
            buttons[2].Action = actionMenu.GetActionC();
        }

        internal void SetActions(Func<GigaHero, ActionResult> a, Func<GigaHero, ActionResult> b, Func<GigaHero, ActionResult> c)
        {
            buttons[0].Action = a;
            buttons[1].Action = b;
            buttons[2].Action = c;
        }

    }
}