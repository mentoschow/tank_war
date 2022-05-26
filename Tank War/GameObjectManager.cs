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
    class GameObjectManager
    {
        private static List<GameObject> redWallList = new List<GameObject>();
        private static List<GameObject> steelWallList = new List<GameObject>();
        private static List<GameObject> bossList = new List<GameObject>();
        private static Player player;
        private static int enemyBornSpeed = 180;
        private static int enemyBornTimer = 180;
        private static int enemyCount = 10;
        private static List<GameObject> enemyList = new List<GameObject>();
        private static Point[] points = new Point[3];
        private static Random random1 = new Random();
        private static Random random2 = new Random();
        private static List<GameObject> bulletList = new List<GameObject>();
        private static List<GameObject> expList = new List<GameObject>();

        public static void CreateMap()
        {
            //double red wall
            CreateObject(1, 1, 4, Resources.wall, redWallList);
            CreateObject(3, 1, 4, Resources.wall, redWallList);
            CreateObject(5, 1, 3, Resources.wall, redWallList);
            CreateObject(7, 1, 3, Resources.wall, redWallList);
            CreateObject(9, 1, 4, Resources.wall, redWallList);
            CreateObject(11 , 1, 4, Resources.wall, redWallList);
            CreateObject(2, 6, 1, Resources.wall, redWallList);
            CreateObject(3, 6, 1, Resources.wall, redWallList);
            CreateObject(5, 5, 1, Resources.wall, redWallList);
            CreateObject(7, 5, 1, Resources.wall, redWallList);
            CreateObject(9, 6, 1, Resources.wall, redWallList);
            CreateObject(10, 6, 1, Resources.wall, redWallList);
            CreateObject(1, 8, 4, Resources.wall, redWallList);
            CreateObject(3, 8, 4, Resources.wall, redWallList);
            CreateObject(5, 7, 3, Resources.wall, redWallList);
            CreateObject(7, 7, 3, Resources.wall, redWallList);
            CreateObject(9, 8, 4, Resources.wall, redWallList);
            CreateObject(11, 8, 4, Resources.wall, redWallList);
            //single red wall
            CreateObject(11, 23, 3, Resources.wall, redWallList, true);
            CreateObject(12, 23, 1, Resources.wall, redWallList, true);
            CreateObject(13, 23, 1, Resources.wall, redWallList, true);
            CreateObject(14, 23, 3, Resources.wall, redWallList, true);
            //double steel wall
            CreateObject(6, 2, 1, Resources.steel, steelWallList);
            CreateObject(6, 8, 1, Resources.steel, steelWallList);
            CreateObject(0, 6, 1, Resources.steel, steelWallList);
            CreateObject(12, 6, 1, Resources.steel, steelWallList);
            //boss
            CreateObject(12, 24, 1, Resources.Boss, bossList, true);
            //enemy
            points[0].X = 0; points[0].Y = 0;
            points[1].X = 180; points[1].Y = 0;
            points[2].X = 360; points[2].Y = 0;
        }

        //@param x:第x列
        //@param y:第y行
        //@param count:创建count个方块墙，按列创建
        private static void CreateObject(int x, int y, int count, Image img, List<GameObject> list, bool isSingle = false)
        {         
            if (!isSingle)
            {
                int xPos = x * 30;
                int yPos = y * 30;
                for (int i = yPos; i < yPos + count * 30; i += 15)
                {
                    ImmovaleObject wall1 = new ImmovaleObject(xPos, i, img);
                    ImmovaleObject wall2 = new ImmovaleObject(xPos + 15, i, img);
                    list.Add(wall1);
                    list.Add(wall2);
                }
            }
            else
            {
                int xPos = x * 15;
                int yPos = y * 15;
                for (int i = yPos; i < yPos + count * 15; i += 15)
                {
                    ImmovaleObject wall = new ImmovaleObject(xPos, i, img);
                    list.Add(wall);
                }
            }            
        }

        public static void CreatePlayer(int x, int y, int speed = 2)  //初始生成位置
        {
            int xPos = x * 15;
            int yPos = y * 15;
            player = new Player(xPos, yPos, speed);
        }

        private static void CreateEnemy()
        {
            enemyBornTimer++;
            if (enemyBornTimer < enemyBornSpeed) return;
            else
            {
                if (enemyList.Count < enemyCount)
                {
                    //random num:0~2                
                    Point position = points[random1.Next(0, 3)];
                    CreateTank(position.X, position.Y, random2.Next(0, 4));
                    SoundManager.Play(SoundState.ADD);
                }             

                enemyBornTimer = 0;
            }
        }

        private static void CreateTank(int x, int y, int index)
        {
            Enemy enemy;
            lock (enemyList)
            {
                switch (index)
                {
                    case 0:
                        enemy = new Enemy(x, y, 2, 60, Resources.GrayUp, Resources.GrayDown, Resources.GrayLeft, Resources.GrayRight);
                        enemyList.Add(enemy);
                        return;
                    case 1:
                        enemy = new Enemy(x, y, 2, 60, Resources.GreenUp, Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);
                        enemyList.Add(enemy);
                        return;
                    case 2:
                        enemy = new Enemy(x, y, 3, 30, Resources.QuickUp, Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);
                        enemyList.Add(enemy);
                        return;
                    case 3:
                        enemy = new Enemy(x, y, 1, 90, Resources.SlowUp, Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);
                        enemyList.Add(enemy);
                        return;
                }
            }            
        }    
        
        public static void CreateBullet(int x, int y, Tag tag, Direction dir)
        {
            Bullet bullet = new Bullet(x, y, dir, tag);
            lock (bulletList)
            {
                bulletList.Add(bullet);
                SoundManager.Play(SoundState.FIRE);
            }
        }

        public static void CreateExplosion(int x, int y)
        {
            Effect exp = new Effect(x, y);
            expList.Add(exp);
            SoundManager.Play(SoundState.BLAST);
        }

        private static void CheckObjectShouldDestroy()
        {
            List<GameObject> objectsShouldDestroy = new List<GameObject>();
            foreach (var item in bulletList)
            {
                if (item.isDestroy)
                {
                    objectsShouldDestroy.Add(item);
                }
            }
            foreach (var item in redWallList)
            {
                if (item.isDestroy)
                {
                    objectsShouldDestroy.Add(item);
                }
            }
            foreach (var item in enemyList)
            {
                if (item.isDestroy)
                {
                    objectsShouldDestroy.Add(item);
                }
            }
            foreach (var item in expList)
            {
                if (item.isDestroy)
                {
                    objectsShouldDestroy.Add(item);
                }
            }           
            foreach (var item in objectsShouldDestroy)
            {
                lock (bulletList)
                {
                    bulletList.Remove(item);
                }
                lock (redWallList)
                {
                    redWallList.Remove(item);
                }
                lock (bossList)
                {
                    bossList.Remove(item);
                }
                lock (enemyList)
                {
                    enemyList.Remove(item);
                }
                lock (expList)
                {
                    expList.Remove(item);
                }
            }
        }

        public static void Update()
        {
            CreateEnemy();
            lock (redWallList)
            {
                foreach (GameObject item in redWallList)
                {
                    item.Update();
                }
            }            
            foreach (GameObject item in steelWallList)
            {
                item.Update();
            }
            lock (bossList)
            {
                foreach (GameObject item in bossList)
                {
                    item.Update();
                }
            }
            lock (enemyList)
            {
                foreach (Enemy item in enemyList)
                {
                    item.Update();
                }
            }           
            lock (bulletList)
            {
                foreach (Bullet item in bulletList)
                {
                    item.Update();
                }
            }
            lock (expList)
            {
                foreach (var item in expList)
                {
                    item.Update();
                }
            }
            player.Update();
            CheckObjectShouldDestroy();
        }

        public static void KeyDown(KeyEventArgs args)
        {
            player.KeyDown(args);
        }
        public static void KeyUp(KeyEventArgs args)
        {
            player.KeyUp(args);
        }

        public static GameObject RedWallCollideCheck(Rectangle rect)  //碰撞检测
        {
            foreach (GameObject item in redWallList)
            {
                if (item.GetRectangle().IntersectsWith(rect))
                {
                    return item;
                }
            }
            return null;
        }
        public static GameObject SteelWallCollideCheck(Rectangle rect)  //碰撞检测
        {
            foreach (GameObject item in steelWallList)
            {
                if (item.GetRectangle().IntersectsWith(rect))
                {
                    return item;
                }
            }
            return null;
        }
        public static GameObject BossCollideCheck(Rectangle rect)  //碰撞检测
        {
            foreach (GameObject item in bossList)
            {
                if (item.GetRectangle().IntersectsWith(rect))
                {
                    return item;
                }
            }
            return null;
        }
        public static GameObject EnemyCollideCheck(Rectangle rect)  //碰撞检测
        {
            foreach (GameObject item in enemyList)
            {
                if (item.GetRectangle().IntersectsWith(rect))
                {
                    return item;
                }
            }
            return null;
        }
        public static Player PlayerCollideCheck(Rectangle rect)  //碰撞检测
        {
            if (player.GetRectangle().IntersectsWith(rect))
            {
                return player;
            }
            return null;
        }
    }
}
