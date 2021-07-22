using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SoundBoard : MonoBehaviour
    {

        public static SoundBoard INSTANCE;
        public AudioSource Notification;

        // Use this for initialization
        void Start()
        {
            INSTANCE = this;
        }
    }
}