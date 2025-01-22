using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Perekr
{
    public class ConsoleDebug : VNObject
    {
        public VNRect shape;
        public VNRect rect;
        public uint size = 17;
        public VNText text;
        public string str = "";
        public List<VNObject> texts = new List<VNObject>();
        class Script_ConsoleDebug : VNObject
        {
            public ConsoleDebug type;
            public override VNObject Init()
            {
                type = (ConsoleDebug)Game;
                type.Sub();
                type.shape = new VNRect()
                {
                    Size = ((Vector2f)Program.Window.Size).MulY(0.25f),
                    OutlineThickness = 2,
                    OutlineColor = Color.Blue,
                    FillColor = new Color(0, 0, 0, 120),
                };
                type.rect = new VNRect()
                {
                    Size = type.shape.AbsSize.y(type.size + type.size * 0.2f),
                    OutlineThickness = 1,
                    OutlineColor = Color.Blue,
                    FillColor = Color.Transparent,
                    Position = new Vector2f(0, type.shape.AbsSize.Y - (type.size + type.size * 0.2f)),
                };
                type.text = new VNText("> ")
                {
                    CharacterSize = type.size,
                    FillColor = Color.White,
                    OutlineThickness = 1,
                    OutlineColor = Color.Red,
                    LetterSpacing = 3,
                    Position = new Vector2f(10, type.rect.AbsPosition.Y),
                };
                type.dict_all = new List<VNObject>
                {
                    type.shape,
                    type.rect,
                    type.text,
                    new VNObject(type.texts) { render = true },
                };
                return this;
            }
            public override void Update(float delta) { }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_ConsoleDebug() { update = true },
        };
        public override void Remove()
        {
            base.Remove();
            if (delete) UnSub();
        }
        private void Window_TextEntered(object sender, TextEventArgs e)
        {
            char ch = Convert.ToChar(e.Unicode);
            if ((ch > 32 && ch < 127) || (ch > 1039 && ch < 1104) || new int[] { 32, 16, 20 }.Contains(ch))
                str = str.Insert(str.Length, e.Unicode);
            text.DisplayedString = "> " + str;
        }
        public override void Sub()
        {
            Program.Window.KeyPressed += Window_KeyPressed;
        }
        public void SubOther()
        {
            Program.Window.TextEntered += Window_TextEntered;
        }
        public void UnSubOther()
        {
            Program.Window.TextEntered -= Window_TextEntered;
            str = "";
            text.DisplayedString = "> ";
        }
        public override void UnSub()
        {
            Program.Window.KeyPressed -= Window_KeyPressed;
            Program.Window.TextEntered -= Window_TextEntered;
        }
        public void SetString(string[] vs)
        {
            foreach (var s in vs)
            {
                float temp = (shape.AbsSize.Y - rect.AbsSize.Y) / ((shape.AbsSize.Y - rect.AbsSize.Y) / size);
                foreach (var j in texts)
                {
                    j.Position += new Vector2f(0, -(temp + 5));
                    if (j.AbsPosition.Y <= 0) j.delete = true;
                }
                VNText txt = new VNText(s)
                {
                    CharacterSize = text.CharacterSize,
                    Font = text.Font,
                    OutlineThickness = text.OutlineThickness,
                    OutlineColor = text.OutlineColor,
                    LetterSpacing = text.LetterSpacing,
                    Position = new Vector2f(0, rect.Position.Y - temp * 1.5f),
                };
                InitOne(txt);
                texts.Add(txt);
                str = "";
                text.DisplayedString = "> ";
            }
        }
        public void Parsing(string str)
        {
            if (str == "") return;
            else if (str == "hello")
            {
                SetString(new string[]
                {
                    "Hello World",
                    "Hello Paris",
                    "Hello Russian",
                });
            }
            else
            {
                SetString(new string[]
                {
                    "Неизвестная команда...",
                });
            }
        }
        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.F1:
                    update = !update;
                    render = update;
                    if (update && render) SubOther();
                    else UnSubOther();
                    break;
                case Keyboard.Key.Escape:
                    goto case Keyboard.Key.F1;
                case Keyboard.Key.Enter:
                    Parsing(str);
                    break;
                case Keyboard.Key.Backspace:
                    str = str.Length != 0 ? str[..^1] : "";
                    break;
            }
        }
    }
}
