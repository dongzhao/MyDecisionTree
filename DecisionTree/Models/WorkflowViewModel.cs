using DecisionTree.MVC.Core.Enums;

namespace DecisionTree.Models
{
    /// <summary>
    /// A hierarchy table model view of db entity HierarchyItem
    /// The object data was converted from populated HierarchyItem 
    /// via automapper 
    /// </summary>
    public class WorkflowViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; } = String.Empty;
        public HierarchyType ItemType { get; set; }
    }
}
