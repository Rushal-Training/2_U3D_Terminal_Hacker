using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	int level;
	string randomPassword;
	enum Screen { MainMenu, Password, Win };
	Screen currentScreen;

	// Passwords for each level.  Each new array line is a new level
	string [][] levelPasswords =
	{
		new string[] { "locker", "classroom", "gym", "teacher", "student" },
		new string[] { "officer", "jail cell", "handcuffs", "evidence" },
		new string[] { "space", "gravity", "astronaut", "rocket", "space station", "lift off" }
	};
	const string menuHint = "You may type menu at any time";

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
		if ( input == "menu" || currentScreen == Screen.Win )
		{
			ShowMainMenu ();
		}
		else if ( input == "quit" || input == "close" || input == "exit" )
		{
			Terminal.WriteLine ( "If on the web, close the tab" );
			Application.Quit();
		}
		else if ( currentScreen == Screen.MainMenu )
		{
			RunMainMenu ( input );
		}
		else if ( currentScreen == Screen.Password )
		{
			CheckPassword ( input );
		}
	}

	void RunMainMenu( string input )
	{
		int inputAsInt = int.Parse ( input );

		// Assume that each array in levelPasswords is another allowed level
		if ( inputAsInt >= 1 && inputAsInt <= levelPasswords.Length )
		{
			level = inputAsInt;
			AskForPassword ();
		}
		else if ( input == "007" ) // Easter egg
		{
			Terminal.WriteLine ( "Welcome, Bond. Choose a level" );
		}
		else if (currentScreen == Screen.MainMenu)
		{
			Terminal.WriteLine ("Please choose a valid level.");
			Terminal.WriteLine ( menuHint );
		}
	}

	void AskForPassword ()
	{
		SetPassword ();
		currentScreen = Screen.Password;
		Terminal.ClearScreen ();
		Terminal.WriteLine ( "Enter your password, hint: " +  randomPassword.Anagram() );
		Terminal.WriteLine ( menuHint );
	}

	void SetPassword ()
	{
		randomPassword = levelPasswords [ level - 1 ] [ Random.Range ( 0, levelPasswords [ level - 1 ].Length ) ];
	}

	void CheckPassword ( string password )
	{
		if ( randomPassword == password )
		{
			DisplayWinScreen ();
		}
		else {
			AskForPassword ();
		}
	}

	void DisplayWinScreen ()
	{
		currentScreen = Screen.Win;
		Terminal.ClearScreen ();
		Terminal.WriteLine ( "Password accepted!" );
		Terminal.WriteLine ( "Press Enter to continue" );
	}
}
