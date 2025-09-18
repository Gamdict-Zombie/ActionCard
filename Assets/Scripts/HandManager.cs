using UnityEngine;
using SinousProductions;
using System.Collections.Generic;
using UnityEngine.XR;
using System;
using Unity.Mathematics;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab; //Assign card prefab in inspector
    public Transform handTransform; //Root of the hand position
    public float fanSpread = -7.5f;
    public float cardSpacing = 150f;
    public float verticalSpacing = 100f;
    public List<GameObject> cardsInHand = new List<GameObject>(); //Hold a list of the card objects in our hand

    void Start()
    {
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
    }
    private void AddCardToHand()
    {
        //Instantiate the card
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        UpdateHandVisuals();
    }

    void Update()
    {
        UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            float nomalizedPosition = (2f * i / (cardCount - 1) - 1f); //nomalize card position between -1, 1
            float verticalOffset = verticalSpacing * (1 - nomalizedPosition * nomalizedPosition);
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
