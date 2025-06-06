namespace ApiFacebook.Dominio.Entities
{
    public class Anuncio
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public long AdsetId { get; set; }
        public string CreativeId { get; set; }
        public string Estado { get; set; }
    }
}