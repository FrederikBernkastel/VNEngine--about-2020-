using SFML.Window;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Perekr
{
    public class Pause : VNObject
    {

    }
    public class MusicBase : VNObject
    {

    }
    public class Proccess : VNObject
    {
        public int currentIndex = 0;
        public string first_proccess;
        public string end_proccess = "";
        public bool command = false;
        public ShowSprite show;
        public Dialoge dialoge;
        public Pause pause;
        public MusicBase music;
        class Script_Proccess : VNObject
        {
            public Proccess type;
            public override VNObject Init()
            {
                type = (Proccess)Game;
                type.show = type.GetComponentUp<ShowSprite>();
                type.dialoge = type.GetComponentUp<Dialoge>();
                type.pause = type.GetComponentUp<Pause>();
                type.music = type.GetComponentUp<MusicBase>();
                type.first_proccess = type.currentIndex == Meta.start.Length ? "" : Meta.start[type.currentIndex];
                type.update = true;
                return this;
            }
            public override void Update(float delta)
            {
                if (type.first_proccess != type.end_proccess)
                {
                    type.end_proccess = type.first_proccess;
                    type.HandlerStack();
                }
                if (type.first_proccess == "") type.Game.delete = true;
            }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_Proccess() { update = true },
        };
        public bool MoveStack()
        {
            currentIndex++;
            first_proccess = currentIndex >= Meta.start.Length ? "" : Meta.start[currentIndex];
            return true;
        }
        public string GetString(string key, string value) => Regex.Match(key, $@"{Regex.Escape(value)}\((.*?)\)").Groups[1].Value;
        public void HandlerStack()
        {
            do
            {
                string str = first_proccess;
                if (str.Contains(';'))
                {
                    string[] str_temp = str.Split(';');
                    dialoge.dialoge.SetText(str_temp[^1], str_temp[1], str_temp[0] != "");
                    //if (str_temp[0] != "")
                    //    dialoge.SetName(str_temp[0]);
                }
                else if (str.Contains("window_hide"))
                {
                    dialoge.update = false;
                    dialoge.render = false;
                    MoveStack();
                }
                else if (str.Contains("play"))
                {
                    //string type = GetString(str, "type");
                    //float fadein = str.Contains("fadein") ? Convert.ToSingle(GetString(str, "fadein")) : 0;
                    //string buffer = GetString(str, "name");
                    //bool loop = type != "sound";
                    //State.music.Push(buffer, fadein, loop);
                }
                else if (str.Contains("stop"))
                {
                    //float fadeout = str.Contains("fadeout") ? Convert.ToSingle(GetString(str, "fadeout")) : 0;
                    //string buffer = GetString(str, "name");
                    //State.music.Stop(buffer, fadeout);
                }
                else if (str.Contains(false, "scene", "cg", "other", "sprite"))
                {
                    //string[] path = GetString(str, "name").Split('|');
                    //string with = str.Contains("with") ? GetString(str, "with") : null;
                    //string[] atl = str.Contains("at") ? GetString(str, "at").Split('|') : null;
                    //Base @base;
                    //if (str.Contains("scene"))
                    //{
                    //    @base = new BaseBG();
                    //    State.dialoge.Hide();
                    //}
                    //else if (str.Contains("cg"))
                    //{
                    //    @base = new BaseCG();
                    //    State.dialoge.Hide();
                    //}
                    //else if (str.Contains("other")) @base = new BaseOther();
                    //else @base = new BaseCharacter();
                    //if (!State.show.sprites.ContainsKey(path[0])) show.sprites[path[0]] = @base;
                    //State.show.sprites[path[0]].Show(path[1], atl?[0], with);
                    //if (atl != null && atl.Length != 1) show.sprites[path[0]].time_cont = Convert.ToBoolean(atl[1]);
                    //State.show.trig = true;

                    string[] path = GetString(str, "name").Split('|');
                    Base @base = new Base();
                    _ = show[path[0], @base];
                    @base.Show(path[1]);
                }
                else if (str == "nvl")
                {
                    dialoge.dialoge = new NVL();
                    dialoge.InitOne(dialoge.dialoge);
                    MoveStack();
                }
                else if (str == "adv")
                {
                    dialoge.dialoge = new ADV();
                    dialoge.InitOne(dialoge.dialoge);
                    MoveStack();
                }
                else if (str == "clear")
                {
                    //dialoge.dialoge
                }
                else if (str == "startwith")
                {
                    //command = true;
                }
                else if (str == "endwith")
                {
                    //command = false;
                }
                else if (str.Contains("pause"))
                {
                    //str = GetString(str, "pause");
                    //if (str == "true")
                    //{
                    //    State.pause.trig = true;
                    //}
                    //else if (str == "false")
                    //{
                    //    State.pause.trig = false;
                    //}
                    //else
                    //{
                    //    State.pause.Set(Convert.ToSingle(str));
                    //}
                }
                else if (str.Contains("hide"))
                {
                    //string path = GetString(str, "name");
                    //string with = str.Contains("with") ? GetString(str, "with") : null;
                    //string[] atl = str.Contains("at") ? GetString(str, "at").Split('|') : null;
                    //State.show.sprites[path].Hide(atl?[0], with);
                    //if (atl != null && atl.Length != 1) State.show.sprites[path].time_cont = Convert.ToBoolean(atl[1]);
                    //State.show.trig = true;
                }
                else MoveStack();
            }
            while (command && MoveStack());
        }
    }
    public class StateGame : VNObject
    {
        class Script_StateGame : VNObject
        {
            public StateGame type;
            public override VNObject Init()
            {
                type = (StateGame)Game;
                type.dict_all = new List<VNObject>
                {
                    new ShowSprite(),
                    new Dialoge(),
                    new MusicBase(),
                    new Pause(),
                    new Proccess(),
                };
                type.update = true;
                type.render = true;
                type.Sub();
                return this;
            }
            public override void Update(float delta) { }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_StateGame() { update = true },
        };
        public override void Sub()
        {
            Program.Window.KeyPressed += Window_KeyPressed;
        }
        public override void UnSub()
        {
            Program.Window.KeyPressed -= Window_KeyPressed;
        }
        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Escape:
                    Program.Window.Close();
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
