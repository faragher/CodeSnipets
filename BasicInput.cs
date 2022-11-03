//Untested basically pseudocode
//Demonstrating the concept of low-level keyscan
//Change public variable "Key" to rebind input

//Defs
Dictionary<string, BTBInputs> KeyInput = new Dictionary<string, BTBInputs>();
KeyInput.Add("Forward", new BTBInputs("Forward",  Keys.Up));

//loop
if (CheckKeypress("Forward")) MoveForward(); //One of these commands for each input
//if (CheckKeypress("Backwards"))...

//Functions & Classes
bool CheckKeypress(string Command)
{
    bool isDown = Keyboard.GetState().IsKeyDown(KeyInput[Command].Key);
    //Removed control code, returns true if the key was pressed **only this frame**
}

public class BTBInputs //Edited for clarity
{
    public string Friendlyname = "Undefined Action"; //Name to display in menus
    public Keys Key; //Keycode to scan

    public BTBInputs(string Friendly, Keys Keycode)
    {
        Friendlyname = Friendly; 
        Key = Keycode; 
    }
}
