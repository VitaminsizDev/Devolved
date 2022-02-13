using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Player playerScript;
    private Material currentMaterial;
    private Vector2 layer1baseSpeed;
    private Vector2 layer2baseSpeed;
    //private static readonly int Layer1Speed = Shader.PropertyToID("Layer1Speed");

    // Start is called before the first frame update
    void Start()
    {
        /*currentMaterial = GetComponent<Renderer>().material;
        layer1baseSpeed = currentMaterial.GetVector(Layer1Speed);
        layer2baseSpeed = currentMaterial.GetVector("Layer2Speed");*/
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.position;
        /*var dir = playerScript.StateMachine.CurrentState.xInput;
        if (dir == 0)
        {
            //Change material layer1Speed.x = 0;
            currentMaterial.SetVector("Layer1Speed", new Vector4(layer1baseSpeed.x, layer1baseSpeed.y, layer1baseSpeed.z, layer1baseSpeed.w));
            currentMaterial.SetVector("Layer2Speed", new Vector4(layer2baseSpeed.x, layer2baseSpeed.y, layer2baseSpeed.z, layer2baseSpeed.w));
        }
        else
        {
            if (dir > 0)
            {
                
                currentMaterial.SetVector("Layer1Speed", new Vector4(layer1baseSpeed.x*-dir, layer1baseSpeed.y, layer1baseSpeed.z, layer1baseSpeed.w));
                currentMaterial.SetVector("Layer2Speed", new Vector4(layer2baseSpeed.x*-dir, layer2baseSpeed.y, layer2baseSpeed.z, layer2baseSpeed.w));
            }
        }*/
    }
}
