using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace WizardLizard
{
    public class LevelBuilder
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
            background.AddComponent(new SpriteRenderer(background, "TutorialScreen", 1f));
            background.Transform.Position = new Vector2(0, 0);
            GameWorld.ObjectToAdd.Add(background);
            director = new Director(new AimerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 0)));
            director = new Director(new PlayerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(100, 675)));
            director = new Director(new PlayerHealthBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(10, 10)));
            director = new Director(new CompanionBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(100, 675)));

            director = new Director(new PlatformBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 850), 1600, 100));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 0), 1288, 48));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 0), 27, 850));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(221, 183), 28, 700));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(452, 48), 28, 634));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(479, 304), 95, 48));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(695, 632), 30, 300));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(695, 632), 518, 48));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1185, 632), 28, 156));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(988, 768), 28, 156));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1570, 0), 30, 672));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1373, 672), 250, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1342, 185), 30, 126));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1342, 311), 300, 48));

            director = new Director(new NonSolidPlatformBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(26, 700), 193, 20));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(26, 555), 193, 20));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(26, 405), 193, 20));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(26, 255), 193, 20));
            
            director = new Director(new MoveableBoxBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(435, 675)));

            director = new Director(new DoorBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1375, 705), 1, "MagicDoor30x150"));

            director = new Director(new LeverBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(780, 800), 1));

            director = new Director(new ArcherBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(490, 120)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1450, 120)));
            
            director = new Director(new GoblinBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1100, 475)));
        }
        public void LevelTwo()
        {
            GameObject background = new GameObject();
            background.AddComponent(new SpriteRenderer(background, "Level01", 1f));
            background.Transform.Position = new Vector2(0, 0);
            GameWorld.ObjectToAdd.Add(background);
            director = new Director(new AimerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(0, 0)));
            director = new Director(new PlayerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(100, 675)));
            director = new Director(new PlayerHealthBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(10, 10)));
            director = new Director(new LeverBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(70, 270), 1));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1160, 143), 51));
            director = new Director(new DoorBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(970, 655), 1));
            director = new Director(new CompanionBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(100, 675)));
            director = new Director(new ArcherBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(315, 375)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1360, 275)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1150, 100)));
            director = new Director(new GoblinBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1183, 460)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(680, 705)));

            director = new Director(new OrcBuilder());
            // GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1183, 481)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1300, 675)));

            director = new Director(new MoveableBoxBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(800, 720)));
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


            //non-solidplatforms

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
        public void LevelThree()
        {
            GameObject background = new GameObject();
            background.AddComponent(new SpriteRenderer(background, "Level02New", 1f));
            background.Transform.Position = new Vector2(0, 0);
            GameWorld.ObjectToAdd.Add(background);
            director = new Director(new AimerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(2, 50)));
            director = new Director(new PlayerBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(2, 20)));
            director = new Director(new CompanionBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(2, 20)));
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
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(50, 500), 150, 200));
            //Jungle stuff
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(200, 600), 50, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(200, 650), 150, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(300, 700), 100, 50));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(350, 750), 100, 50));
            //Lever platform
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(50, 800), 200, 50));
            //Crates
            director = new Director(new MoveableBoxBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1046, 730)));
            //Magiske døre
            director = new Director(new DoorBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(320, 200), 1));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(420, 800), 2, "MagicDoor30x50"));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1550, 700), 3, "MagicDoor30x150"));
            //Levers
            director = new Director(new LeverBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(50, 750), 1));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1400, 150), 2));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1000, 500), 3));
            //Enemies
            //Achers
            director = new Director(new ArcherBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1350, 125)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(950, 480)));
            //Goblins
            director = new Director(new GoblinBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(970, 780)));
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(100, 425)));
            director = new Director(new OrcBuilder());
            GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(1300, 451)));
        }
    }
}
