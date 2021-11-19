using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string name;
    public Setting setting;
    //public List<Saves> saves;
    //public List<Highscores> highscores;

    public User(string name, Setting setting)
    {
        this.name = name;
        this.setting = setting;
    }
}
