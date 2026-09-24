namespace WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

    //    private void button1_Click(object sender, EventArgs e)
    //    {
    //        Task.Run(() =>
    //        {
    //            Thread.Sleep(4000);

    //            textBox1.BeginInvoke(() =>
    //            {
    //                textBox1.Text = "aaa";
    //            });
    //        });
    //}
        private async void button1_Click(object sender,EventArgs e)
        {
            await Task.Delay(4000);
            textBox1.Text = "sss";
            
        }
    }
}
