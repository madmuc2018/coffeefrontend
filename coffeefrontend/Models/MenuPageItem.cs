using System;
namespace coffeefrontend
{
    public class MenuPageItem
    {
        public string Title { get; set; }
        public Type TargetType { get; set; }

        public MenuPageItem(string title, Type targetType)
        {
            this.Title = title;
            this.TargetType = targetType;
        }
    }
}
