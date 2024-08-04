using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeInput : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("prv");
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (previous != null) { previous.Select(); }
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("nxt");
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null) { next.Select(); }
        } 
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("entr");
            submitButton.onClick.Invoke();
        }
    }
    
}
