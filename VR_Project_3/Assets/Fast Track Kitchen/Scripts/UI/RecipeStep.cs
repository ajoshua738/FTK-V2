using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecipeStep : MonoBehaviour
{
    [TextArea(0, 10)]
    public string stepDescription;       // Description of the step
    public PlayHint[] hintGameObjects;  // Game objects associated with this step
}
