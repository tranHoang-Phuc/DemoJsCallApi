using System.Xml.Serialization;

namespace DemoJsCallApi.Dtos
{
    using System.Xml.Serialization;

    [XmlRoot("XmlCreationProduct")]
    public class XmlCreationProduct
    {
        [XmlElement("ProductName")]
        public string? ProductName { get; set; }

        [XmlElement("UnitPrice")]
        public decimal? UnitPrice { get; set; }

        [XmlElement("UnitsInStock")]
        public int? UnitsInStock { get; set; }

        [XmlElement("Image")]
        public string? Image { get; set; }

        [XmlElement("CategoryId")]
        public int? CategoryId { get; set; }
    }

}
