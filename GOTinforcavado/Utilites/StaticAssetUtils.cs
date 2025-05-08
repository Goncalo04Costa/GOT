namespace GOTinforcavado.Utilites
{
    public static class StaticAssetUtils
    {
        // This method will return the path to your image assets
        public static string GetImagePath(string imageName)
        {
            // Define the base path for your images
            return $"/images/{imageName}"; // Adjust path as necessary
        }
    }
}
