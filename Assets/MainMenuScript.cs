using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas controlsMenu;
	public Canvas howToPlayMenu;
	public Canvas soundMenu;
	public Canvas difficultyMenu;
	public Canvas optionsMenu;
	public Canvas quitMenu;
	public Button returnText;
	public Button exitText;
	public Button optionsText;
	public Button menuGear;
	public Image pauseBackground;
	
	private bool isPaused = false;

	// Script qui gere le menu principal du jeu
	void Start () 
	{

		mainMenu = mainMenu.GetComponent<Canvas> ();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		returnText = returnText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		optionsText = optionsText.GetComponent<Button> ();
		menuGear = menuGear.GetComponent<Button> ();
		pauseBackground = pauseBackground.GetComponent<Image> ();
		mainMenu.enabled = false;
		quitMenu.enabled = false;
		optionsMenu.enabled = false;
		pauseBackground.enabled = false;
		controlsMenu.enabled = false;
		soundMenu.enabled = false;
		howToPlayMenu.enabled = false;
		difficultyMenu.enabled = false;
		
	}

	public void DifficultyPress()
	{
		difficultyMenu.enabled = true;
		optionsMenu.enabled = false;
		isPaused = true;
		
	}

	public void DifficultyReturnPress()
	{
		difficultyMenu.enabled = false;
		optionsMenu.enabled = true;
		isPaused = true;
		
	}
	
	public void HowToPlayPress()
	{
		howToPlayMenu.enabled = true;
		mainMenu.enabled = false;
		isPaused = true;
		
	}

	public void HowToPlayReturnPress()
	{
		mainMenu.enabled = true;
		howToPlayMenu.enabled = false;
		isPaused = true;
		
	}
	

	public void SoundPress()
	{
		optionsMenu.enabled = false;
		soundMenu.enabled = true;
		isPaused = true;
		
	}

	public void SoundReturnPress()
	{
		optionsMenu.enabled = true;
		soundMenu.enabled = false;
		isPaused = true;
		
	}

	public void ControlsPress()
	{
		optionsMenu.enabled = false;
		controlsMenu.enabled = true;
		isPaused = true;
		
	}

	public void ControlsReturnPress()
	{
		optionsMenu.enabled = true;
		controlsMenu.enabled = false;
		isPaused = true;
		
	}

	public void ReturnPress()
	{
		mainMenu.enabled = false;
		isPaused = false;
		
	}

	public void OptionsReturnPress()
	{
		mainMenu.enabled = true;
		optionsMenu.enabled = false;
		isPaused = true;
		
	}

	public void OptionsPress()
	{
		mainMenu.enabled = false;
		optionsMenu.enabled = true;
		isPaused = true;
		
	}

	// Gere le bouton de rouge ou le bouton ESC
	public void GearPress()
	{
		if (!mainMenu.enabled)
		{
			mainMenu.enabled = true;
			isPaused = true;
			// Pour sortir rapidement du menu avec ESC ou le bouton d'options
			if(quitMenu.enabled 
			   || 
			   optionsMenu.enabled 
			   || 
			   controlsMenu.enabled 
			   || 
			   soundMenu.enabled
			   ||
			   howToPlayMenu.enabled
			   ||
			   difficultyMenu.enabled) 
			{
				mainMenu.enabled = false;
				quitMenu.enabled = false;
				optionsMenu.enabled = false;
				controlsMenu.enabled = false;
				soundMenu.enabled = false;
				howToPlayMenu.enabled = false;
				difficultyMenu.enabled = false;
				isPaused = false;
			}
		}
		else 
		{
			mainMenu.enabled = false;
			quitMenu.enabled = false;
			optionsMenu.enabled = false;
			controlsMenu.enabled = false;
			soundMenu.enabled = false;
			howToPlayMenu.enabled = false;
			difficultyMenu.enabled = false;
			isPaused = false;
		}
	}
	
	public void QuitPress()
	{
		quitMenu.enabled = true;
		mainMenu.enabled = false;
		isPaused = true;
	}
	
	public void NoPress()
	{
		quitMenu.enabled = false;
		mainMenu.enabled = true;
		isPaused = true;
	}
	
	public void QuitGame()
	{
		Application.LoadLevel (0);	
		isPaused = false;
	}

	void Update () 
	{
		// Fait en sorte que ESC ouvre le menu
		if (Input.GetKeyDown (KeyCode.Escape))
			GearPress ();

		// Stop le temps et le mouvement de camera si le menu est ouvert
		if(isPaused)
		{
			Time.timeScale = 0;
			pauseBackground.enabled = true;
			GameObject.Find("Main Camera").GetComponent<OrbitCamera>().enabled = false;
		}
		// Rétablit tout si le menu est fermé
		else
		{
			Time.timeScale = 1;
			pauseBackground.enabled = false;
			GameObject.Find("Main Camera").GetComponent<OrbitCamera>().enabled = true;
		}
	}

}