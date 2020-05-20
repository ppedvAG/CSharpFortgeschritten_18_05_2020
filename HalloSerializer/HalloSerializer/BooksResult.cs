using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HalloSerializer
{

    [Serializable()]
    public class BooksResult
    {
        public string kind { get; set; }
        public int totalItems { get; set; }
        public Item[] items { get; set; }
    }

    [Serializable()]
    public class Item
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string etag { get; set; }
        public string selfLink { get; set; }
        public Volumeinfo volumeInfo { get; set; }
        public Saleinfo saleInfo { get; set; }
        public Accessinfo accessInfo { get; set; }
        public Searchinfo searchInfo { get; set; }
    }

    [Serializable()]
    public class Volumeinfo
    {
        public string title { get; set; }

        [SoapIgnore]
        public string[] authors { get; set; }
        public string publisher { get; set; }
        public string publishedDate { get; set; }

        [SoapIgnore]
        public Industryidentifier[] industryIdentifiers { get; set; }
        public Readingmodes readingModes { get; set; }
        public int pageCount { get; set; }
        public string printType { get; set; }
        [SoapIgnore]
        public string[] categories { get; set; }
        public string maturityRating { get; set; }
        public bool allowAnonLogging { get; set; }
        public string contentVersion { get; set; }
        public Panelizationsummary panelizationSummary { get; set; }
        public Imagelinks imageLinks { get; set; }
        public string language { get; set; }
        public string previewLink { get; set; }
        public string infoLink { get; set; }
        public string canonicalVolumeLink { get; set; }
        public string subtitle { get; set; }
        public float averageRating { get; set; }
        public int ratingsCount { get; set; }
        public string description { get; set; }
    }

    [Serializable()]
    public class Readingmodes
    {
        public bool text { get; set; }
        public bool image { get; set; }
    }

    [Serializable()]
    public class Panelizationsummary
    {
        public bool containsEpubBubbles { get; set; }
        public bool containsImageBubbles { get; set; }
    }

    [Serializable()]
    public class Imagelinks
    {
        public string smallThumbnail { get; set; }
        public string thumbnail { get; set; }
    }

    [Serializable()]
    public class Industryidentifier
    {
        public string type { get; set; }
        public string identifier { get; set; }
    }

    [Serializable()]
    public class Saleinfo
    {
        public string country { get; set; }
        public string saleability { get; set; }
        public bool isEbook { get; set; }
    }

    [Serializable()]
    public class Accessinfo
    {
        public string country { get; set; }
        public string viewability { get; set; }
        public bool embeddable { get; set; }
        public bool publicDomain { get; set; }
        public string textToSpeechPermission { get; set; }
        public Epub epub { get; set; }
        public Pdf pdf { get; set; }
        public string webReaderLink { get; set; }
        public string accessViewStatus { get; set; }
        public bool quoteSharingAllowed { get; set; }
    }

    [Serializable()]
    public class Epub
    {
        public bool isAvailable { get; set; }
    }

    [Serializable()]
    public class Pdf
    {
        public bool isAvailable { get; set; }
        public string acsTokenLink { get; set; }
    }

    [Serializable()]
    public class Searchinfo
    {
        public string textSnippet { get; set; }
    }

}
