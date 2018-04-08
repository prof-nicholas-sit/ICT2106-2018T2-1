using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text;
using System.Security.Cryptography;

namespace ProjectWebApplication.Models
{
    public class Account
    {
        [BsonId]
        [BsonElement(elementName: "_id")]
        public ObjectId ID { get; set; }

        [BsonElement(elementName: "Name")]
        public string Name { get; set; }

        [BsonElement(elementName: "Title")]
        public string Title { get; set; }

        [BsonElement(elementName: "AdminRole")]
        [BsonRepresentation(BsonType.Boolean)]
        public Boolean AdminRole { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [BsonElement(elementName: "Email")]
        public string Email { get; set; }

        [BsonElement(elementName: "Password")]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Birthday { get; set; }

        [BsonElement(elementName: "DisplayPicURL")]
        public string DisplayPicURL { get; set; }

        [BsonElement(elementName: "Bio")]
        public string Bio { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        public Account() { }
        internal Account(ObjectId id, string name, string title, string email, string password, DateTime birthday, bool adminRole, string displayPicUrl, string bio)
        {
            ID = id;
            Name = name;
            Title = title;
            Email = email;
            Password = password;
            Birthday = birthday;
            AdminRole = adminRole;
            DisplayPicURL = displayPicUrl;
            Bio = bio;
        }

    

    }
}
