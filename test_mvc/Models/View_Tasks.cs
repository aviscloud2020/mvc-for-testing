using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace test_mvc.Models
{
    public class View_Tasks
    {
        public PagedList.IPagedList<L_Avis_Tasks> Page_Avis_Tasks { get; set; }

        [DisplayName("Вид задачи")]
        public string IS_Aviscloud_Task_Type { get; set; } //

        [DisplayName("Степень важности")]
        public string Priority { get; set; }

        [DisplayName("Срок")]
        public DateTime? DueDate { get; set; }

        [DisplayName("Имя задачи")]
        public string Title { get; set; }

        [DisplayName("Кем назначено")]
        public FieldUserValue Author { get; set; }

        [DisplayName("Кому назначено")]
        public int[] AssignedTo_IdList { get; set; }

        [DisplayName("Описание")]
        public string Body { get; set; }

        [DisplayName("Результат")]
        public string IS_Aviscloud_TaskOutcome { get; set; }

        [DisplayName("Комментарии задач")]
        public FieldLookupValue[] IS_Aviscloud_Comment { get; set; }

        [DisplayName("Создано")]
        public DateTime Created { get; set; }

        [DisplayName("% завершения")]
        public double PercentComplete { get; set; }

        [DisplayName("Вид задачи")]
        public bool Sort_Task_Type { get; set; }

        [DisplayName("Степень важности")]
        public bool Sort_Priority { get; set; }

        [DisplayName("Срок")]
        public bool Sort_DueDate { get; set; }

        [DisplayName("Имя задачи")]
        public bool Sort_Title { get; set; }

        [DisplayName("Кем назначено")]
        public bool Sort_Author { get; set; }

        [DisplayName("Кому назначено")]
        public bool Sort_AssignedTo { get; set; }

        [DisplayName("Описание")]
        public bool Sort_Body { get; set; }

        [DisplayName("Результат")]
        public bool Sort_IS_Aviscloud_TaskOutcome { get; set; }

        [DisplayName("Комментарии задач")]
        public bool Sort_IS_Aviscloud_Comment { get; set; }

        [DisplayName("Создано")]
        public bool Sort_Created { get; set; }

        [DisplayName("Сортировка")]
        public string Sort { get; set; }

    }

    public class IEnumerable_Contract
    {
        public IEnumerable<L_Avis_Tasks> tasks { get; set; }
    }

}
