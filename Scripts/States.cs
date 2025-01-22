using SFML.Graphics;
using System.Collections.Generic;

namespace Perekr
{
    class States
    {
        public static RenderTexture rend_txt1 = new RenderTexture(1920, 1080);
        public static RenderTexture rend_txt2 = new RenderTexture(1920, 1080);
        //////////////////////////////////////////////////////////////////////
        public static BlendMode BlendMasking = new BlendMode(BlendMode.Factor.Zero, BlendMode.Factor.One, BlendMode.Equation.Add,
                BlendMode.Factor.Zero, BlendMode.Factor.SrcAlpha, BlendMode.Equation.Add);
        public static BlendMode BlendIN = new BlendMode(BlendMode.Factor.Zero, BlendMode.Factor.One, BlendMode.Equation.Add,
                BlendMode.Factor.Zero, BlendMode.Factor.OneMinusSrcAlpha, BlendMode.Equation.Add);
        public static BlendMode BlendOUT = new BlendMode(BlendMode.Factor.Zero, BlendMode.Factor.One, BlendMode.Equation.Add,
                BlendMode.Factor.Zero, BlendMode.Factor.SrcAlpha, BlendMode.Equation.Add);
        public static BlendMode DialogOUT = new BlendMode(BlendMode.Factor.Zero, BlendMode.Factor.OneMinusSrcColor, BlendMode.Equation.Add,
                BlendMode.Factor.Zero, BlendMode.Factor.OneMinusSrcAlpha, BlendMode.Equation.Add);
        //////////////////////////////////////////////////////////////////////
        public static Dictionary<string, Color> color_pair = new Dictionary<string, Color>
        {
            ["white"] = Color.White,
            ["black"] = Color.Black,
            ["red"] = Color.Red,
            ["blue"] = Color.Blue,
        };
        public static Dictionary<string, VNObject> res_base = new Dictionary<string, VNObject>
        {
            ["black"] = new VNRect(Color.Black),
            ["white"] = new VNRect(Color.White),
            ["red"] = new VNRect(Color.Red),
            ///////////////////////////////////
            ["nastol"] = new VNSprite("nastol.jpg"),
            ["blogpost"] = new VNSprite("blogpost.png"),
            ///////////////////////////////////
            //["air_in1a"] = new VNSprite(""),
            //["air_in1e"] = new VNSprite(""),
            //["air_in1vc"] = new VNSprite(""),
            //["c_e0406_a"] = new VNSprite(""),
            //["chars_ber0"] = new VNSprite(""),
            //["chars_ber_d"] = new VNSprite(""),
            //["bea_a11_1_hoshin2star"] = new VNSprite(""),
            //["bea_a13_1_majime3star"] = new VNSprite(""),
            //["bea_a14_1_warai1"] = new VNSprite(""),
            //["bea_a11_1_majime3star"] = new VNSprite(""),
        };
    }
}
