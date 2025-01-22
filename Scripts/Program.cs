using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Perekr
{
    class Program
    {
        public static RenderWindow Window;
        static void Main()
        {
            Window = new RenderWindow(new VideoMode(1920, 1080), "EngineDebag",
                Styles.Close, new ContextSettings() { AntialiasingLevel = 16 });
            Window.SetVerticalSyncEnabled(true);
            Window.Closed += (object Sender, EventArgs e) => Window.Close();
            VNObject game = new VNGame().Init();
            Time dt = Time.FromSeconds(1.0f / 60);
            Clock timer = new Clock();
            Time lastTime = Time.Zero;
            Time accumulator = Time.Zero;
            while (Window.IsOpen)
            {
                Time time = timer.ElapsedTime;
                Time frameTime = time - lastTime;
                if (frameTime > Time.FromSeconds(0.25f)) frameTime = Time.FromSeconds(0.25f);
                lastTime = time;
                accumulator += frameTime;
                Window.DispatchEvents();
                game.Update_global();
                while (accumulator >= dt)
                {
                    float delta = dt.AsSeconds() * 60f;
                    game.Update(delta);
                    accumulator -= dt;
                }
                game.Remove();
                float interpolation = accumulator.AsSeconds() / dt.AsSeconds();
                Window.Clear(Color.White);
                Window.Draw(game.Interpolation(interpolation));
                Window.Display();
            }
        }
    }
}
