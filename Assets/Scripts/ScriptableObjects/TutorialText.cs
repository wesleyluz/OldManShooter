using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorial", menuName = "Tutorial/TutorialText")]
public class TutorialText : ScriptableObject
{   
    // Cria uma lista de textos que será usada no tutorial
    public List<string> Texts;
    
}
