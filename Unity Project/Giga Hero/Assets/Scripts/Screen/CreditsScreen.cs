using System.Collections;
using UnityEngine;
using System;
using Assets.Scripts.Screen;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CreditsScreen : StatusScreen
    {

        public static CreditsScreen INSTANCE;

        public override void Update()
        {
            
        }

        public void ActivateOrDeactivate(GigaHero engine)
        {
            if (this.gameObject.activeInHierarchy)
            {
                //engine.TicksPerMinute = engine.DEFAULT_SPEED * OptionsMenu.INSTANCE.Speed;
                engine.GameState.Activate(engine);
            } else
            {
                Activate(engine);
            }
        }

        public override void Activate(GigaHero engine)
        {
            // Pause Game
            //engine.TicksPerMinute = 0;
            engine.SetActiveScreen(this);
        }
    }

}