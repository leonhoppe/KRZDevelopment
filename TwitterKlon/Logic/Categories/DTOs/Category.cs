#nullable disable

using System;

namespace TwitterKlon.Logic.Categories.DTOs
{
    public partial class Category : CategoryUpdate
    {
        public string Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not Category) return false;
            Category other = (Category)obj;
            if (other.Id != Id) return false;
            if (other.Name != Name) return false;
            if (other.Tags != Tags) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(Id, Name, Tags);
    }

    public class CategoryUpdate
    {
        public string Name { get; set; }
        public string Tags { get; set; }

        public void EditCategory(Category category)
        {
            category.Name = Name;
            category.Tags = Tags;
        }

        public override bool Equals(object obj)
        {
            if (obj is not CategoryUpdate) return false;
            CategoryUpdate other = (CategoryUpdate)obj;
            if (other.Name != Name) return false;
            if (other.Tags != Tags) return false;
            return true;
        }

        public override int GetHashCode() => HashCode.Combine(Name, Tags);
    }
}
