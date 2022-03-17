using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales; //proportion of camera's movement to move the backgrounds by
    public float parallaxSmoothing = 1f;

    private Transform cam; //ref to main camera's transform
    private Vector3 prevCamPos; //store position of camera in prev frame

    //called before Start(). Great for references.
    void Awake()
    {
        cam = Camera.main.transform;

    }
    // Start is called before the first frame update
    void Start()
    {
        prevCamPos = cam.position;
        parallaxScales =  new float[backgrounds.Length];

        for (int i = 0; i<backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z *-1;

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i =0; i < backgrounds.Length; i++)
        {
            float parallax = (prevCamPos.x - cam.position.x) * parallaxScales[i];
            //set a target x position which is the current pos plus the parallax

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position

            Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current position and target position using lerp

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, parallaxSmoothing * Time.deltaTime);

        }

        // set prevCamPos to camera's position at the end of the frame
        prevCamPos = cam.position;

    }
}
