using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddMenuOpener : MonoBehaviour
{
    public Button OpenMenu;
    bool Open = false;
    public AnimationClip close;
    public AnimationClip open;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        OpenMenu.onClick.AddListener(Action);
    }
    void Action()
    {
        if(Open == false)
        {
            anim.Play("OPEN");
            Open = true;
        }
        else
        {
            anim.Play("Close");
            Open = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
