using UnityEngine;
using TMPro;

public class UISetIngredientInfo : MonoBehaviour
{
    public IngredientSO ingredientSO;
    public TMP_Text title;
    public TMP_Text name;
    public TMP_Text amount;
    public TMP_Text unit;
    // Start is called before the first frame update
    void Start()
    {
        title.text = ingredientSO.ingredientName;
        name.text = ingredientSO.ingredientName;
        amount.text = ingredientSO.ingredientAmount.ToString();
        unit.text = ingredientSO.unit;

    }

    
}
