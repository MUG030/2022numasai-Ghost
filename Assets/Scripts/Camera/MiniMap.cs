using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    [SerializeField] Transform _grid;
    [SerializeField] Image _image;

    // �}�b�v�̐F
    [SerializeField] Color _wallColor;
    [SerializeField] Color _groundColor;
    [SerializeField] Color _noneColor;

    // �}�b�v�p�e�N�X�`��
    Texture2D _texture2d;

    // Start is called before the first frame update
    void Start()
    {
        Tilemap groundTilemap = _grid.Find("BlockTile").GetComponent<Tilemap>();
        Tilemap wallTilemap = _grid.Find("WaterTile").GetComponent<Tilemap>();

        // �e�N�X�`���쐬
        Vector3Int size = groundTilemap.size;
        _texture2d = new Texture2D(size.x, size.y, TextureFormat.ARGB32, false);

        // �������Ȃ��ƁA�摜���ڂ₯��
        _texture2d.filterMode = FilterMode.Point;

        Vector3Int origin = groundTilemap.origin;

        // �e�N�X�`�����W���Ƃ̐F�����߂�
        for (int y = 0; y < size.y; ++y)
        {
            for (int x = 0; x < size.x; ++x)
            {
                // Tilemap�̃O���b�h���W
                Vector3Int cellPos = new Vector3Int(origin.x + x, origin.y + y, 0);

                // �ǃ^�C�������݂���
                if (wallTilemap.GetTile(cellPos) != null)
                {
                    _texture2d.SetPixel(x, y, _wallColor);
                }
                // �n�ʃ^�C�������݂���
                else if (groundTilemap.GetTile(cellPos) != null)
                {
                    _texture2d.SetPixel(x, y, _groundColor);
                }
                // �Ȃɂ��Ȃ��ꏊ
                else
                {
                    _texture2d.SetPixel(x, y, _noneColor);
                }
            }
        }

        // �e�N�X�`���m��
        _texture2d.Apply();

        // �e�N�X�`����Image�ɓK�p
        _image.rectTransform.sizeDelta = new Vector2(size.x, size.y);
        Debug.Log("test");
        _image.sprite = Sprite.Create(_texture2d, new Rect(0, 0, size.x, size.y), Vector2.zero);

        // _image��Grid�̒��S�Ɉړ�
        Vector2 leftDownWorldPos = groundTilemap.CellToWorld(origin);
        Vector2 rightUpWorldPos = groundTilemap.CellToWorld(origin + size);
        _image.transform.position = (leftDownWorldPos + rightUpWorldPos) * 0.5f;
    }

    private void OnDestroy()
    {
        Destroy(_texture2d);
    }
}
