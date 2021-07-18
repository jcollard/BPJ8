using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GigaHero : MonoBehaviour
    {

        private GameState _gameState;
        public GameState GameState { get { return this._gameState; } }

        public GigaButton[] buttons;

        public Animator FrontLayerAnimator;
        public SpriteRenderer BackLayer;

        private float shakeUntil;

        // Use this for initialization
        void Start()
        {
            foreach(GigaButton button in buttons)
            {
                button.Engine = this;
            }
            _gameState = new GameState(this);

        }

        public void HandleResult(ActionResult result)
        {
            if(result == ActionResult.SHAKE && shakeUntil < Time.time)
            {
                shakeUntil = Time.time + 1F;
            }

            if(result == ActionResult.LEVEL_UP)
            {
                FrontLayerAnimator.SetBool("isCracking", true);
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
            FrontLayerAnimator.SetBool("isShaking", shakeUntil > Time.time);
        }
    }
}