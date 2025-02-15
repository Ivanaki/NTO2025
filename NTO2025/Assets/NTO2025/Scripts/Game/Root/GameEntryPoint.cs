using System.Collections;
using BaCon;
using Game.Params;
using Gameplay.Root;
using MainMenu.Root;
using MyUtils;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Root
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private readonly DIContainer _rootContainer = new DIContainer();
        private DIContainer _cachedSceneContainer;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            _coroutines = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);
            
            //var save = new JsonToFileLoadSaveService();
            //_rootContainer.RegisterInstance<ILoadSaveService>(save);

            //_rootContainer.RegisterFactory(_ => new Account(save)).AsSingle();
            //_rootContainer.RegisterFactory(_ => new CursorLocker()).AsSingle();
        }

        private void RunGame()
        {
#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().name == Scenes.GAMEPLAY)
            {
                _coroutines.StartCoroutine(LoadAndStartAfterMainMenu(new MainMenuExitParams(new GameplayEnterParams(false))));
                return;
            }

            if (SceneManager.GetActiveScene().name != Scenes.GAMEPLAY &&
                SceneManager.GetActiveScene().name != Scenes.MAIN_MENU &&
                SceneManager.GetActiveScene().name != Scenes.START_STEAM_VR)
            {
                return;
            }
#endif
            _coroutines.StartCoroutine(LoadAndStartMainMenu());
        }

        private IEnumerator LoadAndStartMainMenu()
        {
            yield return LoadScene(Scenes.MAIN_MENU);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer);
            sceneEntryPoint.Run(gameplayContainer).Subscribe(mainMenuExitParams =>
            {
                _coroutines.StartCoroutine(LoadAndStartAfterMainMenu(mainMenuExitParams));
            });
        }

        private bool flag = true;
        private IEnumerator LoadAndStartAfterMainMenu(MainMenuExitParams exitParams)
        {
            if (flag)
            {
                yield return LoadScene(Scenes.START_STEAM_VR);
                flag = false;
            }

            yield return null;
            _coroutines.StartCoroutine(LoadAndStartGameplay(exitParams));
        }
        
        private IEnumerator LoadAndStartGameplay(MainMenuExitParams mainMenuExitParams)
        {
            yield return LoadScene(Scenes.GAMEPLAY);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            var gameplayContainer = _cachedSceneContainer = new DIContainer(_rootContainer); 
            sceneEntryPoint.Run(gameplayContainer, mainMenuExitParams.GameplayEnterParams).Subscribe(_ =>
            { 
                _coroutines.StartCoroutine(LoadAndStartMainMenu());
            });
        }
        
        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}