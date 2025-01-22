using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Perekr
{
    public class TextStatic
    {
        public static Font stand = new Font(Meta.text_font);

        // TEXT
        public static Vector2f nvl = new Vector2f(100, 200);
        public static Vector2f adv = new Vector2f(180, 700);
        public static uint text_size = 40;
        public static float dlin = new Text(" ", stand, text_size).GetGlobalBounds().Width;
        public static float hei = new Text("A", stand, text_size).GetGlobalBounds().Height;
        public static Color text_color = Color.White;
        public static int max_length = 1620;
        public static Sprite temp_dialog = GetImageFade();
        public static float speed_text = max_length * 0.01f;
        public static Color text_outlineColor = Color.Black;
        public static float text_outlinethickness = 2;
        public static float text_LetterSpacing = 1;

        // NAME
        public static Vector2f name_pos = new Vector2f(180, 630);
        public static uint name_size = 40;
        public static Color name_color = Color.White;
        public static Color name_outlineColor = Color.Black;
        public static float name_outlinethickness = 2;
        public static float name_LetterSpacing = 1.5f;

        public static Sprite GetImageFade()
        {
            Image image = new Image(85, 1);
            for (uint i = 0, temp = 0; i < image.Size.X; i++, temp += 3)
                for (uint j = 0; j < image.Size.Y; j++)
                    image.SetPixel(i, j, new Color(0, 0, 0, (byte)temp));
            return new Sprite(new Texture(image));
        }
    }
    public class Paragraph : VNObject
    {
        public List<VNObject> lines = new List<VNObject>();
        public RenderTexture renders;
        public VNSprite sprite = new VNSprite();
        public VNSprite temp_dialog = new VNSprite(TextStatic.temp_dialog);
        public List<VNRect> shape_mass = new List<VNRect>();
        public int count;
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_Paragraph() { update = true },
        };
        public override void Sub()
        {
            Program.Window.KeyPressed += Window_KeyPressed;
            Program.Window.MouseButtonPressed += Window_MouseButtonPressed;
        }
        public override void UnSub()
        {
            Program.Window.KeyPressed -= Window_KeyPressed;
            Program.Window.MouseButtonPressed -= Window_MouseButtonPressed;
        }
        public void isMoveStack()
        {
            if (update)
            {
                sprite = new VNSprite(Extens.AlphaGluing(lines.ToArray(), renders));
                InitOne(sprite);
                update = false;
            }
            else
            {
                UnSub();
                ( (VNDialoge)Game ).proccess.MoveStack();
            }
        }
        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Enter:
                    isMoveStack();
                    break;
                case Keyboard.Key.Space:
                    goto case Keyboard.Key.Enter;
            }
        }
        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            switch (e.Button)
            {
                case Mouse.Button.Left:
                    isMoveStack();
                    break;
            }
        }
        class Script_Paragraph : VNObject
        {
            public Paragraph type;
            public override VNObject Init()
            {
                type = (Paragraph)Game;
                type.update = true;
                type.render = true;
                type.InitOne(type.temp_dialog);
                type.dict_all = new List<VNObject>
                {
                    //type.sprite,
                };
                return this;
            }
            public override void Update(float delta)
            {
                //type.temp_dialog.Position += new Vector2f(TextStatic.speed_text * delta, 0);
                //type.shape_mass[^type.count].Position += new Vector2f(TextStatic.speed_text * delta, 0);
                //if (type.temp_dialog.Position.X >= type.lines[^type.count].AbsSize.X)
                //{
                //    if (type.count == 1)
                //        type.update = false;
                //    else
                //    {
                //        type.temp_dialog.Scale = new Vector2f(1, type.lines[^(--type.count)].AbsSize.Y);
                //        type.temp_dialog.Position = new Vector2f(-type.temp_dialog.AbsSize.X, type.lines[^type.count].RelPosition.Y);
                //    }
                //}
                //type.temp_dialog.Position = new Vector2f(400, 0);
                //type.sprite = new VNSprite(Extens.DialogFade(type.lines, type.shape_mass, type.temp_dialog, type.renders));
                //type.InitOne(type.sprite);
            }
        }
        public struct TextFormat
        {
            public Color color;
            public Range range;
            public Text.Styles styles;
            public TextFormat(Range range, Color color)
            {
                this.range = range;
                this.color = color;
                styles = Text.Styles.Regular;
            }
            public TextFormat(Range range, Text.Styles styles)
            {
                this.range = range;
                this.styles = styles;
                color = TextStatic.text_color;
            }
        }
        public Paragraph(string str, string pr, bool temp)
        {
            string[] currText = str.Split(' ');
            List<List<TextFormat>> format = new List<List<TextFormat>>();
            string[] temps = { "CL", "BL", "IL", "ST", "UL" };
            Dictionary<string, Text.Styles> keys = new Dictionary<string, Text.Styles>
            {
                ["BL"] = Text.Styles.Bold,
                ["IL"] = Text.Styles.Italic,
                ["ST"] = Text.Styles.StrikeThrough,
                ["UL"] = Text.Styles.Underlined,
            };
            for (int i = 0; i < temps.Length; i++)
            {
                format.Add(new List<TextFormat>());
                for (int j = 0; j < Regex.Matches(pr, temps[i]).Count; j++)
                {
                    string txt = Regex.Match(pr, Regex.Escape(temps[i] + "(") + @"(.*?)" + Regex.Escape(")")).Groups[1].Value;
                    pr = Regex.Replace(pr, $"{temps[i]}({txt})", "");
                    string[] mass = txt.Split('|');
                    string[] mass_text = mass[^1].Split('-');
                    if (mass.Length == 1)
                        format[i].Add(new TextFormat(int.Parse(mass_text[^mass_text.Length])..int.Parse(mass_text[^1]), keys[temps[i]]));
                    else
                        format[i].Add(new TextFormat(int.Parse(mass_text[^mass_text.Length])..int.Parse(mass_text[^1]), States.color_pair[mass[0]]));
                }
            }
            if (temp)
            {
                currText[0] = "\"" + currText[0];
                currText[^1] = currText[^1] + "\"";
            }
            List<VNText> texts = new List<VNText>();
            for (int i = 0, j = -(int)(TextStatic.dlin * 1.5f); i < currText.Length; i++)
            {
                VNText text = new VNText(currText[i])
                {
                    Style = Text.Styles.Regular,
                    FillColor = TextStatic.text_color,
                    CharacterSize = TextStatic.text_size,
                    Font = TextStatic.stand,
                    OutlineThickness = TextStatic.text_outlinethickness,
                    OutlineColor = TextStatic.text_outlineColor,
                    LetterSpacing = TextStatic.text_LetterSpacing,
                };
                InitOne(text);
                j += (int)(TextStatic.dlin * 1.5f + text.AbsSize.X);
                texts.Add(text);
                if (j > TextStatic.max_length)
                {
                    VNObject @objects = new VNObject(texts) { render = true };
                    lines.Add(InitOne(@objects));
                    j = -(int)(TextStatic.dlin * 1.5f);
                    texts.Clear();
                    continue;
                }
            }
            if (texts.Count != 0)
            {
                VNObject @object = new VNObject(texts) { render = true };
                lines.Add(InitOne(@object));
            }
            //foreach (var s in format)
            //    foreach (var n in s)
            //        foreach (var u in texts)
            //            if (Extens.CreatRange(n.range.Start.Value, n.range.End.Value).Contains(currText.ToList().IndexOf(u.DisplayedString)))
            //            {
            //                u.FillColor = n.color;
            //                u.Style |= n.styles;
            //            }
            //foreach (var s in format)
            //{
            //    foreach (var n in s)
            //    {
            //        for (int i = n.range.Start.Value; i < (n.range.End.Value == n.range.Start.Value ?
            //            n.range.End.Value + 1 : n.range.End.Value); i++)
            //        {

            //        }
            //    }
            //}
            temp_dialog.Scale = new Vector2f(1, lines[0].AbsSize.Y);
            temp_dialog.Position = new Vector2f(-temp_dialog.AbsSize.X, 0);
            for (int i = 0; i < lines.Count; i++)
            {
                lines[i].Position = new Vector2f(0, i * 45);
                for (int j = 1; j < lines[i].dict_all.Count; j++)
                    lines[i].dict_all[j].Position = new Vector2f(lines[i].dict_all[j - 1].RelPosition.X + lines[i].dict_all[j - 1].AbsSize.X + TextStatic.dlin * 1.5f, 0);
                VNRect rect = new VNRect(new RectangleShape(new Vector2f(TextStatic.max_length, lines[i].AbsSize.Y)))
                {
                    FillColor = Color.Transparent,
                    Position = new Vector2f(0, lines[i].RelPosition.Y)
                };
                InitOne(rect);
                shape_mass.Add(rect);
            }
            renders = new RenderTexture((uint)TextStatic.max_length, (uint)InitOne(new VNObject(lines)).AbsSize.Y);
            count = lines.Count;
            Sub();
            dict_all.Add(InitOne(new VNObject(lines) { render = true }));
            dict_all[0].Position = new Vector2f(100, 100);
            //dict_all.Add(temp_dialog);
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            foreach (var s in dict_all)
                if (s.render)
                    target.Draw(s, states);

            //states.Transform *= Transform;
            //foreach (var s in lines)
            //    target.Draw(s, states);
        }
    }
    public class ADV : VNDialoge
    {
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_ADV() { update = true },
        };
        class Script_ADV : VNObject
        {
            public ADV type;
            public override VNObject Init()
            {
                type = (ADV)Game;
                type.proccess = ( (Dialoge)type.Game ).proccess;
                type.Position = new Vector2f(100, 100);
                type.update = true;
                type.render = true;
                return this;
            }
            public override void Update(float delta) { }
        }
        public override void SetText(string text, string pr, bool trig)
        {
            Clear();
            dict_all.Add(InitOne(new Paragraph(text, pr, trig)));
            Game.update = true;
            Game.render = true;
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            foreach (var s in dict_all)
                if (s.render)
                    target.Draw(s, states);
        }
    }
    public class NVL : VNDialoge
    {
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_NVL() { update = true },
        };
        class Script_NVL : VNObject
        {
            public NVL type;
            public override VNObject Init()
            {
                type = (NVL)Game;
                type.proccess = ( (Dialoge)type.Game ).proccess;
                type.Position = new Vector2f(100, 100);
                type.update = true;
                type.render = true;
                return this;
            }
            public override void Update(float delta) { }
        }
        public override void SetText(string text, string pr, bool trig)
        {
            dict_all.Add(InitOne(new Paragraph(text, pr, trig)));
            Game.update = true;
            Game.render = true;
        }
    }
    public class Dialoge : VNObject
    {
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_Dialoge() { update = true },
        };
        public VNDialoge dialoge;
        public Proccess proccess;
        class Script_Dialoge : VNObject
        {
            public Dialoge type;
            public override VNObject Init()
            {
                type = (Dialoge)Game;
                type.proccess = type.GetComponentUp<Proccess>();
                type.dialoge = new ADV();
                type.dict_all = new List<VNObject>
                {
                    type.dialoge,
                };
                return this;
            }
            public override void Update(float delta) { }
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            foreach (var s in dict_all)
                if (s.render)
                    target.Draw(s, states);
        }
    }
    public class VNDialoge : VNObject
    {
        public Proccess proccess;
        public bool life_text { get; set; } = false;
        public virtual void SetText(string text, string pr, bool trig) { }
        public void Clear()
        {
            foreach (var s in dict_all)
                s.delete = true;
        }
    }
}
