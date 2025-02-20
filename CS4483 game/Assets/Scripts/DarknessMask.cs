using UnityEngine;
using UnityEngine.UI;

public class DarknessMask : MonoBehaviour {
    public Transform player; // 玩家对象
    public RectTransform lightMask; // 透明光圈的 RectTransform
    public Canvas canvas;

    void Update() {
        if (player != null && lightMask != null) {
            // 将玩家的世界坐标转换为屏幕坐标
            Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position);
            
            // 设置光圈位置，让光圈跟随玩家
            lightMask.position = screenPos;
        }
    }
}
