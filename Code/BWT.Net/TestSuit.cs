using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using System.ComponentModel;


namespace BWT
{
    
    [TestFixtureAttribute]
    public class TestSuit
    {
        const string Reference = "ACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCA";
        const int SEQ_LENGTH = 35;
        readonly SequenceLogics _slogics;


        BackgroundWorker bw;

        public TestSuit()
        {
            this.bw = new BackgroundWorker() { WorkerReportsProgress = true };
            this._slogics = new SequenceLogics();
            this._slogics.Reference = Reference;
            this._slogics.FindGapgs = true;
            //bw);
           
        }
        //Use TestInitialize to run code before running each test
        [SetUpAttribute]
        public virtual void MyTestInitialize()
        {
           
                //try
                //{
                  
                //}
                //catch (Exception ex)
                //{
                //    Assert.Fail("Failed to initialize Test ('MyTestInitialize'): " + Environment.NewLine + ex.ToString());
                //}
        }

        //Use TestCleanup to run code after each test has run
        [TearDownAttribute]
        public virtual void MyTestCleanup()
        {
            //GC.Collect();
            //GC.WaitForFullGCComplete();
        }


        [TestAttribute]
        [TestCase(SEQ_LENGTH,TestName = "Find sequence at beginning")]
        public void FindSequencyAtBegining(int sequenceLength)
        {
            AlignSequence(0, SEQ_LENGTH,0);
        }

        private void AlignSequence(int startIndex, int length,int errorsAllowed)
        {
            var seq = new String(this._slogics.Reference.Skip(startIndex).Take(length).ToArray());            
            var results = this._slogics.iSearch.GetIndex(seq, errorsAllowed);


        }
    }
}





