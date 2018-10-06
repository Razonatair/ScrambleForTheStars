//===================================================
//     Copyright 2018 Dyllin Edward Reid Black
//
//===================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*The DisplayManager handles almost everything related to rendering what the player can see and interact with on their screen, from
 * stars, to planets, and eventually to an entire galaxy that can be navigated by a burgeoning space empire looking to eXplore,
 * eXpand, eXploit, and eXterminate.*/

namespace Scramble
{
    public static class DisplayManager
    {
        private static Mesh drawMesh;                               //A generic square mesh that I use repeatedly with a texture on it.
        private static StellarObject workStellarObject;             //A StellarObject is essentially anything like a planet, moon, star. This records a temporarily used reference to one such object.
        private static Ship workShip;                               //Records a temporary reference to a Ship class.

        private static StellarObject selectedStellarObject;         //Records what StellarObject is currently selected by the player, to know where to render the selection icon.

        private static StarSystem renderStarSystem;                 //Records what star system to render for the player's screen.

        public static float drawYOffset { private set; get; }       //These variables describe how far to offset the drawing of the screen based on Unity units.
        public static float drawXOffset { private set; get; }

        private static float maxCameraOrthographicSize = 150f;      //The variables define the upper and lower bounds for how much of the game world is show to the main camera at any one time.
        private static float minCameraOrthographicSize = 5f;

        //Called every frame to tell the GPU what meshes to render with what textures and where, essentially.
        public static void DrawPlayScreen()
        { 
            //Currently, we're setting the star system to render to the only star system available in the galaxy right now,
            //later, the player will be able to move from system to system.
            renderStarSystem = Galaxy.galaxyArray[0, 0];

            //Once the game knows what system to render, we loop through every possible position in the array.
            //While this might not be the fastest possible way, it's fast enough for now at least.
            for(int x = 0; x < Settings.systemRadius * 2 + 1; x++)
            {
                for (int y = 0; y < Settings.systemRadius * 2 + 1; y++)
                {
                    //Is the position in the array null? If it is, it's meant to be that way, so do nothing.
                    if(renderStarSystem.systemObjects[x, y] != null)
                    {
                        //Grab a temporary reference to the StellarObject contained at x, y position in the system.
                        workStellarObject = renderStarSystem.systemObjects[x, y];   

                        //If this StellarObject is the same as the one currently selected by the player...
                        if(workStellarObject == selectedStellarObject)
                        {
                            //...draw the selection icon at the foremost z layer at its position.
                            InternalDrawMesh(x - Settings.systemRadius + drawXOffset, y - Settings.systemRadius + drawYOffset, 0, MatDict.GetMaterial("Selection"));
                        }

                        //Check if ships are present.
                        if (workStellarObject.ShipsPresent())
                        {
                            //If they are, grab a reference to the top-most ship for display purposes, and order it to be rendered.
                            workShip = workStellarObject.GetDisplayedShip();
                            InternalDrawMesh(x - Settings.systemRadius + drawXOffset, y - Settings.systemRadius + drawYOffset, 1, workShip.rotation, MatDict.GetMaterial(workShip.rootTemplate.graphicName));
                        }

                        //Finally, draw whatever texture the StellarObject has, with an EmptySpace background behind it. Eventually, I'll use a single background image
                        //spread across the whole screen instead of an ugly repeating texture.
                        InternalDrawMesh(x - Settings.systemRadius + drawXOffset, y - Settings.systemRadius + drawYOffset, 2, MatDict.GetMaterial(renderStarSystem.systemObjects[x, y].rootTemplate.graphicName));
                        InternalDrawMesh(x - Settings.systemRadius + drawXOffset, y - Settings.systemRadius + drawYOffset, 3, MatDict.GetMaterial("StellarObject_EmptySpace"));
                    }
                }
            }
        }

        //Called to set what StellarObject the player has selected.
        public static void SetSelectedStellarObject(Vector3 worldMousePos)
        {
            //The player might accidentally click what would be a null position in the system's array, so we need to handle that.
            try
            {
                //Sets the selected StellarObject to the nearest StellarObject in the star system's array, accounting for the offset in the drawn screen as well.
                selectedStellarObject = renderStarSystem.systemObjects[ Mathf.RoundToInt(worldMousePos.x - drawXOffset) + Settings.systemRadius,
                                                                        Mathf.RoundToInt(worldMousePos.y - drawYOffset) + Settings.systemRadius];

                //Check to see if there are ships present in the StellarObject.
                if (selectedStellarObject.ShipsPresent())
                {
                    //If there are, set the player's selected ship to the displayed ship, allowing it to be given move orders and so on.
                    UnitManager.SetSelectedShip(selectedStellarObject.GetDisplayedShip());
                }
                else
                {
                    //Otherwise, make it so no ship is selected.
                    UnitManager.SetSelectedShip(null);
                }
            }
            catch
            {
                //Do nothing, this is an honest mistake that isn't an issue.
            }
        }

        //Called whenever the draw offsets need to be updated, such as when the player scrolls the screen.
        public static void AddToDrawOffsets(float addX, float addY)
        {
            //Both of this basically make it impossible for the player to endlessly scroll far beyond the star system's radius.
            if ((drawXOffset + addX) * (drawXOffset + addX) < (Settings.systemRadius * Settings.systemRadius))
            {
                drawXOffset += addX;
            }
            if ((drawYOffset + addY) * (drawYOffset + addY) < (Settings.systemRadius * Settings.systemRadius))
            {
                drawYOffset += addY;
            }
        }

        //Called whenever the player wants to alter the zoom of the camera.
        public static void AlterCamOrthoSize(float zoomAxis)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + Camera.main.orthographicSize * -zoomAxis * 0.5f;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minCameraOrthographicSize, maxCameraOrthographicSize);     //Implements lower and upper bounds for camera orthographic size.
        }

        //The internal method used to draw the tiles and units of Scramble. Encapsulates a call to DrawMesh that Unity uses and makes it easier
        //to call DrawMesh by not requiring so many freaking parameters every time.
        private static void InternalDrawMesh(float x, float y, int layer, Material drawMaterial)
        {
            Graphics.DrawMesh(drawMesh,
                                new Vector3(x, y, layer),           //Determines where to draw the mesh in the game world.
                                Quaternion.Euler(0, 0, 0),          //Determines what rotation to use.
                                drawMaterial,                       //Determines what Material to use for the mesh.
                                0,                                  //Determines which layer the mesh will be on.
                                null,                               //Determines which camera to render to.
                                0,                                  //Determines which subset of the mesh to draw.
                                null,                               //Doesn't use a MaterialPropertyBlock in this case.
                                false,                              //Determines whether or not to cast shadows.
                                false,                              //Determines whether or not to receive shadows.
                                false);                             //Determines whether or not to use Light Probes.
        }

        //Exactly the same as the other form of InternalDrawMesh except that this one takes rotation of the Mesh into account.
        private static void InternalDrawMesh(float x, float y, int layer, float rotation, Material drawMaterial)
        {
            Graphics.DrawMesh(drawMesh,
                                new Vector3(x, y, layer),           //Determines where to draw the mesh in the game world.
                                Quaternion.Euler(0, 0, rotation),   //Determines what rotation to use.
                                drawMaterial,                       //Determines what Material to use for the mesh.
                                0,                                  //Determines which layer the mesh will be on.
                                null,                               //Determines which camera to render to.
                                0,                                  //Determines which subset of the mesh to draw.
                                null,                               //Doesn't use a MaterialPropertyBlock in this case.
                                false,                              //Determines whether or not to cast shadows.
                                false,                              //Determines whether or not to receive shadows.
                                false);                             //Determines whether or not to use Light Probes.
        }

        //Creates the normal square planar mesh that we use for drawing all the textures basically.
        public static void CreateDrawMesh()
        {
            drawMesh = new Mesh();

            Vector3[] newVertices = new Vector3[4];
            newVertices[0] = new Vector3(-0.5f, 0.5f, 0);       //Using 0.5 instead of say 1.0 is necessary because in Unity, the point that the Mesh rotates around is always at position 0,0,0.
            newVertices[1] = new Vector3(0.5f, 0.5f, 0);
            newVertices[2] = new Vector3(-0.5f, -0.5f, 0);
            newVertices[3] = new Vector3(0.5f, -0.5f, 0);

            //Setup UVs
            Vector2[] newUVs = new Vector2[4];
            newUVs[0] = new Vector2(0, 1);
            newUVs[1] = new Vector2(1, 1);
            newUVs[2] = new Vector2(0, 0);
            newUVs[3] = new Vector2(1, 0);

            //Setup triangles
            int[] newTriangles = new int[] { 0, 1, 2, 3, 2, 1 };

            //Setup normals
            Vector3[] newNormals = new Vector3[4];
            newNormals[0] = Vector3.up;
            newNormals[1] = Vector3.up;
            newNormals[2] = Vector3.up;
            newNormals[3] = Vector3.up;

            //Create quad
            drawMesh.vertices = newVertices;
            drawMesh.uv = newUVs;
            drawMesh.triangles = newTriangles;
            drawMesh.normals = newNormals;
        }
    }
}