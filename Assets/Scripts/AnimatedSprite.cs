using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    [SerializeField] private float _animationTime = 0.25f;
    public int animationFrame { get; private set; }
    public bool loop = true;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), _animationTime, _animationTime);
    }

    private void Advance()
    {
        if(!this.spriteRenderer.enabled) {
            return;
        }

        this.animationFrame++;

        if(this.loop && (this.animationFrame >= sprites.Length)) {
            this.animationFrame = 0;
        }

        if(this.animationFrame >= 0 && this.animationFrame < sprites.Length) {
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    public void Restart()
    {
        this.animationFrame = -1;
        Advance();
    }
}
