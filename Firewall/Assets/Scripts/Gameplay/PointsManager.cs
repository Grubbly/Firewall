using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PointsManager : MonoBehaviour
{
    private float points = 0f;

    public float rate = 0.01f;

    public void spendPoints(float pointsToSpend) {
        points -= pointsToSpend;
    }

    public void setRate(float newRate) {
        rate = newRate;
    }

    public float getPoints() {
        return points;
    }

    private void OnGUI() {
        GUI.Box(new Rect(0,0,100,25), "Points: " + points.ToString("0.00"));
    }

    private void Update() {
        points += rate;
    }
}
