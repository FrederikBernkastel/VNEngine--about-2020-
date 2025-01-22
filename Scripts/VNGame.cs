using System.Collections.Generic;

namespace Perekr
{
    public class VNGame : VNObject
    {
        class Script_VNGame : VNObject
        {
            public VNGame type;
            public override VNObject Init()
            {
                type = (VNGame)Game;
                type.update = !type.update;
                type.render = !type.render;
                type.dict_all = new List<VNObject>
                {
                    new StateGame(),
                    new ConsoleDebug(),
                    new FPS(),
                };
                return this;
            }
            public override void Update(float delta) { }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_VNGame() { update = true },
        };
    }
}
