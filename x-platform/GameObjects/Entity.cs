﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace x_platform.GameObjects
{
    public abstract class Entity
    {
        protected Texture2D texture_;
        protected Vector2 position_;
        protected double updateTimer_;
        protected const double updateFrequency_ = 1/60f; // all inherited objects will update at 60 fps
        public Vector2 Position { get { return position_; } }
        public Texture2D Texture { get { return texture_; } }
        public int objectID { get; protected set; }
        public Rectangle CollisionRectangle { get { return new Rectangle((int)position_.X, (int)position_.Y, texture_.Width, texture_.Height); } }
        public Action<int> HandleDamage;
        protected List<Entity> otherEntities_;
        protected int health;
        protected int armor;
        protected int damage;

        protected Entity(Vector2 startPos, List<Entity> otherEntities)
        {
            this.position_ = startPos;
            this.otherEntities_ = otherEntities;
            this.HandleDamage = ReactToHit;
        }

        protected virtual void Move(Vector2 displacement)
        {
            this.position_ += displacement;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (CheckUpdateTime(gameTime))
            {
                UpdateLogic(gameTime);
                CheckCollisions();
            }
        }
        
        protected bool CheckUpdateTime(GameTime gameTime)
        {
            updateTimer_ += gameTime.ElapsedGameTime.TotalSeconds;
            if (updateTimer_ > updateFrequency_)
            {
                updateTimer_ -= updateFrequency_;
                return true;
            }
            else { return false; }
        }
        protected abstract void UpdateLogic(GameTime gameTime);
        protected abstract void CheckCollisions();
        protected abstract void Destroy();
        protected virtual void ReactToHit(int damage)
        {
            health -= (damage - armor);
            if (health <= 0)
            {
                this.Destroy();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture_, position_, Color.White);
        }


    }
}
