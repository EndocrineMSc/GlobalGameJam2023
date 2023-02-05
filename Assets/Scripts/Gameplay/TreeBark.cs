using GameName.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumCollection;

public class TreeBark : HealthEntity
{
    public Sprite[] treeBarkSprites;
    public SFX[] treeHitSounds;

    public static TreeBark leftTreebark;
    public static TreeBark rightTreebark;

    public AttackDirektion myDirection;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (myDirection == AttackDirektion.Left)
            leftTreebark = this;
        else if (myDirection == AttackDirektion.Right)
            rightTreebark = this;
    }

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Init();
    }

    public void Heal(int healAmount)
    {
        currentLive = Mathf.Min(currentLive + healAmount, maxLive);
        UpdateSprite();
    }

    private void OnDisable()
    {
        if (this.Equals(leftTreebark))
            leftTreebark = null;
        if (this.Equals(rightTreebark))
            rightTreebark = null;
    }

    public override void Hit(int damage)
    {
        PlayTreeHitSound();
        UpdateSprite();

        base.Hit(damage);
    }

    void UpdateSprite()
    {
        // Get the right sprite for the current amount of health.
        int rightSpriteIndex = Mathf.FloorToInt(treeBarkSprites.Length * currentLive / maxLive);
        rightSpriteIndex = Mathf.Min(rightSpriteIndex, treeBarkSprites.Length - 1);
        spriteRenderer.sprite = treeBarkSprites[rightSpriteIndex];
    }

    void PlayTreeHitSound()
    {
        if (AudioManager.Instance == null)
            return;

        EnumCollection.SFX soundEffect = treeHitSounds[Random.Range(0, treeHitSounds.Length)];

        AudioManager.Instance.PlaySoundEffect(soundEffect);
    }
}
