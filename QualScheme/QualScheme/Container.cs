﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace QualScheme
{
    class Container
    {
        private static Solution thisSolution; // TODO: Come up with better name 
        public bool show = false;
        static int textID, whiteppt;
        static float x, y, length, height;
        public Container()
        {
            // For now start in the middle of the screen
            x = 990; y = 540;
            length = 50f;
            height = 150f;
            textID = Textures.loadTexture("clearLiquidTestTube.png");
            whiteppt = Textures.loadTexture("testTubeBlackPpt.png");

        }

        public void colorChange()
        {
            if (show)
            {
                GL.BindTexture(TextureTarget.Texture2D, whiteppt);
                GL.Begin(BeginMode.Quads);

                GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(x - length, y - height);//Top Left Corner
                GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(x - length, y + height);//Bottom Left Corner
                GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(x + length, y + height);//Bottom Right Corner
                GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(x + length, y - height);//Top Right Corner

                GL.End();
            }
        }

        public void draw()
        {
            GL.BindTexture(TextureTarget.Texture2D, textID);
            
            
            //GL.Color3(Color.White);

            GL.Begin(BeginMode.Quads);

            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(x - length, y - height);//Top Left Corner
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(x - length, y + height);//Bottom Left Corner
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(x + length, y + height);//Bottom Right Corner
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(x + length, y - height);//Top Right Corner

            GL.End(); 

        }

        public bool isInCoordinates(float inx, float iny)
        {
            if ((x - length) < inx && inx < (x + length))
            {
                if ((y - height) < iny && iny < (y + height))
                {
                    return true;
                }
            }
            return false;
        }

        public void updateCoordinates(float inx, float iny)
        {
            x = inx;
            y = iny;
        }        
    }
}
