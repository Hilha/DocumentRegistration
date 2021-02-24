using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentRegistration.Models
{
    public class Category
    {
        public List<CategoryModel> categoryList = new List<CategoryModel>();
        public Categories()
        {
            categoryList.Add(new CategoryModel 
            {
                Id = 1,
                Name = "Development"
            });

            categoryList.Add(new CategoryModel
            {
                Id = 2,
                Name = "Test"
            });

            categoryList.Add(new CategoryModel
            {
                Id = 3,
                Name = "Support"
            });
        }


    }
}