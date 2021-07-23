using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test_mvc.Models;

namespace test_mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(View_Tasks view_Tasks, int? page, bool excelFork = false, int pageSize = 25, int taskId = 0, Boolean assignedbyme = true, Boolean completed = false)
        {
            int defaSize = pageSize;
            int pageNumber = (page ?? 1);

            ViewBag.psize = defaSize;
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
                new SelectListItem() { Value="100", Text= "100" },
                new SelectListItem() { Value="", Text= "Все" },
            };

            ViewBag.Priority = new List<SelectListItem>()
            {
                new SelectListItem() { Value="(1) Высокая", Text= "Высокая" },
                new SelectListItem() { Value="(2) Обычная", Text= "Обычная" },
                new SelectListItem() { Value="(3) Низкая", Text= "Низкая" }
            };

            ViewBag.Task_Type = new List<SelectListItem>()
            {
                new SelectListItem() { Value="Согласование", Text= "Согласование" },
                new SelectListItem() { Value="Контроль", Text= "Контроль" },
                new SelectListItem() { Value="Ознакомление", Text= "Ознакомление" },
                new SelectListItem() { Value="Утверждение", Text= "Утверждение" },
                new SelectListItem() { Value="Рассмотрение", Text= "Рассмотрение" },
                new SelectListItem() { Value="Обычная", Text= "Обычная" },
                new SelectListItem() { Value="Для документа", Text= "Для документа" },
                new SelectListItem() { Value="Исполнение", Text= "Исполнение" },
                new SelectListItem() { Value="Подписание", Text= "Подписание" },
                new SelectListItem() { Value="Совещание", Text= "Совещание" },
                new SelectListItem() { Value="Поручение", Text= "Поручение" },
                new SelectListItem() { Value="Посмотреть", Text= "Посмотреть" }
            };

                List<L_Avis_Tasks> lic_Docs = new List<L_Avis_Tasks>();
            Random rnd = new Random();
           
            for (int i = 0; i < 70; i++)
            {
                lic_Docs.Add(
                new L_Avis_Tasks()
                {
                    DueDate = DateTime.Now.AddDays(rnd.Next(-10,10)),
                    IS_Aviscloud_Task_Type = type(rnd.Next(0, 5)),
                    Title = $"Задача №{i}",
                    Priority = priority(rnd.Next(0,2)),
                    IS_Aviscloud_TaskOutcome = "Незавершена",
                    AssignedTo = name(rnd.Next(0,5)),
                    comCount = rnd.Next(0, 5),
                    PercentComplete = rnd.Next(0, 100)


                });
            }

            for (int i = 0; i < 70; i++)
            {
                lic_Docs.Add(
                new L_Avis_Tasks()
                {
                    DueDate = null,
                    IS_Aviscloud_Task_Type = type(rnd.Next(0, 5)),
                    Title = $"Бессрочная задача №{i}",
                    Priority = priority(rnd.Next(0, 2)),
                    IS_Aviscloud_TaskOutcome = "Незавершена",
                    AssignedTo = name(rnd.Next(0, 5)),
                    comCount = rnd.Next(0, 5)
                });
            }
            string type(int r)
            {
                string res = "";
                switch (r)
                {
                    case 1: return "Контроль";
                    case 2: return "Согласование";
                    case 3: return "Ознакомление";
                    case 4: return "Утверждение";
                    case 5: return "Рассмотрение";
                    default: return "Исполнение";
                }
            }

            string priority(int r)
            {
                string res = "";
                switch (r)
                {
                    case 1: return "Важность: обычная";
                    case 2: return "Важность: высокая";
                    default: return "Важность: низкая";
                }
            }
            string name(int r)
             {
                string res = "";
                switch (r)
                {
                    case 1: return "Петров П.П.";
                    case 2: return "Сидоров С.С.";
                    case 3: return "Стариков С.С.";
                    case 4: return "Больших Б.Б.";
                    case 5: return "Малых М.М.";
                    default: return "Иванов И.И.";
                }
             }

            IEnumerable<L_Avis_Tasks> Tasks_Null_DD = lic_Docs.Where(x => x.DueDate == null);
            ViewBag.Tasks_Null_DD = Tasks_Null_DD;
            IEnumerable<L_Avis_Tasks> table_ = lic_Docs.Where(x => x.DueDate != null);
            ViewBag.AssignedTo = lic_Docs.Select(x => new SelectListItem { Value = x.AssignedTo_Id.ToString() as string, Text = x.AssignedTo_IdList.ToString() as string }).Where(x => x.Text != null).OrderBy(x => x.Text);
            ViewBag.Tasks_Null_Count = Tasks_Null_DD.Count();
            view_Tasks.Page_Avis_Tasks = (IPagedList<L_Avis_Tasks>)(lic_Docs.Where(x => x.DueDate != null)).ToPagedList(pageNumber, defaSize);
            return View(view_Tasks);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}