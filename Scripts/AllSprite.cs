using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Perekr
{
    public class Base : VNObject
    {
        class Script_Base : VNObject
        {
            public Base type;
            public override VNObject Init()
            {
                type = (Base)Game;
                type.update = true;
                type.render = true;
                return this;
            }
            public override void Update(float delta) { }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_Base() { update = true },
        };
        public virtual void Show(string name)
        {
            VNObject sprite = (VNObject)States.res_base[name].Clone();
            sprite.Origin = sprite.AbsSize / 2;
            sprite.Position = (Vector2f)Program.Window.Size / 2;
            _ = this[name, sprite];
        }
        public virtual void Hide()
        {
            delete = true;
        }
    }
    class BaseBG : Base { }
    class BaseCG : Base { }
    class BaseOther : Base { }
    class BaseCharacter : Base
    {

    }
    public class ShowSprite : VNObject
    {
        public Proccess proccess;
        class Script_ShowSprite : VNObject
        {
            public ShowSprite type;
            public override VNObject Init()
            {
                type = (ShowSprite)Game;
                type.proccess = type.GetComponentUp<Proccess>();
                type.update = true;
                type.render = true;
                type.Sub();
                return this;
            }
            public override void Update(float delta) { }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_ShowSprite() { update = true },
        };
        public override void Remove()
        {
            base.Remove();
            if (delete) UnSub();
        }
        public override void Sub()
        {
            Program.Window.MouseButtonPressed += Window_MouseButtonPressed;
        }
        public override void UnSub()
        {
            Program.Window.MouseButtonPressed -= Window_MouseButtonPressed;
        }
        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:
                    proccess.MoveStack();
                    break;
            }
        }
    }
}
