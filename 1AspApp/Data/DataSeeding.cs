using _1AspApp.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace _1AspApp.Data
{
    public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<MovieContext>();

            context.Database.Migrate(); // dotnet ef database update dememize gerek yok migrate oluşturuyoruz zaten

            var genres = new List<Genre>()
                        {
                            new Genre {Name="Macera",Movies=
                            new List<Movie>(){
                                new Movie
                                {

                                    Title="Macera Filmi 1",
                                    Description ="Konusu. Dizide, İstanbul Emniyet Müdürlüğü Asayiş Şube Müdürlüğü'nde özel bir ekipte görev yapan" +
                                    " polislerin aile yaşamları ve İstanbul sokaklarındaki maceraları anlatılmaktadır",
                                    //Players=new string[]{"Şebket Çoruh","Furkan Göksel","Zafer Ergin","Özgür Ozan"},
                                    ImageUrl="1.jpg",
                                },
                                new Movie
                                {

                                    Title="Macera Filmi 2",
                                    Description ="1950 yılında Kuzey Kore'nin, Güney Kore'ye saldırısı sonrası Birleşmiş Milletler'in çağrısıyla Kore'ye giden " +
                                    "Türk tugayında bulununan Süleyman Dilbirliği, savaş meydanında Kim Eunja adında küçük bir kız bulur, adını telaffuz " +
                                    "etmekte zorlanan Dilbirliği ona, Ayla ismini verir.",
                                    //Players=new string[]{"İsmail Hacıoğlu","Kim Seol","Lee Kyung-jin","Ali Atay"},
                                    ImageUrl="2.jpg",


                                }

                            } },
                            new Genre {Name="Komedi"},
                            new Genre {Name="Romantik"},
                            new Genre {Name="Savaş"},
                            new Genre {Name="Bilim Kurgu"}
                        };
            var movies = new List<Movie>
            {
                new Movie
                {

                    Title="Arka Sokaklar",
                    Description ="Konusu. Dizide, İstanbul Emniyet Müdürlüğü Asayiş Şube Müdürlüğü'nde özel bir ekipte görev yapan" +
                    " polislerin aile yaşamları ve İstanbul sokaklarındaki maceraları anlatılmaktadır",
                    //Players=new string[]{"Şebket Çoruh","Furkan Göksel","Zafer Ergin","Özgür Ozan"},
                    ImageUrl="1.jpg",
                    Genres= new List<Genre>(){genres[0],new Genre() {Name="Yeni Tür" }, genres[1] }
                },
                new Movie
                {

                    Title="Ayla",
                    Description ="1950 yılında Kuzey Kore'nin, Güney Kore'ye saldırısı sonrası Birleşmiş Milletler'in çağrısıyla Kore'ye giden " +
                    "Türk tugayında bulununan Süleyman Dilbirliği, savaş meydanında Kim Eunja adında küçük bir kız bulur, adını telaffuz " +
                    "etmekte zorlanan Dilbirliği ona, Ayla ismini verir.",
                    //Players=new string[]{"İsmail Hacıoğlu","Kim Seol","Lee Kyung-jin","Ali Atay"},
                    ImageUrl="2.jpg",
                    Genres= new List<Genre>(){genres[2],genres[1] }

                },
                new Movie
                {

                    Title="Mirage",
                    Description ="9 Kasım 1989 yılında çıkan şiddetli bir fırtınayı evin camından izleyen 12 yaşındaki bir çocuk olan Nico, " +
                    "komşusunun evinde bir tuhaflıklar olduğunu fark eder. Evden vahşi sesler duymasının üzerine yardım etmek için eve giden " +
                    "Nico, komşuşu Prieto'nun karısının ölü olduğunu görür.",
                    //Players=new string[]{ "Adriana Ugarte", "Álvaro Morte", "Chino Darín", "Mima Riera"},
                    ImageUrl="3.jpg",
                    Genres= new List<Genre>(){genres[2], genres[0] }
                },
                 new Movie
                {

                    Title="Arka Sokaklar",
                    Description ="Konusu. Dizide, İstanbul Emniyet Müdürlüğü Asayiş Şube Müdürlüğü'nde özel bir ekipte görev yapan" +
                    " polislerin aile yaşamları ve İstanbul sokaklarındaki maceraları anlatılmaktadır",
                    //Players=new string[]{"Şebket Çoruh","Furkan Göksel","Zafer Ergin","Özgür Ozan"},
                    ImageUrl="1.jpg",
                    Genres= new List<Genre>(){genres[0], genres[3] }
                },
                new Movie
                {

                    Title="Ayla",
                    Description ="1950 yılında Kuzey Kore'nin, Güney Kore'ye saldırısı sonrası Birleşmiş Milletler'in çağrısıyla Kore'ye giden " +
                    "Türk tugayında bulununan Süleyman Dilbirliği, savaş meydanında Kim Eunja adında küçük bir kız bulur, adını telaffuz " +
                    "etmekte zorlanan Dilbirliği ona, Ayla ismini verir.",
                    //Players=new string[]{"İsmail Hacıoğlu","Kim Seol","Lee Kyung-jin","Ali Atay"},
                    ImageUrl="2.jpg",
                    Genres= new List<Genre>(){genres[2], genres[1] }

                },
                new Movie
                {

                    Title="Mirage",
                    Description ="9 Kasım 1989 yılında çıkan şiddetli bir fırtınayı evin camından izleyen 12 yaşındaki bir çocuk olan Nico, " +
                    "komşusunun evinde bir tuhaflıklar olduğunu fark eder. Evden vahşi sesler duymasının üzerine yardım etmek için eve giden " +
                    "Nico, komşuşu Prieto'nun karısının ölü olduğunu görür.",
                   // Players=new string[]{ "Adriana Ugarte", "Álvaro Morte", "Chino Darín", "Mima Riera"},
                    ImageUrl="3.jpg",
                    Genres= new List<Genre>(){genres[3], genres[2] }
                }
            };
            var users = new List<User>()
            {
                new User(){Username="UserA",Email="userA@gmail.com",Password="1234",ImageUrl="person1.jpg"},
                new User(){Username="UserB",Email="userB@gmail.com",Password="1234",ImageUrl="person2.jpg"},
                new User(){Username="UserC",Email="userC@gmail.com",Password="1234",ImageUrl="person3.jpg"},
                new User(){Username="UserD",Email="userD@gmail.com",Password="1234",ImageUrl="person4.jpg"}
            };
            var people = new List<Person>()
            {
                new Person()
                {
                    Name = "Personel 1",
                    Biography = "Tanıtım 1",
                    User=users[0]
                },
                new Person()
                {
                    Name = "Personel 2",
                    Biography = "Tanıtım 2",
                    User=users[1]
                }

            };
            var crews = new List<Crew>()
            {
                new Crew(){Movie=movies[0],Person=people[0],Job="Yönetmen"},
                new Crew(){Movie=movies[1],Person=people[1],Job="Yönetmen Yard."}

            };
            var casts = new List<Cast>()
            {
                new Cast(){Movie=movies[0],Person=people[0],Name="Oyuncu Adı 1",Character="Karakter 1"},
                new Cast(){Movie=movies[0],Person=people[0],Name="Oyuncu Adı 2",Character="Karakter 2"}
            };
            if (context.Database.GetPendingMigrations().Count() == 0) //bir database varmı yokmy ona bakıyoruz. database sayımız 0'a eşitse database oluştur.
            {
                if (context.Genres.Count() == 0)
                {
                    context.Genres.AddRange(genres);
                } //Genres'lere bakıyoruz eğer yoksa bu şekilde oluştur. varsa zaten devreye girmiyor. ilk tür bilgilerini oluşturuyoruz.
                if (context.Movies.Count() == 0)
                {
                    context.Movies.AddRange(movies);
                } //Movies'lere bakıyoruz eğer yoksa bu şekilde oluştur                

                if (context.Users.Count() == 0)
                {
                    context.Users.AddRange(users);
                }

                if (context.People.Count() == 0)
                {
                    context.People.AddRange(people);
                }
                if (context.Casts.Count() == 0)
                {
                    context.Casts.AddRange(casts);
                }
                if (context.Crews.Count() == 0)
                {
                    context.Crews.AddRange(crews);
                }
                context.SaveChanges();
            }
        }
    }
}
