using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using AI_Fighter_2d.Components;
using Microsoft.Xna.Framework.Input;

namespace AI_Fighter_2d
{
    class Sprite : BaseComponent
    {
        #region Fields
        private Texture2D _texture;
        private Vector2 _origin;
        private string _path;
        private SpriteFont _font;
        private MouseState _currentMouse, _previousMouse;
        private bool _isHovering;

        private readonly bool _hasOrigin;

        public Texture2D Texture { get => _texture; set => _texture = value; }
        public string Path { get => _path; set => _path = value; }
        public Vector2 Origin { get => _origin; set => _origin = value; }
        #endregion

        #region Properties 
        private Vector2 _position;
        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Rectangle Rectangle { get { return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height); } }
        public Vector2 Position { get => _position; set => _position = value; }
        public string Text { get; set; }
        #endregion

        #region Constructors
        public Sprite(Texture2D texture, string path, SpriteFont font, Vector2 origin)
        {
            _path = path;
            _hasOrigin = true;
            _origin = origin;
            _font = font;
            _texture = texture;
        }
        public Sprite(Texture2D texture, string path, SpriteFont font)
        {
            Path = path;
            _font = font;
            _hasOrigin = false;
            _texture = texture;
        }
        #endregion

        #region Methods
        public void LoadTexture(ContentManager content)
        {
            Texture = content.Load<Texture2D>(_path);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Color color = Color.White;

            if (_isHovering) color = Color.Gray;

            if (_hasOrigin) {
                spriteBatch.Draw(_texture, _position, null, color, 0f, Origin, 1f, SpriteEffects.None, 1f);
            } else {
                spriteBatch.Draw(_texture, _position, color);
            }

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / _texture.Height - 10)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), Color.Black);
            }
        }
        public override void Update()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRect = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRect.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
        #endregion
    }
}
