using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tank_War
{
    class Enemy : MovableObject
    {
        Random rd = new Random();
        public int changeDirSpeed { get; set; }
        private int changeDirTimer = 0;
        public int shootingSpeed { get; set; }
        private int shootingTimer = 0;
        public Enemy(int x, int y, int speed, int shootSpeed, Bitmap bmpUp, Bitmap bmpDown, Bitmap bmpLeft, Bitmap bmpRight)
        {
            X = x;
            Y = y;
            MoveSpeed = speed;
            BitmapUp = bmpUp;
            BitmapDown = bmpDown;
            BitmapLeft = bmpLeft;
            BitmapRight = bmpRight;
            Dir = Direction.DOWN;
            isDestroy = false;
            shootingSpeed = shootSpeed;
            changeDirSpeed = 150;
        }

        public override void Update()
        {
            Move();
            AttackCheck();
            AutoChangeDirection();
            base.Update();  //draw
        }

        private void MoveCheck()
        {
            //检查是否超出窗口边界
            if (Dir == Direction.UP)
            {
                if (Y - MoveSpeed < 0)
                {
                    SoundManager.Play(SoundState.HIT);
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.DOWN)
            {
                if (Y + MoveSpeed + Height > 390)
                {
                    SoundManager.Play(SoundState.HIT);
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.LEFT)
            {
                if (X - MoveSpeed < 0)
                {
                    SoundManager.Play(SoundState.HIT);
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.RIGHT)
            {
                if (X + MoveSpeed + Width > 360)
                {
                    SoundManager.Play(SoundState.HIT);
                    ChangeDirection();
                    return;
                }
            }

            //检查是否和其它物体发生碰撞
            Rectangle rect = GetRectangle();
            switch (Dir)
            {
                case Direction.UP:
                    rect.Y -= MoveSpeed;
                    break;
                case Direction.DOWN:
                    rect.Y += MoveSpeed;
                    break;
                case Direction.LEFT:
                    rect.X -= MoveSpeed;
                    break;
                case Direction.RIGHT:
                    rect.X += MoveSpeed;
                    break;
                default:
                    break;
            }

            if (GameObjectManager.RedWallCollideCheck(rect) != null || GameObjectManager.SteelWallCollideCheck(rect) != null)
            {
                SoundManager.Play(SoundState.HIT);
                ChangeDirection();
                return;
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

        private void ChangeDirection()
        {
            Direction dir = (Direction)rd.Next(0, 4);
            if (dir == Dir)
            {
                ChangeDirection();
            }
            else
            {
                Dir = dir;
            }
            MoveCheck();
        }

        private void AutoChangeDirection()
        {
            changeDirTimer++;
            if (changeDirTimer < changeDirSpeed) return;

            ChangeDirection();
            changeDirTimer = 0;
        }

        private void Attack()
        {                  
            int x = X;
            int y = Y;

            switch (Dir)
            {
                case Direction.UP:
                    x += Width / 2;
                    break;
                case Direction.DOWN:
                    x += Width / 2;
                    y += Height;
                    break;
                case Direction.LEFT:
                    y += Height / 2;
                    break;
                case Direction.RIGHT:
                    x += Width;
                    y += Height / 2;
                    break;
                default:
                    break;
            }
            GameObjectManager.CreateBullet(x, y, Tag.ENEMY, Dir);            
        }

        private void AttackCheck()
        {
            shootingTimer++;
            if (shootingTimer < shootingSpeed) return;

            Attack();
            shootingTimer = 0;
        }
    }
}
