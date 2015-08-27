using UnityEngine;
using System.Collections;
using System;

public class SpawnPoint : MonoBehaviour {
    public static Color COLOR_DEFAULT = new Color32(0x47, 0x77, 0xA5, 0xFF);
    public static Color COLOR_IN_USE = new Color32(0xff, 0x14, 0x14, 0xFF);
    public static Color COLOR_ACTIVE = new Color32(0x47, 0x77, 0x4A, 0xFF);

    public Light[] lights;

    private Color currentColor = COLOR_DEFAULT;

    public bool isOpen = false;
    public bool isSpawn = false;

    public Vector3 getLocation()
    {
        return transform.position + new Vector3(0, 0.003f, 0);
    }

    public Vector3 getCoords()
    {
        return transform.position;
    }

    private void setColor(Color color)
    {
        foreach (Light l in lights)
        {
            currentColor = color;
            l.color = color;
        }
    }

    public void updateColor()
    {
        if (isOpen) {
            setColor(COLOR_IN_USE);
        }
        else if (isSpawn)
        {
            setColor(COLOR_ACTIVE);
        }
        else
        {
            setColor(COLOR_DEFAULT);
        }
    }

    public void setIsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
        updateColor();
    }

    public void setIsSpawn(bool isSpawn)
    {
        this.isSpawn = isSpawn;
        updateColor();
    }

    public Color getColor()
    {
        return currentColor;
    }
}
