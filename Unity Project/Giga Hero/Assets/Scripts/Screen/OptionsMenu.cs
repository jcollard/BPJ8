using System.Collections;
using System;
using UnityEngine;
using Assets.Scripts.Screen;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public enum OptionsMenuType
    {
        SPEED, SFX, MUSIC, BACK
    }

    public class OptionsMenu : MonoBehaviour, IScreen
    {

        public OptionTypeSelector[] Selectors;
        public int Speed = 1;
        public float SFX = 0.5f;
        public float Music = 0.5f;

        public Text GameSpeed;
        public Text SoundFX;
        public Text Volume;

        private OptionTypeSelector _selected;
        private int ix = 0;

        private void Start()
        {
            GigaHero.Screens.Add(this.gameObject);
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
            engine.TicksPerMinute = 0;
            engine.SetActiveScreen(this);
            this._selected = Selectors[ix];
            foreach(OptionTypeSelector s in Selectors)
            {
                s.gameObject.SetActive(false);
            }
            this._selected.gameObject.SetActive(true);
            
        }

        public Func<GigaHero, ActionResult> GetActionA()
        {
            return ActionResult.Nothing((engine) =>
            {
                if (_selected.OptionType == OptionsMenuType.BACK)
                {
                    engine.TicksPerMinute = engine.DEFAULT_SPEED * Speed;
                    engine.GameState.Activate(engine);
                    return;
                }
                if (_selected.OptionType == OptionsMenuType.MUSIC)
                {
                    this.Music -= 0.10F;
                    this.Music = Mathf.Max(0, this.Music);
                    int percentage = (int)(Music * 100);
                    this.Volume.text = "MUSIC: " + percentage;
                    return;
                }
                if (_selected.OptionType == OptionsMenuType.SFX)
                {
                    this.SFX -= 0.10F;
                    this.SFX = Mathf.Max(0, this.SFX);
                    int percentage = (int)(this.SFX * 100);
                    this.SoundFX.text = "SFX: " + percentage;
                    return;
                }
                if (_selected.OptionType == OptionsMenuType.SPEED)
                {
                    this.Speed /= 2;
                    this.Speed = Mathf.Max(1, this.Speed);
                    this.GameSpeed.text = "SPEED: " + this.Speed + "x";
                    return;
                }
            });
        }

        public Func<GigaHero, ActionResult> GetActionB()
        {
            return ActionResult.Nothing((engine) => Next());
        }

        public Func<GigaHero, ActionResult> GetActionC()
        {
            return ActionResult.Nothing((engine) =>
            {
                if (_selected.OptionType == OptionsMenuType.BACK)
                {
                    engine.TicksPerMinute = engine.DEFAULT_SPEED * Speed;
                    engine.GameState.Activate(engine);
                    return;
                }
                if (_selected.OptionType == OptionsMenuType.MUSIC)
                {
                    this.Music += 0.10F;
                    this.Music = Mathf.Min(1, this.Music);
                    int percentage = (int)(Music * 100);
                    this.Volume.text = "MUSIC: " + percentage;
                    return;
                }
                if (_selected.OptionType == OptionsMenuType.SFX)
                {
                    this.SFX += 0.10F;
                    this.SFX = Mathf.Min(1, this.SFX);
                    int percentage = (int)(this.SFX * 100);
                    this.SoundFX.text = "SFX: " + percentage;
                    return;
                }
                if (_selected.OptionType == OptionsMenuType.SPEED)
                {
                    this.Speed *= 2;
                    this.Speed = Mathf.Min(16, this.Speed);
                    this.GameSpeed.text = "SPEED: " + this.Speed + "x";
                    return;
                }
            });
        }

        public GameObject GetGameObject()
        {
            return this.gameObject;
        }
    }
}