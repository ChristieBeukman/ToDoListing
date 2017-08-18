using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.ToDo.Model;

namespace Framework.ToDo.Services
{
    public class DataAccessCategoryItem : IDataAccesCategory
    {
        ToDoDbEntities cxt = new ToDoDbEntities();

        public DataAccessCategoryItem()
        {
            cxt = new ToDoDbEntities();
        }

        public void CreateCategories(CategoryItem Cat)
        {
            cxt.CategoryItems.Add(Cat);
            cxt.SaveChanges();
        }

        public void DeleteCategories(CategoryItem cat)
        {
            cxt.CategoryItems.Remove(cat);
            cxt.SaveChanges();
        }

        public ObservableCollection<CategoryItem> GetCategories()
        {
            ObservableCollection<CategoryItem> CategoryItems = new ObservableCollection<CategoryItem>();

            var Query = from a in cxt.CategoryItems
                        select a;
            foreach (var item in Query)
            {
                CategoryItems.Add(item);
            }
            return CategoryItems;
        }

        public void UpdateCategories(CategoryItem NewCat, CategoryItem OldCat)
        {
            if (NewCat != null)
            {
                var v = from c in cxt.CategoryItems
                        where c.CategoryItemId == NewCat.CategoryItemId
                        select c;
                cxt.Entry(NewCat).State = System.Data.Entity.EntityState.Modified;
                cxt.SaveChanges();

            }
        }

    }
}

