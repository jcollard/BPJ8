using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundBoard : MonoBehaviour
    {

        public static SoundBoard INSTANCE;
        public AudioSource Notification;
        public AudioSource PowerUp;
        public AudioSource NoNo;

        // Use this for initialization
        void Start()
        {
            INSTANCE = this;
            OptionsMenu.AddSFX(Notification);
            OptionsMenu.AddSFX(PowerUp);
            OptionsMenu.AddSFX(NoNo);
        }
    }
}