using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Perekr
{
    public class VNRect : VNObject
    {
        public RectangleShape rect = new RectangleShape();
        class Script_VNRect : VNObject
        {
            public VNRect type;
            public override VNObject Init()
            {
                type = (VNRect)Game;
                type.render = true;
                return this;
            }
            public override void Update(float delta) { }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_VNRect() { update = true },
        };
        public Color FillColor
        {
            get => rect.FillColor;
            set => rect.FillColor = value;
        }
        public Color OutlineColor
        {
            get => rect.OutlineColor;
            set => rect.OutlineColor = value;
        }
        public float OutlineThickness
        {
            get => rect.OutlineThickness;
            set => rect.OutlineThickness = value;
        }
        public Texture Texture
        {
            get => rect.Texture;
            set => rect.Texture = value;
        }
        public IntRect TextureRect
        {
            get => rect.TextureRect;
            set => rect.TextureRect = value;
        }
        public Vector2f Size
        {
            get => rect.Size;
            set => rect.Size = value;
        }
        public override Vector2f Position
        {
            get => rect.Position;
            set => rect.Position = value;
        }
        public override Vector2f Scale
        {
            get => rect.Scale;
            set => rect.Scale = value;
        }
        public override float Rotation
        {
            get => rect.Rotation;
            set => rect.Rotation = value;
        }
        public override Vector2f Origin
        {
            get => rect.Origin;
            set => rect.Origin = value;
        }
        public VNRect(Color color)
        {
            rect = new RectangleShape(new Vector2f(1920, 1080)) { FillColor = color };
        }
        public VNRect(RectangleShape rect)
        {
            this.rect = new RectangleShape(rect.Size)
            {
                FillColor = rect.FillColor,
                OutlineColor = rect.OutlineColor,
                OutlineThickness = rect.OutlineThickness,
                Texture = rect.Texture,
                TextureRect = rect.TextureRect,
            };
        }
        public VNRect() { }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rect, states);
        }
        public override Vector2f RelPosition => new Vector2f(rect.GetGlobalBounds().Left, rect.GetGlobalBounds().Top);
        public override Vector2f AbsSize => new Vector2f(rect.GetGlobalBounds().Width, rect.GetGlobalBounds().Height);
        public override object Clone()
        {
            return new VNRect
            {
                rect = new RectangleShape()
                {
                    Size = rect.Size,
                    FillColor = rect.FillColor,
                    OutlineColor = rect.OutlineColor,
                    OutlineThickness = rect.OutlineThickness,
                    Texture = rect.Texture,
                    TextureRect = rect.TextureRect,
                },
            };
        }
        public override string ToString()
        {
            return AbsSize.ToString();
        }
    }
    public class VNSprite : VNObject
    {
        public Sprite sprite = new Sprite();
        class Script_VNSprite : VNObject
        {
            public VNSprite type;
            public override VNObject Init()
            {
                type = (VNSprite)Game;
                type.render = true;
                return this;
            }
            public override void Update(float delta) { }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_VNSprite() { update = true },
        };
        public Color Color
        {
            get => sprite.Color;
            set => sprite.Color = value;
        }
        public Texture Texture
        {
            get => sprite.Texture;
            set => sprite.Texture = value;
        }
        public IntRect TextureRect
        {
            get => sprite.TextureRect;
            set => sprite.TextureRect = value;
        }
        public override Vector2f Position
        {
            get => sprite.Position;
            set => sprite.Position = value;
        }
        public override Vector2f Scale
        {
            get => sprite.Scale;
            set => sprite.Scale = value;
        }
        public override float Rotation
        {
            get => sprite.Rotation;
            set => sprite.Rotation = value;
        }
        public override Vector2f Origin
        {
            get => sprite.Origin;
            set => sprite.Origin = value;
        }
        public VNSprite(string path)
        {
            sprite = new Sprite(new Texture(path));
        }
        public override Vector2f RelPosition => new Vector2f(sprite.GetGlobalBounds().Left, sprite.GetGlobalBounds().Top);
        public override Vector2f AbsSize => new Vector2f(sprite.GetGlobalBounds().Width, sprite.GetGlobalBounds().Height);
        public override object Clone()
        {
            return new VNSprite
            {
                sprite = new Sprite()
                {
                    Texture = sprite.Texture,
                    Color = sprite.Color,
                    TextureRect = sprite.TextureRect,
                },
            };
        }
        public VNSprite() { }
        public VNSprite(Sprite sprite)
        {
            this.sprite = new Sprite()
            {
                Texture = sprite.Texture,
                Color = sprite.Color,
                TextureRect = sprite.TextureRect,
            };
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(sprite, states);
        }
        public override string ToString()
        {
            return Texture.ToString();
        }
    }
    public class VNText : VNObject
    {
        public Text text = new Text();
        class Script_VNText : VNObject
        {
            public VNText type;
            public override VNObject Init()
            {
                type = (VNText)Game;
                type.render = true;
                return this;
            }
            public override void Update(float delta) { }
        }
        public override List<VNObject> dict_script { get; set; } = new List<VNObject>
        {
            new Script_VNText() { update = true },
        };
        public Color FillColor
        {
            get => text.FillColor;
            set => text.FillColor = value;
        }
        public float LetterSpacing
        {
            get => text.LetterSpacing;
            set => text.LetterSpacing = value;
        }
        public float LineSpacing
        {
            get => text.LineSpacing;
            set => text.LineSpacing = value;
        }
        public Color OutlineColor
        {
            get => text.OutlineColor;
            set => text.OutlineColor = value;
        }
        public float OutlineThickness
        {
            get => text.OutlineThickness;
            set => text.OutlineThickness = value;
        }
        public Text.Styles Style
        {
            get => text.Style;
            set => text.Style = value;
        }
        public string DisplayedString
        {
            get => text.DisplayedString;
            set => text.DisplayedString = value;
        }
        public Font Font
        {
            get => text.Font;
            set => text.Font = value;
        }
        public uint CharacterSize
        {
            get => text.CharacterSize;
            set => text.CharacterSize = value;
        }
        public override Vector2f Position
        {
            get => text.Position;
            set => text.Position = value;
        }
        public override Vector2f Scale
        {
            get => text.Scale;
            set => text.Scale = value;
        }
        public override float Rotation
        {
            get => text.Rotation;
            set => text.Rotation = value;
        }
        public override Vector2f Origin
        {
            get => text.Origin;
            set => text.Origin = value;
        }
        public override Vector2f RelPosition => new Vector2f(text.GetGlobalBounds().Left, text.GetGlobalBounds().Top);
        public override Vector2f AbsSize => new Vector2f(text.GetGlobalBounds().Width, text.GetGlobalBounds().Height);
        public override object Clone()
        {
            return new VNText
            {
                text = new Text()
                {
                    DisplayedString = text.DisplayedString,
                    Font = text.Font,
                    CharacterSize = text.CharacterSize,
                    FillColor = text.FillColor,
                    LetterSpacing = text.LetterSpacing,
                    LineSpacing = text.LineSpacing,
                    OutlineColor = text.OutlineColor,
                    OutlineThickness = text.OutlineThickness,
                    Style = text.Style,
                },
            };
        }
        public VNText() { }
        public VNText(string str)
        {
            text = new Text(str, Meta.sys_font, 30);
        }
        public VNText(Text text)
        {
            this.text = new Text()
            {
                DisplayedString = text.DisplayedString,
                Font = text.Font,
                CharacterSize = text.CharacterSize,
                FillColor = text.FillColor,
                LetterSpacing = text.LetterSpacing,
                LineSpacing = text.LineSpacing,
                OutlineColor = text.OutlineColor,
                OutlineThickness = text.OutlineThickness,
                Style = text.Style,
            };
        }
        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(text, states);
        }
        public override string ToString()
        {
            return DisplayedString;
        }
    }
}
