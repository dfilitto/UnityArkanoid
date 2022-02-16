using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
    [SerializeField]
    private GameObject[] blocks;
    private int tlBlocks;

    public static BlockController instance;
    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this);
            }
        }
    }
    void Start () {
        this.CreateBlock();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public int GetTLBlocks()
    {
        return tlBlocks;
    }

    public void DecTLBlocks()
    {
        tlBlocks--;
    }
    void CreateBlock()
    {
        float px = -96f;
        float py = 80f;
        tlBlocks = 0;
        for (int i = 0; i < 5; i++) //linha
        {
            px = -96f;
            for (int j = 0; j < 13; j++)
            {
                Vector3 pos = new Vector3(px, py, 0);
                //criar o block na tela
                Instantiate(blocks[i], pos, Quaternion.identity);
                px = px + 16;
                tlBlocks++;
            }
            py = py - 8;
        }
    }
}
