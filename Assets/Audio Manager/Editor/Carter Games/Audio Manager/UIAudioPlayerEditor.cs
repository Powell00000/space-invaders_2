using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

/****************************************************************************************************************************
 * 
 *  --{   Audio Manager   }--
 *							  
 *	UI Audio Player Editor
 *      The editor script for the UI Audio Player, handles the custom inspector for the UI player.
 *			
 *	Please refrain from editing this script as it will cause who knows what to the UI audio player.
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
    [CustomEditor(typeof(UIAudioPlayer)), CanEditMultipleObjects]
    public class UIAudioPlayerEditor : Editor
    {
        private Color32 grnCol = new Color32(41, 176, 97, 255);
        private Color32 redCol = new Color32(190, 42, 42, 255);
        private SerializedProperty file;
        private UIAudioPlayer player;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Method | OnEnable | Assigns the script and does any setup needed.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            file = serializedObject.FindProperty("audioManagerFile");
            player = (UIAudioPlayer)target;
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Method | OnInspectorGUI | Overrides the default inspector of the UI Audio Player Script with this custom one.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            HeaderDisplay();

            if (file.objectReferenceValue == null)
            {
                file = serializedObject.FindProperty("audioManagerFile");
            }

            if (!player)
            {
                player = (UIAudioPlayer)target;
            }


            EditorGUILayout.BeginVertical("Box");
            GUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("AMF", EditorStyles.boldLabel, GUILayout.MaxWidth(55f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);

            GetClipString();

            // Audio Manager File (AMF) field
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(file, new GUIContent("File To Use: "));
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);
            EditorGUILayout.EndVertical();

            GUILayout.Space(5f);

            EditorGUILayout.BeginVertical("Box");
            GUILayout.Space(5f);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel, GUILayout.MaxWidth(65f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Volume: ", GUILayout.MaxWidth(55f));
            player.volume = EditorGUILayout.FloatField(player.volume);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Pitch: ", GUILayout.MaxWidth(55f));
            player.pitch = EditorGUILayout.FloatField(player.pitch);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);

            DisplayMixers();

            GUILayout.Space(5f);

            EditorGUILayout.LabelField("Clips To Play:", GUILayout.MaxWidth(100f));

            GUILayout.Space(5f);

            if (player.clipsChosen != null && player.clipsChosen.Count > 0)
            {
                for (int i = 0; i < player.clipsChosen.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    player.clipsChosen[i] = EditorGUILayout.Popup(player.clipsChosen[i], player.clipsToPlay.ToArray());


                    GUI.color = grnCol;

                    if (GUILayout.Button("+", GUILayout.Width(25)))
                        player.clipsChosen.Add(0);


                    if (!i.Equals(0))
                    {
                        GUI.color = Color.red;

                        if (GUILayout.Button("-", GUILayout.Width(25)))
                            player.clipsChosen.RemoveAt(i);
                    }
                    else
                    {
                        GUI.color = Color.black;

                        if (GUILayout.Button("", GUILayout.Width(25)))
                        {
                        }
                    }

                    GUI.color = Color.white;

                    EditorGUILayout.EndHorizontal();
                }
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Add Clip(s)", GUILayout.Width(85)))
                    player.clipsChosen.Add(0);

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(5f);
            EditorGUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// UI Audio Player Editor Method | Shows the header info including logo, asset name and documentation/discord buttons.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void HeaderDisplay()
        {
            GUILayout.Space(10f);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Shows either the Carter Games Logo or an alternative for if the icon is deleted/not included when you import the package
            // Note: if you are using an older version of the asset, the directory/name of the logo may not match this and therefore will display the text title only
            if (Resources.Load<Texture2D>("Carter Games/Audio Manager/LogoAM"))
            {
                if (GUILayout.Button(Resources.Load<Texture2D>("Carter Games/Audio Manager/LogoAM"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    GUI.FocusControl(null);
                }
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);

            // Label that shows the name of the script / tool & the Version number for user reference sake.
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("UI Audio Player (Alpha)", EditorStyles.boldLabel, GUILayout.Width(160f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Version: 2.4.1", GUILayout.Width(87.5f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);

            // Links to the docs and discord server for the user to access quickly if needed.
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Documentation", GUILayout.Width(110f)))
            {
                Application.OpenURL("https://carter.games/audiomanager");
            }
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("Discord", GUILayout.Width(65f)))
            {
                Application.OpenURL("https://carter.games/discord");
            }
            GUI.backgroundColor = redCol;
            if (GUILayout.Button("Report Issue", GUILayout.Width(100f)))
            {
                Application.OpenURL("https://carter.games/report");
            }
            GUI.backgroundColor = Color.white;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);
            EditorGUILayout.HelpBox("This script is still in its early days. Some functionality may be missing. Some elements of this script may not work. Feel free to give us suggestions on how to improve this script on our discord server, or report issues using the buttons provided above.", MessageType.Warning);

            GUILayout.Space(10f);
        }

        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// UI Audio Player Editor Method | Creates the display that is used to show all the mixers
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void DisplayMixers()
        {
            // Going through all the audio clips and making an element in the Inspector for them
            if (player.audioManagerFile && player.audioManagerFile.audioMixer != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Mixer To Use:", GUILayout.MaxWidth(90f));
                player.mixerChosen = EditorGUILayout.Popup(player.mixerChosen, AudioMixerListToStringArray(player.audioManagerFile.audioMixer));
                EditorGUILayout.EndHorizontal();
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// UI Audio Player Editor Method | Gets the clip strings for the enum selection of clips to play.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void GetClipString()
        {
            if (player && player.audioManagerFile && player.audioManagerFile.library != null)
            {
                player.clipsToPlay.Clear();

                for (int i = 0; i < player.audioManagerFile.library.Count; i++)
                {
                    player.clipsToPlay.Add(player.audioManagerFile.library[i].key);
                }
            }
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// UI Audio Player Editor Method | Returns the names of all the audio mixers in the AMF.
        /// </summary>
        /// <param name="mixers">List of AudioMixerGroup | The mixers in the AMF to give options for.</param>
        /// <returns>String Array | the names of each audio mixer group for use in the mixer selector.</returns>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private string[] AudioMixerListToStringArray(List<AudioMixerGroup> mixers)
        {
            string[] toReturn = new string[mixers.Count];

            for (int i = 0; i < mixers.Count; i++)
            {
                toReturn[i] = mixers[i].ToString();
            }

            return toReturn;
        }
    }
}