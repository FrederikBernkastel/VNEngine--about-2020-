using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Perekr
{
    public class FPS : VNObject
    {
        public VNText text;
        public Clock delayTimer = new Clock();
        public Clock fpsTimer = new Clock();
        public float fps = 0;
        public int frameCount = 0;
        class Script_FPS : VNObject
        {
            public FPS type;
            public override VNObject Init()
            {
                type = (FPS)Game;
                type.text = new VNText("")
                {
                    CharacterSize = 25,
                    OutlineThickness = 2,
                    OutlineColor = Color.Black
                };
                type.Sub();
                type.Game.dict_time_up.Add(Game);
                type.update = true;
                type.dict_all = new List<VNObject>
                {
                    type.text,
                };
                return this;
            }
            public override void Update(float delta)
            {
                type.frameCount++;
                if (type.delayTimer.ElapsedTime.AsSeconds() > 0.2f)
                {
                    type.fps = type.frameCount / type.fpsTimer.Restart().AsSeconds();
                    type.frameCount = 0;
                    type.delayTimer.Restart();
                }
                type.text.DisplayedString = ((int)type.fps).ToString();
                type.text.Position = new Vector2f(10, 10);
            }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_FPS() { update = true },
        };
        public override void UnSub()
        {
            Program.Window.KeyPressed -= Window_KeyPressed;
        }
        public override void Sub()
        {
            Program.Window.KeyPressed += Window_KeyPressed;
        }
        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.F2:
                    render = !render;
                    break;
            }
        }
        public override void Remove()
        {
            base.Remove();
            if (delete) UnSub();
        }
    }
}
