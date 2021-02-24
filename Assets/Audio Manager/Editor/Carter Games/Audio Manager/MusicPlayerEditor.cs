using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using System.Collections.Generic;

/****************************************************************************************************************************
 * 
 *  --{   Audio Manager   }--
 *							  
 *	Music Player Editor
 *      The editor script for the Music Player script, handles the look of the inspector
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
    [CustomEditor(typeof(MusicPlayer)), CanEditMultipleObjects]
    public class MusicPlayerEditor : Editor
    {
        private Color32 redCol = new Color32(190, 42, 42, 255);
        private MusicPlayer player;


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Method | OnEnable | Assigns the script and does any setup needed.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void OnEnable()
        {
            player = (MusicPlayer)target;
        }

        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Unity Method | OnInspectorGUI | Overrides the default inspector of the Music Player Script with this custom one.
        /// </summary>
        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public override void OnInspectorGUI()
        {
            HeaderDisplay();

            if (!player)
                player = (MusicPlayer)target;


            EditorGUILayout.BeginVertical("Box");
            GUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Track To Play", EditorStyles.boldLabel, GUILayout.MaxWidth(92f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            player.clips[0] = (AudioClip)EditorGUILayout.ObjectField(player.clips[0], typeof(AudioClip), false);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Mixer", EditorStyles.boldLabel, GUILayout.MaxWidth(55f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            player.mixer = (AudioMixerGroup)EditorGUILayout.ObjectField(player.mixer, typeof(AudioMixerGroup), false);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Should Loop Track?", EditorStyles.boldLabel, GUILayout.MaxWidth(130f));
            player.shouldLoop = EditorGUILayout.Toggle(player.shouldLoop);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5f);
            EditorGUILayout.EndVertical();
        }


        /// ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Audio Manager Editor Method | Shows the header info including logo, asset name and documentation/discord buttons.
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
            EditorGUILayout.LabelField("Music Player (Alpha)", EditorStyles.boldLabel, GUILayout.Width(140f));
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
    }
}