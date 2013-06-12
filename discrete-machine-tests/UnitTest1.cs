using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using discrete_machine.Elements;

namespace discrete_machine
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestInverter()
        {
            Register reg = new Register();
            reg.P.Value = 5;
            Inverter inv = new Inverter("Invertinator");
            Wire wire = new Wire(reg.Output.FirstOrDefault());
            wire.ConnectTo(inv.Input.FirstOrDefault());

            reg.Step();
            wire.Execute();
            inv.Invert();

            Assert.AreEqual(inv.Output.FirstOrDefault().Value, 0);
        }

        public void TestSummator(Int32 a, Int32 b, Int32 actualR, Int32 actualP, Int32 p = 0)
        { 
            Register reg;
            Register reg2;
            Summator sum;
            Wire wire;
            Wire wire2;

            reg = new Register();
            reg.P.Value = a;
            reg2 = new Register();
            reg2.P.Value = b;
            sum = new Summator("Summator");
            sum.P.Value = p;

            wire = new Wire(reg.Output.FirstOrDefault());
            wire2 = new Wire(reg2.Output.FirstOrDefault());
            wire.ConnectTo(sum.Input.FirstOrDefault());
            wire2.ConnectTo(sum.Input.LastOrDefault());
            
            reg.Step();
            reg2.Step();
            wire.Execute();
            wire2.Execute();
            sum.Sum();

            Assert.AreEqual(sum.Output.FirstOrDefault().Value, actualR, "a=" + a + ", b=" + b + ", p=" + p);
            Assert.AreEqual(sum.P.Value, actualP, "a="+a+", b="+b+", p="+p);
        }

        public void TestConjuctor(Int32 a, Int32 b, Int32 actualR)
        {
            Register reg;
            Register reg2;
            Сonjunctor conj;
            Wire wire;
            Wire wire2;

            reg = new Register();
            reg.P.Value = a;
            reg2 = new Register();
            reg2.P.Value = b;
            conj = new Сonjunctor("Conjuct");

            wire = new Wire(reg.Output.FirstOrDefault());
            wire2 = new Wire(reg2.Output.FirstOrDefault());
            wire.ConnectTo(conj.Input.FirstOrDefault());
            wire2.ConnectTo(conj.Input.LastOrDefault());

            reg.Step();
            reg2.Step();
            wire.Execute();
            wire2.Execute();
            conj.Сonjunct();

            Assert.AreEqual(conj.Output.FirstOrDefault().Value, actualR, "a=" + a + ", b=" + b);
        }


        public void TestDisjunctor(Int32 a, Int32 b, Int32 actualR)
        {
            Register reg;
            Register reg2;
            Disjunctor dis;
            Wire wire;
            Wire wire2;

            reg = new Register();
            reg.P.Value = a;
            reg2 = new Register();
            reg2.P.Value = b;
            dis = new Disjunctor("Disjuct");

            wire = new Wire(reg.Output.FirstOrDefault());
            wire2 = new Wire(reg2.Output.FirstOrDefault());
            wire.ConnectTo(dis.Input.FirstOrDefault());
            wire2.ConnectTo(dis.Input.LastOrDefault());

            reg.Step();
            reg2.Step();
            wire.Execute();
            wire2.Execute();
            dis.Disjunct();

            Assert.AreEqual(dis.Output.FirstOrDefault().Value, actualR, "a=" + a + ", b=" + b);
        }

        [TestMethod]
        public void TestSummator()
        {
            TestSummator(0, 0, 0, 0);
            TestSummator(0, 1, 1, 0);
            TestSummator(1, 1, 1, 1);

            TestSummator(0, 0, 1, 0, 1);
            TestSummator(0, 1, 1, 1, 1);
            TestSummator(1, 1, 1, 2, 1);
        }

        [TestMethod]
        public void TestConjuctor()
        {
            TestConjuctor(0, 0, 0);
            TestConjuctor(1, 0, 0);
            TestConjuctor(0, 1, 0);
            TestConjuctor(1, 1, 1);
        }

        [TestMethod]
        public void TestDisjunctor()
        {
            TestDisjunctor(0, 0, 0);
            TestDisjunctor(1, 0, 1);
            TestDisjunctor(0, 1, 1);
            TestDisjunctor(1, 1, 1);
        }
    }
}
