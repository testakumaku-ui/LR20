using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerWORK.Core.Models;

namespace TaskTrackerWORK.Core.Migration
{
    public static class TaskNormalizer
    {
        public static bool Normalize(TaskItem t)
        {
            bool changed = false;

            if (t.Title == null) { t.Title = ""; changed = true; }
            if (t.Description == null) { t.Description = ""; changed = true; }

            var titleTrim = t.Title.Trim();
            if (titleTrim != t.Title) { t.Title = titleTrim; changed = true; }

            var descTrim = t.Description.Trim();
            if (descTrim != t.Description) { t.Description = descTrim; changed = true; }

            int maxTitle = 50;
            int maxDesc = 200;

            if (t.Title.Length > maxTitle) { t.Title = t.Title.Substring(0, maxTitle); changed = true; }
            if (t.Description.Length > maxDesc) { t.Description = t.Description.Substring(0, maxDesc); changed = true; }

            return changed;
        }
    }
}
