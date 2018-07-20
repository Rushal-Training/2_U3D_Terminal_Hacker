using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	int level;
	enum Screen { MainMenu, Password, Win };
	Screen currentScreen;

	// Use this for initialization
	void Start ()
    {
		ShowMainMenu ();
    }

    private void ShowMainMenu ()
    {
		currentScreen = Screen.MainMenu;
		Terminal.ClearScreen ();
		Terminal.WriteLine ( "What would you like to hack into?" );
		Terminal.WriteLine ( "" );
		Terminal.WriteLine ( "1. Let's try the school district." );
		Terminal.WriteLine ( "2. Maybe the Police Station." );
		Terminal.WriteLine ( "3. How about NASA?" );
		Terminal.WriteLine ( "" );
		Terminal.WriteLine ( "Choose a #. Press Enter to continue." );
    }

	void OnUserInput ( string input )
	{
		if ( input == "menu" )
		{
			ShowMainMenu ();
		}
		else if ( currentScreen == Screen.MainMenu )
		{
			RunMainMenu ( input );
		}
	}

	void RunMainMenu( string input )
	{
		if ( input ==  "1" )
		{
			level = 1;
			StartGame ();
		}
		else if ( input == "2" )
		{
			level = 2;
			StartGame ();
		}
		else if ( input == "3" )
		{
			level = 3;
			StartGame ();
		}
		else if ( input == "4" )
		{
			level = 4;
			StartGame ();
		}
		else if ( input == "007" )
		{
			Terminal.WriteLine ( "Welcome, Bond. Choose a level" );
		}
		else
		{
			Terminal.WriteLine ( "Please choose a valid level." );
		}
	}

	void StartGame()
	{
		currentScreen = Screen.Password;
		Terminal.WriteLine ( "You have chosen level " + level);
		Terminal.WriteLine ( "Enter a password");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
