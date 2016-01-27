﻿namespace _02.Todos.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.UI.WebControls;

    using Data;
    using Models;

    public partial class Categories : System.Web.UI.Page
    {
        private TodosDbContext dbContext;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.dbContext = new TodosDbContext();
        }

        public List<Category> CategoriesListView_GetData()
        {
            return this.dbContext.Categories.ToList();
        }

        public void CategoriesListView_UpdateItem(int id)
        {
            var category = this.dbContext.Categories.FirstOrDefault(t => t.Id == id);
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            if (category == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }

            TryUpdateModel(category);

            if (ModelState.IsValid)
            {
                this.dbContext.SaveChanges();
            }
        }

        public void CategoriesListView_DeleteItem(int id)
        {
            var category = this.dbContext.Categories.FirstOrDefault(t => t.Id == id);
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            if (category == null)
            {
                // The item wasn't found
                ModelState.AddModelError("", String.Format("Item with id {0} was not found", id));
                return;
            }
            if (ModelState.IsValid)
            {
                this.dbContext.Categories.Remove(category);
                this.dbContext.SaveChanges();
            }
        }

        public void CategoriesListView_InsertItem(Category category)
        {
            var item = category;

            //item.Category = this.dbContext.Categories.FirstOrDefault();
            this.dbContext.Categories.Add(item);
            if (ModelState.IsValid)
            {
                this.dbContext.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("Error", "Invalid Todo");
            }
        }
    }
}