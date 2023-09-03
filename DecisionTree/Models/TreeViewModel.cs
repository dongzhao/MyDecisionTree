namespace DecisionTree.Models
{
    /// <summary>
    /// A hierarchy tree node view model for user interface presentation
    /// the data structure is used for rendering the tree diagram via D3.JS engine
    /// </summary>
    public class TreeViewModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; } = string.Empty;
        public string Title { get; set; } = String.Empty;
        public string ItemType { get; set; } = string.Empty;

        public List<TreeViewModel> Children = new List<TreeViewModel>();
    }
}
