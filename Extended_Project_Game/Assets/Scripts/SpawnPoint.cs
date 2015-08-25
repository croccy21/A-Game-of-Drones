using UnityEngine;
using System.Collections;
using System;

public class SpawnPoint : MonoBehaviour {

    public static Color COLOR_DEFAULT = new Color((float)0x47 / 0xff, (float)0x77 / 0xff, (float)0xA5 / 0xff);
    public static Color COLOR_IN_USE = new Color((float)0xff / 0xff, (float)0x14 / 0xff, (float)0x14 / 0xff);
    public static Color COLOR_ACTIVE = new Color();

    public Light[] lights;

    private Color currentColor = COLOR_DEFAULT;

    private bool isOpen = false;


    public Vector3 getLocation()
    {
        return transform.position + new Vector3(0, 0.003f, 0);
    }

    public Vector3 getCoords()
    {
        return transform.position;
    }

    public void setColor(Color color)
    {
        foreach (Light l in lights)
        {
            currentColor = color;
            l.color = color;
        }
    }

    public void setIsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
    }

    public bool getIsOpen()
    {
        return isOpen;
    }

    public Color getColor()
    {
        return currentColor;
    }
}
