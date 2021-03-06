﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicLayout : MonoBehaviour {

    [SerializeField]
    private int col, row;

	// Use this for initialization
	void Start () {
        RectTransform parent = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(parent.rect.width / col, parent.rect.height / row);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
