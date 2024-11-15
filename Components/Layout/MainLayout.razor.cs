namespace Spill.Core.Web.Components.Layout
{
    public partial class MainLayout
    {
        private string sidebarClass = "";
        private bool isExpanded = false;

        private void ExpandSidebar()
        {
            sidebarClass = "expanded";
            isExpanded = true;
        }

        private void CollapseSidebar()
        {
            sidebarClass = "";
            isExpanded = false;
        }
    }
}
