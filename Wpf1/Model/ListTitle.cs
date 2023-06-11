using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf1.Entities;

namespace Wpf1.Model
{
    public class ListTitle : ObservableCollection<Title>
    {
        public ListTitle()
        {
            var titles = PageEmployee.DataEntitiesEmployee.Title;
            var queryTitle = from title in titles select title;
            foreach (Title titl in queryTitle)
            {
                this.Add(titl);
            }
        }
    }

}
