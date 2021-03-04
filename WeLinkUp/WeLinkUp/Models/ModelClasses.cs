using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    public class Person 
    { 
        public int PersonId { get; set; }
    public string PersonName { get; set; }
    }

    public class PersonDatabase : List<Person>
    {
        public PersonDatabase() {

            Add(new Person() { PersonId = 1, PersonName = "MS" });
        }
    }
    
}
