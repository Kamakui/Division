using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : Singleton<PlayerInfoUI>
{
    public RectTransform hpBar;
    public RectTransform shiledBar;
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI haveAmmo;

    public Image skillSlot1;
    public Image skillSlot2;
    public Image reloadImage;
}
