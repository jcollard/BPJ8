using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TrainSelector : Selector
    {

        public ActionMenu TrainMenu;

        public override ActionResult Select(GigaHero engine)
        {
            TrainMenu.Activate(engine);
            return ActionResult.NOTHING;
        }
    }
}