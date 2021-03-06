﻿using IBLL;
using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using Tools;

namespace MemberCard.Common
{
    public class BLLLoader
    {
        private static IFactory GetIFactory()
        {
            IFactory factory = GetBllFactory();
            if (factory == null)
            {
                throw new NullReferenceException("factory is null");
            }
            return factory;
        }

        public static IGoodsBLL GetGoodsBll() {
            return GetIFactory().CreateGoodsInstance();
        }

        public static ISaleLogBLL GetSaleLogBll()
        {
            return GetIFactory().CreateSaleLogInstance();
        }

        public static IMemberCardBLL GetMemberCardBll()
        {
            return GetIFactory().CreateMemberCardInstance();
        }

        public static IMemberCardCategoryBLL GetMemberCardCategoryBll()
        {
            return GetIFactory().CreateMemberCardCategoryInstance();
        }

        public static IMemberCardRecordBLL GetMemberCardRecordBll()
        {
            return GetIFactory().CreateMemberCardRecordInstance();
        }

        /// <summary>
        /// 获取BLL构造器
        /// </summary>
        /// <returns></returns>
        private static IFactory GetBllFactory() {
            String BLLPath = ConfigurationManager.AppSettings["BLLModule"].ToString();
            Type type = ReflectionTools.GetTypeObject(Application.StartupPath, BLLPath, String.Format("{0}.Factory", BLLPath));
            if (type == null)
                throw new MissingMethodException("Factory not found!");

            ConstructorInfo constructor = type.GetConstructor(System.Type.EmptyTypes);
            if (constructor == null)
                throw new MissingMethodException("No public constructor defined for this object");
            
            IFactory factory = constructor.Invoke(null) as IFactory;
            factory.SetStartupPath(Application.StartupPath);
            return factory;
        }
    }
}
