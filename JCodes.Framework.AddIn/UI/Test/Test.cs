using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using System.Reflection.Emit;
using System.Diagnostics;
using JCodes.Framework.Common;

namespace JCodes.Framework.AddIn.Test
{
    public partial class Test : DevExpress.XtraEditors.XtraForm
    {
        public Test()
        {
            LogHelper.WriteLog(JCodes.Framework.jCodesenum.BaseEnum.LogLevel.LOG_LEVEL_CRIT, "吴建明测试 Test 0 Start", typeof(Test));

            InitializeComponent();

            LogHelper.WriteLog(JCodes.Framework.jCodesenum.BaseEnum.LogLevel.LOG_LEVEL_CRIT, "吴建明测试 Test 1 Start", typeof(Test));
        }

        public delegate void SetValueDelegate(object target, object arg);

        private void button1_Click(object sender, EventArgs e)
        {
            txtLog.AppendText(System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion()+"\r\n");

            int count = 1000000;

            OrderInfo testObj = new OrderInfo();
            PropertyInfo propInfo = typeof(OrderInfo).GetProperty("OrderId");

            txtLog.AppendText("直接访问花费时间：       " + "\r\n");
            Stopwatch watch1 = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
                testObj.OrderId = 123;

            watch1.Stop();
            txtLog.AppendText(watch1.Elapsed.ToString() + "\r\n");

            SetValueDelegate setter2 = DynamicMethodFactory.CreatePropertySetter(propInfo);
            txtLog.AppendText("EmitSet花费时间：        " + "\r\n");
            Stopwatch watch2 = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
                setter2(testObj, 123);

            watch2.Stop();
            txtLog.AppendText(watch2.Elapsed.ToString() + "\r\n");

            txtLog.AppendText("纯反射花费时间：　       " + "\r\n");
            Stopwatch watch3 = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
                propInfo.SetValue(testObj, 123, null);

            watch3.Stop();
            txtLog.AppendText(watch3.Elapsed.ToString() + "\r\n");

            txtLog.AppendText("-------------------" + "\r\n");
            txtLog.AppendText(string.Format("{0} / {1} = {2}",
                watch3.Elapsed.ToString(),
                watch1.Elapsed.ToString(),
                watch3.Elapsed.TotalMilliseconds / watch1.Elapsed.TotalMilliseconds) + "\r\n");

            txtLog.AppendText(string.Format("{0} / {1} = {2}",
                watch3.Elapsed.ToString(),
                watch2.Elapsed.ToString(),
                watch3.Elapsed.TotalMilliseconds / watch2.Elapsed.TotalMilliseconds) + "\r\n");

            txtLog.AppendText(string.Format("{0} / {1} = {2}",
                watch2.Elapsed.ToString(),
                watch1.Elapsed.ToString(),
                watch2.Elapsed.TotalMilliseconds / watch1.Elapsed.TotalMilliseconds) + "\r\n");
        }

        public static class DynamicMethodFactory
        {
            public static SetValueDelegate CreatePropertySetter(PropertyInfo property)
            {
                if (property == null)
                    throw new ArgumentNullException("property");

                if (!property.CanWrite)
                    return null;

                MethodInfo setMethod = property.GetSetMethod(true);

                DynamicMethod dm = new DynamicMethod("PropertySetter", null,
                    new Type[] { typeof(object), typeof(object) }, property.DeclaringType, true);

                ILGenerator il = dm.GetILGenerator();

                if (!setMethod.IsStatic)
                {
                    il.Emit(OpCodes.Ldarg_0);
                }
                il.Emit(OpCodes.Ldarg_1);

                EmitCastToReference(il, property.PropertyType);
                if (!setMethod.IsStatic && !property.DeclaringType.IsValueType)
                {
                    il.EmitCall(OpCodes.Callvirt, setMethod, null);
                }
                else
                    il.EmitCall(OpCodes.Call, setMethod, null);

                il.Emit(OpCodes.Ret);

                return (SetValueDelegate)dm.CreateDelegate(typeof(SetValueDelegate));
            }

            private static void EmitCastToReference(ILGenerator il, Type type)
            {
                if (type.IsValueType)
                    il.Emit(OpCodes.Unbox_Any, type);
                else
                    il.Emit(OpCodes.Castclass, type);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtLog.Text = string.Empty;
        }
    }

    public class OrderInfo {
        private int orderId;

        /// <summary>
        /// 订单号
        /// </summary>
        public int OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
    }
}