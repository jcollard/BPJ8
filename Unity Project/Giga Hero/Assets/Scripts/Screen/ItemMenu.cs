using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Screen
{
    public class ItemMenu : MonoBehaviour, IScreen
    {

        public ItemOption[] Options;
        private ItemOption _option;
        private int ix = 0;

        private void Start()
        {
            GigaHero.Screens.Add(this.gameObject);
        }

        public void Next()
        {
            ix = (ix + 1) % Options.Length;
            this._option.gameObject.SetActive(false);
            this._option = Options[ix];
            this._option.gameObject.SetActive(true);
        }

        public void Activate(GigaHero engine)
        {
            engine.SetActiveScreen(this);
            this._option = Options[ix];
            this._option.gameObject.SetActive(true);
        }

        public Func<GigaHero, ActionResult> GetActionA()
        {
            return (engine) => _option.Select(engine);
        }

        public Func<GigaHero, ActionResult> GetActionB()
        {
            return ActionResult.Nothing((engine) => engine.ActionMenu.Activate(engine));
        }

        public Func<GigaHero, ActionResult> GetActionC()
        {
            return ActionResult.Nothing((engine) => Next());
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
    }
}