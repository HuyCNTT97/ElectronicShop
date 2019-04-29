using _Models.DAO;
using _Models.EF;
using ShopOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace ShopOnline.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        // GET: Admin/Contact
        public ActionResult Index(int page = 1, int pagesize = 2)
        {
            ContactDAO contactDAO = new ContactDAO();
            var list = contactDAO.ListAll().ToPagedList(page,pagesize);
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult Create(Contact contact,string tempstatus)
        {
            if (ModelState.IsValid)
            {
                if (tempstatus == "Kích Hoạt")
                {
                    contact.Status = true;
                }
                else contact.Status = false;
                ContactDAO contactDAO = new ContactDAO();
                int check = contactDAO.Create(contact);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Create Success");
                    RedirectToAction("Create", "Contact");
                }
                else
                {
                    ModelState.AddModelError("", "Create fail");
                }
            }
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ContactDAO contactDAO = new ContactDAO();
            var contacting = contactDAO.GetContact(id);
            return View(contacting);
        }
        [HttpPost]
        public JsonResult ChangeStatus(int contactid)
        {
            ContactDAO contactDAO = new ContactDAO();
            var tempstatus = contactDAO.ChangeStatus(contactid);

            return Json(new
            {
                status = tempstatus
            });
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult Edit(Contact contact,string tempstatus,int id)
        {
            contact.ContactID = id;
            if (ModelState.IsValid)
            {
                ContactDAO contactDAO = new ContactDAO();
                if (tempstatus == "Kích Hoạt")
                {
                    contact.Status = true;
                }
                else contact.Status = false;
                var check = contactDAO.Edit(contact);
                if (check > 0)
                {
                    TempData["msg"] = MessageBox.Show("Edit Thành Công");
                    return RedirectToAction("Index", "Contact");
                }
                else
                {
                    ModelState.AddModelError("", "Edit không thành công");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Remove(int ContactID)
        {
            ContactDAO contactDAO = new ContactDAO();
            contactDAO.Remove(ContactID);
            TempData["msg"] = MessageBox.Show("Xóa Thành Công");
            return RedirectToAction("Index", "Contact");
        }
    }
}