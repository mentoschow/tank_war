using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tank_War.Properties;
using System.Windows.Forms;

namespace Tank_War
{
    class Player : MovableObject
    {
        public bool isMoving { get; set; }
        public bool isAttacking { get; set; }
        private static int shootingSpeed = 8;
        private static int shootingTimer = 8;
        public int HP { get; set; }
        public Player(int x, int y, int speed)
        {
            X = x;
            Y = y;
            MoveSpeed = speed;
            BitmapUp = Resources.MyTankUp;
            BitmapDown = Resources.MyTankDown;
            BitmapLeft = Resources.MyTankLeft;
            BitmapRight = Resources.MyTankRight;
            Dir = Direction.UP;
            isMoving = false;
            isDestroy = false;
            isAttacking = false;
            HP = 5;
        }

        public override void Update()
        {
            Move();
            Attack();
            base.Update();  //draw
        }

        private void MoveCheck()
        {
            //检查是否超出窗口边界
            if (Dir == Direction.UP)
            {
                if (Y - MoveSpeed < 0)
                {
                    isMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.DOWN)
            {
                if (Y + MoveSpeed + Height > 390)
                {
                    isMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.LEFT)
            {
                if (X - MoveSpeed < 0)
                {
                    isMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.RIGHT)
            {
                if (X + MoveSpeed > 360)
                {
                    isMoving = false;
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
                //SoundManager.Play(SoundState.HIT);
                isMoving = false;
                return;
            }
        }

        private void Move()
        {
            MoveCheck();
            if (!isMoving) return;

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

        public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Up:
                    Dir = Direction.UP;
                    isMoving = true;
                    break;
                case Keys.Down:
                    Dir = Direction.DOWN;
                    isMoving = true;
                    break;
                case Keys.Left:
                    Dir = Direction.LEFT;
                    isMoving = true;
                    break;
                case Keys.Right:
                    Dir = Direction.RIGHT;
                    isMoving = true;
                    break;
                case Keys.Space:
                    //shoot                   
                    isAttacking = true;
                    break;
                default:
                    break;
            }
        }

        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Up:
                    isMoving = false;
                    break;
                case Keys.Down:
                    isMoving = false;
                    break;
                case Keys.Left:
                    isMoving = false;
                    break;
                case Keys.Right:
                    isMoving = false;
                    break;
                case Keys.Space:
                    isAttacking = false;
                    shootingTimer = shootingSpeed - 2;
                    break;
                default:
                    break;
            }
        }    
        
        private void Attack()
        {
            if (!isAttacking) return;

            shootingTimer++;
            if (shootingTimer < shootingSpeed) return;

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
            GameObjectManager.CreateBullet(x, y, Tag.PLAYER, Dir);
            shootingTimer = 0;
        }

        public void TakeDamage()
        {
            HP--;
            if (HP <= 0)
            {
                GameObjectManager.CreatePlayer(8, 24);
            }
        }
    }
}
