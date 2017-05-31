#pragma strict

var entro : boolean = false;

 var isPause = false;
 var MainMenu : Rect = Rect(10, 10, 200, 200);

 /*
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
 */



function OnTriggerEnter (){

	entro = true;
}

function OnTriggerExit () {
	
	entro = false;
}

function OnGUI () {


	if((entro == true)) {
		
        GUI.Window(0, MainMenu, TheMainMenu, "Pause Menu");
	 	 Time.timeScale=0;

	}

	}
	/*

	 function Update2 (){
 	 if(entro == true) {
	 	 Time.timeScale=0;
	 }
 }*/

function TheMainMenu () {
 if(GUILayout.Button("Main Menu")){
 Application.LoadLevel("Menu");
 }
 if(GUILayout.Button("Restart")){
 Application.LoadLevel("Porsche_CheckPoint");
 }
 if(GUILayout.Button("Quit")){
 Application.Quit();
 }

 }


/*

function Start () {
	
}

function Update () {
	
}

function OnTriggerEnter (object : Collider){
    if(object.CompareTag("Player")){
       //add code for what is done when the player enters here...


    }
 }
 */