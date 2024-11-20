using UnityEngine;

public class AverageColorFromRenderTexture : MonoBehaviour
{
    public RenderTexture renderTexture; // Reference to your render texture
    public Light targetLight; // Reference to the light you want to change
    private int frames = 0;

    private Texture2D tempTexture;

    void Start()
    {
        // Create a temporary texture to capture the frame
        tempTexture = new Texture2D(renderTexture.width, renderTexture.height);
    }

    void Update()
    {
        frames++;
        if (frames == 2)
        {
            // Copy the render texture frame into the temporary texture
            RenderTexture.active = renderTexture;
            tempTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            tempTexture.Apply();
            RenderTexture.active = null;

            // Calculate the average color
            Color averageColor = CalculateAverageColor(tempTexture);

            // Set the light's color to the average color
            targetLight.color = averageColor;
            frames = 0;
        }
    }

    Color CalculateAverageColor(Texture2D texture)
    {
        Color[] pixels = texture.GetPixels();
        Color averageColor = Color.black;
        int skip = 3; // Number of pixels to skip before processing the next one

        for (int i = 0; i < pixels.Length; i += skip)
        {
            averageColor += pixels[i];
        }

        // Adjust the final sum based on the number of pixels processed
        averageColor /= (pixels.Length + skip - 1) / skip;

        return averageColor;
    }

}
