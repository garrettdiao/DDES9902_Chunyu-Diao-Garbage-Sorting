using UnityEngine;

public class PollutionEffectController : MonoBehaviour
{
    [Header("Pollution Value")]
    public float pollutionValue = 0f;
    public float maxPollution = 100f;
    public float wrongIncreaseAmount = 20f;

    [Header("3D Pollution Bar")]
    public Transform pollutionBarFill;
    public float maxBarLength = 3f;

    [Header("Smoke Zones")]
    public ParticleSystem[] smokeZones;
    public float minSmokeRate = 5f;
    public float maxSmokeRate = 80f;

    [Header("Trash Stages")]
    public GameObject[] trashStages;

    [Header("Dirt Stages")]
    public GameObject[] dirtStages;
    public float minDirtAlpha = 0f;
    public float maxDirtAlpha = 1f;

    [Header("Final Failure Effects")]
    public GameObject[] finalFailTexts;
    public GameObject redFailureLight;

    private Vector3 originalBarScale;
    private Vector3 originalBarPosition;
    private bool finalTriggered = false;

    void Start()
    {
        pollutionValue = 0f;

        if (pollutionBarFill != null)
        {
            originalBarScale = pollutionBarFill.localScale;
            originalBarPosition = pollutionBarFill.localPosition;

            pollutionBarFill.localScale = new Vector3(
                0f,
                originalBarScale.y,
                originalBarScale.z
            );
        }

        HideSmokeZones();
        HideTrashStages();
        HideDirtStages();
        HideFinalEffects();
    }

    public void AddPollution()
    {
        if (finalTriggered) return;

        pollutionValue += wrongIncreaseAmount;
        pollutionValue = Mathf.Clamp(pollutionValue, 0f, maxPollution);

        float percent = pollutionValue / maxPollution;

        UpdatePollutionBar(percent);
        UpdateSmokeZones(percent);
        UpdateTrashStages(percent);
        UpdateDirtStages(percent);

        if (pollutionValue >= maxPollution)
        {
            TriggerFinalFailure();
        }
    }

    private void UpdatePollutionBar(float percent)
    {
        if (pollutionBarFill == null) return;

        float currentLength = maxBarLength * percent;

        pollutionBarFill.localScale = new Vector3(
            currentLength,
            originalBarScale.y,
            originalBarScale.z
        );

        pollutionBarFill.localPosition = new Vector3(
            originalBarPosition.x + currentLength / 2f,
            originalBarPosition.y,
            originalBarPosition.z
        );
    }

    private void UpdateSmokeZones(float percent)
    {
        if (smokeZones == null) return;

        float smokeRate = Mathf.Lerp(minSmokeRate, maxSmokeRate, percent);

        foreach (ParticleSystem smoke in smokeZones)
        {
            if (smoke == null) continue;

            smoke.gameObject.SetActive(true);

            var emission = smoke.emission;
            emission.rateOverTime = smokeRate;
        }
    }

    private void UpdateTrashStages(float percent)
    {
        if (trashStages == null) return;

        for (int i = 0; i < trashStages.Length; i++)
        {
            if (trashStages[i] == null) continue;

            float threshold = (i + 1f) / trashStages.Length;

            if (percent >= threshold)
            {
                trashStages[i].SetActive(true);
            }
        }
    }

    private void UpdateDirtStages(float percent)
    {
        if (dirtStages == null) return;

        float alpha = Mathf.Lerp(minDirtAlpha, maxDirtAlpha, percent);

        foreach (GameObject dirt in dirtStages)
        {
            if (dirt == null) continue;

            dirt.SetActive(true);

            Renderer[] renderers = dirt.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                foreach (Material mat in renderer.materials)
                {
                    Color color = mat.color;
                    color.a = alpha;
                    mat.color = color;
                }
            }
        }
    }

    private void TriggerFinalFailure()
    {
        finalTriggered = true;

        if (finalFailTexts != null)
        {
            foreach (GameObject text in finalFailTexts)
            {
                if (text != null)
                {
                    text.SetActive(true);
                }
            }
        }

        if (redFailureLight != null)
        {
            redFailureLight.SetActive(true);
        }

        Debug.Log("Final failure triggered.");
    }

    private void HideSmokeZones()
    {
        if (smokeZones == null) return;

        foreach (ParticleSystem smoke in smokeZones)
        {
            if (smoke == null) continue;

            var emission = smoke.emission;
            emission.rateOverTime = 0f;

            smoke.gameObject.SetActive(false);
        }
    }

    private void HideTrashStages()
    {
        if (trashStages == null) return;

        foreach (GameObject trash in trashStages)
        {
            if (trash != null)
            {
                trash.SetActive(false);
            }
        }
    }

    private void HideDirtStages()
    {
        if (dirtStages == null) return;

        foreach (GameObject dirt in dirtStages)
        {
            if (dirt == null) continue;

            Renderer[] renderers = dirt.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                foreach (Material mat in renderer.materials)
                {
                    Color color = mat.color;
                    color.a = minDirtAlpha;
                    mat.color = color;
                }
            }

            dirt.SetActive(false);
        }
    }

    private void HideFinalEffects()
    {
        if (finalFailTexts != null)
        {
            foreach (GameObject text in finalFailTexts)
            {
                if (text != null)
                {
                    text.SetActive(false);
                }
            }
        }

        if (redFailureLight != null)
        {
            redFailureLight.SetActive(false);
        }
    }
}
