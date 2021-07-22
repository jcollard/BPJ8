using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class OptionsSelector : Selector
    {

        public OptionsMenu OptionsMenu;

        public override ActionResult Select(GigaHero engine)
        {
            OptionsMenu.Activate(engine);
            return ActionResult.NOTHING;
        }
    }
}