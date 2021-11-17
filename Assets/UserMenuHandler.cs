﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserMenuHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject userView;
    private Vector2 userViewDefaultSize;
    [SerializeField]
    private TMP_InputField usernameInput;
    [SerializeField]
    private GameObject userBlockPref;

    private UserDatabase userDatabase;
    private PopupManager popupManager;

    private void Start()
    {
        userViewDefaultSize = GetRect(userView).sizeDelta;
        popupManager =
            GameObject.Find("GlobalManagers")
            .GetComponent<PopupManager>();
        userDatabase = 
            GameObject.Find("Database")
            .GetComponent<UserDatabase>();

        ShowUsers();
    }
    private RectTransform GetRect(GameObject go)
    {
        return go.transform as RectTransform;
    }
    private void ShowUsers()
    {
        GetRect(userView).sizeDelta = userViewDefaultSize;
        foreach (Transform child in userView.transform)
        {
            if (!child.CompareTag("DD"))
                Destroy(child.gameObject);
        }
        foreach (User user in userDatabase.GetUsers())
        {
            GameObject block = Instantiate(userBlockPref, userView.transform);
            block.name = "UserBlock";

            ExpandBoard(GetRect(block).sizeDelta);

            UserBlock ub = block.GetComponent<UserBlock>();
            ub.user = user;
            ub.deleteButton.onClick.AddListener(() => DeleteUser(ub.user));
            ub.selectButton.onClick.AddListener(() => SelectUser(ub.user));
            block.SetActive(true);
        }
    }
    public void CreateUser()
    {
        if (usernameInput.text == "")
        {
            popupManager.PopWarning("You can't create account without a name");
            return;
        }
        if (IsDuplicate(usernameInput.text))
        {
            popupManager.PopWarning("Account with this name is already created");
            return;
        }
        else
        {
            userDatabase.AddUser(new User(
                usernameInput.text));

            ShowUsers();
        }
    }
    public void DeleteUser(User user)
    {
        userDatabase.DeleteUser(user);
        popupManager.PopSuccess("Account with name " + user.name + " was successfully deleted");

        ShowUsers();
    }
    public void SelectUser(User user)
    {
        //Switch current user
        popupManager.PopSuccess("Hello " + user.name + "!");

        ShowUsers();
    }

    private void ExpandBoard(Vector2 sizeToExpand)
    {
        GetRect(userView).sizeDelta += new Vector2(0, sizeToExpand.y);
    }
    private bool IsDuplicate(string name)
    {
        foreach (User user in userDatabase.GetUsers())
        {
            if (user.name == name)
                return true;
        }
        return false;
    }

}
