# UnityAR
A STEM project

-----NOTICES-----  
*Place all the .obj files into \Asset\Resources\  
*Place the XML file in the root folder of the project  
*The XML file must be named "data.xml"
*DO NOT make any changes to ARIO.cs and ObjectData.cs

-----HOW TO USE-----
1. Create a Unity project as usual
2. Add the Vuforia Camera into the scene as usual  
3. Add image targets into the scene as usual  
4. Add a BoxcCollider for each image targets
5. Rename the BoxCollider into a single English alphabet  
   For example, if you create an image target with a letter 'A' on it, and create a BoxCollider for it, you should then rename that BoxCollider into "A" <br>  https://i.imgur.com/ia1eZ6h.png
6. Set reference for both image targets and BoxCollider in the scene to "public GameObject[] Targets" and "public BoxCollider[] Boxes" respectively
7. Put all the .obj files you gonna use into the folder \Asset\Resources, no need to add them into the scene
8. Use the XML creator program i made to serialize data into a XML file
9. Rename the XML file into "data.xml"
10. Place the XML file into the root folder of the project
11. Done

-----DEBUG-----
1. Exception: FAILED TO LOAD XML FILE  <br>
reason 1: The XML file is placed in a wrong folder <br>
reason 2: Wrong naming of the XML file
2. Exception: Reference not set to an instance of an object <br>
reason 1: No matching .obj files are found in the Resources folder <br>
3. No 3D object is presented when the texts are scanned <br>
reason 1: Did you follow HOW TO USE step 6? <br>
reason 2: Problem with your Vuforia Camera config <br>
4. Bugs are not listed above <br>
reason 1: You're an idiot <br>
reason 2: I'm an idiot
