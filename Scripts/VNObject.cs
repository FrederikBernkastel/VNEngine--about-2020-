using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Perekr
{
    public class VNObject : Transformable, Drawable, ICloneable
    {
        public bool update = false;
        public bool render = false;
        public bool delete = false;
        public string name = "";
        public VNObject Game;
        public virtual List<VNObject> dict_time_up { get; set; } = new List<VNObject>();
        public virtual List<VNObject> dict_all { get; set; } = new List<VNObject>();
        public virtual List<VNObject> dict_script { get; set; } = new List<VNObject>();
        public VNObject this[string na_me, VNObject obj]
        {
            get
            {
                if (!dict_all.Any(u => u.name == na_me)) dict_all.Add(InitOne(obj.SetName(na_me)));
                return this;
            }
        }
        public VNObject this[VNObject obj]
        {
            get
            {
                dict_all.Add(InitOne(obj));
                return this;
            }
        }
        public T GetComponentUp<T>()
        {
            if (Game == null) return default;
            for (int i = 0; i < Game.dict_all.Count; i++)
                if (Game.dict_all[i] is T t) return t;
            return default;
        }
        public T GetComponentDown<T>()
        {
            for (int i = 0; i < dict_all.Count; i++)
                if (dict_all[i] is T t) return t;
            return default;
        }
        public VNObject this[string na_me] => dict_all.Find(u => u.name == na_me);
        public VNObject SetName(string na_me)
        {
            name = na_me;
            return this;
        }
        public VNObject(params VNObject[] objects)
        {
            dict_all.AddRange(objects);
        }
        public VNObject(IEnumerable<VNObject> objects)
        {
            dict_all.AddRange(objects);
        }
        public VNObject(List<VNObject> sprites)
        {
            dict_all = sprites;
        }
        public virtual Vector2f AbsPosition => Game == null ? Position : Game.Position - Game.Origin + Position - Origin;
        public new virtual Vector2f Position
        {
            get => base.Position;
            set => base.Position = value;
        }
        public new virtual Vector2f Scale
        {
            get => base.Scale;
            set => base.Scale = value;
        }
        public new virtual float Rotation
        {
            get => base.Rotation;
            set => base.Rotation = value;
        }
        public new virtual Vector2f Origin
        {
            get => base.Origin;
            set => base.Origin = value;
        }
        public virtual Vector2f RelPosition => Position;
        public virtual Vector2f AbsSize
        {
            get => Extens.Max(dict_all.Select(u => u.RelPosition + u.AbsSize)) - Extens.Min(dict_all.Select(u => u.RelPosition));
        }
        public VNObject InitOne(VNObject @object)
        {
            @object.Game = this;
            return @object.Init();
        }
        public virtual VNObject Init()
        {
            foreach (var j in dict_script)
            {
                j.Game = this;
                if (j.update) j.Init();
            }
            foreach (var s in dict_all)
            {
                s.Game = this;
                s.Init();
            }
            return this;
        }
        public virtual void Sub() { }
        public virtual void UnSub() { }
        public virtual VNObject Interpolation(float inter)
        {
            foreach (var s in dict_all) s.Interpolation(inter);
            return this;
        }
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            foreach (var s in dict_all) if (s.render) target.Draw(s, states);
        }
        public virtual void Remove()
        {
            dict_all.RemoveAll(u => u.delete && (!dict_time_up.Remove(u) || true));
            foreach (var s in dict_all) s.Remove();
        }
        public virtual void Update_global()
        {
            foreach (var s in dict_script) if (s.update) s.Update(0);
            foreach (var s in dict_time_up) if (s.update) s.Update(0);
        }
        public virtual void Update(float delta)
        {
            foreach (var s in dict_script) if (s.update) s.Update(delta);
            foreach (var s in dict_all) if (!dict_time_up.Contains(s) && s.update) s.Update(delta);
        }
        public virtual object Clone() => MemberwiseClone();
    }
}
