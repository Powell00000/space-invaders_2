using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

/****************************************************************************************************************************
 * 
 *  --{   Audio Manager   }--
 *							  
 *	Music Player
 *      plays background music and allows transitions between tracks.
 *			
 *  Written by:
 *      Jonathan Carter
 *      E: jonathan@carter.games
 *      W: https://jonathan.carter.games
 *		
 *  Version: 2.4.1
 *	Last Updated: 31/01/2021 (d/m/y)							
 * 
****************************************************************************************************************************/

namespace CarterGames.Assets.AudioManager
{
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// MonoBehaviour Class (*Not Static*) | The Music player, designed to play background music in your game.
    /// </summary>
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class MusicPlayer : MonoBehaviour
    {
        public AudioClip[] clips = new AudioClip[2];
        public AudioMixerGroup mixer;
        public enum Effects { None, FadeIn, FadeOut, CrossFade };

        public Effects activeEffect;
        public bool shouldLoop;

        private AudioSource[] source;
        private int activeSource;
        private bool runEffect;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Method | Awake | Runs basic startup for the script.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            source = GetComponents<AudioSource>();
            activeSource = 0;

            source[1].volume = 0;

            if (shouldLoop)
            {
                source[0].loop = true;
                source[1].loop = true;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Method | Update | Runs the effects.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Update()
        {
            if (runEffect)
            {
                switch (activeEffect)
                {
                    case Effects.None:
                        ChangeActiveTrack();
                        break;
                    case Effects.FadeIn:
                        FadeClipIn();
                        break;
                    case Effects.FadeOut:
                        FadeClipOut();
                        break;
                    case Effects.CrossFade:
                        CrossFadeClips();
                        break;
                    default:
                        break;
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Music Player Method | Changes the track to the second clip, ** Instant Effect **
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ChangeActiveTrack()
        {
            if (activeSource.Equals(0))
                source[0].clip = clips[1];
            else
                source[1].clip = clips[1];
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Music Player Method | Changes the active track to the track inputted, ** Instant Effect **
        /// </summary>
        /// <param name="request">AudioClip | Track to change to.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ChangeActiveTrack(AudioClip request)
        {
            if (activeSource.Equals(0))
                source[0].clip = request;
            else
                source[1].clip = request;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Music Player Method | Plays the Fade In Effect.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void FadeIn()
        {
            activeEffect = Effects.FadeIn;
            runEffect = true;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Music Player Method | Plays the Fade Out Effect.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void FadeOut()
        {
            activeEffect = Effects.FadeOut;
            runEffect = true;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Music Player Method | Plays the Cross Fade Effect.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void CrossFade()
        {
            activeEffect = Effects.CrossFade;
            runEffect = true;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Music Player Method | Players the cross fade effect to the clip inputted, with the option to match the time index if needed.
        /// <param name="toFadeTo">AudioClip | The clip to change to.</param>
        /// <param name="shouldMatchTime"/>Bool | Should the time index be matched with the running clip?</param>
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void CrossFade(AudioClip toFadeTo, bool shouldMatchTime = false)
        {
            if (activeSource.Equals(0))
            {
                source[1].clip = toFadeTo;

                if (shouldMatchTime)
                    source[1].time = source[0].time;

                source[1].Play();
            }
            else
            {
                if (shouldMatchTime)
                    source[0].time = source[1].time;

                source[0].clip = toFadeTo;
                source[0].Play();
            }

            activeEffect = Effects.CrossFade;
            runEffect = true;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Music Player Method | Cross Fades the 2 clips.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void CrossFadeClips()
        {
            if (activeSource.Equals(0) && runEffect)
            {
                source[0].volume -= 2 * Time.unscaledDeltaTime;
                source[1].volume += 2 * Time.unscaledDeltaTime;
                activeSource = 1;
                runEffect = false;
            }
            else
            {
                source[0].volume += 2 * Time.unscaledDeltaTime;
                source[1].volume -= 2 * Time.unscaledDeltaTime;
                activeSource = 0;
                runEffect = false;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Music Player Method | Fades in the active source clip.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void FadeClipIn()
        {
            if (activeSource.Equals(0) && runEffect)
            {
                source[0].volume += 2 * Time.unscaledDeltaTime;
                activeSource = 1;
                runEffect = false;
            }
            else
            {
                source[1].volume += 2 * Time.unscaledDeltaTime;
                activeSource = 0;
                runEffect = false;
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Music Player Method | Fades out the active source clip.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void FadeClipOut()
        {
            if (activeSource.Equals(0) && runEffect)
            {
                source[0].volume -= 2 * Time.unscaledDeltaTime;
                activeSource = 1;
                runEffect = false;
            }
            else
            {
                source[1].volume -= 2 * Time.unscaledDeltaTime;
                activeSource = 0;
                runEffect = false;
            }
        }
    }
}