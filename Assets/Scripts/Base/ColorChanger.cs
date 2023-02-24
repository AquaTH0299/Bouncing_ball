using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    private void Start() 
    {
    }

    public void ChangeMaterial(Color materials)
    {
        // Change the color of the background
        GameManager.instance.backGround.color = materials;

        // Change the color of the ground
        //GameManager.instance.groundRenderer.color = materials;
        foreach (SpriteRenderer renderer in GameManager.instance.groundRenderer)
        {
            renderer.color = materials;
            foreach(Transform child in renderer.transform)
            {
                if(child.CompareTag("Obstacle"))
                {
                    child.GetComponent<SpriteRenderer>().color = materials;
                }
            }
        }

        // Change the color of the particle effect
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
        ParticleSystemRenderer particleRenderer = particleSystem.GetComponent<ParticleSystemRenderer>();
        particleRenderer.material.color = materials;

        // change the color of the textmeshprogui
        GameManager.instance.textObject.color = materials;
    }

    public void ResetColor()
    {
        // Thiết lập lại màu sắc ban đầu của các đối tượng
        GameManager.instance.backGround.color = GameManager.instance.initialMaterial;

        //GameManager.instance.groundRenderer.color = GameManager.instance.initialMaterial;
        foreach (SpriteRenderer renderer in GameManager.instance.groundRenderer)
        {
            renderer.color = GameManager.instance.initialMaterial;
            foreach(Transform child in renderer.transform)
            {
                if(child.CompareTag("Obstacle"))
                {
                    child.GetComponent<SpriteRenderer>().color = GameManager.instance.initialMaterial;
                }
            }
        }

        //GameManager.instance.obstacle.color = GameManager.instance.initialMaterial;

        GameManager.instance.textObject.color  = GameManager.instance.initialMaterial;
    }
}
