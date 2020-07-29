using AG_AAC103.Models;
using System.Web.Mvc;
using Dapper;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System;

namespace AG_AAC103.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View(DapperORM.ReturnList<EmpClass>("GetEmployees",null));
        }
        [HttpGet]
        //../Employee/AddorEdit
        //..//Employee/AddorEdit/id
        public ActionResult Edit(int id = 0 )
        {
            if (id == 0 )
            return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", id);
                return View(DapperORM.ReturnList<EmpClass>("EmployeeViewByID", param).FirstOrDefault<EmpClass>());
            }
        }
      
        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("Id", id);
                return View(DapperORM.ReturnList<EmpClass>("EmployeeViewByID", param).FirstOrDefault<EmpClass>());
            }
        }
        [HttpPost]
        public ActionResult Edit(EmpClass empClass)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                string createdby = HttpContext.User.Identity.Name;
                string updatedby = HttpContext.User.Identity.Name;
                param.Add("@Id", empClass.Id);
                param.Add("@EmpName", empClass.Empname);
                param.Add("@Empemail", empClass.Empemail);
                param.Add("@Emplocation", empClass.Emplocation);
                param.Add("@Empdesignation", empClass.Empdesignation);
                param.Add("@UserName", empClass.UserName);
                param.Add("@Password", empClass.Password);
                param.Add("@IsActive", empClass.IsActive);
                param.Add("@Role", empClass.Role);
                param.Add("@squad", empClass.Squad);
                param.Add("@CreatedDate", empClass.CreatedDate);
                param.Add("@CreatedBy", createdby);
                param.Add("@UpdatedDate", empClass.UpdatedDate);
                param.Add("@Updatedby", updatedby);
                DapperORM.ExecuteWithoutReturn("AddUpdateEmp", param);
                return RedirectToAction("Index");

            }

            catch(Exception)
                {
                ViewBag.message = "Invalid Credentials";
                return View();
                }


        }
    

        public ActionResult Delete(EmpClass empClass)
        {
            string updatedby = HttpContext.User.Identity.Name;
            
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id",empClass.Id);
            param.Add("@Updatedby", updatedby);
            param.Add("@UpdatedDate", empClass.UpdatedDate);
            DapperORM.ExecuteWithoutReturn("DeleteEmpById", param);
            return RedirectToAction("Index");

        }
        


    }
}