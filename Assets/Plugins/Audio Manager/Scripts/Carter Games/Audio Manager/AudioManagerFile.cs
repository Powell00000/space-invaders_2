using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/****************************************************************************************************************************
 * 
 *  --{   Audio Manager   }--
 *							  
 *	Audio Manager File (AMF) Scriptable Object
 *      Holds data for the audio manager script to then use to play audio. 
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
    /// Audio Manager Scriptable Object (*Not Static*) | Holds the audio data that is used in the Audio Manager asset.
    /// </summary>
    /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    [System.Serializable, CreateAssetMenu(fileName = "Audio Manager File", menuName = "Carter Games/Audio Manager File")]
    public class AudioManagerFile : ScriptableObject
    {
        [SerializeField] public List<AudioMixerGroup> audioMixer;  // Holds a list of the audio clips stored in the AMF.
        [SerializeField] public GameObject soundPrefab;     // Holds the prefab spawned in to play sound from this AMF.
        [SerializeField] public bool isPopulated;           // Holds the boolean value for whether or not this AMF has been used to store audio.
        [SerializeField] public bool hasDirectories;        // Holds the boolean value for whether or not this AMF has directories stored on it.
        [SerializeField] public List<string> directory;     // Holds a list of directory strings for use in the AM.
        [SerializeField] public List<DataStore> library;  // Moved in 2.3.5 to be here instead of in the AM referance as it caused some issues with the automation.
    }
}