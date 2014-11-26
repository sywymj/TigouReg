using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


class LoggerClass
{
    private ListBox mListBoxControl;
    public LoggerClass(ListBox pListboxControl)
    {
        mListBoxControl = pListboxControl;
    }
    delegate void loggerHandle(string message);
    public void writeLogger(string message)
    {
        if (this.mListBoxControl.InvokeRequired)
        {
            this.mListBoxControl.Invoke(new loggerHandle(writeLogger), new object[] { message });
            return;
        }
        if (this.mListBoxControl.Items.Count > 80)
        {
            this.mListBoxControl.Items.Clear();
        }
        message = DateTime.Now.ToShortTimeString() + "::" + message;
        //this.mListBoxControl.Items.Add(message);
        this.mListBoxControl.Items.Insert(0, message);
    }
}
