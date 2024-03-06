using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class MainUIController : MonoBehaviour, IPointerClickHandler , IDragHandler
{
    public GameObject CurrentFurniture;
    public LayerMask furniturelayer;
    public GameObject OptionsPanel;

    public GameObject Player;

    public ObjectsStorage objectsStorage;

    public FixedJoystick movejoystick;
    public FixedJoystick lookjoystick;

    public float rotationSpeed;

    float pitch;
    float yaw;

    float xrot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CurrentFurniture != null)
        {
            if (CurrentFurniture.GetComponent<Objects>().movable) 
            {
                CurrentFurniture.transform.DOMove(CurrentFurniture.transform.position + (new Vector3(eventData.delta.x, 0, eventData.delta.y)) * 0.05f, 0.1f);
            }
        }
        else
        {
            Debug.Log("Furniture Not Selected");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(eventData.position), out hit, 100, furniturelayer))
        {
            if (hit.collider.gameObject != CurrentFurniture)
            {
                CurrentFurniture = hit.collider.gameObject;
                Cursor.lockState = CursorLockMode.None;
                if (CurrentFurniture.GetComponent<Objects>().Options == null)
                {
                    OptionsPanel.SetActive(true);
                    CurrentFurniture.transform.DOPunchScale(CurrentFurniture.transform.localScale * 0.2f , 0.2f , 1 , 1);
                    //GameObject Options = Instantiate(OptionsCanvas, CurrentFurniture.transform.position, Quaternion.identity, GameObject.Find("OptionsCanvas").transform);
                    //CurrentFurniture.GetComponent<Objects>().Options = Options;
                    //Options.transform.DOLookAt(Camera.main.transform.position, 0.01f);
                    //Options.transform.DOMove((CurrentFurniture.transform.position - Camera.main.transform.position) * 0.5f, 0.1f).SetDelay(0.1f);
                }
            }
            else
            {
                CurrentFurniture = null;
                OptionsPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        else
        {
            CurrentFurniture = null;
            OptionsPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    public void ClickNext()
    {
        List<GameObject> listObject = new List<GameObject>();
        List<Material> listMaterial = new List<Material>();
        Objects.ObjectType s = CurrentFurniture.GetComponent<Objects>().objectType;

        if (s == Objects.ObjectType.sofa)
        {
            listObject = objectsStorage.sofas;
        }
        else if (s == Objects.ObjectType.Cupboards)
        {
            listObject = objectsStorage.Cupboard;
        }
        else if (s == Objects.ObjectType.chair)
        {
            listObject = objectsStorage.Chair;
        }
        else if (s == Objects.ObjectType.TV)
        {
            listObject = objectsStorage.TV;
        }
        else if (s == Objects.ObjectType.CellingLights)
        {
            listObject = objectsStorage.CelingLights;
        }
        else if (s == Objects.ObjectType.FloorLamp)
        {
            listObject = objectsStorage.FloorLamp;
        }
        else if (s == Objects.ObjectType.wall)
        {
            listMaterial = objectsStorage.walls;
        }


        if (s == Objects.ObjectType.wall)
        {
            int index = 0;
            for (int i = 0; i < listMaterial.Count; i++)
            {
                if (CurrentFurniture.GetComponent<Renderer>().sharedMaterial == listMaterial[i])
                {
                    index = i; break;
                }
            }

            
            index++;
            Debug.Log(index);
            CurrentFurniture.GetComponent<Renderer>().sharedMaterial = listMaterial[(index) % listMaterial.Count];
        }
        else
        {
            int index = 0;
            for (int i = 0; i < listObject.Count; i++)
            {
                if (CurrentFurniture.name.StartsWith(listObject[i].name))
                {
                    index = i; break;
                }
            }

            Debug.Log(index);
            index++;
            Debug.Log(" qw "
                + (index) % listObject.Count);
            GameObject temp = CurrentFurniture;
            CurrentFurniture = Instantiate(listObject[(index) % listObject.Count], temp.transform.position, temp.transform.rotation);
            CurrentFurniture.transform.localScale = temp.transform.localScale;
            Destroy(temp);
        }
        
    }


    public void ClickPrevious()
    {
        List<GameObject> listObject = new List<GameObject>();
        List<Material> listMaterial = new List<Material>();
        Objects.ObjectType s = CurrentFurniture.GetComponent<Objects>().objectType;

        if (s == Objects.ObjectType.sofa)
        {
            listObject = objectsStorage.sofas;
        }
        else if (s == Objects.ObjectType.Cupboards)
        {
            listObject = objectsStorage.Cupboard;
        }
        else if (s == Objects.ObjectType.chair)
        {
            listObject = objectsStorage.Chair;
        }
        else if (s == Objects.ObjectType.TV)
        {
            listObject = objectsStorage.TV;
        }
        else if (s == Objects.ObjectType.CellingLights)
        {
            listObject = objectsStorage.CelingLights;
        }
        else if (s == Objects.ObjectType.FloorLamp)
        {
            listObject = objectsStorage.FloorLamp;
        }
        else if (s == Objects.ObjectType.wall)
        {
            listMaterial = objectsStorage.walls;
        }


        if (s == Objects.ObjectType.wall)
        {
            int index = 0;
            for (int i = 0; i < listMaterial.Count; i++)
            {
                if (CurrentFurniture.GetComponent<Renderer>().sharedMaterial == listMaterial[i])
                {
                    index = i; break;
                }
            }


            if (index <= 0)
                index = listMaterial.Count;
            Debug.Log(index);
            CurrentFurniture.GetComponent<Renderer>().sharedMaterial = listMaterial[--index];
        }
        else
        {
            int index = 0;
            for (int i = 0; i < listObject.Count; i++)
            {
                if (CurrentFurniture.name.StartsWith(listObject[i].name))
                {
                    index = i; break;
                }
            }

            Debug.Log(index);
            if (index <= 0)
                index = listObject.Count;
            
            GameObject temp = CurrentFurniture;
            CurrentFurniture = Instantiate(listObject[--index], temp.transform.position, temp.transform.rotation);
            CurrentFurniture.transform.localScale = temp.transform.localScale;
            Destroy(temp);
        }

    }


    public void Rotate(bool left)
    {
        if (CurrentFurniture.GetComponent<Objects>().movable) 
        {
            if (left)
            {
                if (CurrentFurniture != null)
                {
                    CurrentFurniture.transform.DORotate(CurrentFurniture.transform.rotation.eulerAngles - new Vector3(0, 50, 0), 0.5f);
                }
            }
            else
            {
                if (CurrentFurniture != null)
                {
                    CurrentFurniture.transform.DORotate(CurrentFurniture.transform.rotation.eulerAngles + new Vector3(0, 50, 0), 0.5f);
                }
            }
        }
    }

    private void Update()
    {
        if (CurrentFurniture != null)
        {
            CurrentFurniture.transform.DORotate(CurrentFurniture.transform.rotation.eulerAngles + new Vector3(0, Input.GetAxis("Mouse ScrollWheel"), 0), 0.2f);
        }
        else
        {
            
        }
        
        if (Input.GetMouseButton(0) && Cursor.lockState == CursorLockMode.Locked)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector2(Screen.width/2,  Screen.height/2)), out hit, 100, furniturelayer))
            {
                CurrentFurniture = hit.collider.gameObject;
                Cursor.lockState = CursorLockMode.None;
                if (CurrentFurniture.GetComponent<Objects>().Options == null)
                {
                    OptionsPanel.SetActive(true);

                    //GameObject Options = Instantiate(OptionsCanvas, CurrentFurniture.transform.position, Quaternion.identity, GameObject.Find("OptionsCanvas").transform);
                    //CurrentFurniture.GetComponent<Objects>().Options = Options;
                    //Options.transform.DOLookAt(Camera.main.transform.position, 0.01f);
                    //Options.transform.DOMove((CurrentFurniture.transform.position - Camera.main.transform.position) * 0.5f, 0.1f).SetDelay(0.1f);
                }
            }
            else
            {
                CurrentFurniture = null;
                OptionsPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
            }
        }

        float mx = lookjoystick.Horizontal * 100 * Time.deltaTime;
        float my = lookjoystick.Vertical * 100 * Time.deltaTime;

        xrot -= my;
        xrot = Mathf.Clamp(xrot, -90, 90);

        Camera.main.transform.localRotation = Quaternion.Euler(xrot, 0, 0);
        Player.transform.Rotate(Vector3.up, mx);



        Player.transform.position += new Vector3(movejoystick.Horizontal, 0, movejoystick.Vertical) * 5 * Time.deltaTime;
    }
}
