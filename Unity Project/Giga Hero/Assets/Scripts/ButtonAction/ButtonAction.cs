using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{

    public class ButtonAction
    {
        public static readonly Func<GigaHero, ActionResult> Poke = (engine) => engine.GameState.Poke();
        public static readonly Func<GigaHero, ActionResult> OpenActionMenu = ActionResult.Nothing((engine) => engine.ActionMenu.Activate(engine));
        public static readonly Func<GigaHero, ActionResult> Nothing = (engine) => ActionResult.NOTHING;
        public static readonly Func<GigaHero, ActionResult> OpenStatusScreen = ActionResult.Nothing((engine) => ObjectLookup.StatusScreen.Activate(engine));
        public static readonly Func<GigaHero, ActionResult> OpenMainScreen = ActionResult.Nothing((engine) => engine.SetActiveScreen(engine.GameState));
        public static readonly Func<GigaHero, ActionResult> OpenOptionsScreen = ActionResult.Nothing((engine) => {
            if (OptionsMenu.INSTANCE != null && OptionsMenu.INSTANCE.gameObject.activeInHierarchy) {
                OptionsMenu.INSTANCE.Close(engine);
            } else
            {
                OptionsMenu.INSTANCE.Activate(engine);
            }
        });
    }

}