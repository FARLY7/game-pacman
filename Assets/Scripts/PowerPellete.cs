using UnityEngine;

public class PowerPellete : Pellet
{
    public float duration = 8.0f;

    override protected void Eat()
    {
        FindObjectOfType<GameManager>().PowerPelletEaten(this);
    }

}
