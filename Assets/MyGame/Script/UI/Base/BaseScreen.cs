﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : BaseUIElement
{
    public override void Init()
    {
        base.Init();
        this.uiType = UIType.SCREEN;//chọn loại
    }
    public override void Show(object data)
    {
        base.Show(data);
    }
    public override void Hide()
    {
        base.Hide();
    }
    public override void OnClickedBackButton()
    {
        base.OnClickedBackButton();
    }
}
