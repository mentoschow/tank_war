using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank_War.Properties;

namespace Tank_War
{
    enum Tag
    {
        PLAYER,
        ENEMY
    }
    class Bullet : MovableObject
    {
        public Tag tag { get; set; }

        public Bullet(int x, int y, Direction dir, Tag tag, int speed = 7)
        {
            X = x;
            Y = y;
            MoveSpeed = speed;
            BitmapUp = Resources.BulletUp;
            BitmapDown = Resources.BulletDown;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;
            Dir = dir;
            this.tag = tag;
            X -= Width / 2;
            Y -= Height / 2;
            isDestroy = false;
        }

        public override void Update()
        {
            Move();
            base.Update();  //draw
        }

        private void MoveCheck()
        {
            //检查是否超出窗口边界
            if (Dir == Direction.UP)
            {
                if (Y + Height / 2 + 3 < 0)
                {
                    isDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.DOWN)
            {
                if (Y + Height / 2 - 3 > 390)
                {
                    isDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.LEFT)
            {
                if (X + Width / 2 - 3 < 0)
                {
                    isDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.RIGHT)
            {
                if (X + 3 > 360)
                {
                    isDestroy = true;
                    return;
                }
            }

            //检查是否和其它物体发生碰撞
            Rectangle rect = GetRectangle();
            rect.X += 3;
            rect.Y += Height / 2 - 3;
            rect.Width = 3;
            rect.Height = 3;

            int xExpPos = X + Width / 2;
            int yExpPos = Y + Height / 2;

            if (GameObjectManager.RedWallCollideCheck(rect) != null)
            {
                isDestroy = true;
                GameObjectManager.RedWallCollideCheck(rect).isDestroy = true;
                GameObjectManager.CreateExplosion(xExpPos, yExpPos);
                return;
            }
            else if (GameObjectManager.SteelWallCollideCheck(rect) != null)
            {
                isDestroy = true;
                GameObjectManager.CreateExplosion(xExpPos, yExpPos);
                return;
            }
            if (tag == Tag.PLAYER)
            {
                if (GameObjectManager.EnemyCollideCheck(rect) != null)
                {
                    isDestroy = true;
                    GameObjectManager.EnemyCollideCheck(rect).isDestroy = true;
                    GameObjectManager.CreateExplosion(xExpPos, yExpPos);
                    return;
                }
                //else if (GameObjectManager.BossCollideCheck(rect) != null)
                //{
                //    isDestroy = true;
                //    GameObjectManager.CreateExplosion(xExpPos, yExpPos);
                //    GameFramework.ChangeToGameOver();
                //    return;
                //}
            }
            else if(tag == Tag.ENEMY)
            {
                Player player;
                if ((player = GameObjectManager.PlayerCollideCheck(rect)) != null)
                {
                    isDestroy = true;
                    player.TakeDamage();
                    GameObjectManager.CreateExplosion(xExpPos, yExpPos);
                    return;
                }
                else if (GameObjectManager.BossCollideCheck(rect) != null)
                {
                    isDestroy = true;
                    GameObjectManager.CreateExplosion(xExpPos, yExpPos);
                    GameFramework.ChangeToGameOver();
                    return;
                }
            }
        }

        private void Move()
        {
            MoveCheck();

            switch (Dir)
            {
                case Direction.UP:
                    Y -= MoveSpeed;
                    break;
                case Direction.DOWN:
                    Y += MoveSpeed;
                    break;
                case Direction.LEFT:
                    X -= MoveSpeed;
                    break;
                case Direction.RIGHT:
                    X += MoveSpeed;
                    break;
                default:
                    break;
            }
        }
    }
}
