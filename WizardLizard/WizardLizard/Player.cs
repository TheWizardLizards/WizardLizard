﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace WizardLizard
{
    class Player : Component, ILoadable, IUpdateable, IAnimateable, ICollisionEnter, ICollisionExit
    {
        private Transform transform;
        private Vector2 velocity;
        private Animator animator;
        private bool canControle = true;
        private int speed = 200;
        private static int health;
        private bool hasJumped;
        private bool fireball = true;
        private bool lightning = true;
        private bool shield = true;
        private Director director;
        private bool canInteract = false;
        private bool haveInteracted = true;

        public static int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public Player(GameObject gameObject) : base(gameObject)
        {
            animator = (Animator)GameObject.GetComponent("Animator");
            transform = gameObject.Transform;
            health = 4;
            hasJumped = true;
        }
        public void LoadContent(ContentManager content)
        {

        }
        public void Update()
        {
            KeyboardState keyState = Keyboard.GetState();

            Vector2 translation = Vector2.Zero;

            MouseState mouseState = Mouse.GetState();

            PlayerController(keyState,translation,mouseState);
        }

        public void OnAnimationDone(string animationName)
        {

        }    
        private void MeleeAttack( MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                //Attack
            }
        }
        private void Interact( KeyboardState keyState)
        {
            //press E to interact
            if (keyState.IsKeyDown(Keys.E) && canInteract == true && haveInteracted == true)
            {
                director = new Director(new FireballBuilder());
                GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X, transform.Position.Y)));
                haveInteracted = false;
                canInteract = false;
            }
            if (keyState.IsKeyUp(Keys.E))
            {
                haveInteracted = true;
            }

        }
        private void CreateShield(KeyboardState keyState)
        {
            //Creates a shield around the player
            if (keyState.IsKeyDown(Keys.Q) && shield == true)
            {
                director = new Director(new PlayerShieldBuilder());
                GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X, transform.Position.Y)));
                shield = false;
            }
            if (keyState.IsKeyUp(Keys.Q))
            {
                shield = true;
            }

        }
        private void ShootLighting(KeyboardState keyState, MouseState mouseState)
        {
            //Shoots a lightningstrike from above towards the moueses position
            if (keyState.IsKeyDown(Keys.R) && lightning == true)
            {
                director = new Director(new LightningStrikeBuilder());
                GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(mouseState.X - 51, mouseState.Y - 479)));
                lightning = false;
            }
            if (keyState.IsKeyUp(Keys.R))
            {
                lightning = true;
            }
        }
        private void ShootFireball( MouseState mouseState)
        {
            //Shoots a fireball towards the moueses position
            if (mouseState.RightButton == ButtonState.Pressed && fireball == true)
            {
                director = new Director(new FireballBuilder());
                //Opdater fireball spawn punkt.
                GameWorld.ObjectToAdd.Add(director.Construct(new Vector2(transform.Position.X + 180, transform.Position.Y + 98)));
                fireball = false;
            }
            if (mouseState.RightButton == ButtonState.Released)
            {
                fireball = true;
            }
        }
        private void ShiftToPet(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Space) && canControle == true)
            {
                Pet.Petcontrol = true;
                canControle = false;
            }
            if (keyState.IsKeyUp(Keys.Space))
            {
                canControle = true;
            }
        }
        private void Jump(KeyboardState keyState, Vector2 translation )
        {
          if (keyState.IsKeyDown(Keys.W) && hasJumped == false)
                {
                    translation.Y -= 5f;
                    velocity.Y = -5f;
                    hasJumped = true;
                }
        }
        private void PlayerController(KeyboardState keyState, Vector2 translation, MouseState mouseState)
        {  
            if (Pet.Petcontrol == false)
            {

                if (keyState.IsKeyDown(Keys.D))
                {
                    translation += new Vector2(1, 0);
                }
                if (keyState.IsKeyDown(Keys.A))
                {
                    translation += new Vector2(-1, 0);
                }
                if (keyState.IsKeyDown(Keys.S))
                {
                    translation += new Vector2(0, 1);
                }

                Jump(keyState, translation);

                ShiftToPet(keyState);

                ShootFireball(mouseState);

                ShootLighting(keyState,mouseState);

                CreateShield(keyState);

                Interact(keyState);

                MeleeAttack(mouseState);  
            }

            float i = 5;
            velocity.Y += 0.05f * i;
            translation += velocity;

            if (velocity.Y > 10)
            {
                velocity.Y = 10;
            }

            morph(keyState);

            transform.Translate(translation * GameWorld.DeltaTime * speed);

        }
        private void morph(KeyboardState keyState)
        {
            
            if (keyState.IsKeyUp(Keys.F))
            {
                Morph.HasMorphed = false;
            }


            if (keyState.IsKeyDown(Keys.F) && Morph.HasMorphed == false)
            {
                Morph.HasMorphed = true;
                director = new Director(new MorphBuilder());
                GameWorld.Instance.AddGameObject(director.Construct(this.transform.Position));

                GameWorld.Instance.RemoveGameObject(this.GameObject);

                foreach (GameObject go in GameWorld.GameObjects)
                {
                    if (go.GetComponent("Pet") != null)
                    {
                        GameWorld.Instance.RemoveGameObject(go);
                    }
                }

            }

        }

        public void OnCollisionEnter(Collider other)
        {
            if (other.GameObject.GetComponent("Lever") != null)
            {
                canInteract = true;
            }
            if (other.GameObject.GetComponent("SolidPlatform") != null)
            {
                Collider collider = (Collider)GameObject.GetComponent("Collider");
                if (collider.CollisionBox.Intersects(other.TopLine) && collider.CollisionBox.Intersects(other.BottomLine))
                {
                    if (collider.CollisionBox.Intersects(other.RightLine))
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.Y = other.CollisionBox.X + other.CollisionBox.Width;
                        GameObject.Transform.Position = position;
                    }
                    if (collider.CollisionBox.Intersects(other.LeftLine))
                    {
                        Vector2 position = GameObject.Transform.Position;
                        position.X = other.CollisionBox.X - collider.CollisionBox.Width;
                        GameObject.Transform.Position = position;
                    }
                }
                else if (collider.CollisionBox.Intersects(other.TopLine))
                {
                    Vector2 position = GameObject.Transform.Position;
                    position.Y = other.CollisionBox.Y - collider.CollisionBox.Height;
                    GameObject.Transform.Position = position;
                    hasJumped = false;
                    velocity.Y = 0;
                }
                if (collider.CollisionBox.Intersects(other.BottomLine))
                {
                    Vector2 position = GameObject.Transform.Position;
                    position.Y = other.CollisionBox.Y + other.CollisionBox.Height;
                    GameObject.Transform.Position = position;
                    velocity.Y = 0;
                }
            }
        }
        public void OnCollisionExit(Collider other)
        {

        }
    }
}
