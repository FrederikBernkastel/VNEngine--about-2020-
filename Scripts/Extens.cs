using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Perekr
{
    public static class Extens
    {
        public static Vector2f Max(IEnumerable<Vector2f> vectors)
        {
            Vector2f vector = new Vector2f();
            foreach (var s in vectors)
            {
                if (s.X > vector.X && s.Y > vector.Y)
                    vector = s;
            }
            return vector;
        }
        public static Vector2f Min(IEnumerable<Vector2f> vectors)
        {
            Vector2f vector = new Vector2f();
            foreach (var s in vectors)
            {
                if (s.X < vector.X && s.Y < vector.Y)
                    vector = s;
            }
            return vector;
        }
        public static int[] CreatRange(int start, int end)
        {
            int[] vs = new int[end - start];
            for (int i = start; i < end; i++)
                vs[end + i - vs.Length] = i;
            return vs;
        }
        public static bool Contains(this string str, bool trig, params string[] vs)
        {
            int temp = 0;
            for (int i = 0; i < vs.Length; i++)
                if (str.Contains(vs[i]))
                {
                    if (!trig) return true;
                    else temp++;
                }
            return temp == vs.Length;
        }
        public static Vector2f x(this Vector2f vector, float X)
        {
            vector.X = X;
            return vector;
        }
        public static Vector2f MulX(this Vector2f vector, float mul)
        {
            vector.X *= mul;
            return vector;
        }
        public static Vector2f DivX(this Vector2f vector, float div)
        {
            vector.X /= div;
            return vector;
        }
        public static Vector2f y(this Vector2f vector, float Y)
        {
            vector.Y = Y;
            return vector;
        }
        public static Vector2f MulY(this Vector2f vector, float mul)
        {
            vector.Y *= mul;
            return vector;
        }
        public static Vector2f DivY(this Vector2f vector, float div)
        {
            vector.Y /= div;
            return vector;
        }
        public static Sprite DialogFade(IEnumerable<Drawable> sprite1, IEnumerable<Drawable> sprite2, Drawable fade, RenderTexture render)
        {
            render.Clear(Color.Transparent);
            foreach (var s in new List<Drawable>(sprite1)) render.Draw(s);
            foreach (var s in new List<Drawable>(sprite2)) render.Draw(s, new RenderStates(BlendMode.None));
            render.Draw(fade, new RenderStates(States.DialogOUT));
            render.Display();
            return new Sprite(render.Texture);
        }
        public static Sprite DissolveChange(IEnumerable<Drawable> sprite1, Drawable sprite2, Drawable shape, RenderTexture[] render)
        {
            render[0].Clear(Color.Transparent);
            render[0].Draw(AlphaSprite(new List<Drawable>(sprite1), new Drawable[] { shape }, States.BlendIN, render[1]));
            render[0].Draw(AlphaSprite(new List<Drawable>(sprite1), new Drawable[] { AlphaGluing(new Drawable[] { sprite2 }, render[1]) }, States.BlendMasking, render[2]));
            render[0].Draw(AlphaSprite(sprite2, shape, States.BlendOUT, render[1]));
            render[0].Display();
            return new Sprite(render[0].Texture);
        }
        public static Sprite AlphaSprite(Drawable sprite1, Drawable sprite2, BlendMode blend, RenderTexture render)
        {
            render.Clear(Color.Transparent);
            render.Draw(sprite1);
            render.Draw(sprite2, new RenderStates(blend));
            render.Display();
            return new Sprite(render.Texture);
        }
        public static Sprite AlphaGluing(IEnumerable<Drawable> sprite, RenderTexture render)
        {
            render.Clear(Color.Transparent);
            foreach (var s in new List<Drawable>(sprite)) render.Draw(s);
            render.Display();
            return new Sprite(render.Texture);
        }
        public static float Lerp(float start, float end, float time) => start * (1 - time) + end * time;
        public static Vector2f Lerp(Vector2f start, Vector2f end, float time) => new Vector2f(Lerp(start.X, end.X, time),
            Lerp(start.Y, end.Y, time));
        public static Sprite AlphaSprite(IEnumerable<Drawable> sprite1, IEnumerable<Drawable> sprite2, BlendMode blend, RenderTexture render)
        {
            render.Clear(Color.Transparent);
            foreach (var s in new List<Drawable>(sprite1)) render.Draw(s);
            foreach (var s in new List<Drawable>(sprite2)) render.Draw(s, new RenderStates(blend));
            render.Display();
            return new Sprite(render.Texture);
        }
        //public static VNText GetT(IEnumerable<VNObject> objects)
        //{
        //    foreach (var s in objects)
        //        if (s is VNText t)
        //            return t;
        //    foreach (var s in objects)
        //    {

        //    }
        //}
    }
}
