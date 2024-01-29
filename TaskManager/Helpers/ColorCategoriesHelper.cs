using System.Drawing;
using TaskManager.Dto.Categories.Response;

namespace TaskManager.Helpers
{
    public static class ColorCategoriesHelper
    {
        /// <summary>
        /// Method for generate color for category
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static List<CategoryDto> GenerateColorForCategory(List<CategoryDto> categories)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                Random random = new Random();
                Color color = Color.FromArgb(random.Next(200, 255), random.Next(200, 255), random.Next(200, 255));
                string redHex = color.R.ToString("X2");
                string greenHex = color.G.ToString("X2");
                string blueHex = color.B.ToString("X2");
                string colorString = $"#{redHex}{greenHex}{blueHex}";

                categories[i].Color = colorString;
            }

            return categories;
        }
    }
}
