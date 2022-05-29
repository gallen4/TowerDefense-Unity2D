using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    [RequireComponent(typeof(AudioSource))]

    public class SoundPlayer : MonoSingleton<SoundPlayer>
    {
        [SerializeField] private Sounds m_Sounds;
        [SerializeField] private AudioClip m_BGM;
        private AudioSource m_AS;

        protected override void Awake()
        {
            m_AS = GetComponent<AudioSource>();

            base.Awake();
            Instance.m_AS.clip = m_BGM;
            Instance.m_AS.Play();
        }
        public void Play(Sound sound)
        {
            m_AS.PlayOneShot(m_Sounds[sound]);
        }
    }
}