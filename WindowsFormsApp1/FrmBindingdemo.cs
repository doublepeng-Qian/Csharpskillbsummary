using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmBindingdemo : Form
    {
        MyData _myData;

        //第一步：获取UI线程同步上下文（在窗体构造函数或FormLoad事件中）
        /// <summary>
        /// UI线程的同步上下文
        /// </summary>
        SynchronizationContext m_SyncContext = null;

        public FrmBindingdemo()
        {
            InitializeComponent();

            // 跨线程，更新UI，不推荐使用这种方式
            // Control.CheckForIllegalCrossThreadCalls = false;

            //获取UI线程同步上下文
            m_SyncContext = SynchronizationContext.Current;
        }

        private void FrmBindingdemo_Load(object sender, EventArgs e)
        {
            #region 单向绑定和双向绑定
            _myData = new MyData();
            textBox1.DataBindings.Add("Text", _myData, "TheValue", false, DataSourceUpdateMode.OnPropertyChanged);//双向绑定
            textBox2.DataBindings.Add("Text", _myData, "TheValue", false, DataSourceUpdateMode.Never);//单向绑定
            #endregion
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            // 在同一个线程的方法
            //kryptonTextBox1.Text = "正常！";

            // 创建一个线程用来更新UI
            Thread thread1 = new Thread(new ParameterizedThreadStart(UpdateLabel));
            thread1.Start("同步或异步更新Label");

            // 创建一个后台线程，用来更新UI
            //using (BackgroundWorker bw = new BackgroundWorker())
            //{
            //    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            //    bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //    bw.RunWorkerAsync("后台更新！");
            //}
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            // 这里是后台线程， 是在另一个线程上完成的
            // 这里是真正做事的工作线程
            // 可以在这里做一些费时的，复杂的操作
            Thread.Sleep(5000);
            e.Result = e.Argument + "工作线程完成";
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //这时后台线程已经完成，并返回了主线程，所以可以直接使用UI控件了 
            this.kryptonTextBox1.Text = e.Result.ToString();
        }

        private void UpdateLabel(object str)
        {
            //this.kryptonTextBox1.Text = str.ToString();

            //if (kryptonTextBox1.InvokeRequired)
            //{
            //    // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
            //    // Action<string> actionDelegate = (x) => { this.kryptonTextBox1.Text = x.ToString(); };
            //    // 或者
            //     Action<string> actionDelegate = delegate(string txt) { this.kryptonTextBox1.Text = txt; };

            //    // Invoke是同步的；BeginInvoke是异步的。
            //    this.kryptonTextBox1.BeginInvoke(actionDelegate, str); 
            //}
            //else
            //{
            //    this.kryptonTextBox1.Text = str.ToString();
            //}

            //在线程中更新UI（通过UI线程同步上下文m_SyncContext）
            m_SyncContext.Post(SetTextSafePost, "This text was set safely by SynchronizationContext-Post.");
        }

        //第三步：定义更新UI控件的方法
        /// <summary>
        /// 更新文本框内容的方法
        /// </summary>
        /// <param name="text"></param>
        private void SetTextSafePost(object text)
        {
            this.kryptonTextBox1.Text = text.ToString();
        }
    }


    #region 单向绑定和双向绑定
    /// <summary>
    /// 
    /// </summary>
    public class MyData : INotifyPropertyChanged
    {
        private string _theValue = string.Empty;

        public string TheValue
        {
            get { return _theValue; }
            set
            {
                if (string.IsNullOrEmpty(value) && value == _theValue)
                    return;

                _theValue = value;
                NotifyPropertyChanged(() => TheValue);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        public void NotifyPropertyChanged<T>(Expression<Func<T>> property)
        {
            if (PropertyChanged == null)
                return;

            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
        }
    }
    #endregion
}
