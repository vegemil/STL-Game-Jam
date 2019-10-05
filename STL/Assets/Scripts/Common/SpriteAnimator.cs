using UnityEngine;
using static CharacterSpriteData;

public class SpriteAnimator : MonoBehaviour
{
    public enum AnimationState
    {
        Idle,
        Walk,
    }

    [SerializeField] private SpriteRenderer targetSpriteRenderer; 
    private CharacterSpriteData characterSpriteData;
    private AnimationData currentAnimationData;
    private AnimationClipData currentClip;
    [SerializeField]  private AnimationState animationState;
    private float delayCount;
    private int spriteIndex;
    private float animationSpeed = 1f;

    public AnimationState CurrentAnimationState => animationState;

    private void Update()
    {
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (currentClip == null)
            return;
        float frameDelay = currentClip.loopDelay / currentClip.sprites.Length;
        delayCount += Time.deltaTime * animationSpeed;
        if (delayCount < frameDelay)
            return;

        delayCount = 0;
        spriteIndex++;
        if (spriteIndex >= currentClip.sprites.Length)
            spriteIndex = 0;
         
        targetSpriteRenderer.sprite = currentClip.sprites[spriteIndex];
    }

    public void Initalize(CharacterSpriteData newAnimationData)
    {
        characterSpriteData = newAnimationData;
        SetAnimationData(CharacterSpriteData.AnimationTyps.Level1);
        SwitchState(AnimationState.Idle); 

    }

    public void SetAnimationData(CharacterSpriteData.AnimationTyps animationType)
    {
        int animationIndex = (int)animationType;
        SetAnimationData(characterSpriteData.animtaionDatas[animationIndex]); 
    }

    public void SetAnimationData(AnimationData newAnimationData)
    {
        delayCount = 0;
        spriteIndex = 0;
        currentAnimationData = newAnimationData;
        SwitchState(animationState);
    }

    public void SetAnimationSpeed(float newAnimationSpeed)
    {
        animationSpeed = newAnimationSpeed;
    }

    public void SetImageFlipX(bool isFlip)
    {
        targetSpriteRenderer.flipX = isFlip;
    }

    public void SwitchState(AnimationState newState)
    {
        animationSpeed = 1f;
        delayCount = 1000;
        spriteIndex = 0;
        animationState = newState;


        switch(animationState)
        {
            case AnimationState.Idle:
                currentClip = currentAnimationData.idleData;
                break;
            case AnimationState.Walk:
                currentClip = currentAnimationData.walkData;
                break;
        }

    }


}
