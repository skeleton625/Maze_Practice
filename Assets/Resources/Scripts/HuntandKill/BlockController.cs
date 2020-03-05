using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Blocks;

    public void SetBlockEnable(bool[] _block)
    {
        for(int i = 0; i < 4; i++)
            Blocks[i].SetActive(_block[i]);
    }
}
