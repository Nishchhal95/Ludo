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
        if(currentRollDuration <= 0)
        {
            diceGFX.sprite = sprites[targetRollNumber - 1];
            roll = false;
            rollNumber = targetRollNumber;
            EventsManager.onDiceRollComplete?.Invoke(targetRollNumber);
            ResetRoll();
        }
        currentRollDuration -= Time.deltaTime;
        if(Time.frameCount % 5 == 0)
        diceGFX.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void ResetRoll()
    {
        currentRollDuration = rollDuration;
        targetRollNumber = Random.Range(1, 7);
    }

    public void Roll()
    {
        roll = true;
    }
}
