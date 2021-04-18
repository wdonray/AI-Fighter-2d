using System;
using System.Collections.Generic;
using System.Text;

namespace AI_Fighter_2d
{
    class SceneManager
    {
        [Flags]
        public enum Scene
        {
            MainMenu = 0,
            TestScene = 1
        }

        private static SceneManager instance;
        private Scene _activeScene;
        public Scene ActiveScene { get => _activeScene; set => _activeScene = value; }
        public static SceneManager Instance { get {
            if (instance == null)
            {
                instance = new SceneManager();
            }
            return instance;
        }}

        public void Initialize()
        {
            ActiveScene = Scene.MainMenu;
        }

        public Scene GetNextScene()
        {
            Scene sceneToProcess = 0;
            int currentSceneIndex = (int)ActiveScene;
            foreach(int i in Enum.GetValues(typeof(Scene)))
            {
                if (i - currentSceneIndex == 1)
                {
                    sceneToProcess = (Scene)i;
                }
            }
            return sceneToProcess;
        }

        public void GetNextScene_Click(object sender, System.EventArgs e)
        {
            _activeScene = GetNextScene();
        }

        public string GetCurrentSceneName()
        {
            return ActiveScene.ToString();
        }
    }
}
