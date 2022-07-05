using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MandelbrotExplorer : MonoBehaviour
{
    public Material m;
    public Vector2 p;
    public float s,a;

    private Vector2 pos;
    private float scale,angle;


    //Fixing Scaling issues
    private void UpdateShader()
    {
        pos = Vector2.Lerp(pos, p, 0.03f);
        scale = Mathf.Lerp(scale, s, 0.03f);
        angle = Mathf.Lerp(angle, a, 0.03f);

        float aspect = (float)Screen.width / (float)Screen.height;

        float scaleX = scale;
        float scaleY = scale;

        if (aspect > 1f)
            scaleY = scaleY / aspect;
        else
            scaleX = scaleX * aspect;

        m.SetVector("_Area", new Vector4(pos.x, pos.y, scaleX, scaleY));
        m.SetFloat("_Angle", angle);
    }

    private void UserControl()
    {
        //Zoom  In
        if (Input.GetKey(KeyCode.UpArrow))
            s *= 0.99f;
        //Zoom  Out
        if (Input.GetKey(KeyCode.DownArrow))
            s *= 1.01f;

        Vector2 dX = new Vector2(-0.003f * scale, 0);
        float cos = Mathf.Cos(a);
        float sin = Mathf.Sin(a);
        dX = new Vector2(dX.x * cos, dX.x * sin);


        Vector2 dY = new Vector2(-dX.y, dX.x);

        //Move Up
        if (Input.GetKey(KeyCode.W))
            p += dY;
        //Move Down
        if (Input.GetKey(KeyCode.S))
            p -= dY;
        //Move Left
        if (Input.GetKey(KeyCode.A))
            p -= dX;
        //Move Right
        if (Input.GetKey(KeyCode.D))
            p += dX;

        //Roate Left
        if (Input.GetKey(KeyCode.LeftArrow))
            a -= 0.01f;
        //Rotate Right
        if (Input.GetKey(KeyCode.RightArrow))
            a += 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShader();
        UserControl();
    }
}
