using UnityEngine;
using System.Collections.Generic;

/****************************************************************************************************************************
 * 
 *  --{   Audio Manager   }--
 *							  
 *	UI Audio Player Script
 *      A script to play allow sounds to be played on a UI event using the Audio Manager Asset. 		
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
    /// MonoBehaviour Class (*Not Static*) | The UI audio player, designed to play audio from an AMF from a UI object.
    /// </summary>
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public class UIAudioPlayer : MonoBehaviour
    {
        public AudioManagerFile audioManagerFile;
        public List<string> clipsToPlay;
        public List<int> clipsChosen;
        public int mixerChosen;
        public float volume = 1f;
        public float pitch = 1f;
        private Dictionary<string, AudioClip> lib;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Method | Awake | Runs basic startup for the script.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            lib = new Dictionary<string, AudioClip>();

            for (int i = 0; i < audioManagerFile.library.Count; i++)
            {
                lib.Add(audioManagerFile.library[i].key, audioManagerFile.library[i].value);
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// UI Audio Player Method | Plays the clip(s) selected in the inspector as they are with the volume/pitch/mixer from the inspector.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play()
        {
            for (int i = 0; i < clipsChosen.Count; i++)
            {
                if (audioManagerFile.library != null)
                {
                    if (lib.ContainsKey(Request(clipsChosen[i])))                    // If the sound is in the library
                    {
                        GameObject _clip = Instantiate(audioManagerFile.soundPrefab);                      // Instantiate a Sound prefab
                        AudioSource _source = default;

                        if (_clip.GetComponent<AudioSource>())
                            _source = _clip.GetComponent<AudioSource>();
                        else
                            Debug.LogWarning("* AM: UI Audio Player * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                        _source.clip = lib[Request(clipsChosen[i])];                 // Get the prefab and add the requested clip to it
                        _source.volume = volume;                                          // changes the volume if a it is inputted
                        _source.pitch = pitch;                                            // changes the pitch if a change is inputted
                        _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerChosen];
                        _source.Play();                                                   // play the audio from the prefab
                        Destroy(_clip, _source.clip.length);                              // Destroy the prefab once the clip has finished playing
                    }
                    else
                        Debug.LogWarning("* AM: UI Audio Player * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// UI Audio Player Method | Plays the clip(s) selected in the inspector as they are with the volume/pitch/mixer from the inspector from the specified time.
        /// </summary>
        /// <param name="time">Float | Time to play from.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(float time)
        {
            for (int i = 0; i < clipsChosen.Count; i++)
            {
                if (audioManagerFile.library != null)
                {
                    if (lib.ContainsKey(Request(clipsChosen[i])))                    // If the sound is in the library
                    {
                        GameObject _clip = Instantiate(audioManagerFile.soundPrefab);                      // Instantiate a Sound prefab
                        AudioSource _source = default;

                        if (_clip.GetComponent<AudioSource>())
                            _source = _clip.GetComponent<AudioSource>();
                        else
                            Debug.LogWarning("* AM: UI Audio Player * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                        _source.clip = lib[Request(clipsChosen[i])];                 // Get the prefab and add the requested clip to it
                        _source.volume = volume;                                          // changes the volume if a it is inputted
                        _source.pitch = pitch;                                            // changes the pitch if a change is inputted
                        _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerChosen];
                        _source.time = time;
                        _source.Play();                                                   // play the audio from the prefab
                        Destroy(_clip, _source.clip.length);                              // Destroy the prefab once the clip has finished playing
                    }
                    else
                        Debug.LogWarning("* AM: UI Audio Player * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
                }
            }
        }



        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// UI Audio Player Method | Plays the clip(s) selected in the inspector as they are with the volume/pitch/mixer from the inspector after the specified time has passed.
        /// </summary>
        /// <param name="delay">Float | Time to wait before playing.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(float delay)
        {
            for (int i = 0; i < clipsChosen.Count; i++)
            {
                if (audioManagerFile.library != null)
                {
                    if (lib.ContainsKey(Request(clipsChosen[i])))                    // If the sound is in the library
                    {
                        GameObject _clip = Instantiate(audioManagerFile.soundPrefab);                      // Instantiate a Sound prefab
                        AudioSource _source = default;

                        if (_clip.GetComponent<AudioSource>())
                            _source = _clip.GetComponent<AudioSource>();
                        else
                            Debug.LogWarning("* AM: UI Audio Player * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                        _source.clip = lib[Request(clipsChosen[i])];                 // Get the prefab and add the requested clip to it
                        _source.volume = volume;                                          // changes the volume if a it is inputted
                        _source.pitch = pitch;                                            // changes the pitch if a change is inputted
                        _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerChosen];
                        _source.PlayDelayed(delay);                                                   // play the audio from the prefab
                        Destroy(_clip, _source.clip.length);                              // Destroy the prefab once the clip has finished playing
                    }
                    else
                        Debug.LogWarning("* AM: UI Audio Player * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
                }
            }
        }


        /// <summary>
        /// UI Audio Player Method | Gets the requested clip as a string rather than a int.
        /// </summary>
        /// <param name="chosenClip">Int | The clip to find the string value for.</param>
        /// <returns>String | The clip name of the requested clip.</returns>
        private string Request(int chosenClip)
        {
            return clipsToPlay[chosenClip];
        }
    }
}