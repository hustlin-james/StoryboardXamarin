using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace StoryboardTables
{
    public partial class ItemViewController : UITableViewController
    {
        List<Chores> chores;
        public ItemViewController (IntPtr handle) : base (handle)
        {
            chores = new List<Chores> {
              new Chores {Name="Groceries", Notes="Buy bread, cheese, apples", Done=false},
              new Chores {Name="Devices", Notes="Buy Nexus, Galaxy, Droid", Done=false}
            };  
        }

  
		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            AddButton.Clicked += (sender, e) =>
            {
                // first, add the task to the underlying data
                var newId = chores[chores.Count - 1].Id + 1;
                var newChore = new Chores { Id = newId };
                //chores.Add(newChore);

                // then open the detail view to edit it
                var detail = Storyboard.InstantiateViewController("detail") as TaskDetailViewController;
                detail .SetTask(this, newChore);
                NavigationController.PushViewController(detail, true);

            };
		}
		public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var db = DBSingleton.DB;
       

            var c = (from i in db.Table<Chores>() select i).ToList();

           // TableView.Source = new RootTableSource(chores.ToArray());
            TableView.Source = new RootTableSource(c.ToArray());

            TableView.ReloadData();
        }

        public void SaveTask(Chores chore)
        {
            //var oldTask = chores.Find(t => t.Id == chore.Id);
            var db = DBSingleton.DB;
            db.Insert(chore);

            NavigationController.PopViewController(true);
        }

        public void DeleteTask(Chores chore)
        {
            var oldTask = chores.Find(t => t.Id == chore.Id);
            chores.Remove(oldTask);
            NavigationController.PopViewController(true);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "TaskSegue")
            { // set in Storyboard
                var navctlr = segue.DestinationViewController as TaskDetailViewController;
                if (navctlr != null)
                {
                    var source = TableView.Source as RootTableSource;
                    var rowPath = TableView.IndexPathForSelectedRow;
                    var item = source.GetItem(rowPath.Row);
                    navctlr.SetTask(this, item); // to be defined on the TaskDetailViewController
                }
            }
        }
    }
}