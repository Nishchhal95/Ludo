using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public static Dice Instance = null;

    public Sprite[] sprites;

    public int targetRollNumber;
    public int rollNumber;
    public float rollDuration;
    public float currentRollDuration;

    public SpriteRenderer diceGFX;
    public bool roll;

    public Animator diceAnimator;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetRoll();
    }

    // Update is called once per frame
    void Update()
    {
        if (roll)
        {
            RollDice();
        }
    }

    private void RollDice()
    {
        currentRollDuration -= Time.deltaTime;
        if (currentRollDuration <= 0)
        {
            rollNumber = targetRollNumber;
            roll = false;
            ResetRoll();
            diceGFX.sprite = sprites[targetRollNumber - 1];
            EventsManager.onDiceRollComplete?.Invoke(rollNumber);
        }
    }

    private void ResetRoll()
    {
        diceAnimator.enabled = false;
    }

    public void Roll()
    {
        currentRollDuration = rollDuration;
        targetRollNumber = Random.Range(1, 7);
        roll = true;
        diceAnimator.enabled = true;
    }
}
