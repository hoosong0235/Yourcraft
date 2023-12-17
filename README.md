# Yourcraft

<div align="center">
    <a href="https://youtu.be/iNsEbDwvsV8">
        <img src="/ReadmeAssets/Yourcraft_cover.png">
    </a>
</div>

## Release

https://hugeson.itch.io/yourcraft

## Demo

<div align="center">
    <a href="https://youtu.be/iNsEbDwvsV8">
        <img src="/ReadmeAssets/Thumbnail.png">
    </a>
</div>

## Screenshots

<div align="center">
    <img src="/ReadmeAssets/Screenshots.png">
</div>

## Phase 1: Concept and Design

<!-- Brainstorm a concept for your VR experience, drawing inspiration from the articles.
Sketch a basic design for your virtual environment, considering elements like nature, Earth, space, wildlife, humanity, and the emotional impact you want to convey. -->

Inspired of following game, Minecraft, I decided to develop the game similar to it. I will implement some basic functionalities of the game, such as terrain generation, player control, destroying and placing block, and user interface including inventory, debugging screen, main menu.

<blockquote>
Explore your own unique world, survive the night, and create anything you can imagine!
</blockquote>

<div align="right">
    <a href="https://www.minecraft.net/en-us">https://www.minecraft.net/en-us</a>
</div>

<br>

<p align="center">
    <img src="/ReadmeAssets/Minecraft_cover.png" width="50%">
</p>

https://play.google.com/store/apps/details?id=com.yodo1.crossyroad&hl=en_US&gl=US  
https://apps.apple.com/us/app/crossy-road/id924373886

## Phase 2: Asset Compilation

<!-- Use readily available 3D models, textures, and assets (or create simple ones) to build your virtual environment.
Ensure that the assets align with your chosen theme and design. -->

<p align="center">
    <img src="/ReadmeAssets/3DBlocks.webp">
</p>

https://assetstore.unity.com/packages/3d/environments/3d-blocks-150775

## Phase 3: Game Development

<!-- Using the Unreal Engine VR Template, begin constructing your first environment.
Implement basic user interactions, such as the ability to look around and navigate the space. -->

### 1. Player

<a href="/Assets/Scripts/PlayerController.cs">🔥 PlayerController.cs</a>

### 2. Camera

<a href="/Assets/Scripts/CameraController.cs">🔥 CameraController.cs</a>

### 3. Terrain

<a href="/Assets/Scripts/TerrainGenerator.cs">🔥 TerrainGenerator.cs</a>

### 4. Block

<a href="/Assets/Scripts/BlockController.cs">🔥 BlockController.cs</a>

### 5. Item

<a href="/Assets/Scripts/ItemController.cs">🔥 ItemController.cs</a>

### 6. User Interface

<a href="/Assets/Scripts/CanvasController.cs">🔥 CanvasController.cs</a>

## Phase 4: Testing and Iteration

<!-- Test your VR experience to ensure it runs smoothly and effectively.
Gather feedback from peers and make necessary adjustments to enhance the user experience. -->

### 1. Optimization

When I first build the game and played it, it was so laggy that I can't even normally control the player. In this project, I really focused on optimizing the game in several asperct.

First, I manipulated the size of map. exact size of the map whose center is the current location of the player is rendered every single frame. It was unneccesarily large initially, so I reduced it until minimum that doesn't affect gameplay of the user.

Second, I applied occlusion culling to the game. Occulusion culling is a process which prevents Unity from performing rendering calculations for GameObjects that are completely hidden from view (occluded) by other GameObjects. Every frame, Cameras perform culling operations that examine the Renderes in the Scene and exclude (cull) those that do not need to be drawn. There are numerous number of blocks in the scene, so it is very effective to not render those that are not to be seen.

Third, I disabled block controller script to all blocks in the scene by default, and enable it for only blocks that are pointed or placed by the user. Every actions that the user can affect block, placing and destroying, is only can be happened after pointing the block. Since each block controller executes some sort of calculation every frame, disabling block controller from most of the blocks reduced meaningful amount of latency.

### 2. Destroying and Placing Block

The another part that I really focused on while making this game is algorithm that supervises destroying and placing block, which is also the core mechanism of the game "Yourcraft". There are several cases to handle like 1. player unpresses mouse before the block is destroyed, 2. player presses mouse but the aim is out before the block is destroyed, 3. player presses mouse but the aim is moved to another block before original block is destroyed, and so on.

If you are interested in how I handled this cases, see <a href="/Assets/Scripts/PlayerController.cs">🔥 PlayerController.cs</a> or

<details>
<summary>Snippet of 🔥 PlayerController.cs</summary>

    void pointBlock()
    {
        if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceInteract))
        {
            if (goPoint != hitInfoDestroy.collider.gameObject)
            {
                try
                {
                if (goPoint != null) goPoint.GetComponent<BlockController>().endPoint();
                }
                catch
                {
                print("[ERROR 5-1] endPoint with null goPoint");
                }

                try
                {
                    goPoint = hitInfoDestroy.collider.gameObject;
                    goPoint.GetComponent<BlockController>().enabled = true;
                    goPoint.GetComponent<BlockController>().startPoint();
                }
                catch
                {
                    print("[ERROR 4-1] startPoint with null goPoint");
                }
            }
            else
            {
                try
                {
                    goPoint = hitInfoDestroy.collider.gameObject;
                    goPoint.GetComponent<BlockController>().startPoint();
                }
                catch
                {
                    print("[ERROR 4-1] startPoint with null goPoint");
                }
            }
        }
        else
        {
            try
            {
                if (goPoint != null) goPoint.GetComponent<BlockController>().endPoint();
            }
            catch
            {
                print("[ERROR 5-2] endPoint with null goPoint");
            }

            goPoint = null;
        }
    }

    void destroyBlock()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceInteract))
            {
                try
                {
                    goDestroy = hitInfoDestroy.collider.gameObject;
                    goDestroy.GetComponent<BlockController>().startDestroy();
                }
                catch
                {
                    print("[ERROR 1-1] startDestroy with null goDestroy");
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceInteract))
            {
                if (goDestroy != hitInfoDestroy.collider.gameObject)
                {
                    try
                    {
                        if (goDestroy != null) goDestroy.GetComponent<BlockController>().endDestroy();
                    }
                    catch
                    {
                        print("[ERROR 3-1] endDestroy with null nonnull goDestroy");
                    }

                    try
                    {
                        goDestroy = hitInfoDestroy.collider.gameObject;
                        goDestroy.GetComponent<BlockController>().startDestroy();
                    }
                    catch
                    {
                        print("[ERROR 1-2] startDestroy with null goDestroy");
                    }
                }
                else
                {
                    try
                    {
                        goDestroy = hitInfoDestroy.collider.gameObject;
                        goDestroy.GetComponent<BlockController>().startDestroy();
                    }
                    catch
                    {
                        print("[ERROR 1-2] startDestroy with null goDestroy");
                    }
                }
            }
            else
            {
                try
                {
                    if (goDestroy != null) goDestroy.GetComponent<BlockController>().endDestroy();
                }
                catch
                {
                    print("[ERROR 3-2] endDestroy with null nonnull goDestroy");
                }

                goDestroy = null;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoDestroy, maxDistanceInteract))
            {
                try
                {
                    goDestroy.GetComponent<BlockController>().endDestroy();
                }
                catch
                {
                    print("[ERROR 2-1] endDestroy wiht null goDestroy");
                }

                goDestroy = null;
            }
        }
    }

    void placeBlock()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(tfCamera.position, tfCamera.TransformDirection(Vector3.forward), out hitInfoPlace, maxDistanceInteract))
            {
                hitOrigin = hitInfoPlace.collider.gameObject.transform.position;
                hitPoint = hitInfoPlace.point;

                if (canvasController.getBlockNums() > 0)
                {
                    goPlace = Instantiate(blocks[canvasController.getIndex()]);
                    goPlace.transform.position = new Vector3(
                        hitOrigin.x - 0.5f == hitPoint.x ? hitOrigin.x - 1f : (hitOrigin.x + 0.5f == hitPoint.x ? hitOrigin.x + 1f : hitOrigin.x),
                        hitOrigin.y - 0.5f == hitPoint.y ? hitOrigin.y - 1f : (hitOrigin.y + 0.5f == hitPoint.y ? hitOrigin.y + 1f : hitOrigin.y),
                        hitOrigin.z - 0.5f == hitPoint.z ? hitOrigin.z - 1f : (hitOrigin.z + 0.5f == hitPoint.z ? hitOrigin.z + 1f : hitOrigin.z)
                    );

                    goPlace.GetComponent<BlockController>().enabled = true;
                    goPlace.GetComponent<BlockController>().isPlaced = true;

                    canvasController.subBlockNums();
                }
            }
        }
    }

</details>

## Phase 5 Documentation and Presentation

<!-- Create documentation, including a brief project description, screenshots of your experience, a list of assets used, and a reflection on the emotional impact you aimed to achieve.
Present your VR experiences to the class, sharing your inspirations and insights. -->

### 1. How to Play

#### 1) Web

https://hugeson.itch.io/yourcraft

#### 2) Windows

https://github.com/hoosong0235/Yourcraft/releases/tag/v1.0.0

### 2. Reference

#### 1) Minecraft

https://www.minecraft.net/en-us

#### 2) Asset

https://assetstore.unity.com/packages/3d/environments/3d-blocks-150775
