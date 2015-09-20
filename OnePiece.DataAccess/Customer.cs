using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnePiece.Models
{
    public class Customer
    {
        public Guid Id { get; set; }                //UNIQUEIDENTIFIER DEFAULT(NEWSEQUENTIALID()) PRIMARY KEY,	
        public string CustomerName { get; set; }    //NVARCHAR(50),	
        public DateTime AddTime { get; set; }       //DATETIME DEFAULT(GETDATE()) NOT NULL,
        public bool IsDelete { get; set; }          //BIT DEFAULT(0) NOT NULL,
        public byte CustomerLevel { get; set; }     //TINYINT DEFAULT(1) NOT NULL,
        public string PhoneNo { get; set; }         //CHAR(13) NOT NULL,
        public string Token { get; set; }           //VARCHAR(500),
        public double VipPrice { get; set; }        //DECIMAL(6,2),
        public string Remark { get; set; }          //NVARCHAR(100)
    }

}