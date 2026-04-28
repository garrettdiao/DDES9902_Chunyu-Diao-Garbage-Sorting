using UnityEngine;

public class BinTrigger : MonoBehaviour
{
    public int binID;
    public WorldSignUI worldSignUI;

    public PollutionEffectController pollutionEffectController;

    private void OnTriggerEnter(Collider other)
    {
        Trash3D trash = other.GetComponent<Trash3D>();
        if (trash == null) return;

        if (trash.isHeld) return;

        if (trash.correctBinID == binID)
        {
            // 保持原效果：扔对后垃圾消失
            other.gameObject.SetActive(false);
        }
        else
        {
            // 保持原效果：扔错后垃圾回到原位
            trash.ResetPosition();

            // 保持原效果：显示原来的错误提示文字
            if (worldSignUI != null)
            {
                worldSignUI.ShowMessage(trash.wrongBinMessage);
            }

            // 新增效果：扔错后增加污染
            if (pollutionEffectController != null)
            {
                pollutionEffectController.AddPollution();
            }
        }
    }
}
