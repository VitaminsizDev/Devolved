using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput;

    public Vector2 RawDashDirectionInput;
    public Vector2 DashDirectionInput;
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool dashInput;
    public bool DashInputStop { get; private set; }

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RawMovementInput.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashInput = true;
            DashInputStop = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            dashInput = false;
            DashInputStop = true;
        }


        RawDashDirectionInput.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


      //  RawDashDirectionInput = cam.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;


        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }
}
