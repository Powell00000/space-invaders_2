using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;
using System.Linq;

/****************************************************************************************************************************
 * 
 *  --{   Audio Manager   }--
 *							  
 *	Audio Manager Script
 *      The main script of the Audio Manager asset, controls the playing of audio clips in a scene. 
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
    /// MonoBehaviour Class (*Not Static*) | The main Audio Manager script used to play audio in your game.
    /// </summary>
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    [System.Serializable]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private bool hasScannedOnce;   // Tells the script whether or not the AMF has being scanned before.
        [SerializeField] private bool shouldShowDir;    // Tells the script whether it should be displaying the directories in the inspector.
        [SerializeField] private bool shouldShowClips;  // Tells the script whether it should be displaying the clips in the inspector.

        [SerializeField] public AudioManagerFile audioManagerFile;      // The AMF currently in use by this instance of the Audio Manager.

        private Dictionary<string, AudioClip> lib;

        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Method | Awake | Runs basic startup for the script.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void Awake()
        {
            if (audioManagerFile.soundPrefab == null)
                Debug.LogWarning("* Audio Manager * | Warning Code 1 | Prefab has not been assigned! Please assign a prefab in the inspector before using the manager.");

            GetComponent<AudioSource>().hideFlags = HideFlags.HideInInspector;

            lib = new Dictionary<string, AudioClip>();

            for (int i = 0; i < audioManagerFile.library.Count; i++)
            {
                lib.Add(audioManagerFile.library[i].key, audioManagerFile.library[i].value);
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound that is scanned into the audio manager.
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive).</param>
        /// <param name="volume">(*Optional*) Float | The volume that the clip will be played at | Default = 1.</param>
        /// <param name="pitch">(*Optional*) Float | The pitch that the sound is played at | Default = 1.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play(string request, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))                   // If the sound is in the library
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);                      // Instantiate a Sound prefab
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[request];                 // Get the prefab and add the requested clip to it
                _source.volume = volume;                                          // changes the volume if a it is inputted
                _source.pitch = pitch;                                            // changes the pitch if a change is inputted
                _source.Play();                                                   // play the audio from the prefab
                Destroy(_clip, _source.clip.length);                              // Destroy the prefab once the clip has finished playing
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound that is scanned into the audio manager in a mixer group.
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive).</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) Float | The volume that the clip will be played at | Default = 1.</param>
        /// <param name="pitch">(*Optional*) Float | The pitch that the sound is played at | Default = 1.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play(string request, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))                   // If the sound is in the library
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);                      // Instantiate a Sound prefab

                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[request];                 // Get the prefab and add the requested clip to it
                _source.volume = volume;                                          // changes the volume if a it is inputted
                _source.pitch = pitch;                                            // changes the pitch if a change is inputted
                _source.outputAudioMixerGroup = mixer;                            // sets the mixer group to the defined group
                _source.Play();                                                   // play the audio from the prefab
                Destroy(_clip, _source.clip.length);                              // Destroy the prefab once the clip has finished playing
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound that is scanned into the audio manager in a mixer group.
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive).</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) Float | The volume that the clip will be played at | Default = 1.</param>
        /// <param name="pitch">(*Optional*) Float | The pitch that the sound is played at | Default = 1.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play(string request, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))                   // If the sound is in the library
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);                      // Instantiate a Sound prefab

                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[request];                 // Get the prefab and add the requested clip to it
                _source.volume = volume;                                          // changes the volume if a it is inputted
                _source.pitch = pitch;                                            // changes the pitch if a change is inputted

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID]; // sets the mixer group to the defined group
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.Play();                                                   // play the audio from the prefab
                Destroy(_clip, _source.clip.length);                              // Destroy the prefab once the clip has finished playing
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a different gameobject instead of the default prefab object
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive).</param>
        /// <param name="gameObject">GameObject | GameObject that the audio will play from.</param>
        /// <param name="volume">(*Optional*) Float | The volume that the clip will be played at | Default = 1.</param>
        /// <param name="pitch">(*Optional*) Float | The pitch that the sound is played at | Default = 1.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play(string request, GameObject gameObject, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the GameObject. Please ensure a AudioSouce Component is attached to the GameObject.");

                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a different gameobject instead of the default prefab object, in a mixer group. 
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive).</param>
        /// <param name="gameObject">GameObject | GameObject that the audio will play from.</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) Float | The volume that the clip will be played at | Default = 1.</param>
        /// <param name="pitch">(*Optional*) Float | The pitch that the sound is played at | Default = 1.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play(string request, GameObject gameObject, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the GameObject. Please ensure a AudioSouce Component is attached to the GameObject.");

                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a different gameobject instead of the default prefab object, in a mixer group. 
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive).</param>
        /// <param name="gameObject">GameObject | GameObject that the audio will play from.</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) Float | The volume that the clip will be played at | Default = 1.</param>
        /// <param name="pitch">(*Optional*) Float | The pitch that the sound is played at | Default = 1.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play(string request, GameObject gameObject, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the GameObject. Please ensure a AudioSouce Component is attached to the GameObject.");

                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound at a defined position
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive).</param>
        /// <param name="position">Vector3 | position where the sound should play from.</param>
        /// <param name="volume">(*Optional*) Float | The volume that the clip will be played at | Default = 1.</param>
        /// <param name="pitch">(*Optional*) Float | The pitch that the sound is played at | Default = 1.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play(string request, Vector3 position, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _clip.gameObject.transform.position = position;
                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound at a defined position
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive).</param>
        /// <param name="position">Vector3 | position where the sound should play from.</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) Float | The volume that the clip will be played at | Default = 1.</param>
        /// <param name="pitch">(*Optional*) Float | The pitch that the sound is played at | Default = 1.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play(string request, Vector3 position, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _clip.gameObject.transform.position = position;
                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound at a defined position
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive).</param>
        /// <param name="position">Vector3 | position where the sound should play from.</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) Float | The volume that the clip will be played at | Default = 1.</param>
        /// <param name="pitch">(*Optional*) Float | The pitch that the sound is played at | Default = 1.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void Play(string request, Vector3 position, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _clip.gameObject.transform.position = position;
                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a particular timecode on the audio clip audioManagerFile
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">Float | The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="volume">(*Optional*)Float | The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*)Float | The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(string request, float time, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[request];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a particular timecode on the audio clip audioManagerFile
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">Float | The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*)Float | The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*)Float | The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(string request, float time, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[request];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a particular timecode on the audio clip audioManagerFile
        /// </summary>
        /// <param name="request">String | The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">Float | The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*)Float | The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*)Float | The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(string request, float time, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[request];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a particular timecode on a different gameobject
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="gameObject">GameObject that the audio will play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(string request, float time, GameObject gameObject, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the GameObject. Please ensure a AudioSouce Component is attached to the GameObject.");

                _source.clip = lib[request];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a particular timecode on a different gameobject
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="gameObject">GameObject that the audio will play from</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(string request, float time, GameObject gameObject, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the GameObject. Please ensure a AudioSouce Component is attached to the GameObject.");

                _source.clip = lib[request];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a particular timecode on a different gameobject
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="gameObject">GameObject that the audio will play from</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(string request, float time, GameObject gameObject, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the GameObject. Please ensure a AudioSouce Component is attached to the GameObject.");

                _source.clip = lib[request];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a particular timecode at a defined position
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="position">position where the sound should play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(string request, float time, Vector3 position, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _clip.gameObject.transform.position = position;
                _source.clip = lib[request];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a particular timecode at a defined position
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="position">position where the sound should play from</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(string request, float time, Vector3 position, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _clip.gameObject.transform.position = position;
                _source.clip = lib[request];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound from a particular timecode at a defined position
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="position">position where the sound should play from</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayFromTime(string request, float time, Vector3 position, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _clip.gameObject.transform.position = position;
                _source.clip = lib[request];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound after a defined amount of time
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(string request, float delay, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound after a defined amount of time
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(string request, float delay, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound after a defined amount of time
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(string request, float delay, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound after a defined amount of time on a different gameobject
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="gameObject">GameObject that the audio will play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(string request, float delay, GameObject gameObject, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the GameObject. Please ensure a AudioSouce Component is attached to the GameObject.");

                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.PlayDelayed(delay);                            // Only difference, played with a delay rather that right away
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound after a defined amount of time on a different gameobject
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="gameObject">GameObject that the audio will play from</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(string request, float delay, GameObject gameObject, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the GameObject. Please ensure a AudioSouce Component is attached to the GameObject.");

                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound after a defined amount of time on a different gameobject
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="gameObject">GameObject that the audio will play from</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(string request, float delay, GameObject gameObject, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = gameObject;
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the GameObject. Please ensure a AudioSouce Component is attached to the GameObject.");

                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound after a defined amount of time at a defined position
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="position">position where the sound should play from</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(string request, float delay, Vector3 position, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;
                _clip.gameObject.transform.position = position;
                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound after a defined amount of time at a defined position
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="position">position where the sound should play from</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(string request, float delay, Vector3 position, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _clip.gameObject.transform.position = position;
                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a sound after a defined amount of time at a defined position
        /// </summary>
        /// <param name="request">The name of the audio clip you want to play (note it is case sensitive)</param>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="position">position where the sound should play from</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayWithDelay(string request, float delay, Vector3 position, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(request))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _clip.gameObject.transform.position = position;
                _source.clip = lib[request];
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a random sound that has been scanned by this manager
        /// </summary>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayRandom(float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(GetRandomSound().name))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[GetRandomSound().name];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a random sound that has been scanned by this manager
        /// </summary>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayRandom(AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(GetRandomSound().name))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[GetRandomSound().name];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a random sound that has been scanned by this manager
        /// </summary>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayRandom(int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(GetRandomSound().name))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[GetRandomSound().name];
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a random sound that has been scanned by this manager, from a particular time
        /// </summary>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayRandomFromTime(float time, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(GetRandomSound().name))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[GetRandomSound().name];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a random sound that has been scanned by this manager, from a particular time
        /// </summary>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayRandomFromTime(float time, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(GetRandomSound().name))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[GetRandomSound().name];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a random sound that has been scanned by this manager, from a particular time
        /// </summary>
        /// <param name="time">The time you want to clip the be played from (float value for seconds)</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayRandomFromTime(float time, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(GetRandomSound().name))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[GetRandomSound().name];
                _source.time = time;
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.Play();
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a random sound that has been scanned by this manager
        /// </summary>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayRandomWithDelay(float delay, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(GetRandomSound().name))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[GetRandomSound().name];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a random sound that has been scanned by this manager
        /// </summary>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="mixer">AudioMixerGroup | The mixer group to play this sound under.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayRandomWithDelay(float delay, AudioMixerGroup mixer, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(GetRandomSound().name))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[GetRandomSound().name];
                _source.volume = volume;
                _source.pitch = pitch;
                _source.outputAudioMixerGroup = mixer;
                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Play a random sound that has been scanned by this manager
        /// </summary>
        /// <param name="delay">The amount of time you want the clip to wait before playing</param>
        /// <param name="mixerID">Int | The mixer group to play this sound under from it's ID in the editor.</param>
        /// <param name="volume">(*Optional*) The volume that the clip will be played at | Default = 1</param>
        /// <param name="pitch">(*Optional*) The pitch that the sound is played at | Default = 1</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void PlayRandomWithDelay(float delay, int mixerID, float volume = 1, float pitch = 1)
        {
            if (lib.ContainsKey(GetRandomSound().name))
            {
                GameObject _clip = Instantiate(audioManagerFile.soundPrefab);
                AudioSource _source = default;

                if (_clip.GetComponent<AudioSource>())
                    _source = _clip.GetComponent<AudioSource>();
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 4 | No AudioSource Component found on the Sound Prefab. Please ensure a AudioSouce Component is attached to your prefab.");

                _source.clip = lib[GetRandomSound().name];
                _source.volume = volume;
                _source.pitch = pitch;

                if (audioManagerFile.audioMixer[mixerID] != null)
                    _source.outputAudioMixerGroup = audioManagerFile.audioMixer[mixerID];
                else
                    Debug.LogWarning("* Audio Manager * | Warning Code 3 | Could not find audio mixer, Please ensure the mixer is in the inspector and you have the right ID.");

                _source.PlayDelayed(delay);
                Destroy(_clip, _source.clip.length);
            }
            else
                Debug.LogWarning("* Audio Manager * | Warning Code 2 | Could not find clip. Please ensure the clip is scanned and the string you entered is correct (Note the input is CaSe SeNsItIvE).");
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | gets the number of clips currently in this instance of the Audio Manager.
        /// </summary>
        /// <returns>Int | number of clips in the AMF on this Audio Manager.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public int GetNumberOfClips()
        {
            if (audioManagerFile.library != null)
                return audioManagerFile.library.Count;
            else
                return 0;

        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Picks a random sound from the current AMF and returns it.
        /// </summary>
        /// <returns>AudioClip | a random AudioClip from the active AMF</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public AudioClip GetRandomSound()
        {
            return lib.Values.ElementAt(Random.Range(0, audioManagerFile.library.Count - 1));
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Changes the Audio Manager File to what is inputted.
        /// </summary>
        /// <param name="newFile">AMF | file to use instead of the current one.</param>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void ChangeAudioManagerFile(AudioManagerFile newFile)
        {
            audioManagerFile = newFile;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Method | Gets the current AMF in use.
        /// </summary>
        /// <returns>AMF | The file currently in use by this instance of the Audio Manager</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public AudioManagerFile GetAudioManagerFile()
        {
            return audioManagerFile;
        }
    }


    [System.Serializable]
    public class DataStore
    {
        public string key;
        public AudioClip value;

        public DataStore(string _key, AudioClip _value)
        {
            key = _key;
            value = _value;
        }
    }
}