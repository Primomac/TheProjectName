using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteKilled : MonoBehaviour
{
    public Sprite newImage;
    private Image myIMGcomponent;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        myIMGcomponent = this.GetComponent<Image>();
        myIMGcomponent.sprite = FindEnemySprites.enemySprite[ID];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
