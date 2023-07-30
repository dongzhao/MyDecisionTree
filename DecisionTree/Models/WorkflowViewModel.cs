using DecisionTree.MVC.Core.Enums;

namespace DecisionTree.Models
{
    public class WorkflowViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; } = String.Empty;
        public HierarchyType ItemType { get; set; }
    }
}
