﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public enum Sound
    {
        None,
        Hover,
        Action,
        MoneyIncome,
        StandardClick,
        Notification,
        Selected,

        Tweak,
        Upgrade,

        SignContract,
        CorporatePolicyTweak
    }

    public class AudioManager: MonoBehaviour
    {
        Dictionary<AudioClip, AudioSource> sources = new Dictionary<AudioClip, AudioSource>();
        Dictionary<Sound, AudioClip> sounds = new Dictionary<Sound, AudioClip>();

        public AudioClip coinSound;
        public AudioClip standardClickSound;
        public AudioClip notificationSound;
        public AudioClip toggleScreenSound;
        public AudioClip toggleButtonSound;
        public AudioClip hintSound;
        public AudioClip monthlyMoneySound;
        public AudioClip itemSelectedSound;
        public AudioClip penOnPaperSoung;

        void Start()
        {
            AddSound(standardClickSound, Sound.StandardClick);
            AddSound(notificationSound, Sound.Notification);
            AddSound(toggleScreenSound, Sound.Action);
            AddSound(hintSound, Sound.Hover);
            AddSound(monthlyMoneySound, Sound.MoneyIncome);
            AddSound(itemSelectedSound, Sound.Selected);

            AddSound(toggleScreenSound, Sound.Upgrade);
            AddSound(itemSelectedSound, Sound.Tweak);
            AddSound(penOnPaperSoung, Sound.SignContract);
            AddSound(itemSelectedSound, Sound.CorporatePolicyTweak);
        }

        void AddSound(AudioClip audioClip, Sound sound = Sound.None)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClip;

            sources[audioClip] = audioSource;
            sounds[sound] = audioClip;
        }

        void PlayClip(AudioClip clip)
        {
            if (sources.ContainsKey(clip))
                sources[clip].Play();
            else
                Debug.LogErrorFormat("Clip {0} doesn't exist in AudioManager", clip);
        }

        public void Play(Sound sound)
        {
            if (sounds.ContainsKey(sound))
                PlayClip(sounds[sound]);
            else
                Debug.LogErrorFormat("Sound {0} doesn't exist in AudioManager", sound);
        }

        public void PlayToggleButtonSound()
        {
            PlayClip(toggleButtonSound);
        }

        public void PlayOnHintHoverSound()
        {
            Play(Sound.Hover);
        }

        public void PlayCoinSound()
        {
            PlayClip(monthlyMoneySound);
        }

        public void PlayClickSound()
        {
            PlayClip(standardClickSound);
        }

        public void PlayPrepareAdSound()
        {
            PlayClip(standardClickSound);
        }

        public void PlayStartAdSound()
        {
            PlayClip(standardClickSound);
        }

        public void PlayNotificationSound()
        {
            PlayClip(notificationSound);
        }

        public void PlayToggleScreenSound()
        {
            PlayClip(toggleScreenSound);
        }

        public void PlayWaterSplashSound()
        {
            PlayClip(standardClickSound);
        }
    }
}
