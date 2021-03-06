﻿using Assets.Code.Gameplay;
using Assets.Code.Gameplay.Waves;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Gameplay
{
    //base class for setting gameplay states and maintaining basic flow
    //gives lots of events for other systems
    public class GameplayController : MonoBehaviour, IInitializable, IDisposable
    {
        [Inject] private PlayerController playerCtrl = null;
        [Inject] private WaveManagerBase waveManager = null;
        [Inject] private InputController inputCtrl = null;
        [Inject] private PlayableArea gameBounds = null;
        [Inject] private GameplaySettings gameplaySettings = null;

        private GameplayStateMachine gameplayState;
        public Action OnLevelStarting;
        public Action OnLevelRestarting;
        public Action<EGameplayState> OnStateChanged;

        public EGameplayState CurrentGameplayState => gameplayState.CurrentState;
        public GameplaySettings GameplaySettings => gameplaySettings;

        //wait for a moment for game start
        private MEC.CoroutineHandle warmupCoroutineHandle;

        void IInitializable.Initialize()
        {
            gameplayState = new GameplayStateMachine();
            gameplayState.OnStateEnter += StateChanged;

            playerCtrl.OnPlayerDied += GameOver;
            waveManager.OnAllWavesFinished += GameOver;
            inputCtrl.Restart += Restart;

            gameBounds.CalculateBounds();
            Warmup();
        }

        private void Warmup()
        {
            gameplayState.ChangeState(EGameplayState.Warmup);

            playerCtrl.MaintainPlayerSpawn();

            warmupCoroutineHandle = MEC.Timing.CallDelayed(2f, StartPlaying);
        }

        private void StartPlaying()
        {
            gameplayState.ChangeState(EGameplayState.Playing);

            if (OnLevelStarting != null)
            {
                OnLevelStarting();
            }
        }

        public void Restart()
        {
            if (CurrentGameplayState == EGameplayState.Warmup)
            {
                return;
            }

            if (OnLevelRestarting != null)
            {
                OnLevelRestarting();
            }

            Warmup();
        }

        public void ExitToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void SetPause(bool paused = true)
        {
            if (paused)
            {
                gameplayState.ChangeState(EGameplayState.Pause);
            }
            else
            {
                gameplayState.ChangeState(EGameplayState.Playing);
            }
        }

        //both winning and losing
        private void GameOver()
        {
            gameplayState.ChangeState(EGameplayState.GameOver);
        }

        public void EndGame()
        {
            GameOver();
        }

        private void StateChanged(EGameplayState state)
        {
            if (OnStateChanged != null)
            {
                OnStateChanged(state);
            }
        }

        public void Dispose()
        {
            if (playerCtrl != null)
            {
                playerCtrl.OnPlayerDied -= GameOver;
            }

            if (waveManager != null)
            {
                waveManager.OnAllWavesFinished += GameOver;
            }

            if (inputCtrl != null)
            {
                inputCtrl.Restart += Restart;
            }
        }
    }
}