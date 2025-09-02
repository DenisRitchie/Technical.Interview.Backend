namespace Technical.Interview.Backend.Data.Seed;

using Microsoft.EntityFrameworkCore;

using Technical.Interview.Backend.Entities;

public static class SeedBrandExtensions
{
    private static (string Name, string OriginCountry, string Website)[] BrandData
      => [
            ("Honda",        "Japan",          "https://www.honda.com/"),
            ("Chevrolet",    "United States",  "https://www.chevrolet.com/"),
            ("BMW",          "Germany",        "https://www.bmw.com/"),
            ("Mercedes-Benz","Germany",        "https://www.mercedes-benz.com/"),
            ("Hyundai",      "South Korea",    "https://www.hyundai.com/"),
            ("Kia",          "South Korea",    "https://www.kia.com/"),
            ("Nissan",       "Japan",          "https://www.nissan-global.com/"),
            ("Mazda",        "Japan",          "https://www.mazda.com/"),
            ("Subaru",       "Japan",          "https://www.subaru.com/"),
            ("Mitsubishi",   "Japan",          "https://www.mitsubishi-motors.com/"),
            ("Lexus",        "Japan",          "https://www.lexus.com/"),
            ("Infiniti",     "Japan",          "https://www.infiniti.com/"),
            ("Acura",        "Japan",          "https://www.acura.com/"),
            ("Audi",         "Germany",        "https://www.audi.com/"),
            ("Porsche",      "Germany",        "https://www.porsche.com/"),
            ("Opel",         "Germany",        "https://www.opel.com/"),
            ("Peugeot",      "France",         "https://www.peugeot.com/"),
            ("Renault",      "France",         "https://www.renault.com/"),
            ("Citroën",      "France",         "https://www.citroen.com/"),
            ("Fiat",         "Italy",          "https://www.fiat.com/"),
            ("Ferrari",      "Italy",          "https://www.ferrari.com/"),
            ("Lamborghini",  "Italy",          "https://www.lamborghini.com/"),
            ("Maserati",     "Italy",          "https://www.maserati.com/"),
            ("Alfa Romeo",   "Italy",          "https://www.alfaromeo.com/"),
            ("Jaguar",       "United Kingdom", "https://www.jaguar.com/"),
            ("Land Rover",   "United Kingdom", "https://www.landrover.com/"),
            ("Aston Martin", "United Kingdom", "https://www.astonmartin.com/"),
            ("Bentley",      "United Kingdom", "https://www.bentleymotors.com/"),
            ("Rolls-Royce",  "United Kingdom", "https://www.rolls-roycemotorcars.com/"),
            ("Mini",         "United Kingdom", "https://www.mini.com/"),
            ("Volvo",        "Sweden",         "https://www.volvocars.com/"),
            ("Saab",         "Sweden",         "https://www.saab.com/"),
            ("Scania",       "Sweden",         "https://www.scania.com/"),
            ("Tesla",        "United States",  "https://www.tesla.com/"),
            ("Chrysler",     "United States",  "https://www.chrysler.com/"),
            ("Dodge",        "United States",  "https://www.dodge.com/"),
            ("Jeep",         "United States",  "https://www.jeep.com/"),
            ("RAM",          "United States",  "https://www.ramtrucks.com/"),
            ("Buick",        "United States",  "https://www.buick.com/"),
            ("Cadillac",     "United States",  "https://www.cadillac.com/"),
            ("GMC",          "United States",  "https://www.gmc.com/"),
            ("Toyota",       "Japan",          "https://www.toyota.com/"),
            ("Ford",         "United States",  "https://www.ford.com/"),
            ("Volkswagen",   "Germany",        "https://www.vw.com/"),
      ];

    extension(ModelBuilder ModelBuilder)
    {
        public void SeedBrand()
        {
            ModelBuilder.Entity<MarcasAuto>().HasData(
                BrandData.Select(Brand => MarcasAuto.Create(Brand.Name, Brand.OriginCountry, Brand.Website)));
        }
    }
}

