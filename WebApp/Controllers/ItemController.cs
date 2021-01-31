using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ItemController : Controller
    {
        // List
        public ActionResult List()
        {
            ItemTestEntities db = new ItemTestEntities();
            IList<ItemViewModel> itemList = new List<ItemViewModel>();

            foreach (var item in db.Items)
            {
                ItemViewModel vm = new ItemViewModel
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price
                };
                itemList.Add(vm);

            }


            return View(itemList);
        }
        public ActionResult CreateForm()
        {
            return View();
        }

        // Post
        public ActionResult Create(ItemViewModel model)
        {
            try
            {

                ItemTestEntities db = new ItemTestEntities();

                Item item = new Item
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price
                };

                db.Items.Add(item);
                db.SaveChanges();

                int latestItemId = item.ItemId;



            }
            catch (Exception e)
            {
                throw e;
            }


            return RedirectToAction(nameof(List));
        }

        // Delete
        public ActionResult Delete(int id)
        {
            ItemTestEntities db = new ItemTestEntities();

            var itemToRemove = db.Items.Find(id);

            if (itemToRemove != null)
            {
                db.Items.Remove(itemToRemove);
                db.SaveChanges();
            }
            else
            {
                throw new ArgumentException("null");
            }

            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ItemTestEntities db = new ItemTestEntities();
            var itemToEdit = db.Items.Find(id);

            return View(itemToEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item model)
        {
            ItemTestEntities db = new ItemTestEntities();

            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw new ArgumentException("Error");
            }


            return RedirectToAction(nameof(List));
        }
    }
}