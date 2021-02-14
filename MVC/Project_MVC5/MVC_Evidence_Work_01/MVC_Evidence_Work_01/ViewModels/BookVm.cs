using MVC_Evidence_Work_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Evidence_Work_01.ViewModels
{
    public class BookVm
    {
        public int BookId { get; set; }

        public string BookName { get; set; }

        public Category? Category { get; set; }

        public DateTime PublishDate { get; set; }
        
        public string Picture { get; set; }
    }
    

}