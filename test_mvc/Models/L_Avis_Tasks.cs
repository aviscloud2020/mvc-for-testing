using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test_mvc.Models
{
    public class L_Avis_Tasks
    {
        public static bool isRefreshing { get; set; }
        public string UniqueId { get; set; }
        public int Id { get; set; }

        [DisplayName("Название"), Required(ErrorMessage = RegExps.Required_ErrorMsg)]
        public string Title { get; set; }

        [DisplayName("Автор")]
        public FieldUserValue Author { get; set; }

        [DisplayName("Кем изменено")]
        public FieldUserValue Editor { get; set; }

        [DisplayName("Дата создания")]
        public DateTime Created { get; set; }

        [DisplayName("Когда измененено")]
        public DateTime Modified { get; set; }

        [DisplayName("Место хранения оригинала")]
        public string IS_Aviscloud_Storage { get; set; }
        public static List<L_Avis_Tasks> cachedList { get; set; } = new List<L_Avis_Tasks>();
        public List<string> ArrayAssignedTo { get; set; }
        public string ParentIDId { get; set; }
        public List<string> ListOfAssignedTo { get; set; }
        public string AuthorName { get; set; }
        public int comCount { get; set; }

        public int? ParentID { get; set; }

        public L_Avis_Tasks ParentTask { get; set; }

        [Required(ErrorMessage = RegExps.Required_ErrorMsg)]
        [DisplayName("Название")]

        public bool Checkmark { get; set; }

        [DisplayName("Состояние")]
        public string Status { get; set; }

        [DisplayName("Важность")]
        public string Priority { get; set; }

        [DisplayName("Дата начала")]
        //[DataType(DataType.Date)]
        [RegularExpression(RegExps.DateTime, ErrorMessage = RegExps.DateTime_ErrorMsg)]
        public DateTime? StartDate { get; set; }

        [DisplayName("Срок")]
        [RegularExpression(RegExps.DateTime, ErrorMessage = RegExps.DateTime_ErrorMsg)]
        public DateTime? DueDate { get; set; }

        [DisplayName("% завершения")]
        public double PercentComplete { get; set; }

        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }


        [DisplayName("Кому назначено")]
        [Required(ErrorMessage = RegExps.Required_ErrorMsg)]
        [LookupIdCheck(ErrorMessage = RegExps.Required_ErrorMsg)]
        public string AssignedTo { get; set; }

        [DisplayName("Кому назначено")]
        [Required(ErrorMessage = RegExps.Required_ErrorMsg)]
        public int[] AssignedTo_IdList { get; set; }

        [DisplayName("Кому назначено")]
        [Required(ErrorMessage = RegExps.Required_ErrorMsg)]
        public int AssignedTo_Id { get; set; }

       
        [DisplayName("Вид задачи")]
        public string IS_Aviscloud_Task_Type { get; set; } //
        public FieldUrlValue IS_Aviscloud_DocumentURL { get; set; }
        public string IS_Aviscloud_GUID_Doc { get; set; }

        public string IS_Aviscloud_GUID { get; set; } //new 11.05.2018

        //public string IS_Aviscloud_ListName { get; set; } //TODO: поле с названием списка
        public FieldLookupValue[] IS_Aviscloud_Incoming_documents { get; set; }
        public FieldLookupValue IS_Aviscloud_Contract { get; set; }

        /// <summary>Завершенный этап согласования</summary>
        public bool IS_Aviscloud_Completed_phase_matching { get; set; }
        public FieldLookupValue[] IS_Aviscloud_Outgoing_documents { get; set; }

        [DisplayName("Комментарии")]
        [DataType(DataType.MultilineText)]
        public FieldLookupValue[] IS_Aviscloud_Comment { get; set; }

        [DisplayName("Комментарии")]
        public ListItemCollection Comments { get; set; }

        public string IS_Aviscloud_text { get; set; }
        public bool IS_Aviscloud_notify_Executive { get; set; }

        /// <summary>Согласован/Утвержден/Отклонен</summary>
        [DisplayName("Результат выполнения задачи")]
        public string IS_Aviscloud_TaskOutcome { get; set; }

        public List<L_Avis_Tasks> subtasks { get; set; }

        /// <summary> Url формы просмотра документа </summary>
        public string docViewUrl { get; set; }

        /// <summary>Приказ/Договор/Входящий документ/Исходящий документ/Письмо/Служебная записка/Командировка</summary>
        public string DocType { get; set; }

        public DateTime? IS_Aviscloud_Task_Completed_Date { get; set; }

        [DisplayName("Дата принятия задачи к исполнению")]
        public DateTime? IS_Aviscloud_Task_ForExecution_Date { get; set; } //new 28.05.18

        /// <summary>Задача недоступна</summary>
        public bool? IS_Aviscloud_Disabled { get; set; } //new 06.06.18

        /// <summary>Library name</summary>
        public string IS_Aviscloud_Document_base { get; set; }

        /// <summary>Вид документа</summary>
        [DisplayName("Вид документа")]
        public string IS_Aviscloud_TypeRelatedDoc { get; set; }

        [DisplayName("Модуль")] public FieldLookupValue IS_Av_Avis_Menu { get; set; }
        [DisplayName("Модуль")] public int? IS_Av_Avis_Menu_LookupId { get; set; }

    }
    public class RegExps
    {
        public const string FolderName = @"^[0-9a-zA-Zа-яёА-ЯЁ\[\]\{\}:;',!@#№$%\^\&()~=\+_\-. ]+$";
        public const string DateTime = "^(([0][1-9]|[1|2][0-9]|[3][0|1])[./-]([0][1-9]|[1][0-2])[./-]([0-9]{4})|(([0][1-9]|[1|2][0-9]|[3][0|1])[./-]([0][1-9]|[1][0-2])[./-]([0-9]{4}))[ ]([0-2][0-9])[:](([0-5][0-9]))?)$";
        public const string DateTime_ErrorMsg = "Неверный формат даты-времени (пример: '10.07.2018 09:15')";
        public const string dateRegex = @"/^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-.\/])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/"; //учитывая високосные года
        /// <summary>Дата в формате DD/MM/YYYY</summary>
        public const string Date = @"(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d";
        public const string Date_ErrorMsg = "Неверный формат даты (пример: '20.07.2018')";
        /// <summary>Число целое или дробное с 2 знаками после точки</summary>
        public const string Money = "^[0-9]*[.]?[0-9]{0,2}$";
        public const string Money_ErrorMsg = "Введите целое или дробное число с разделителем '.'";
        public const string Required_ErrorMsg = "Обязательное поле";

        public const string EMail = @"[\w-]+@([\w-]+\.)+[\w-]+";
        public const string EMail_ErrorMsg = "Введите правильный e-mail";
    }

    public class LookupIdCheckAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            if (value.GetType() == typeof(FieldUserValue))
            {
                if ((value as FieldUserValue).LookupId < 1) return false;
                else return true;
            }
            if (value.GetType() == typeof(FieldLookupValue))
            {
                if ((value as FieldLookupValue).LookupId < 1) return false;
                else return true;
            }

            return true;
        }
    }

}
