namespace DB
{
    /// <summary>
    /// Class for serializing UI elements for storing UI elements. 
    /// </summary>
    public class ElementUI
    {
        public string Name { get; set; }
        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
    }
}