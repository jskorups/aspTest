using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_21032019.Models
{
    public class Komputer
    {
       
        public int Id { get; set; }
        public string Firma { get; set; }
        public string Dostawca { get; set; }
        public int UzytkownikId { get; set; }

        public string Uzytkownik { get; set; }


        public Komputer( string firma, string dostawca, int uzytkownikId)
        {
            Firma = firma;
            Dostawca = dostawca;
            UzytkownikId = uzytkownikId;
        }


        public Komputer()
        {

        }

        public static List<Komputer> fromKomputery (List<komputery> kList)
        {
            List<Komputer> komputeryList = kList.Select(x => new Komputer() { Firma = x.firma, Dostawca = x.dostawca, UzytkownikId = x.uzykownikId, Id = x.komputerId, Uzytkownik = x.uzytkownicy.Imie+" "+x.uzytkownicy.Nazwisko }).ToList();
            return komputeryList;

        }
        public Komputer(komputery comps)
        {
            Firma = comps.firma;
            Dostawca = comps.dostawca;
            UzytkownikId = comps.uzykownikId;
            Id = comps.komputerId;
        }



        public static komputery ConvertToKopmputery(Komputer p)
        {
            komputery k = new komputery();
            k.dostawca = p.Dostawca;
            k.firma = p.Firma;
            k.uzykownikId = p.UzytkownikId;
            k.komputerId = p.Id;
            return k;
        }

 




    }
}