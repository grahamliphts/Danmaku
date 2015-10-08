using UnityEngine;
using System.Collections;


public class LoadSprite : MonoBehaviour
{
    public SpriteRenderer spriteBoss;
    public enum Color { Black, Blue, Green, Orange};
    public void SelectSprite(int color)
    {
        Sprite newSprite = null;
        if(color == (int)Color.Black)
            newSprite = Resources.Load<Sprite>("enemyBlack");
        else if (color == (int)Color.Blue)
            newSprite = Resources.Load<Sprite>("enemyBlue");
        else if (color == (int)Color.Green)
            newSprite= Resources.Load<Sprite>("enemyGreen");
        else if (color == (int)Color.Orange)
            newSprite = Resources.Load<Sprite>("enemyRed");
        if(newSprite != null)
            spriteBoss.sprite = newSprite;

    }
}
