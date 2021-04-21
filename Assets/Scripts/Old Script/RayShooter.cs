using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera _camera;
    public Texture cursorTexture;
    public int cursorSize;

    public GameObject sniperScope;
    private bool sniperMode = false;

    void Start()
    {
        _camera = GetComponent<Camera>();

        (sniperScope.GetComponent<Renderer>()).enabled = false;

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if(target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }   
            }
        }
        if(Input.GetMouseButtonDown(1) && !sniperMode)
        {
            _camera.fieldOfView = 10f;
            MouseLook sensVert = GetComponent<MouseLook>();
            sensVert.sensitivityVert = 1f;

            (sniperScope.GetComponent<Renderer>()).enabled = true;

            PlayerCharacter player = GetComponentInParent<PlayerCharacter>();
            MouseLook sensHor = player.GetComponent<MouseLook>();
            sensHor.sensitivityHor = 1f;
            sniperMode = true;
        }
        if(Input.GetMouseButtonUp(1) && sniperMode)
        {
            _camera.fieldOfView = 60f;
            MouseLook sensVert = GetComponent<MouseLook>();
            sensVert.sensitivityVert = 9.0f;

            (sniperScope.GetComponent<Renderer>()).enabled = false;

            PlayerCharacter player = GetComponentInParent<PlayerCharacter>();
            MouseLook sensHor = player.GetComponent<MouseLook>();
            sensHor.sensitivityHor = 9.0f;
            sniperMode = false;
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }

    void OnGUI()
    {
        float posX = _camera.pixelWidth/2 - cursorSize/2;
        float posY = _camera.pixelHeight/2 - cursorSize/2;
        GUI.Label(new Rect(posX, posY, cursorSize, cursorSize), cursorTexture);
    }
}
