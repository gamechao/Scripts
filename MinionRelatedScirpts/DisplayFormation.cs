using UnityEngine;
using System.Collections;

public class DisplayFormation : MonoBehaviour 
{
	public Texture2D formIcon;//the icon of formation button
	public bool displayFormation = false;//if we need to display the formation
	public Formation form;//get formation
	public int formationType;//formation type
	
	//formation button tuning values in editor
	public int buttonXPos;
	public int buttonYPos;
	public int buttonWidth;
	public int buttonHeight;
	
	//sub buttons tuning values in editor
	public int subStartXPos;
	public int subStartYPos;
	public int xPosModifier;
	public int subButtonWidth;
	public int subButtonHeight;
	
	// Use this for initialization
	void Start () 
	{
		form = FindObjectOfType(typeof(Formation))as Formation;
	}
	
	
	public void OnGUI()//display
	{
		if(formIcon != null)//if we have formation icon then create button
		{
			bool formationButton = GUI.Button(new Rect(Screen.width-buttonXPos,buttonYPos,buttonWidth,buttonHeight),new GUIContent(formIcon,"Formation"));
			GUI.Label(new Rect(Screen.width-buttonXPos-20,buttonYPos+40, 100, 40), GUI.tooltip);
			GUI.tooltip = null;
			//if(GUI.Button(new Rect(Screen.width-40,60,40,40),formIcon))
			//if(GUI.Button(new Rect(Screen.width-buttonXPos,buttonYPos,buttonWidth,buttonHeight),new GUIContent(formIcon,"Formation")))//if button is clicked toggle display formation
			if(formationButton)
			{
				
				//print (GUI.tooltip);
				//print ("make button");
				if(displayFormation)
				{
					displayFormation = false;
				}
				else
				{
					displayFormation = true;
				}
			}
		}
		
		if(displayFormation)//create the formation type buttons and use them to change formation
		{
			bool formationOneButton = GUI.Button (new Rect (Screen.width-subStartXPos,subStartYPos,subButtonWidth,subButtonHeight),new GUIContent( "Formation 1","Left click to change"));
			//GUI.Label(new Rect(Screen.width-buttonXPos-60,buttonYPos+40, 100, 40), GUI.tooltip);
			bool formationTwoButton = GUI.Button (new Rect (Screen.width-subStartXPos+xPosModifier,subStartYPos,subButtonWidth,subButtonHeight),new GUIContent( "Formation 2","Left click to change"));
			//GUI.Label(new Rect(Screen.width-buttonXPos-20,buttonYPos+40, 100, 40), GUI.tooltip);
			bool formationThreeButton = GUI.Button (new Rect (Screen.width-subStartXPos+(xPosModifier*2),subStartYPos,subButtonWidth,subButtonHeight), new GUIContent( "Formation 3","Left click to change"));
			GUI.Label(new Rect(Screen.width-buttonXPos-80,buttonYPos+40, 150, 40), GUI.tooltip);
			if(formationOneButton)
			{
				formationType = 0;
				ChangeDisplayFormation(formationType);
			}
			if(formationTwoButton)
			{
				formationType = 1;
				ChangeDisplayFormation(formationType);
			}
			if(formationThreeButton)
			{
				formationType = 2;
				ChangeDisplayFormation(formationType);
			}
		}
	}
	
	public void ChangeDisplayFormation(int type)//change formation
	{
		if(formationType == type)
		{
			form.ChangeFormation(type);
		}
	}

}
