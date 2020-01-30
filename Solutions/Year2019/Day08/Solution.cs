using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Solutions.Year2019
{
    class Day08 : ASolution
    {
        private const char PIXEL_BLACK = '0';
        private const char PIXEL_WHITE = '1';
        private const char PIXEL_TRANSPARENT = '2';

        public Day08() : base(8, 2019, "") { }

        protected override string SolvePartOne()
        {
            var layers = ConvertImageIntoLayers(Input, 25, 6);
            var layerWithFewest0 = GetLayerWithFewestAmountOfCharacter(layers, '0');
            var amountOf1InLayer = FindAmountOfCharsInLayer(layerWithFewest0, '1');
            var amountOf2InLayer = FindAmountOfCharsInLayer(layerWithFewest0, '2');
            return (amountOf1InLayer * amountOf2InLayer).ToString();
        }

        protected override string SolvePartTwo()
        {
            var layers = ConvertImageIntoLayers(Input, 25, 6);
            return "\n" + DecodeImage(layers.ToList(), 25, 6);
        }

        public int FindAmountOfCharsInLayer(string layer, char value) => layer.Count(v => v == value);

        public string GetLayerWithFewestAmountOfCharacter(IEnumerable<string> layers, char value) =>
            layers.Aggregate((l1, l2) => l1.Count(v => v == value) < l2.Count(v => v == value) ? l1 : l2);

        public IEnumerable<string> ConvertImageIntoLayers(string input, int width, int length)
        {
            var createdBatches = 0;
            var batchSize = width * length;
            var maximumBatches = input.Length / batchSize;
            while(createdBatches < maximumBatches)
            {
                var batchSizeModifier = batchSize * createdBatches;

                var sb = new StringBuilder();
                for(var i = batchSizeModifier; i < batchSize + batchSizeModifier; i++)
                {
                    sb.Append(input[i]);
                }

                yield return sb.ToString();
                createdBatches++;
            }
        }

        public string DecodeImage(List<string> layers, int width, int length)
        {
            var image = new StringBuilder();
            var sizeOfSingleLayer = width * length;

            var currentWidth = 0;
            for(var i = 0; i < sizeOfSingleLayer; i++)
            {
                foreach(var layer in layers)
                {
                    if(layer[i] == PIXEL_TRANSPARENT)
                    {
                        continue;
                    }
                    else if (layer[i] == PIXEL_BLACK)
                    {
                        image.Append(' ');
                    }
                    else if(layer[i] == PIXEL_WHITE)
                    {
                        image.Append('#');
                    }

                    currentWidth++;
                    if (currentWidth == width)
                    {
                        currentWidth = 0;
                        image.Append("\n");
                    }

                    break;
                }
            }

            return image.ToString();
        }
    }
}
