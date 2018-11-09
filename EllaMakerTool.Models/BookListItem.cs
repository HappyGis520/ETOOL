using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMSidekick.ViewModels;

namespace EllaMakerTool.Models
{
    public class BookListItem : BindableBase<BookListItem>
    {
        public string id { set; get; }
        private  string isbn;
        private  string name;
        private  decimal price;
        private string publisherID;
        private string publisherName;
        private string authorID;
        private string authorName;
        private string bookSetID;
        private string bookSetName;
        private int eBookCount;

        public  string Isbn
        {
            get
            {
                return isbn;
            }
            set
            {
                this.isbn = string.ReferenceEquals(value, null) ? null : value.Trim();
            }
        }
        public  string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public  string PublisherID
        {
            get
            {
                return publisherID;
            }
            set
            {
                publisherID = value;
            }
        }
        public  string PublisherName
        {
            get
            {
                return publisherName;
            }
            set
            {
                publisherName = value;
            }
        }
        public  string AuthorName
        {
            get
            {
                return authorName;
            }
            set
            {
                authorName = value;
            }
        }
        public  string BookSetName
        {
            get
            {
                return bookSetName;
            }
            set
            {
                bookSetName = value;
            }
        }
        public  int EBookCount
        {
            get
            {
                return eBookCount;
            }
            set
            {
                this.eBookCount = value;
            }
        }
        public  string AuthorID
        {
            get
            {
                return authorID;
            }
            set
            {
                authorID = value;
            }
        }
        public  string BookSetID
        {
            get
            {
                return bookSetID;
            }
            set
            {
                bookSetID = value;
            }
        }
        public  decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
    }
}
