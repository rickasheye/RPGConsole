using System;
using System.Collections.Generic;
using System.Text;

namespace RPGConsole.EngineStuff
{
    public class GameObject : Component
    {
        public Vector2 position;
        public Vector2 size;
        public List<Component> components = new List<Component>();

        public GameObject(string name, Vector2 position, Vector2 size) : base(name)
        {
            this.position = position;
            this.size = size;
        }

        public GameObject(string name, Vector2 position) : base(name)
        {
            this.position = position;
            this.size = null;
        }

        public GameObject(string name) : base(name)
        {
            this.position = null;
            this.size = null;
        }

        public GameObject()
        {
            //unfortunately
            if (Program.debugMode == true) { Program.unit.AddConsoleItem("Not recommended to leave a gameobject empty"); }
        }

        public void Move(Vector2 position)
        {
            this.position = position;
        }

        public void Scale(Vector2 size)
        {
            this.size = size;
        }

        public Component AddComponent(Component comp)
        {
            if (ContainsComponent(comp))
            {
                Program.unit.AddConsoleItem("Unfortunately that component already exists!");
                return null;
            }
            else
            {
                components.Add(comp);
                Program.unit.AddConsoleItem("Fortunately your component has been added to the register!");
                return comp;
            }
        }

        public void RemoveComponent(Component comp)
        {
            if (ContainsComponent(comp))
            {
                for(int i = 0; i < components.Count - 1; i++)
                {
                    components.RemoveAt(i);
                }
            }
            else
            {
                Program.unit.AddConsoleItem("Unable to remove since this component doesnt exist!");
            }
        }

        public bool ContainsComponent(Component comp)
        {
            for(int i = 0; i < components.Count -1; i++)
            {
                if(comp.name == components[i].name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
