using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using AI_Fighter_2d.Components;
using System.Collections.Generic;

namespace AI_Fighter_2d
{
    public class CoreGame : Game
    {
        private List<BaseComponent> _components;
        private Texture2D _grassTexture;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _screenCenter;

        public CoreGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            SceneManager.Instance.Initialize();

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;   
            _graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _screenCenter = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);

            var grassSprite = new Sprite(_grassTexture, "Grass-002", Content.Load<SpriteFont>("default"))
            {
                Position = _screenCenter,
                Text = "Click Me!"
            };

            grassSprite.LoadTexture(Content);

            grassSprite.Click += SceneManager.Instance.GetNextScene_Click;

            _components = new List<BaseComponent>()
            {
                grassSprite
            };

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _components.ForEach(x => x.Update());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (SceneManager.Instance.ActiveScene == SceneManager.Scene.MainMenu)
            {
                GraphicsDevice.Clear(Color.BlueViolet);
            }
            else
            {
                GraphicsDevice.Clear(Color.White);
            }

            _spriteBatch.Begin();
            _components.ForEach(x => x.Draw(_spriteBatch));
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
