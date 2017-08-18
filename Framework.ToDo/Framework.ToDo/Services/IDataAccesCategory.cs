using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.ToDo.Model;

namespace Framework.ToDo.Services
{
    public interface IDataAccesCategory
    {
        ObservableCollection<CategoryItem> GetCategories();
        void CreateCategories(CategoryItem Cat);
        void DeleteCategories(CategoryItem cat);
        void UpdateCategories(CategoryItem NewCat, CategoryItem OldCat);
    }
}
