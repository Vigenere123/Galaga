﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using x_platform.GameObjects;

namespace x_platform
{
    public class Wave : Entity
    {
        private int waveSize_;
        private Enemy[] waveUnits_;
        private Texture2D enemyTexture_, projectileTexture_;
        public List<Entity> ToEnemyArray { get { return waveUnits_.ToList<Entity>(); } }

        public Wave(int numEnemies, Vector2 startPos, List<Entity> otherEntities) : base(startPos, otherEntities)
        {
            waveSize_ = numEnemies;
            enemyTexture_ = GameLoop.textureDict[GameLoop.TextureNames.Enemy];
            projectileTexture_ = GameLoop.textureDict[GameLoop.TextureNames.Projectile];
            waveUnits_ = new Enemy[waveSize_];
            for (int i = 0; i < waveSize_; i++)
            {
                var pos = new Vector2((float)i * 200, 200);
                waveUnits_[i] = new Enemy(enemyTexture_, pos, projectileTexture_, otherEntities);
            }
        }
        public void Start()
        {
            //waveUnits_ = new Enemy[waveSize_];
            //for (int i = 0; i < waveSize_; i++)
            //{
            //    var startPos = new Vector2((float)i * 200, 200);
            //    waveUnits_[i] = new Enemy(enemyTexture_, startPos, projectileTexture_, otherEntities_);
            //}
        }
        public void RemoveActiveEnemy(Enemy enemy)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enem in waveUnits_)
            {
                enem.Draw(spriteBatch);
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void UpdateLogic(GameTime gameTime)
        {
            if (waveUnits_ == null)
                this.Start();
            foreach (var enem in waveUnits_)
            {
                enem.Update(gameTime);
            }
        }

        protected override void CheckCollisions()
        {
        }

        protected override void Destroy()
        {
        }
    }
}
