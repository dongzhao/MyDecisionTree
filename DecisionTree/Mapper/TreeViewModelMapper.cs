
using DecisionTree.Models;
using DecisionTree.Models.Documents;
using DecisionTree.MVC.Core.Entities;
using DecisionTree.MVC.Core.Enums;

namespace DecisionTree.Mapper
{
    public class TreeViewModelMapper 
    {
        private TreeViewModelMapper() { }

        public static TreeViewModel Map(HierarchyItem item)
        {
            var viewModel = new TreeViewModel();
            ToTreeView(item, viewModel);
            return viewModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hierarchy"></param>
        /// <param name="treeNode"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ToTreeView(HierarchyItem hierarchy, TreeViewModel treeNode)
        {
            if (treeNode == null) throw new ArgumentNullException("Empty Treeview model");

            // init root node if it's not availalbe
            if (string.IsNullOrEmpty(treeNode.Id))
            {
                treeNode.Id = hierarchy.Id.ToString();
                treeNode.Title = hierarchy.Title;
                treeNode.ItemType = HierarchyType.Start.ToString();
            }

            // populate child's node
            foreach (var child in hierarchy.Children)
            {
                var childNode = new TreeViewModel()
                {
                    ParentId = hierarchy.Id.ToString(),
                    Id = child.Id.ToString(),
                    Title = child.Title,
                    ItemType = child.ItemType.ToString(),
                };

                treeNode.Children.Add(childNode);
                ToTreeView(child, childNode);
            }
        }
    }
}
