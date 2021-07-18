using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Selector : MonoBehaviour
    {

        public virtual ActionResult Select(GigaHero engine)
        {
            return ActionResult.NOTHING;
        }
    }
}