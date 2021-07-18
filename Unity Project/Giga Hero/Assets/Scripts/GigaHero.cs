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

        public int TicksPerMinute = 60;
        public float lastTick;

        public ActionMenu ActionMenu;

        public GigaButton[] buttons;

        public static readonly ISet<GameObject> Screens = new HashSet<GameObject>();

        public PlayScreen PlayScreen;
        private Animator _frontAnimator;
        private SpriteRenderer _backLayerRenderer;

        // Use this for initialization
        void Start()
        {
            
            foreach (GigaButton button in buttons)
            {
                button.Engine = this;
            }

            _gameState = new GameState(this, PlayScreen);
            SetActiveScreen(_gameState);

        }

        private void Update()
        {
            float nextTick = lastTick + (TicksPerMinute/60);
            if(Time.time > nextTick)
            {
                HandleResult(GameState.Tick());
                lastTick = Time.time;
            }
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
            result.PerformResult(GameState);
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