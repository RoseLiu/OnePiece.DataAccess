using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OnePiece.DataAccess.MSTest
{
    using OnePiece.DataAccess;
    using OnePiece.Models;

    [TestClass]
    public class UnitTest1
    {

        /// <summary>
        /// 获取或设置测试上下文
        /// 上下文提供有关当前测试运行以及功能的信息
        /// </summary>
        private TestContext _context;
        public TestContext Context
        {
            get { return _context; }
            set { _context = value; }
        }


        private CustomerDAL dal;

        [TestInitialize]
        public void initialize()
        {
            dal = new CustomerDAL();
        }

        [TestCleanup]
        public void finalize()
        {
            dal = null;
        }


        [TestMethod]
        public void TestMethod_Insert()
        {
            Customer model = new Customer()
            {
                Id = Guid.NewGuid(),
                AddTime = DateTime.Now,
                CustomerLevel = 1,
                CustomerName = "ROSE",
                IsDelete = false,
                PhoneNo = "17727510587",
                VipPrice = 10.5,
                Token = Guid.NewGuid().ToString(),
                Remark = "测试"
            };

            var result = dal.CustomerInsert(model);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestMethod_Update()
        {
            Customer model = new Customer();
            model.Id = Guid.Parse("a8097724-fabe-4431-b4ec-56364f207b78");
            model.CustomerName = "rose";
            model.PhoneNo = "13147306612";


            var result = dal.CustomerUpdate(model);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestMethod_Delete()
        {
            Guid id = Guid.Parse("a8097724-fabe-4431-b4ec-56364f207b78");

            var result = dal.CustomerDelete(id);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestMethod_GetList()
        {
            var list = dal.CustomerGetList();

            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void TestMethod_GetModelById()
        {
            Guid id = Guid.Parse("a8097724-fabe-4431-b4ec-56364f207b78");

            var result = dal.CustomerGetModelById(id);

            Assert.IsTrue(result != null);
        }

    }
}
