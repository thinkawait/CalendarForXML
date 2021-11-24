using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;


namespace CalendarForXML
{
    public class ApplicationDbContext : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'ApplicationDbContext' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'CalendarForXML.ApplicationDbContext' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'ApplicationDbContext' 連接字串。
        public ApplicationDbContext()
            : base("name=calendarDbContext")
        {
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

         public virtual DbSet<Calendar> Calendars { get; set; }
    }
    public class Calendar
    {
        [Key]//主鍵 PK
        [Display(Name = "編號")]//顯示名稱
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//自動生成編號
        public int id { get; set; }

        [Display(Name = "時間")]//顯示名稱
        public DateTime date { get; set; }

        [Display(Name = "節日")]//顯示名稱
        public string name { get; set; }

        [Display(Name = "是否放假")]//顯示名稱
        public bool isHoliday { get; set; }

        [Display(Name = "假期類別")]//顯示名稱
        public string holidayCategory { get; set; }

        [Display(Name = "描述")]//顯示名稱
        public string description { get; set; }
    }
    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}