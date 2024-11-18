using Common.EnumExtensions;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Common.SceneEx
{
    public static class SceneManagerEx
    {
        private static string nextScene;

        /// <summary>
        /// 씬 로드 함수
        /// </summary>
        public static void LoadScene(SceneType type)
        {
            SceneManager.LoadScene(type.EnumToString());
        }

        /// <summary>
        /// 로딩 후 씬 로드 함수
        /// </summary>
        public static void LoadingAndNextScene(SceneType nextSceneType)
        {
            nextScene = nextSceneType.EnumToString();
            SceneManager.LoadScene("Loading");
        }

        /// <summary>
        /// 씬 다음 씬 비동기 로드 함수
        /// </summary>
        public static AsyncOperation LoadNextSceneAsync()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
            op.allowSceneActivation = false;

            return op;
        }

        public static void OnLoadCompleted(UnityAction<Scene, LoadSceneMode> callback)
        {
            SceneManager.sceneLoaded += callback;
        }
    }
}
