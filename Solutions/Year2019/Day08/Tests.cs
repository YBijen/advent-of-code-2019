using System.Linq;
using Xunit;

namespace AdventOfCode.Solutions.Year2019
{
    public class Day08_Tests
    {
        private readonly Day08 _day08;

        public Day08_Tests()
        {
            _day08 = new Day08();
        }

        [Fact]
        public void Test_Grouping()
        {
            var image = "123456789012";
            var layers = _day08.ConvertImageIntoLayers(image, 3, 2);

            Assert.Equal(2, layers.Count());
            Assert.Equal("123456", layers.ElementAt(0));
            Assert.Equal("789012", layers.ElementAt(1));
        }

        [Fact]
        public void Test_GetLayerWithFewestAmountOfCharacters()
        {
            var image = "111456789012";
            var layers = _day08.ConvertImageIntoLayers(image, 3, 2);

            Assert.Equal("789012", _day08.GetLayerWithFewestAmountOfCharacter(layers, '1'));
        }

        [Fact]
        public void Test_FindAmountOfCharsInLayer()
        {
            var image = "111456789012";
            var layers = _day08.ConvertImageIntoLayers(image, 3, 2);
            var layer = layers.First();

            Assert.Equal(3, _day08.FindAmountOfCharsInLayer(layer, '1'));
        }

        [Fact]
        public void Test_Part1()
        {
            Assert.Equal("1463", _day08.Part1);
        }

        [Fact]
        public void Test_DecodeImage()
        {
            var image = "0222112222120000";
            var width = 2;
            var length = 2;

            var layers = _day08.ConvertImageIntoLayers(image, width, length);
            var decodedImage = _day08.DecodeImage(layers.ToList(), width, length);

            Assert.Equal(" #\n# \n", decodedImage);
        }
    }
}

