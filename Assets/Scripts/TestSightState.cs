using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicToc.Mechanics
{
    [RequireComponent(typeof(SightState))]
    [RequireComponent(typeof(MeshRenderer))]
    public class TestSightState : MonoBehaviour
    {
        SightState sightState;
        MeshRenderer meshRenderer;

        private void Start()
        {
            sightState = GetComponent<SightState>();
            meshRenderer = GetComponent<MeshRenderer>();
            VisualizeState(false);
            sightState.SightStateChanged += OnSightStateChanged;
        }

        private void OnSightStateChanged(object sender, SightStateEventArgs e)
        {
            VisualizeState(e.InSight);
        }

        private void VisualizeState(bool state)
        {
            if(state)
            {
                meshRenderer.material.color = Color.green;
            }
            else
            {
                meshRenderer.material.color = Color.red;
            }
        }
    }
}