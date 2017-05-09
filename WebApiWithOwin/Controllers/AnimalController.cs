using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiWithOwin.Models;

namespace WebApiWithOwin.Controllers
{
    public class AnimalController : ApiController
    {
        public IEnumerable<Animal> GetAnimals()
        {
            var animals = new List<Animal>() {
                new Animal() { Name = "Dog", Description = "A dog. " },
                new Animal() { Name = "Cat", Description = "A cat. " },
                new Animal() { Name = "Bird", Description = "A bird. " }
            };

            return animals;
        }
    }
}
