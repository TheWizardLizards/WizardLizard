﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardLizard
{
    class LevelBuilder
    {
        Director director;
        private List<GameObject> gameObjects;
        private Dictionary<int, GameObject> spawnList;
        public LevelBuilder()
        {
            this.gameObjects = GameWorld.GameObjects;
            this.spawnList = GameWorld.spawnList;
        }
        public void LevelOne()
        {
            GameObject background = new GameObject();
            background.AddComponent(new SpriteRenderer(background, "Level01", 1f));
            background.Transform.Position = new Vector2(0, 0);
            GameWorld.ObjectToAdd.Add(background);
            director = new Director(new AimerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 0)));
            director = new Director(new PlayerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(100, 700)));
            director = new Director(new PlayerHealthBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(10, 10)));
            director = new Director(new LeverBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(70, 270), 1));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1160, 143), 51));
            director = new Director(new DoorBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(970, 655), 1));
            director = new Director(new CompanionBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(100, 700)));
            director = new Director(new ArcherBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(315, 395)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1360, 300)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1150, 120)));
            director = new Director(new GoblinBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1183, 481)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(680, 730)));

            director = new Director(new OrcBuilder());
           // GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1183, 481)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1300, 700)));

            director = new Director(new MoveableBoxBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(800, 743)));
            director = new Director(new PlatformBuilder());
            //højre side bund
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 850), 1600, 100));
            //venstre side bund
            //gameObjects.Add(director.Construct(new Vector2(483, 850), 1117, 100));
            //venstre væg
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 0), 60, 850));
            //første platform over hulen
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(966, 608), 386, 48));
            //højeste platform over hulen
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1352, 560), 194, 48));
            //højre væg
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1498, 240), 48, 480));
            //højre boks
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1546, 240), 50, 50));
            //højre grænse
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1596, 0), 50, 240));
            //venstre stenplatform
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(190, 488), 196, 48));


            spawnList.Add(51, director.Construct(new Vector2(482, 535), "MagicPlatform"));


            //To be: non-solidplatforms
            
            director = new Director(new NonSolidPlatformBuilder());
            //midterste gren
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1350, 240), 148, 40));
            //øverste gren
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1150, 195), 200, 45));
            //nederste gren
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1360, 395), 138, 30));
            //venstre gren
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(60, 320), 178, 30));
        }
        public void LevelTwo()
        {
            GameObject background = new GameObject();
            background.AddComponent(new SpriteRenderer(background, "Level02New", 1f));
            background.Transform.Position = new Vector2(0, 0);
            GameWorld.ObjectToAdd.Add(background);
            director = new Director(new AimerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(2, 50)));
            //foreach (GameObject go in GameWorld.GameObjects)
            //{
            //    if (go.GetComponent("Player") != null)
            //    {
            //        go.Transform.Position = new Vector2(2, 50);
            //    }
            //    if (go.GetComponent("Companion") != null)
            //    {
            //        go.Transform.Position = new Vector2(2, 50);
            //    }
            //    if (go.GetComponent("PlayerHealth") != null)
            //    {
            //        go.Transform.Position = new Vector2(10, 10);
            //    }
            //}
            director = new Director(new PlayerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(2, 50)));
            director = new Director(new CompanionBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(2, 50)));
            //Player health
            director = new Director(new PlayerHealthBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(10, 10)));
            //Toppen
            director = new Director(new PlatformBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 0), 1600, 48));
            //Bunden
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 850), 1600, 50));
            //Player start platform
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 150), 548, 50));
            //Venstre væg
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 200), 50, 650));
            //Højre væg
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1550, 48), 50, 650));
            //Goblin shooter platform
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1350, 200), 200, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1350, 250), 50, 100));
            //Midter platform
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(300, 400), 1048, 50));
            //Midter platform kasser
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(350, 300), 50, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(350, 350), 100, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(900, 350), 100, 50));
            //Platform under midter platform med goblin på
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(950, 550), 250, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1150, 450), 50, 100));
            //Kasse ved venstre væg
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(50, 500),150, 200));
            //Jungle stuff
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(200, 600), 50,50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(200, 650), 150, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(300, 700), 100, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(350, 750), 100, 50));
            //Lever platform
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(50, 800), 200, 50));
            //Crates
            director = new Director(new MoveableBoxBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1300, 750)));
            //Magiske døre
            director = new Director(new DoorBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(320, 200), 1));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(420,800), 2,"MagicDoor30x50"));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1550, 700), 3, "MagicDoor30x150"));
            //Levers
            director = new Director(new LeverBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(50, 750), 1));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1400, 150), 2));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1000, 500), 3));
            //Enemies
            //Achers
            director = new Director(new ArcherBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1350, 155)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(950, 505)));
            //Goblins
            director = new Director(new GoblinBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(970, 805)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(100, 455)));
            director = new Director(new OrcBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1183, 481)));
        }
        public void LevelThree()
        {

        }
        public void LevelFour()
        {

        }
    }
}
