using UnityEngine;
using UnityEngine.InputSystem;

public class GazeVisionMaskController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Material maskMaterial;

    [Header("Vision Settings")]
    [SerializeField] private float radius = 0.12f;
    [SerializeField] private float softness = 0.03f;
    [SerializeField] private float smoothSpeed = 12f;

    [Header("Testing")]
    [SerializeField] private bool useMouseForTesting = true;

    [Header("Coordinate Fix")]
    [SerializeField] private bool flipY = false;

    private Vector2 currentUV = new Vector2(0.5f, 0.5f);

    private void Start()
    {
        if (maskMaterial == null)
        {
            Debug.LogError("[VisionMask] No material assigned.");
            return;
        }

        maskMaterial.SetFloat("_Radius", radius);
        maskMaterial.SetFloat("_Softness", softness);
        maskMaterial.SetVector("_HoleCenter", new Vector4(currentUV.x, currentUV.y, 0f, 0f));
    }

    private void Update()
    {
        if (maskMaterial == null)
            return;

        Vector2 targetUV;
        bool gotValidGaze = TryGetGazeUV(out targetUV);

        if (!gotValidGaze)
        {
            if (useMouseForTesting)
            {
                if (Mouse.current == null)
                    return;

                Vector2 mousePos = Mouse.current.position.ReadValue();

                targetUV = new Vector2(
                    mousePos.x / Screen.width,
                    mousePos.y / Screen.height
                );
            }
            else
            {
                return;
            }
        }

        float t = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime);
        currentUV = Vector2.Lerp(currentUV, targetUV, t);

        maskMaterial.SetFloat("_Radius", radius);
        maskMaterial.SetFloat("_Softness", softness);
        maskMaterial.SetVector("_HoleCenter", new Vector4(currentUV.x, currentUV.y, 0f, 0f));
    }

    private bool TryGetGazeUV(out Vector2 uv)
    {
        uv = Vector2.zero;

        if (TobiiManager.Instance == null)
            return false;

        if (!TobiiManager.Instance.HasValidGazeData)
            return false;

        Vector2 gaze = TobiiManager.Instance.GazePointViewport;

        if (flipY)
            gaze.y = 1f - gaze.y;

        uv = gaze;
        Debug.Log("[VisionMask] Tobii gaze: " + gaze);
        return true;
    }
}