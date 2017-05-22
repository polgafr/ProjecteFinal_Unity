 var isPause = false;
 var MainMenu : Rect = Rect(10, 10, 200, 200);
  
 function Update () {
  if( Input.GetKeyDown(KeyCode.Escape))
    {
       isPause = !isPause;
       if(isPause)
          Time.timeScale = 0;
       else
          Time.timeScale = 1;
    }
 }
  
 function OnGUI()
 {
    if(isPause)
        GUI.Window(0, MainMenu, TheMainMenu, "Pause Menu");
 }
 
 function TheMainMenu () {
 if(GUILayout.Button("Main Menu")){
 Application.LoadLevel("MainMenu");
 }
 if(GUILayout.Button("Restart")){
 Application.LoadLevel("InGame");
 }
 if(GUILayout.Button("Quit")){
 Application.Quit();
 }
 }