using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAPICodePack.Taskbar;
using Microsoft.WindowsAPICodePack.Shell;
using System.IO;
using System.Reflection;

namespace Win7FTP.HelperClasses
{
    public class TaskBarManager
    {
        #region Taskbar Progressbar
        /// <summary>
        /// Public Method to set the Progress of the Taskbar Progressbar.
        /// </summary>
        /// <param name="Progress">Current Progress</param>
        /// <param name="Maximum">Maximum for Progressbar</param>
        public static void SetProgressValue(int Progress, int Maximum)
        {
            TaskbarProgressBarState state = TaskbarProgressBarState.Normal;
            TaskbarManager.Instance.SetProgressState(state);
            TaskbarManager.Instance.SetProgressValue(Progress, Maximum);
        }

        /// <summary>
        /// Set the State for the Taskbar Progressbar. Useful when we want to display the error progress.
        /// </summary>
        /// <param name="state">State to display. ex: Red, Yellow, Green</param>
        public static void SetTaskBarProgressState(TaskbarProgressBarState state)
        {
            TaskbarManager.Instance.SetProgressState(state);
        }

        /// <summary>
        /// Clear the Progress of Progressbar.
        /// </summary>
        public static void ClearProgressValue()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
        }
        #endregion
    }
}
