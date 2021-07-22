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

        public readonly int DEFAULT_SPEED = 15;
        public int TicksPerMinute = 60;
        public float lastTick;

        public ActionMenu ActionMenu;
        public GigaButton[] buttons;
        public GigaButton OptionMenuButton;

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
            OptionMenuButton.Engine = this;
            //OptionMenuButton.Action = ButtonAction.OpenOptionsScreen;
            OptionMenuButton.Action = (engine) => ActionResult.LEVEL_UP;

            SetActiveScreen(_gameState);

        }

        private void Update()
        {

            float nextTick = TicksPerMinute > 0 ? lastTick + (60F / (float)TicksPerMinute) : float.PositiveInfinity;
            if(TicksPerMinute > 0 && Time.time > nextTick)
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