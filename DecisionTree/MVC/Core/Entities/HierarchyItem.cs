using DecisionTree.MVC.Core.Enums;
using DecisionTree.MVC.Core.Interfaces;
using DecisionTree.MVC.Core.Values;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecisionTree.MVC.Core.Entities
{
    [Table("HierarchyItem")]
    public class HierarchyItem : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public HierarchyType ItemType { get; set; } = HierarchyType.Start;
        /// <summary>
        /// 
        /// </summary>
        public ItemMetaData Metadata { get; set; } = new();
        /// <summary>
        /// 
        /// </summary>
        public List<ItemMetaData> MetadataList { get; set; } = new();
        public virtual HierarchyItem Parent { get; set; }
        public virtual List<HierarchyItem> Children { get; set; } = new();

    }
}
