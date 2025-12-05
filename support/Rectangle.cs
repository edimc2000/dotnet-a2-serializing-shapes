namespace SerializingShapes.support

{
    public class Rectangle: Shape
    {

        public required string Colour { get; set; }
        public required double Height{ get; set; }
        public required double Width { get; set; }

        public double Area => Height * Width; 

    }
}