using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

	// TODO RESET GAME

	int currentLevel, numberOfTimesEnterPressed, totalWordsInGame, completedWordsCombinedTotal;
	int[] perLevelCompletedWords;
	string randomPassword;
	enum Screen { MainMenu, Password, Win };
	Screen currentScreen;

	List<List<string>> levels = new List<List<string>> ();
	const string menuHint = "You may type menu at any time";

	void Start ()
	{
		SetLevelPasswords ();
		GetTotalWords ();
		SetCompletedWordsCombinedTotal ();
		ShowMainMenu ();
	}

	void SetLevelPasswords ()
	{
		// each add is a new level.
		// level text displayed on terminal is the first add item
		levels.Add ( new List<string> { "1. Let's try the school district.", "2. Maybe the Police Station.", "3. How about NASA?" } );
		levels.Add ( new List<string> { "locker", "classroom", "gym", "teacher", "student" } );
		levels.Add ( new List<string> { "officer", "jail cell", "handcuffs", "evidence" } );
		levels.Add ( new List<string> { "space", "gravity", "astronaut", "rocket", "space station", "lift off" } );

		// initialize the array to the number of levels available
		perLevelCompletedWords = new int [levels.Count - 1];
	}

	void ShowMainMenu ()
	{
		currentScreen = Screen.MainMenu;
		Terminal.ClearScreen ();
		Terminal.WriteLine ( "What would you like to hack into?" );
		Terminal.WriteLine ( "" );

		ShowLevels ();

		Terminal.WriteLine ( "" );
		Terminal.WriteLine ( "Choose a #. Press Enter to continue." );
	}

	void ShowLevels ()
	{
		for ( int i = 0; i < levels.Count - 1; i++ )
		{
			Terminal.WriteLine ( levels [0] [i] + " " + perLevelCompletedWords [i] + "/" + (levels [i + 1].Count + perLevelCompletedWords [i]) );
		}
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
		else if ( input == null || input == "" || input == " " )
		{
			Terminal.WriteLine ( "Please enter a level number" );
			numberOfTimesEnterPressed++;
			if ( (numberOfTimesEnterPressed % 5) == 0 ) { Terminal.WriteLine ( menuHint ); }

		}
		else if ( currentScreen == Screen.MainMenu )
		{
			RunMainMenu ( input );
		}

		if ( currentScreen == Screen.Password )
		{
			CheckPassword ( input );
		}
	}

	void RunMainMenu( string input )
	{
		int inputAsInt = int.Parse ( input );

		// Assume that each array in levelPasswords is another allowed level
		if ( inputAsInt >= 1 && inputAsInt <= levels.Count )
		{
			currentLevel = inputAsInt;
			int perLevelTotalWords = levels [currentLevel].Count + perLevelCompletedWords [ currentLevel - 1 ];

			if ( perLevelTotalWords == perLevelCompletedWords[ currentLevel - 1 ] )
			{
				Terminal.WriteLine ( "You've completed that level." );
				Terminal.WriteLine ( "Please choose another." );
				numberOfTimesEnterPressed++;
				if ( (numberOfTimesEnterPressed % 5) == 0 ) { Terminal.WriteLine ( menuHint ); }
			}
			else
			{
				AskForPassword ();
			}
		}
		else if ( input == "007" ) // Easter egg
		{
			Terminal.WriteLine ( "Welcome, Bond. Choose a level" );
		}
		else if (currentScreen == Screen.MainMenu )
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
		randomPassword = levels[ currentLevel ] [ Random.Range ( 0, levels[ currentLevel ].Count ) ];
	}

	void CheckPassword ( string password )
	{
		if ( randomPassword == password )
		{
			levels[ currentLevel ].Remove( randomPassword );
			perLevelCompletedWords[ currentLevel - 1 ]++;
			SetCompletedWordsCombinedTotal ();

			if ( completedWordsCombinedTotal >= totalWordsInGame )
			{
				DisplayWinScreen ();
			}
			else
			{
				DisplayContinueScreen ();
			}
		}
		else {
			AskForPassword ();
		}
	}

	void DisplayContinueScreen ()
	{
		currentScreen = Screen.Win;
		Terminal.ClearScreen ();
		Terminal.WriteLine ( "Password accepted!" );
		Terminal.WriteLine ( "There are still: " + (totalWordsInGame - completedWordsCombinedTotal ) + " words left to crack" );
		Terminal.WriteLine ( "Press Enter to continue" );
	}

	void DisplayWinScreen ()
	{
		currentScreen = Screen.Win;
		Terminal.ClearScreen ();
		Terminal.WriteLine ( "You've cracked all the passwords!" );
		Terminal.WriteLine ( "Press Enter to restart!" );

		SetLevelPasswords ();
		GetTotalWords ();
		SetCompletedWordsCombinedTotal ();
	}

	void GetTotalWords ()
	{
		foreach (List<string> words in levels)
		{
			totalWordsInGame += words.Count;
		}
		totalWordsInGame = totalWordsInGame - levels [0].Count;
	}

	void SetCompletedWordsCombinedTotal ()
	{
		completedWordsCombinedTotal = 0;
		foreach ( int total in perLevelCompletedWords )
		{
			completedWordsCombinedTotal += total;
		}
	}
}