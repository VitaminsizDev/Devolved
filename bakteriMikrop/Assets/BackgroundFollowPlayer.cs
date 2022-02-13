using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Player playerScript;
    private Material currentMaterial;
    private Vector4 layer1baseSpeed;
    private Vector4 layer2baseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentMaterial = GetComponent<Renderer>().material;
        layer1baseSpeed = currentMaterial.GetVector("_Layer1Speed");
        layer2baseSpeed = currentMaterial.GetVector("_Layer2Speed");
    }

    // Update is called once per frame
    void Update()
    {
        var dir = playerScript.StateMachine.CurrentState.xInput;
        if (dir == 0)
        {
            //Change material layer1Speed.x = 0;
            currentMaterial.SetVector("_Layer1Speed", new Vector4(layer1baseSpeed.x, layer1baseSpeed.y, layer1baseSpeed.z, layer1baseSpeed.w));
            currentMaterial.SetVector("_Layer2Speed", new Vector4(layer2baseSpeed.x, layer2baseSpeed.y, layer2baseSpeed.z, layer2baseSpeed.w));
        }
        else
        {
            if (dir > 0)
            {
                
                currentMaterial.SetVector("_Layer1Speed", new Vector4(0, layer1baseSpeed.y, layer1baseSpeed.z, layer1baseSpeed.w));
                currentMaterial.SetVector("_Layer2Speed", new Vector4(0, layer2baseSpeed.y, layer2baseSpeed.z, layer2baseSpeed.w));
            }
        }
    }
}
