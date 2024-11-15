namespace WebUI.Models
{
    public class CreateProductRequest
    {

        public string Name { get; set; }


        public int Price { get; set; }

        public int Rating { get; set; }

        public int CategoryId { get; set; }
    }
}
