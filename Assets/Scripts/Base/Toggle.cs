using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
   [SerializeField] Sprite volummOn;
   [SerializeField] Sprite volummOff;

   private bool isOn;
   public bool IsOn => isOn;

   public void ToggleAudio(){
        if(isOn)
        {
            this.GetComponent<SpriteRenderer>().sprite = volummOn;
            //tat am thanh
        } else
        {
            this.GetComponent<SpriteRenderer>().sprite = volummOff;
            //bat am thanh
        }
        isOn = !isOn;
   }
}
