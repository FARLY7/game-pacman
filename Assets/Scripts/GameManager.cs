using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ghost[] _ghosts;
    [SerializeField] private Pacman _pacman; 
    [SerializeField] private Transform _pellets;

    public int lives { get; private set; }
    public int score { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if(this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    public void NewRound()
    {
        /* Turn on all pellets */
        foreach(Transform pellet in _pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    public void ResetState()
    {
        /* Turn on all ghosts */
        for(int i = 0 ; i < _ghosts.Length ; i++)
        {
            _ghosts[i].gameObject.SetActive(true);
        }

        /* Turn on player */
        this.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        /* Turn off all pellets */
        foreach(Transform pellet in _pellets)
        {
            pellet.gameObject.SetActive(false);
        }
        
        /* Turn off all ghosts */
        for(int i = 0 ; i < _ghosts.Length ; i++)
        {
            _ghosts[i].gameObject.SetActive(false);
        }

        /* Turn on player */
        this.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(this.score + ghost.points);
    }

    public void PacmanEaten()
    {
        this.gameObject.SetActive(false);
        SetLives(this.lives - 1);

        if(this.lives > 0) {
            Invoke(nameof(ResetState), 2.0f);
        }
        else {
            Invoke(nameof(GameOver), 2.0f);
        }
    }
}
