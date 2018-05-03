using Foundation;
using System;
using UIKit;

namespace StoryboardTables
{
    public partial class TaskDetailViewController : UITableViewController
    {

        Chores currentTask {get;set;}
        public ItemViewController Delegate {get;set;}


        public TaskDetailViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();
            SaveButton.TouchUpInside += (object sender, EventArgs e) => {
                Console.WriteLine("saving");

                currentTask.Name = TitleText.Text;
                currentTask.Notes = NotesText.Text;
                currentTask.Done = DoneSwitch.On;
                Delegate.SaveTask(currentTask);
            };

            DeleteButton.TouchUpInside += (object sender, EventArgs e) => {
                Delegate.DeleteTask(currentTask);
            };
		}

		public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TitleText.Text = currentTask.Name;
            NotesText.Text = currentTask.Notes;
            DoneSwitch.On = currentTask.Done;

   

        }

        public void SetTask(ItemViewController d, Chores task)
        {
            Delegate = d;
            currentTask = task;
        }


    }
}