﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

enum State { Solid, Liquid };

namespace QualScheme
{
    class Program
    {
        // Id of bakcground texture
        static int labID;
        // The main solution being tested
        static Solution mainSolution;  
        // Present and past positions of mouse      
        static MouseDevice current, previous; // Maybe not need previous, we'll see
        // Main menu
        static MainMenu titleScreen;
        // Bools to control the flow of the game
        static bool inMain, inGame;
        [STAThread]
        public static void Main()
        {
            using (var game = new GameWindow())
            {
                GL.Enable(EnableCap.Texture2D);

                game.Load += (sender, e) =>
                {
                    // Go fullscreen for now
                    game.WindowState = WindowState.Fullscreen;
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;

                    // Initialize several variables
                    labID = Textures.loadTexture("labTable.png");
                    titleScreen = new MainMenu();
                    inMain = true;
                    inGame = false;              
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                                                         
                };

                game.KeyDown += (sender, e) =>
                {
                    // add game logic, input handling
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }

                    if (game.Keyboard[Key.Down])
                    {
                        // Example of keyboardness
                    }
                };

                game.MouseDown += (sender, e) =>
                {
                    current = game.Mouse;

                    if (current[MouseButton.Left])
                    {
                        Console.WriteLine(current.X + ", " + current.Y);

                        if (inMain)
                        {
                            bool check = titleScreen.checkClick(current.X, current.Y);

                            if (!check)
                            {
                                inMain = false;
                                inGame = true;

                                mainSolution = titleScreen.genSolution();
                                mainSolution.printSolution();
                            }
                        }
                    }
                    previous = current;
                };

                game.RenderFrame += (sender, e) =>
                {
                    // Clear screen
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    GL.MatrixMode(MatrixMode.Projection);

                    // Setupt 2D drawing in a 1920 x 1080 monitor
                    GL.LoadIdentity();
                    GL.Ortho(0, 1920, 1080, 0, -1, 1);

                    if (inMain)
                    {
                        titleScreen.draw();
                    }

                    else
                    {
                        GL.BindTexture(TextureTarget.Texture2D, labID);


                        // Start background
                        GL.Begin(BeginMode.Quads);

                        GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(0f, 0f);//Top Left Corner
                        GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(0f, 1080f);//Bottom Left Corner
                        GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1920f, 1080f);//Bottom Right Corner
                        GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1920f, 0f);//Top Right Corner

                        GL.End();
                    }

                    game.SwapBuffers();
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }
    }
}
