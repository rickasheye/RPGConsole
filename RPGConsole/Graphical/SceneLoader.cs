using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.Graphical
{
    public class SceneLoader
    {
        public Scene currentScene;
        public List<Scene> currentScenes = new List<Scene>();

        public void LoadScene(Scene scene)
        {
            if (sceneExists(scene.name))
            {
                currentScene = scene;
            }
            else
            {
                Program.unit.AddConsoleItem(new ConsoleItem(3, "Incorrect scene!"));
            }
        }

        public void AddScene(Scene scene)
        {
            if (sceneExists(scene.name))
            {
                //scene exists so dont add the scene
                Program.unit.AddConsoleItem(new ConsoleItem(3, "scene exists so cancelling!"));
            }
            else
            {
                currentScenes.Add(scene);
            }
        }

        public void RemoveScene(Scene scene)
        {
            if (sceneExists(scene.name))
            {
                currentScenes.Remove(scene);
            }
            else
            {
                Program.unit.AddConsoleItem(new ConsoleItem(3, "scene doesnt exist so cancelling!"));
            }
        }

        public Scene getScene(string name)
        {
            foreach (Scene currentScene in currentScenes)
            {
                if (currentScene.name == name)
                {
                    return currentScene;
                }
            }
            return null;
        }

        public void CleanScene()
        {
            currentScene = null;
        }

        public bool sceneExists(string sceneName)
        {
            foreach(Scene currentScene in this.currentScenes)
            {
                if(currentScene.name == sceneName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
