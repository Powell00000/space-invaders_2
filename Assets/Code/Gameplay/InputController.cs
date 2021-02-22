using UnityEngine;

namespace Game.Gameplay
{
    //just input
    public class InputController : MonoBehaviour
    {
        [Zenject.Inject] private GameplayController gameplayCtrl = null;
        private float horizontal = 0;
        private bool fire;

        //debug
        private bool restart;
        private bool destroyAll;

        public float Horizontal => horizontal;
        public bool Fire => fire;

        public System.Action Restart;
        public System.Action KillAllEnemies;
        public System.Action GodModePressed;

        private void Update()
        {
            restart = false;
            destroyAll = false;
            horizontal = 0;
            fire = false;

            restart = Input.GetKeyDown(KeyCode.F1);

            if (restart)
            {
                if (Restart != null)
                {
                    Restart();
                }
            }

            //cluttered code
            if (gameplayCtrl.CurrentGameplayState == EGameplayState.Playing)
            {
                #region DEBUGS
                destroyAll = Input.GetKeyDown(KeyCode.F2);

                if (Input.GetKeyDown(KeyCode.F3))
                {
                    if (GodModePressed != null)
                    {
                        GodModePressed();
                    }
                }
                #endregion

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
                float screenMiddle = Screen.width / 2;
                var isTouching = Input.GetMouseButton(0);
                if (isTouching)
                {
                    var touchPosition = Input.GetTouch(0).position;

                    Vector2 direction;

                    if (touchPosition.x < screenMiddle)
                    {
                        direction = Vector2.left;
                    }
                    else
                    {
                        direction = Vector2.right;
                    }
                    horizontal = direction.x;
                }
                else
                {
                    horizontal = 0;
                }

                fire = Input.touchCount == 2;
#else
                horizontal = Input.GetAxisRaw("Horizontal");
                fire = Input.GetButtonDown("Fire1");
#endif

                if (destroyAll)
                {
                    if (KillAllEnemies != null)
                    {
                        KillAllEnemies();
                    }
                }
            }

        }
    }
}