using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroContext : Context
{
    public HeroBase info;

    public HeroBase Gets()
    {
        if(info ==null)
            info = gameObject.GetComponent<HeroBase>();

        return info;
    }
}
