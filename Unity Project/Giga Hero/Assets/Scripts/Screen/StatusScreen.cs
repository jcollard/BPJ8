using System.Collections;
using UnityEngine;
using System;
using Assets.Scripts.Screen;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class StatusScreen : MonoBehaviour, IScreen
    {

        public Text textArea;
        public GigaHero engine;

        private void Start()
        {
            GigaHero.Screens.Add(this.gameObject);
        }

        public virtual void Update()
        {
            textArea.text = engine.GameState.Hero.Status();
        }

        public virtual void Activate(GigaHero engine)
        {
            textArea.text = engine.GameState.Hero.Status();
            engine.SetActiveScreen(this);
        }
        public Func<GigaHero, ActionResult> GetActionA()
        {
            return ButtonAction.OpenMainScreen;
        }
        
        public Func<GigaHero, ActionResult> GetActionB()
        {
            return ButtonAction.OpenActionMenu;
        }
        public Func<GigaHero, ActionResult> GetActionC()
        {
            return ActionResult.Nothing((engine) => engine.SetActiveScreen(engine.GameState));
        }
        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
    }

}