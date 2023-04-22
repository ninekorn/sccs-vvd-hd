using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;

using SharpDX;
//using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
//using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
//using System.Windows.Threading;

using System;

//using Win32.Shared;
using System.Diagnostics;
using SharpDX.DXGI;

using SharpDX.Direct2D1;
using SharpDX.Mathematics;
using System.Runtime.InteropServices;

/*
using Jitter.LinearMath;
using sccs.scgraphics;
using sccs.sccore;
using sccs.scconsole;
using WindowsInput;*/
using System.Threading.Tasks;
//using System.Speech.Recognition;
//using System.Speech.Synthesis;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text;
using System;
using System.Windows;
//using System.Windows.Interop;

//using Win32.DwmThumbnail.Interop;
//using Win32.Shared;
//using System.Windows.Controls;
//using Win32.DesktopDuplication;
using System;

//using Win32.Shared;
//using Win32.Shared.Interfaces;
//using Win32.Shared.Interop;


using Win32;
using Win32.DwmSharedSurface;

using Jitter;
using Jitter.Dynamics;
using Jitter.LinearMath;

using sccs.scgraphics;
using sccs.sccore;
using sccs.scconsole;

using WindowsInput;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



//using SharpDX;
using SharpDX.DirectInput;
using System.Threading;
using System.Runtime;
using System.Runtime.CompilerServices;
using sccs.scmessageobject;
using System.ComponentModel;
using Jitter.DataStructures;
using Jitter.Forces;
using System.Windows.Interop;
using SharpDX.Windows;


using sccsr14sc;

using System.Diagnostics;
using System.Runtime.InteropServices;

using SharpDX.RawInput;
using SharpDX.Multimedia;
using WinRT.GraphicsCapture;
using Win32.Shared.Interop;
using Win32.Shared;

using System.Runtime.Remoting.Messaging;
using Matrix = SharpDX.Matrix;


using System.Runtime.ConstrainedExecution;
using SCCoreSystems.SC_Graphics;
using SharpDX.DirectWrite;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing.Imaging;
using System.Drawing;

//using AForge;
//using AForge.Vision;
//using AForge.Imaging;




using System.IO;

using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Tensorflow.Keras.Engine;
// </SnippetAddUsings>


using Microsoft.Azure;

using Microsoft.Azure.Management.Compute.Models;




namespace sccs
{
    internal static unsafe class Program
    {

        public static IntPtr last_hWnd = IntPtr.Zero;


        [DllImport("user32.dll")]
        public static extern int FindWindow(
     string lpClassName, // class name 
     string lpWindowName // window name 
     );


        static System.Drawing.Bitmap lastbitmap0;
        static System.Drawing.Bitmap lastbitmap1;


        static System.Drawing.Bitmap lastsomebitmap;
        static System.Drawing.Bitmap lastnewbitmap;

        static int bitmapcounterVEVD = 0;
        static int perframetheimagerecogcounterswtcVEVD = 0;
        static int perframetheimagerecogcounterVEVD = 0;
        static int perframetheimagerecogcountermaxVEVD = 100;
        static int classifythreadswtcVEVD = 0;
        static Thread classifythreadVEVD;

        static Thread classifythread;
        static int classifythreadswtc;
        static int perframetheimagerecogcounterswtc = 0;
        static int perframetheimagerecogcounter = 0;
        static int perframetheimagerecogcountermax = 100;





        static int storecountgreenpixelsx = -1;
        static int storecountgreenpixelsy = -1;

        static int laststorecountgreenpixelsx = 0;
        static int laststorecountgreenpixelsy = 0;

        static System.Drawing.Bitmap newBmp;

        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/47c5a003-1d26-4213-9370-fba3aa170c21/fastest-method-to-convert-bitmap-object-to-byte-array?forum=csharpgeneral
        private static byte[] GetRGBValues(System.Drawing.Bitmap bmp)
        {

            // Lock the bitmap's bits. 
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
             bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
             bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes); bmp.UnlockBits(bmpData);

            return rgbValues;
        }










        //https://stackoverflow.com/questions/3115076/adjust-the-contrast-of-an-image-in-c-sharp-efficiently
        public static System.Drawing.Bitmap AdjustContrast(System.Drawing.Bitmap Image, float Value)
        {
            Value = (100.0f + Value) / 100.0f;
            Value *= Value;
            System.Drawing.Bitmap NewBitmap = (System.Drawing.Bitmap)Image.Clone();
            BitmapData data = NewBitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, NewBitmap.Width, NewBitmap.Height),
                ImageLockMode.ReadWrite,
                NewBitmap.PixelFormat);
            int Height = NewBitmap.Height;
            int Width = NewBitmap.Width;

            unsafe
            {
                for (int y = 0; y < Height; ++y)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
                    int columnOffset = 0;
                    for (int x = 0; x < Width; ++x)
                    {
                        byte B = row[columnOffset];
                        byte G = row[columnOffset + 1];
                        byte R = row[columnOffset + 2];

                        float Red = R / 255.0f;
                        float Green = G / 255.0f;
                        float Blue = B / 255.0f;
                        Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                        Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                        Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                        int iR = (int)Red;
                        iR = iR > 255 ? 255 : iR;
                        iR = iR < 0 ? 0 : iR;
                        int iG = (int)Green;
                        iG = iG > 255 ? 255 : iG;
                        iG = iG < 0 ? 0 : iG;
                        int iB = (int)Blue;
                        iB = iB > 255 ? 255 : iB;
                        iB = iB < 0 ? 0 : iB;

                        row[columnOffset] = (byte)iB;
                        row[columnOffset + 1] = (byte)iG;
                        row[columnOffset + 2] = (byte)iR;

                        columnOffset += 4;
                    }
                }
            }

            NewBitmap.UnlockBits(data);

            return NewBitmap;
        }


        public struct percentrecognitionstruct
        {
            public int zoomtypevevdswtc;
            public int zoomtypevevd;
            public float percentrecogfloat;
        }

        static percentrecognitionstruct[] percentrecognitiondata = new percentrecognitionstruct[1];
        static percentrecognitionstruct[] percentrecognitiondataVEVD = new percentrecognitionstruct[1];






        //static Stopwatch theimagerecogstopwatch = new Stopwatch();

        //MICROSOFT MACHINE LEARNING https://github.com/dotnet/samples
        //MICROSOFT MACHINE LEARNING https://github.com/dotnet/samples
        //MICROSOFT MACHINE LEARNING https://github.com/dotnet/samples
        //https://stackoverflow.com/questions/68490886/ml-net-image-classification-can-i-predict-an-image-using-byte-array-instead-o
        //https://github.com/dotnet/machinelearning-modelbuilder/issues/851

        // <SnippetDeclareGlobalVariables>
        static readonly string _assetsPath = System.IO.Path.Combine(Environment.CurrentDirectory, "assets");
        static string _imagesFolder = System.IO.Path.Combine(_assetsPath, "images");
        //static string _otherfolder = System.IO.Path.Combine(_assetsPath, "otherfolder");

        static string _imagesFolderVEVD = System.IO.Path.Combine(_assetsPath, "sccsvevirtualscreenimages");


        static readonly string _trainTagsTsvVEVD = System.IO.Path.Combine(_imagesFolderVEVD, "tags-vevd.tsv");
        static readonly string _testTagsTsvVEVD = System.IO.Path.Combine(_imagesFolderVEVD, "test-tags-vevd.tsv");





        static readonly string _trainTagsTsv = System.IO.Path.Combine(_imagesFolder, "tags.tsv");
        static readonly string _testTagsTsv = System.IO.Path.Combine(_imagesFolder, "test-tags.tsv");

        //static readonly string _predictSingleImage = System.IO.Path.Combine(_imagesFolder, "toaster3.jpg");
        //static readonly string _predictSingleImage1 = System.IO.Path.Combine(_imagesFolder, "0_5040.png");


        static readonly string _inceptionTensorFlowModel = System.IO.Path.Combine(_assetsPath, "inception", "tensorflow_inception_graph.pb");
        static readonly string _imagenettsv = System.IO.Path.Combine(_assetsPath, "inception", "imagenet.tsv");





        public class ModelInput
        {
            /*[ColumnName(@"Label")]
            public string Label { get; set; }

            [ColumnName(@"ImageSource")]
            public string ImageSource { get; set; }
            */
            [ColumnName(@"Label")]
            public string Label { get; set; }

            [ColumnName(@"Features")]
            public byte[] ImageBytes { get; set; }

        }

        public class ModelInputBytes
        {
            [ColumnName(@"Label")]
            public string Label { get; set; }

            [ColumnName(@"Features")]
            public byte[] ImageBytes { get; set; }

        }

        public class ModelOutput
        {
            [ColumnName("PredictedLabel")]
            public string Prediction { get; set; }

            public float[] Score { get; set; }
        }

        public static ModelOutput Predict(ModelInput input)
        {
            MLContext mlContext = new MLContext();


            // Load model & create prediction engine
            ITransformer mlModel = mlContext.Model.Load(_imagesFolder, out var modelInputSchema);
            //IDataView mlModel = mlContext.Data.LoadFromTextFile<ImageData>(path: _testTagsTsv, hasHeader: false);

            ITransformer dataPreProcessTransform = LoadImageFromFileTransformer(input, mlContext);

            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(dataPreProcessTransform.Append(mlModel));

            ModelOutput result = predEngine.Predict(input);
            return result;
        }

        public static ITransformer LoadImageFromFileTransformer(ModelInput input, MLContext mlContext)
        {
            var dataPreProcess = mlContext.Transforms.Conversion.MapValueToKey(@"Label", @"Label")
                                    .Append(mlContext.Transforms.LoadRawImageBytes(@"ImageSource_featurized", @"ImageSource"))
                                    .Append(mlContext.Transforms.CopyColumns(@"Features", @"ImageSource_featurized"));

            var dataView = mlContext.Data.LoadFromEnumerable(new[] { input });
            var dataPreProcessTransform = dataPreProcess.Fit(dataView);
            return dataPreProcessTransform;
        }

        public static ModelOutput PredictFromBytes(ModelInputBytes input)
        {
            //MLContext mlContext = new MLContext();
            // Load model & create prediction engine      

            //ITransformer mlModel = mlContext.Model.Load(_imagenettsv, out var modelInputSchema);

            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInputBytes, ModelOutput>(model);

            ModelOutput result = predEngine.Predict(input);
            return result;
        }


        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {


             var dataPreProcess = context.Transforms.Conversion.MapValueToKey(@"Label", @"Label")
                                     .Append(context.Transforms.LoadRawImageBytes(@"ImageSource_featurized", @"ImageSource"))
                                     .Append(context.Transforms.CopyColumns(@"Features", @"ImageSource_featurized"));

             /*var trainingPipeline = context.MulticlassClassification.Trainers.ImageClassification(labelColumnName: @"Label")
                                     .Append(context.Transforms.Conversion.MapKeyToValue(@"PredictedLabel", @"PredictedLabel"));

             var processedData = dataPreProcess.Fit(trainData).Transform(trainData);

             var model = trainingPipeline.Fit(processedData);*/
            
            return null;// model;
        }



        static ITransformer modelForVEVD;
        static MLContext mlContextForVEVD;
        static MLContext mlContext;
        static ITransformer model;



        // Build and train model
        public static ITransformer GenerateModel(MLContext mlContext)
        {


            //_imagesFolder = Environment.CurrentDirectory + @"\assets\images\";

            //Program.MessageBox((IntPtr)0, "_assetsPath" + _assetsPath + "/_inceptionTensorFlowModel:" + _inceptionTensorFlowModel, "sccsmsg",0);
            // <SnippetImageTransforms>
            //IEstimator<ITransformer> pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: _imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
            IEstimator<ITransformer> pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: _imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
            //IEstimator<ITransformer> pipeline = mlContext.Transforms.ConvertToImage(outputColumnName: "input", imagesFolder: _imagesFolder, inputColumnName: nameof(ImageData.ImagePath));  

            // The image transforms transform the images into the model's expected format.
            .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input", imageWidth: InceptionSettings.ImageWidth, imageHeight: InceptionSettings.ImageHeight, inputColumnName: "input"))
            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input", interleavePixelColors: InceptionSettings.ChannelsLast, offsetImage: InceptionSettings.Mean))
            // </SnippetImageTransforms>
            // The ScoreTensorFlowModel transform scores the TensorFlow model and allows communication
            // <SnippetScoreTensorFlowModel>
            .Append(mlContext.Model.LoadTensorFlowModel(_inceptionTensorFlowModel).
                ScoreTensorFlowModel(outputColumnNames: new[] { "softmax2_pre_activation" }, inputColumnNames: new[] { "input" }, addBatchDimensionInput: true))
            // </SnippetScoreTensorFlowModel>
            // <SnippetMapValueToKey>
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey", inputColumnName: "Label"))
            // </SnippetMapValueToKey>
            // <SnippetAddTrainer>
            .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "LabelKey", featureColumnName: "softmax2_pre_activation"))
            // </SnippetAddTrainer>
            // <SnippetMapKeyToValue>
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
            .AppendCacheCheckpoint(mlContext);
            // </SnippetMapKeyToValue>


            /*// <SnippetLoadData>
            IDataView trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: _trainTagsTsv, hasHeader: false);
            // </SnippetLoadData>

            // Train the model
            Console.WriteLine("=============== Training classification model ===============");
            // Create and train the model
            // <SnippetTrainModel>
            ITransformer model = pipeline.Fit(trainingData);


            
            mlContext.Model.Save(model, trainingData.Schema, Environment.CurrentDirectory + "model.zip");

            ITransformer mlModel = mlContext.Model.Load(Environment.CurrentDirectory + "model.zip", out var modelInputSchema);

            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInputBytes, ModelOutput>(mlModel);

            Program.MessageBox((IntPtr)0, "predEngine loaded:", "sccsmsg", 0);*/

            //ITransformer mlModel = mlContext.Model.Load(_imagenettsv, out var modelInputSchema);

            /*
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInputBytes, ModelOutput>(model);

            ModelOutput result = predEngine.Predict(input);*/



            
            // <SnippetLoadData>
            IDataView trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: _trainTagsTsv, hasHeader: false);
            // </SnippetLoadData>

            // Train the model
            Console.WriteLine("=============== Training classification model ===============");
            // Create and train the model
            // <SnippetTrainModel>
            ITransformer model = pipeline.Fit(trainingData);
            // </SnippetTrainModel>

            // Generate predictions from the test data, to be evaluated
            // <SnippetLoadAndTransformTestData>
            IDataView testData = mlContext.Data.LoadFromTextFile<ImageData>(path: _testTagsTsv, hasHeader: false);

            IDataView predictions = model.Transform(testData);

            // Create an IEnumerable for the predictions for displaying results
            IEnumerable<ImagePrediction> imagePredictionData = mlContext.Data.CreateEnumerable<ImagePrediction>(predictions, true);
            DisplayResults(imagePredictionData);
            // </SnippetLoadAndTransformTestData>

            // Get performance metrics on the model using training data
            Console.WriteLine("=============== Classification metrics ===============");

            // <SnippetEvaluate>
            MulticlassClassificationMetrics metrics =
                mlContext.MulticlassClassification.Evaluate(predictions,
                  labelColumnName: "LabelKey",
                  predictedLabelColumnName: "PredictedLabel");
            // </SnippetEvaluate>

            //<SnippetDisplayMetrics>
            Console.WriteLine($"LogLoss is: {metrics.LogLoss}");
            Console.WriteLine($"PerClassLogLoss is: {String.Join(" , ", metrics.PerClassLogLoss.Select(c => c.ToString()))}");
            //</SnippetDisplayMetrics>
            
            // <SnippetReturnModel>
            return model;
            // </SnippetReturnModel>
        }















        // Build and train model
        public static ITransformer GenerateModelForVEVirtualScreen(MLContext mlContext)
        {


            //_imagesFolder = Environment.CurrentDirectory + @"\assets\images\";

            //Program.MessageBox((IntPtr)0, "_assetsPath" + _assetsPath + "/_inceptionTensorFlowModel:" + _inceptionTensorFlowModel, "sccsmsg",0);
            // <SnippetImageTransforms>
            //IEstimator<ITransformer> pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: _imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
            IEstimator<ITransformer> pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: _imagesFolderVEVD, inputColumnName: nameof(ImageData.ImagePath))
            //IEstimator<ITransformer> pipeline = mlContext.Transforms.ConvertToImage(outputColumnName: "input", imagesFolder: _imagesFolder, inputColumnName: nameof(ImageData.ImagePath));  

            // The image transforms transform the images into the model's expected format.
            .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input", imageWidth: InceptionSettings.ImageWidth, imageHeight: InceptionSettings.ImageHeight, inputColumnName: "input"))
            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input", interleavePixelColors: InceptionSettings.ChannelsLast, offsetImage: InceptionSettings.Mean))
            // </SnippetImageTransforms>
            // The ScoreTensorFlowModel transform scores the TensorFlow model and allows communication
            // <SnippetScoreTensorFlowModel>
            .Append(mlContext.Model.LoadTensorFlowModel(_inceptionTensorFlowModel).
                ScoreTensorFlowModel(outputColumnNames: new[] { "softmax2_pre_activation" }, inputColumnNames: new[] { "input" }, addBatchDimensionInput: true))
            // </SnippetScoreTensorFlowModel>
            // <SnippetMapValueToKey>
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey", inputColumnName: "Label"))
            // </SnippetMapValueToKey>
            // <SnippetAddTrainer>
            .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "LabelKey", featureColumnName: "softmax2_pre_activation"))
            // </SnippetAddTrainer>
            // <SnippetMapKeyToValue>
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
            .AppendCacheCheckpoint(mlContext);
            // </SnippetMapKeyToValue>


            /*// <SnippetLoadData>
            IDataView trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: _trainTagsTsv, hasHeader: false);
            // </SnippetLoadData>

            // Train the model
            Console.WriteLine("=============== Training classification model ===============");
            // Create and train the model
            // <SnippetTrainModel>
            ITransformer model = pipeline.Fit(trainingData);


            
            mlContext.Model.Save(model, trainingData.Schema, Environment.CurrentDirectory + "model.zip");

            ITransformer mlModel = mlContext.Model.Load(Environment.CurrentDirectory + "model.zip", out var modelInputSchema);

            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInputBytes, ModelOutput>(mlModel);

            Program.MessageBox((IntPtr)0, "predEngine loaded:", "sccsmsg", 0);*/

            //ITransformer mlModel = mlContext.Model.Load(_imagenettsv, out var modelInputSchema);

            /*
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInputBytes, ModelOutput>(model);

            ModelOutput result = predEngine.Predict(input);*/




            // <SnippetLoadData>
            IDataView trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: _trainTagsTsvVEVD, hasHeader: false);
            // </SnippetLoadData>

            // Train the model
            Console.WriteLine("=============== Training classification model ===============");
            // Create and train the model
            // <SnippetTrainModel>
            ITransformer model = pipeline.Fit(trainingData);
            // </SnippetTrainModel>

            // Generate predictions from the test data, to be evaluated
            // <SnippetLoadAndTransformTestData>
            IDataView testData = mlContext.Data.LoadFromTextFile<ImageData>(path: _testTagsTsvVEVD, hasHeader: false);

            IDataView predictions = model.Transform(testData);

            // Create an IEnumerable for the predictions for displaying results
            IEnumerable<ImagePrediction> imagePredictionData = mlContext.Data.CreateEnumerable<ImagePrediction>(predictions, true);
            DisplayResults(imagePredictionData);
            // </SnippetLoadAndTransformTestData>

            // Get performance metrics on the model using training data
            Console.WriteLine("=============== Classification metrics ===============");

            // <SnippetEvaluate>
            MulticlassClassificationMetrics metrics =
                mlContext.MulticlassClassification.Evaluate(predictions,
                  labelColumnName: "LabelKey",
                  predictedLabelColumnName: "PredictedLabel");
            // </SnippetEvaluate>

            //<SnippetDisplayMetrics>
            Console.WriteLine($"LogLoss is: {metrics.LogLoss}");
            Console.WriteLine($"PerClassLogLoss is: {String.Join(" , ", metrics.PerClassLogLoss.Select(c => c.ToString()))}");
            //</SnippetDisplayMetrics>

            // <SnippetReturnModel>
            return model;
            // </SnippetReturnModel>
        }





        public static float ClassifySingleImageVEVD(MLContext mlContext, ITransformer model, string filename)
        {

            string _predictSingleImage2 = System.IO.Path.Combine(_imagesFolderVEVD, filename);


            // load the fully qualified image file name into ImageData
            // <SnippetLoadImageData>
            var imageData = new ImageData()
            {
                ImagePath = _predictSingleImage2 // _predictSingleImage
            };
            // </SnippetLoadImageData>

            // <SnippetPredictSingle>
            // Make prediction function (input = ImageData, output = ImagePrediction)
            var predictor = mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);
            var prediction = predictor.Predict(imageData);
            // </SnippetPredictSingle>

            //Console.WriteLine("=============== Making single image classification ===============");
            // <SnippetDisplayPrediction>
            Console.WriteLine($"Image: {System.IO.Path.GetFileName(imageData.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");
            // </SnippetDisplayPrediction>

            return prediction.Score.Max();
        }







        public static float ClassifySingleImage(MLContext mlContext, ITransformer model, string filename)
        {

            string _predictSingleImage2 = System.IO.Path.Combine(_imagesFolder, filename);


            // load the fully qualified image file name into ImageData
            // <SnippetLoadImageData>
            var imageData = new ImageData()
            {
                ImagePath = _predictSingleImage2 // _predictSingleImage
            };
            // </SnippetLoadImageData>

            // <SnippetPredictSingle>
            // Make prediction function (input = ImageData, output = ImagePrediction)
            var predictor = mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);
            var prediction = predictor.Predict(imageData);
            // </SnippetPredictSingle>

            //Console.WriteLine("=============== Making single image classification ===============");
            // <SnippetDisplayPrediction>
            //Console.WriteLine($"Image: {System.IO.Path.GetFileName(imageData.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");
            // </SnippetDisplayPrediction>

            return prediction.Score.Max();
        }

        private static void DisplayResults(IEnumerable<ImagePrediction> imagePredictionData)
        {
            // <SnippetDisplayPredictions>
            foreach (ImagePrediction prediction in imagePredictionData)
            {
                Console.WriteLine($"Image: {System.IO.Path.GetFileName(prediction.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");
            }
            // </SnippetDisplayPredictions>
        }

        // <SnippetInceptionSettings>
        private struct InceptionSettings
        {
            public const int ImageHeight = 224;
            public const int ImageWidth = 224;
            public const float Mean = 117;
            public const float Scale = 1;
            public const bool ChannelsLast = true;
        }
        // </SnippetInceptionSettings>

        // <SnippetDeclareImageData>
        public class ImageData
        {
            [LoadColumn(0)]
            public string ImagePath;

            [LoadColumn(1)]
            public string Label;
        }
        // </SnippetDeclareImageData>

        // <SnippetDeclareImagePrediction>
        public class ImagePrediction : ImageData
        {
            public float[] Score;

            public string PredictedLabelValue;
        }
        // </SnippetDeclareImagePrediction>
        //MICROSOFT MACHINE LEARNING https://github.com/dotnet/samples
        //MICROSOFT MACHINE LEARNING https://github.com/dotnet/samples
        //MICROSOFT MACHINE LEARNING https://github.com/dotnet/samples













        static int onboardcpuiconw = 50;
        static int onboardcpuiconh = 40;

        static int onboardcpuiconmodw = 50;
        static int onboardcpuiconmodh = 40;
        static System.Drawing.Bitmap dstImg;


        //https://stackoverflow.com/questions/734930/how-do-i-crop-an-image-using-c
        public static System.Drawing.Bitmap cropAtRect(this System.Drawing.Bitmap b, System.Drawing.Rectangle r)
        {
            using (var nb = new System.Drawing.Bitmap(r.Width, r.Height))
            {
                using (Graphics g = Graphics.FromImage(nb))
                {
                    g.DrawImage(b, -r.X, -r.Y);
                    return nb;
                }
            }
        }


        static System.Drawing.Bitmap somebitmap;
        static BitmapData bmpData;
        static int _bytesTotalobcpu = 0;
        static System.Drawing.Bitmap bitmaponboardcomputer;
        static byte[] _textureByteArrayobcpu;
        static int columnsonboardcomputer = 0;
        static int rowsonboardcomputer = 0;
        static int memoryBitmapStrideonboardcomputer = 0;
        static Texture2D theonboardcomputertextureFINAL;
        static int bitmaponboardcomputercounter = 0;

        public static bool IsWindowTopMost(IntPtr hWnd)
        {
            int exStyle = sccsr14sc.Form1.GetWindowLong(hWnd, GWL_EXSTYLE);
            return (exStyle & WS_EX_TOPMOST) == WS_EX_TOPMOST;
        }




        //https://stackoverflow.com/questions/9503027/p-pnvoke-setfocus-to-a-particular-control
        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThread();
        [DllImport("user32.dll")]
        static extern bool AttachThreadInput(uint idAttach, uint idAttachTo,
   bool fAttach);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(HandleRef hWnd);
        /// <summary>
        ///     Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is
        ///     directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher
        ///     priority to the thread that created the foreground window than it does to other threads.
        ///     <para>See for https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539%28v=vs.85%29.aspx more information.</para>
        /// </summary>
        /// <param name="hWnd">
        ///     C++ ( hWnd [in]. Type: HWND )<br />A handle to the window that should be activated and brought to the foreground.
        /// </param>
        /// <returns>
        ///     <c>true</c> or nonzero if the window was brought to the foreground, <c>false</c> or zero If the window was not
        ///     brought to the foreground.
        /// </returns>
        /// <remarks>
        ///     The system restricts which processes can set the foreground window. A process can set the foreground window only if
        ///     one of the following conditions is true:
        ///     <list type="bullet">
        ///     <listheader>
        ///         <term>Conditions</term><description></description>
        ///     </listheader>
        ///     <item>The process is the foreground process.</item>
        ///     <item>The process was started by the foreground process.</item>
        ///     <item>The process received the last input event.</item>
        ///     <item>There is no foreground process.</item>
        ///     <item>The process is being debugged.</item>
        ///     <item>The foreground process is not a Modern Application or the Start Screen.</item>
        ///     <item>The foreground is not locked (see LockSetForegroundWindow).</item>
        ///     <item>The foreground lock time-out has expired (see SPI_GETFOREGROUNDLOCKTIMEOUT in SystemParametersInfo).</item>
        ///     <item>No menus are active.</item>
        ///     </list>
        ///     <para>
        ///     An application cannot force a window to the foreground while the user is working with another window.
        ///     Instead, Windows flashes the taskbar button of the window to notify the user.
        ///     </para>
        ///     <para>
        ///     A process that can set the foreground window can enable another process to set the foreground window by
        ///     calling the AllowSetForegroundWindow function. The process specified by dwProcessId loses the ability to set
        ///     the foreground window the next time the user generates input, unless the input is directed at that process, or
        ///     the next time a process calls AllowSetForegroundWindow, unless that process is specified.
        ///     </para>
        ///     <para>
        ///     The foreground process can disable calls to SetForegroundWindow by calling the LockSetForegroundWindow
        ///     function.
        ///     </para>
        /// </remarks>
        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        public static void SetFocus(IntPtr hwndTarget, string childClassName)
        {

            IntPtr targetThreadID = GetWindowThreadProcessId(hwndTarget, IntPtr.Zero); //target thread id
            IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id
            try
            {
                IntPtr hFocus = IntPtr.Zero;
                IntPtr hFore;
                uint id = 0;
                hFore = GetForegroundWindow();
                var idAttach = GetWindowThreadProcessId(hFore, out id);
                var curThreadId = GetCurrentThreadId();
                // To attach to current thread
                bool lRet = AttachThreadInput(idAttach, curThreadId, true);
                // To dettach from current thread
                //AttachThreadInput(idAttach, curThreadId, false);


                // if it's not already in the foreground...
                lRet = BringWindowToTop(hwndTarget);
                SetForegroundWindow(hwndTarget);


                // if you know the child win class name do something like this (enumerate windows using Win API again)...
                /*var hwndChild = EnumAllWindows(hwndTarget, childClassName).FirstOrDefault();

                if (hwndChild == IntPtr.Zero)
                {
                    // or use keyboard etc. to focus, i.e. send keys/input...
                    // SendInput (...);
                    return;
                }
                */
                // you can use also the edit control's hwnd or some child window (of target) here
                sccsr14sc.Form1.SetFocus(hwndTarget); // hwndTarget);

            }
            finally
            {
                IntPtr hFocus = IntPtr.Zero;
                IntPtr hFore;
                uint id = 0;
                hFore = GetForegroundWindow();
                var idAttach = GetWindowThreadProcessId(hFore, out id);
                var curThreadId = GetCurrentThreadId();

                // To dettach from current thread
                AttachThreadInput(idAttach, curThreadId, false);


            }




            // hwndTarget is the other app's main window 
            // ...
            /*IntPtr targetThreadID = GetWindowThreadProcessId(hwndTarget, IntPtr.Zero); //target thread id
            IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id
            try
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, -1); // attach current thread id to target window

                // if it's not already in the foreground...
                lRet = BringWindowToTop(hwndTarget);
                SetForegroundWindow(hwndTarget);

                // if you know the child win class name do something like this (enumerate windows using Win API again)...
                var hwndChild = EnumAllWindows(hwndTarget, childClassName).FirstOrDefault();

                if (hwndChild == IntPtr.Zero)
                {
                    // or use keyboard etc. to focus, i.e. send keys/input...
                    // SendInput (...);
                    return;
                }

                // you can use also the edit control's hwnd or some child window (of target) here
                SetFocus(hwndChild); // hwndTarget);
            }
            finally
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, 0); //detach from foreground window
            }*/
        }

        public static void UnSetFocus(IntPtr hwndTarget, string childClassName)
        {

            //IntPtr targetThreadID = GetWindowThreadProcessId(hwndTarget, IntPtr.Zero); //target thread id
            //IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id

            IntPtr hFocus = IntPtr.Zero;
            IntPtr hFore;
            uint id = 0;
            hFore = GetForegroundWindow();
            var idAttach = GetWindowThreadProcessId(hFore, out id);
            var curThreadId = GetCurrentThreadId();

            // To dettach from current thread
            AttachThreadInput(idAttach, curThreadId, false);




            // hwndTarget is the other app's main window 
            // ...
            /*IntPtr targetThreadID = GetWindowThreadProcessId(hwndTarget, IntPtr.Zero); //target thread id
            IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id
            try
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, -1); // attach current thread id to target window

                // if it's not already in the foreground...
                lRet = BringWindowToTop(hwndTarget);
                SetForegroundWindow(hwndTarget);

                // if you know the child win class name do something like this (enumerate windows using Win API again)...
                var hwndChild = EnumAllWindows(hwndTarget, childClassName).FirstOrDefault();

                if (hwndChild == IntPtr.Zero)
                {
                    // or use keyboard etc. to focus, i.e. send keys/input...
                    // SendInput (...);
                    return;
                }

                // you can use also the edit control's hwnd or some child window (of target) here
                SetFocus(hwndChild); // hwndTarget);
            }
            finally
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, 0); //detach from foreground window
            }*/
        }
        internal struct WINDOWINFO
        {
            public uint ownerpid;
            public uint childpid;
        }

        #region User32
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        // When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        /// <summary>
        /// Delegate for the EnumChildWindows method
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to bail.</returns>
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowProc lpEnumFunc, IntPtr lParam);
        #endregion

        #region Kernel32
        public const UInt32 PROCESS_QUERY_INFORMATION = 0x400;
        public const UInt32 PROCESS_VM_READ = 0x010;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] int dwFlags, [Out] StringBuilder lpExeName, ref int lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(
            UInt32 dwDesiredAccess,
            [MarshalAs(UnmanagedType.Bool)]
            Boolean bInheritHandle,
            Int32 dwProcessId
        );
        #endregion
        public static string GetProcessName(IntPtr hWnd)
        {
            string processName = null;

            //hWnd = GetForegroundWindow();

            if (hWnd == IntPtr.Zero)
                return null;

            uint pID;
            GetWindowThreadProcessId(hWnd, out pID);

            IntPtr proc;
            if ((proc = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, (int)pID)) == IntPtr.Zero)
                return null;

            int capacity = 2000;
            StringBuilder sb = new StringBuilder(capacity);
            QueryFullProcessImageName(proc, 0, sb, ref capacity);

            processName = sb.ToString(0, capacity);

            // UWP apps are wrapped in another app called, if this has focus then try and find the child UWP process
            if (System.IO.Path.GetFileName(processName).Equals("ApplicationFrameHost.exe"))
            {
                processName = UWP_AppName(hWnd, pID);
            }

            return processName;
        }
        #region Get UWP Application Name

        /// <summary>
        /// Find child process for uwp apps, edge, mail, etc.
        /// </summary>
        /// <param name="hWnd">hWnd</param>
        /// <param name="pID">pID</param>
        /// <returns>The application name of the UWP.</returns>
        private static string UWP_AppName(IntPtr hWnd, uint pID)
        {
            WINDOWINFO windowinfo = new WINDOWINFO();
            windowinfo.ownerpid = pID;
            windowinfo.childpid = windowinfo.ownerpid;

            IntPtr pWindowinfo = Marshal.AllocHGlobal(Marshal.SizeOf(windowinfo));

            Marshal.StructureToPtr(windowinfo, pWindowinfo, false);

            EnumWindowProc lpEnumFunc = new EnumWindowProc(EnumChildWindowsCallback);
            EnumChildWindows(hWnd, lpEnumFunc, pWindowinfo);

            windowinfo = (WINDOWINFO)Marshal.PtrToStructure(pWindowinfo, typeof(WINDOWINFO));

            IntPtr proc;
            if ((proc = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, (int)windowinfo.childpid)) == IntPtr.Zero)
                return null;

            int capacity = 2000;
            StringBuilder sb = new StringBuilder(capacity);
            QueryFullProcessImageName(proc, 0, sb, ref capacity);

            Marshal.FreeHGlobal(pWindowinfo);

            return sb.ToString(0, capacity);
        }

        /// <summary>
        /// Callback for enumerating the child windows.
        /// </summary>
        /// <param name="hWnd">hWnd</param>
        /// <param name="lParam">lParam</param>
        /// <returns>always <c>true</c>.</returns>
        private static bool EnumChildWindowsCallback(IntPtr hWnd, IntPtr lParam)
        {
            WINDOWINFO info = (WINDOWINFO)Marshal.PtrToStructure(lParam, typeof(WINDOWINFO));

            uint pID;
            GetWindowThreadProcessId(hWnd, out pID);

            if (pID != info.ownerpid)
                info.childpid = pID;

            Marshal.StructureToPtr(info, lParam, true);

            return true;
        }
        #endregion






        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);


        

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        [DllImport("gdi32.dll")]

        static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);



        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern long GetWindowLongPtr(IntPtr hWnd, int nIndex);












        public enum WindowLongParam
        {
            /// <summary>Sets a new address for the window procedure.</summary>
            /// <remarks>You cannot change this attribute if the window does not belong to the same process as the calling thread.</remarks>
            GWL_WNDPROC = -4,

            /// <summary>Sets a new application instance handle.</summary>
            GWLP_HINSTANCE = -6,

            GWLP_HWNDPARENT = -8,

            /// <summary>Sets a new identifier of the child window.</summary>
            /// <remarks>The window cannot be a top-level window.</remarks>
            GWL_ID = -12,

            /// <summary>Sets a new window style.</summary>
            GWL_STYLE = -16,

            /// <summary>Sets a new extended window style.</summary>
            /// <remarks>See <see cref="ExWindowStyles"/>.</remarks>
            GWL_EXSTYLE = -20,

            /// <summary>Sets the user data associated with the window.</summary>
            /// <remarks>This data is intended for use by the application that created the window. Its value is initially zero.</remarks>
            GWL_USERDATA = -21,

            /// <summary>Sets the return value of a message processed in the dialog box procedure.</summary>
            /// <remarks>Only applies to dialog boxes.</remarks>
            DWLP_MSGRESULT = 0,

            /// <summary>Sets new extra information that is private to the application, such as handles or pointers.</summary>
            /// <remarks>Only applies to dialog boxes.</remarks>
            DWLP_USER = 8,

            /// <summary>Sets the new address of the dialog box procedure.</summary>
            /// <remarks>Only applies to dialog boxes.</remarks>
            DWLP_DLGPROC = 4
        }
        /*[DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        [DllImport("gdi32.dll")]

        static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
        */
        /*[DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_TOPMOST = 0x0008;
        */

        static Stopwatch thestopwatch = new Stopwatch();


        public static IntPtr lastcapturedhwnd;
        public static string lastcapturedwindowname;

        public static int typeofwindowpicker = 0;

        static int firstframededicatedbackgroundworker = 0;
        static int firstframededicatedthread = 0;

        public static string lastcapturetypevalue = "";
        static int hasstartedcapture = 0;
        static int initform = 0;
        public static int iswtchingcapturetypesmaybe = 0;

        static int isfullscreen = 0;

        internal struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        /*[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetForegroundWindow();
        */
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        /// <summary>
        ///     The MoveWindow function changes the position and dimensions of the specified window. For a top-level window, the
        ///     position and dimensions are relative to the upper-left corner of the screen. For a child window, they are relative
        ///     to the upper-left corner of the parent window's client area.
        ///     <para>
        ///     Go to https://msdn.microsoft.com/en-us/library/windows/desktop/ms633534%28v=vs.85%29.aspx for more
        ///     information
        ///     </para>
        /// </summary>
        /// <param name="hWnd">C++ ( hWnd [in]. Type: HWND )<br /> Handle to the window.</param>
        /// <param name="X">C++ ( X [in]. Type: int )<br />Specifies the new position of the left side of the window.</param>
        /// <param name="Y">C++ ( Y [in]. Type: int )<br /> Specifies the new position of the top of the window.</param>
        /// <param name="nWidth">C++ ( nWidth [in]. Type: int )<br />Specifies the new width of the window.</param>
        /// <param name="nHeight">C++ ( nHeight [in]. Type: int )<br />Specifies the new height of the window.</param>
        /// <param name="bRepaint">
        ///     C++ ( bRepaint [in]. Type: bool )<br />Specifies whether the window is to be repainted. If this
        ///     parameter is TRUE, the window receives a message. If the parameter is FALSE, no repainting of any kind occurs. This
        ///     applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the
        ///     parent window uncovered as a result of moving a child window.
        /// </param>
        /// <returns>
        ///     If the function succeeds, the return value is nonzero.<br /> If the function fails, the return value is zero.
        ///     <br />To get extended error information, call GetLastError.
        /// </returns>
        /*[DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        */

        public static int _useOculusRift = 0;
        public static int createconsole = 0; //put app solution in console mode instead of window mode. // hide mode == 1 // showmode == 0

        [return: MarshalAs(UnmanagedType.Bool)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CloseWindowStation(IntPtr hWinsta);

        [DllImport("user32.dll")]
        static extern bool CloseWindow(IntPtr hWnd);

        /*
        //these are imports that make the necesary WinAPI calls available
        #region pinvoke stuff
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestor_Flags gaFlags);

        private enum GetAncestor_Flags { GetParent = 1, GetRoot = 2, GetRootOwner = 3 }

        [DllImport("user32.dll")]
        private static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetTitleBarInfo(IntPtr hwnd, ref TITLEBARINFO pti);

        [StructLayout(LayoutKind.Sequential)]
        struct TITLEBARINFO
        {
            public const int CCHILDREN_TITLEBAR = 5;
            public uint cbSize;
            public RECT rcTitleBar;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHILDREN_TITLEBAR + 1)]
            public uint[] rgstate;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT { public int Left, Top, Right, Bottom; }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int smIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, UIntPtr dwNewLong)
        {
            if (IntPtr.Size == 8) { return SetWindowLongPtr64(hWnd, nIndex, dwNewLong); }
            else { return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToUInt32())); }
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, UIntPtr dwNewLong);

        #endregion

        //which window title is selected in the listbox
        private string SelectedTitle = null;

        //initialize
        public Form1()
        {
            InitializeComponent();
            refresh();
        }

        //user selects an item in the listbox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTitle = listBox1.SelectedItem.ToString();
            button1.Enabled = true;
        }

        //user clicks 'Refresh' button
        private void button2_Click(object sender, EventArgs e)
        {
            refresh();
        }

        //user clicks 'Activate' button
        private void button1_Click(object sender, EventArgs e)
        {
            executeModeChange();
            refresh();
        }

        //Remove listbox selection state, Scan for windows, refresh contents of listbox
        public void refresh()
        {
            SelectedTitle = null;
            button1.Enabled = false;
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            EnumWindows(enumWindowProc, IntPtr.Zero);
            listBox1.EndUpdate();
        }

        //attempt to change the selected item to borderless windowed "mode"
        private void executeModeChange()
        {
            //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
            IntPtr hWnd = FindWindow(null, SelectedTitle);
            if (hWnd == null) { MessageBox.Show("Failed to acquire window handle.\nPlease try again."); }
            //set the window to a borderless style
            const int GWL_STYLE = -16; //want to change the window style
            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
            IntPtr sult = SetWindowLongPtr(hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);
            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                MessageBox.Show("Unable to alter window style.\nSorry.");
                return;
            }
            //otherwise we need to resize and reposition the window to take up the full screen
            const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
            int screenWidth = GetSystemMetrics(0);
            int screenHeight = GetSystemMetrics(1);
            SetWindowPos(hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
        }

        //this is the procedure passed to the window enumerator so that it can identify the active windows, extract their titles, and add them to the listbox
        private bool enumWindowProc(IntPtr hWnd, IntPtr lParam)
        {
            //ignore self
            if (hWnd == this.Handle) { return true; }

            //if the window has a titlebar title and can be alt-tabbed to then it's probably one we want to list
            int size = GetWindowTextLength(hWnd);
            if (size++ > 0 && isAltTabWindow(hWnd))
            {
                //grab the window's title
                StringBuilder sb = new StringBuilder(size);
                GetWindowText(hWnd, sb, size);
                //add it to the list
                listBox1.Items.Add(sb.ToString());
            }
            return true;
        }

        //This function checks to see if a window is alt-tabbable.
        //It sometimes returns false positives for tray icons and similar, but
        //it's never caused any real problems.
        private bool isAltTabWindow(IntPtr hWnd)
        {
            //ignore windows that aren't visible
            if (!IsWindowVisible(hWnd)) { return false; }

            //check to see if this window is its own root owner
            //derived from R.Chen's method here:
            //https://blogs.msdn.microsoft.com/oldnewthing/20071008-00/?p=24863/
            IntPtr hWndWalk = IntPtr.Zero;
            IntPtr hWndTry = GetAncestor(hWnd, GetAncestor_Flags.GetRootOwner);
            while (hWndTry != hWndWalk)
            {
                hWndWalk = hWndTry;
                hWndTry = GetLastActivePopup(hWndWalk);
                if (IsWindowVisible(hWndTry)) { break; }
            }
            if (hWndWalk != hWnd) { return false; }

            //fetch the properties of the title bar
            TITLEBARINFO ti = new TITLEBARINFO();
            ti.cbSize = (uint)Marshal.SizeOf(ti);
            GetTitleBarInfo(hWnd, ref ti);
            //if the title bar is set to invisible then we don't want this window
            const uint STATE_SYSTEM_INVISIBLE = 0x8000;
            if (ti.rgstate[0] == STATE_SYSTEM_INVISIBLE) { return false; }

            //if the window style is the one used for a floating toolbar then we don't want this window
            const int GWL_EXSTYLE = -20;
            const int WS_EX_TOOLWINDOW = 0x80;
            if (GetWindowLong(hWnd, GWL_EXSTYLE) == WS_EX_TOOLWINDOW) { return false; }

            return true;
        }

        //adjust the emelements if the form gets resized
        private void Form1_Resize(object sender, EventArgs e)
        {
            Control form = (Control)sender;
            //These magic numbers are just sizes for margins in the form.
            //It seemed needlessly pedantic to give them their own variables in this case.
            listBox1.Size = new Size(form.Size.Width - pixelcheckx, form.Size.Height - 100);
            button1.Location = new Point(form.Size.Width - 180, form.Size.Height - 82);
            button2.Location = new Point(button2.Location.X, form.Size.Height - 82);
        }*/



        //these are imports that make the necesary WinAPI calls available
        #region pinvoke stuff
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        /*[DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        */
        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        /*[DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        */
        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestor_Flags gaFlags);

        private enum GetAncestor_Flags { GetParent = 1, GetRoot = 2, GetRootOwner = 3 }

        [DllImport("user32.dll")]
        private static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetTitleBarInfo(IntPtr hwnd, ref TITLEBARINFO pti);

        [StructLayout(LayoutKind.Sequential)]
        struct TITLEBARINFO
        {
            public const int CCHILDREN_TITLEBAR = 5;
            public uint cbSize;
            public RECT rcTitleBar;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHILDREN_TITLEBAR + 1)]
            public uint[] rgstate;
        }

        /*[StructLayout(LayoutKind.Sequential)]
        public struct RECT { public int Left, Top, Right, Bottom; }*/

        /*[DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        */
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int smIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, UIntPtr dwNewLong)
        {
            if (IntPtr.Size == 8) { return SetWindowLongPtr64(hWnd, nIndex, dwNewLong); }
            else { return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToUInt32())); }
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, UIntPtr dwNewLong);

        #endregion

        //which window title is selected in the listbox
        public static string SelectedTitle = null;

        //initialize
        /* public Form1()
         {
             InitializeComponent();
             refresh();
         }

         //user selects an item in the listbox
         private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
         {
             SelectedTitle = listBox1.SelectedItem.ToString();
             button1.Enabled = true;
         }

         //user clicks 'Refresh' button
         private void button2_Click(object sender, EventArgs e)
         {
             refresh();
         }

         //user clicks 'Activate' button
         private void button1_Click(object sender, EventArgs e)
         {
             executeModeChange();
             refresh();
         }*/

        //Remove listbox selection state, Scan for windows, refresh contents of listbox
        /*public void refresh()
        {
            SelectedTitle = null;
            button1.Enabled = false;
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            EnumWindows(enumWindowProc, IntPtr.Zero);
            listBox1.EndUpdate();
        }*/

        //attempt to change the selected item to borderless windowed "mode"
        public static void executeModeChange(IntPtr handleofcapturedwindow)
        {

            sccs.Program.MessageBox((IntPtr)0, "Program.csline1640=>SelectedTitle:" + SelectedTitle, "scmsg", 0);

            //Program.MessageBox((IntPtr)0, "executeModeChange start " + SelectedTitle, "sccsmsg", 0);


            //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
            IntPtr hWnd = scdirectx.FindWindow(null, SelectedTitle);
            if (hWnd == null)
            {
                //MessageBox.Show("Failed to acquire window handle.\nPlease try again.");
                //Console.WriteLine("Failed to acquire window handle.\nPlease try again.");
                //Program.MessageBox((IntPtr)0, "Failed to acquire window handle.\nPlease try again." + SelectedTitle, "sccsmsg", 0);
            }

            //set the window to a borderless style
            const int GWL_STYLE = -16; //want to change the window style
            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
            //IntPtr sult = SetWindowLongPtr(hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);
            IntPtr sult = SetWindowLongPtr(handleofcapturedwindow, GWL_STYLE, (UIntPtr)WS_POPUP);


            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                //MessageBox.Show("Unable to alter window style.\nSorry.");
                Console.WriteLine("Unable to alter window style.\nSorry.");
                //Program.MessageBox((IntPtr)0, "Unable to alter window style.\nSorry." + SelectedTitle, "sccsmsg", 0);

                return;
            }
            //otherwise we need to resize and reposition the window to take up the full screen
            const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
            int screenWidth = GetSystemMetrics(0);
            int screenHeight = GetSystemMetrics(1);
            SetWindowPos(hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
            //Program.MessageBox((IntPtr)0, "executeModeChange end " + SelectedTitle, "sccsmsg", 0);

            //progcanpause = 1;

        }

        //static int lastscreencapturetype = 0;

        //this is the procedure passed to the window enumerator so that it can identify the active windows, extract their titles, and add them to the listbox
        public static bool enumWindowProc(IntPtr hWnd, IntPtr lParam)
        {



            //Program.MessageBox((IntPtr)0, "Program.csline1689=>Program.vewindowsfoundedz:" + Program.vewindowsfoundedz + "/enumwindow:" + hWnd, "sccsmsg", 0);

            //Console.WriteLine("cwindow:" + Program.vewindowsfoundedz + "/enumwindow:" + hWnd);

            //if (hWnd == Program.vewindowsfoundedz)
            {
                //Program.MessageBox((IntPtr)0, "cwindow:" + Program.vewindowsfoundedz + "/enumwindow:" + hWnd, "sccsmsg", 0);


                if (screencapturetype == 0)
                {
                    capturedwindowname = ((GraphicsCapture)updatescript.captureMethod as GraphicsCapture).capturedwindowname;
                    SelectedTitle = ((GraphicsCapture)updatescript.captureMethod as GraphicsCapture).capturedwindowname;
                }
                else if (screencapturetype == 1)
                {
                    capturedwindowname = ((DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface).capturedwindowname;
                    SelectedTitle = ((DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface).capturedwindowname;
                }


                //ignore self
                if (hWnd == sccsr14sc.Form1.theHandle)
                {
                    return true;
                             
                }





                //if the window has a titlebar title and can be alt-tabbed to then it's probably one we want to list
                int size = GetWindowTextLength(hWnd);
                if (size++ > 0 && isAltTabWindow(hWnd))
                {
                    //Program.MessageBox((IntPtr)0, "" + SelectedTitle, "sccsmsg", 0);
                    //grab the window's title
                    StringBuilder sb = new StringBuilder(size);
                    GetWindowText(hWnd, sb, size);
                    //add it to the list
                    //listBox1.Items.Add(sb.ToString());

                    //Program.MessageBox((IntPtr)0, "" + SelectedTitle + " / " + sb.ToString(), "sccsmsg", 0);

                    /*int iHandle = FindWindow(null, sb.ToString());// "VoidExpanse");
                    SelectedTitle = sb.ToString();
                    //Console.WriteLine("iHandle:" + iHandle + "/title:" + sb.ToString());
                    Program.MessageBox((IntPtr)0, "iHandle:" + iHandle + "/title:" + sb.ToString(), "sccsmsg", 0);
                    */

                    //Program.MessageBox((IntPtr)0, "isequalornot0line1736:" + SelectedTitle + " / " + sb.ToString(), "sccsmsg", 0);


                    if (sb.ToString() == SelectedTitle)
                    {

                        vewindowsfoundedz = hWnd;

                        /*if (sccsr14sc.Form1.capturedprogram == vewindowsfoundedz)
                        {
                            //Program.MessageBox((IntPtr)0, "isequalornot1line1749=>sccsr14sc.Form1.capturedprogram == vewindowsfoundedz:" + SelectedTitle + " / " + sb.ToString(), "sccsmsg", 0);

                            sccsr14sc.Form1.capturedprogram = vewindowsfoundedz;
                        }
                        else
                        {
                            //Program.MessageBox((IntPtr)0, "isequalornot1line1749=>sccsr14sc.Form1.capturedprogram != vewindowsfoundedz:" + SelectedTitle + " / " + sb.ToString(), "sccsmsg", 0);
                            sccsr14sc.Form1.capturedprogram = vewindowsfoundedz;

                        }*/
                        //Program.MessageBox((IntPtr)0, "isequalornot1line1746:" + SelectedTitle + " / " + sb.ToString(), "sccsmsg", 0);

                        //executeModeChange(hWnd);

                        //Console.WriteLine("executeModeChange:");
                        //EnumWindows(enumWindowProc, IntPtr.Zero);



                        /*
                        if (hWnd == vewindowsfoundedz)
                        {
                            executeModeChange();
                            //EnumWindows(enumWindowProc, IntPtr.Zero);
                        }*/
                    }
                }
            }



            return true;
        }

        //This function checks to see if a window is alt-tabbable.
        //It sometimes returns false positives for tray icons and similar, but
        //it's never caused any real problems.
        public static bool isAltTabWindow(IntPtr hWnd)
        {
            //ignore windows that aren't visible
            if (!IsWindowVisible(hWnd)) { return false; }

            //check to see if this window is its own root owner
            //derived from R.Chen's method here:
            //https://blogs.msdn.microsoft.com/oldnewthing/20071008-00/?p=24863/
            IntPtr hWndWalk = IntPtr.Zero;
            IntPtr hWndTry = GetAncestor(hWnd, GetAncestor_Flags.GetRootOwner);
            while (hWndTry != hWndWalk)
            {
                hWndWalk = hWndTry;
                hWndTry = GetLastActivePopup(hWndWalk);
                if (IsWindowVisible(hWndTry)) { break; }
            }
            if (hWndWalk != hWnd) { return false; }

            //fetch the properties of the title bar
            TITLEBARINFO ti = new TITLEBARINFO();
            ti.cbSize = (uint)Marshal.SizeOf(ti);
            GetTitleBarInfo(hWnd, ref ti);
            //if the title bar is set to invisible then we don't want this window
            const uint STATE_SYSTEM_INVISIBLE = 0x8000;
            if (ti.rgstate[0] == STATE_SYSTEM_INVISIBLE) { return false; }

            //if the window style is the one used for a floating toolbar then we don't want this window
            const int GWL_EXSTYLE = -20;
            const int WS_EX_TOOLWINDOW = 0x80;
            if (GetWindowLong(hWnd, GWL_EXSTYLE) == WS_EX_TOOLWINDOW) { return false; }

            return true;
        }

        //adjust the emelements if the form gets resized
        /*private static void Form1_Resize(object sender, EventArgs e)
        {
            Control form = (Control)sender;
            //These magic numbers are just sizes for margins in the form.
            //It seemed needlessly pedantic to give them their own variables in this case.
            listBox1.Size = new Size(form.Size.Width - pixelcheckx, form.Size.Height - 100);
            button1.Location = new Point(form.Size.Width - 180, form.Size.Height - 82);
            button2.Location = new Point(button2.Location.X, form.Size.Height - 82);
        }*/











        /// <summary>
        /// Window Styles.
        /// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
        /// </summary>
        [Flags()]
        public enum WindowStyles : uint
        {
            /// <summary>The window has a thin-line border.</summary>
            WS_BORDER = 0x800000,

            /// <summary>The window has a title bar (includes the WS_BORDER style).</summary>
            WS_CAPTION = 0xc00000,

            /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>
            WS_CHILD = 0x40000000,

            /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>
            WS_CLIPCHILDREN = 0x2000000,

            /// <summary>
            /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
            /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
            /// </summary>
            WS_CLIPSIBLINGS = 0x4000000,

            /// <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>
            WS_DISABLED = 0x8000000,

            /// <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>
            WS_DLGFRAME = 0x400000,

            /// <summary>
            /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
            /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
            /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
            /// </summary>
            WS_GROUP = 0x20000,

            /// <summary>The window has a horizontal scroll bar.</summary>
            WS_HSCROLL = 0x100000,

            /// <summary>The window is initially maximized.</summary>
            WS_MAXIMIZE = 0x1000000,

            /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
            WS_MAXIMIZEBOX = 0x10000,

            /// <summary>The window is initially minimized.</summary>
            WS_MINIMIZE = 0x20000000,

            /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
            WS_MINIMIZEBOX = 0x20000,

            /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
            WS_OVERLAPPED = 0x0,
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_MAXIMIZE)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW

            /// <summary>The window is an overlapped window.</summary>
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

            /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>
            WS_POPUP = 0x80000000u,

            /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

            /// <summary>The window has a sizing border.</summary>
            WS_SIZEFRAME = 0x40000,

            /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>
            WS_SYSMENU = 0x80000,

            /// <summary>
            /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
            /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.  
            /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
            /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
            /// </summary>
            WS_TABSTOP = 0x10000,

            /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>
            WS_VISIBLE = 0x10000000,

            /// <summary>The window has a vertical scroll bar.</summary>
            WS_VSCROLL = 0x200000
        }

        const int SW_HIDE = 0;
        const int SW_SHOWNORMAL = 1;
        const int SW_NORMAL = 1;
        const int SW_SHOWMINIMIZED = 2;
        const int SW_SHOWMAXIMIZED = 3;
        const int SW_MAXIMIZE = 3;
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_SHOW = 5;
        const int SW_MINIMIZE = 6;
        const int SW_SHOWMINNOACTIVE = 7;
        const int SW_SHOWNA = 8;
        const int SW_RESTORE = 9;
        const int SW_SHOWDEFAULT = 10;
        const int SW_FORCEMINIMIZE = 11;
        const int SW_MAX = 11;
        public const UInt32 SWP_NOSIZE = 0x0001;
        public const UInt32 SWP_NOMOVE = 0x0002;
        public const UInt32 SWP_NOZORDER = 0x0004;
        public const UInt32 SWP_NOREDRAW = 0x0008;
        public const UInt32 SWP_NOACTIVATE = 0x0010;
        public const UInt32 SWP_FRAMECHANGED = 0x0020;
        public const UInt32 SWP_SHOWWINDOW = 0x0040;
        public const UInt32 SWP_HIDEWINDOW = 0x0080;
        public const UInt32 SWP_NOCOPYBITS = 0x0100;
        public const UInt32 SWP_NOOWNERZORDER = 0x0200;
        public const UInt32 SWP_NOSENDCHANGING = 0x0400;
        public const UInt32 SWP_DRAWFRAME = SWP_FRAMECHANGED;
        public const UInt32 SWP_NOREPOSITION = SWP_NOOWNERZORDER;

        public const UInt32 SWP_DEFERERASE = 0x2000;
        public const UInt32 SWP_ASYNCWINDOWPOS = 0x4000;

        /*
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, bottom, right;
        }*/



        /// <summary>
        /// Sets the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpwndpl">
        /// A pointer to a WINDOWPLACEMENT structure that specifies the new show state and window positions.
        /// <para>
        /// Before calling SetWindowPlacement, set the length member of the WINDOWPLACEMENT structure to sizeof(WINDOWPLACEMENT). SetWindowPlacement fails if the length member is not set correctly.
        /// </para>
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd,
           [In] ref WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// Retrieves the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpwndpl">
        /// A pointer to the WINDOWPLACEMENT structure that receives the show state and position information.
        /// <para>
        /// Before calling GetWindowPlacement, set the length member to sizeof(WINDOWPLACEMENT). GetWindowPlacement fails if lpwndpl-> length is not set correctly.
        /// </para>
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }
        public enum ShowWindowCommands
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window
            /// for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3, // is this the right value?
            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>      
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value
            /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except
            /// the window is not activated.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position.
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level
            /// window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the
            /// window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is
            /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the
            /// window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
            /// that owns the window is not responding. This flag should only be
            /// used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }

        /// <summary>
        /// Contains information about the placement of a window on the screen.
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            /// <summary>
            /// The length of the structure, in bytes. Before calling the GetWindowPlacement or SetWindowPlacement functions, set this member to sizeof(WINDOWPLACEMENT).
            /// <para>
            /// GetWindowPlacement and SetWindowPlacement fail if this member is not set correctly.
            /// </para>
            /// </summary>
            public int Length;

            /// <summary>
            /// Specifies flags that control the position of the minimized window and the method by which the window is restored.
            /// </summary>
            public int Flags;

            /// <summary>
            /// The current show state of the window.
            /// </summary>
            public ShowWindowCommands ShowCmd;

            /// <summary>
            /// The coordinates of the window's upper-left corner when the window is minimized.
            /// </summary>
            public POINT MinPosition;

            /// <summary>
            /// The coordinates of the window's upper-left corner when the window is maximized.
            /// </summary>
            public POINT MaxPosition;

            /// <summary>
            /// The window's coordinates when the window is in the restored position.
            /// </summary>
            public RECT NormalPosition;

            /// <summary>
            /// Gets the default (empty) value.
            /// </summary>
            public static WINDOWPLACEMENT Default
            {
                get
                {
                    WINDOWPLACEMENT result = new WINDOWPLACEMENT();
                    result.Length = Marshal.SizeOf(result);
                    return result;
                }
            }
        }











        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        /// <summary>
        /// Window handles (HWND) used for hWndInsertAfter
        /// </summary>
        public static class HWND
        {
            public static IntPtr
            NoTopMost = new IntPtr(-2),
            TopMost = new IntPtr(-1),
            Top = new IntPtr(0),
            Bottom = new IntPtr(1);
        }

        /// <summary>
        /// SetWindowPos Flags
        /// </summary>
        public static class SWP
        {
            public static readonly int
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
        }

        /// <summary>
        ///     Special window handles
        /// </summary>
        public enum SpecialWindowHandles
        {
            // ReSharper disable InconsistentNaming
            /// <summary>
            ///     Places the window at the top of the Z order.
            /// </summary>
            HWND_TOP = 0,
            /// <summary>
            ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            HWND_BOTTOM = 1,
            /// <summary>
            ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
            /// </summary>
            HWND_TOPMOST = -1,
            /// <summary>
            ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            HWND_NOTOPMOST = -2
            // ReSharper restore InconsistentNaming
        }



        [Flags]
        public enum SetWindowPosFlags : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            // ReSharper restore InconsistentNaming
        }









        /*[Flags()]
        private enum SetWindowPosFlags : uint
        {
            /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
            /// the system posts the request to the thread that owns the window. This prevents the calling thread from
            /// blocking its execution while other threads process the request.</summary>
            /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
            AsynchronousWindowPosition = 0x4000,
            /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
            /// <remarks>SWP_DEFERERASE</remarks>
            DeferErase = 0x2000,
            /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            /// <remarks>SWP_DRAWFRAME</remarks>
            DrawFrame = 0x0020,
            /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
            /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
            /// is sent only when the window's size is being changed.</summary>
            /// <remarks>SWP_FRAMECHANGED</remarks>
            FrameChanged = 0x0020,
            /// <summary>Hides the window.</summary>
            /// <remarks>SWP_HIDEWINDOW</remarks>
            HideWindow = 0x0080,
            /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
            /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
            /// parameter).</summary>
            /// <remarks>SWP_NOACTIVATE</remarks>
            DoNotActivate = 0x0010,
            /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
            /// contents of the client area are saved and copied back into the client area after the window is sized or
            /// repositioned.</summary>
            /// <remarks>SWP_NOCOPYBITS</remarks>
            DoNotCopyBits = 0x0100,
            /// <summary>Retains the current position (ignores X and Y parameters).</summary>
            /// <remarks>SWP_NOMOVE</remarks>
            IgnoreMove = 0x0002,
            /// <summary>Does not change the owner window's position in the Z order.</summary>
            /// <remarks>SWP_NOOWNERZORDER</remarks>
            DoNotChangeOwnerZOrder = 0x0200,
            /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
            /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
            /// window uncovered as a result of the window being moved. When this flag is set, the application must
            /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            /// <remarks>SWP_NOREDRAW</remarks>
            DoNotRedraw = 0x0008,
            /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
            /// <remarks>SWP_NOREPOSITION</remarks>
            DoNotReposition = 0x0200,
            /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
            /// <remarks>SWP_NOSENDCHANGING</remarks>
            DoNotSendChangingEvent = 0x0400,
            /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            /// <remarks>SWP_NOSIZE</remarks>
            IgnoreResize = 0x0001,
            /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            /// <remarks>SWP_NOZORDER</remarks>
            IgnoreZOrder = 0x0004,
            /// <summary>Displays the window.</summary>
            /// <remarks>SWP_SHOWWINDOW</remarks>
            ShowWindow = 0x0040,
        }*/
        /*Flags]
        public enum SetWindowPosFlags : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            // ReSharper restore InconsistentNaming
        }*/


        static Vector3 saveplayerposition = Vector3.Zero;
        static Matrix saveplayerfinalRotationMatrix = Matrix.Identity;

        static Matrix saveplayerrotatingMatrixForPelvis = Matrix.Identity;

        public static Vector3 saveplayermovePos = Vector3.Zero;
        public static float saveplayerrotx = 0;
        public static float saveplayerroty = 0;
        public static float saveplayerrotz = 0;
        /*
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, UIntPtr dwNewLong)
        {
            if (IntPtr.Size == 8) { return SetWindowLongPtr64(hWnd, nIndex, dwNewLong); }
            else { return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToUInt32())); }
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, UIntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int smIndex);
        */
        /// <summary>
        ///     Special window handles
        /// </summary>
        /*public enum SpecialWindowHandles
        {
            // ReSharper disable InconsistentNaming
            /// <summary>
            ///     Places the window at the top of the Z order.
            /// </summary>
            HWND_TOP = 0,
            /// <summary>
            ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            HWND_BOTTOM = 1,
            /// <summary>
            ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
            /// </summary>
            HWND_TOPMOST = -1,
            /// <summary>
            ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            HWND_NOTOPMOST = -2
            // ReSharper restore InconsistentNaming
        }*/

        /*[Flags]
        public enum SetWindowPosFlags //: uint
        {

            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,



            // ReSharper restore InconsistentNaming

            /*
             SWP_FRAMECHANGED = 0x0020,
             SWP_NOMOVE = 0x0002,
             SWP_NOSIZE = 0x0001,
             SWP_NOZORDER = 0x0004,
             SWP_NOOWNERZORDER = 0x0200,

            // ReSharper restore InconsistentNaming
        }*/



        /*
        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
        */
        /*[DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        */

        public static int usetypeofvoxel = 0; //0.000000001f //0.0f
        //public static int _useOculusRift = 0;

        //public static int createconsole = 1; //put app solution in console mode instead of window mode. // hide mode == 1 // showmode == 0

        static int getnewbitmaponce = 0;
        static int bmpstride;
        //static DwmSharedSurface sometest;
        //static GraphicsCapture somegcap;

        static SwapChain1 swapChain1;

        static Texture2D texture2d;
        public static string capturedwindowname = "";

        public static int lastscreencapturetype = 0;
        public static int changedscreencapturetype = 0;
        public static int screencapturetype = 0;

        //public static int usesharpdxscreencapture = 0;


        public static RenderForm somerenderform;
        public static Form1 someform;
        static byte* srcPointer;
        static byte* dstPointer;


        public static IntPtr consoleHandle;

        public static int usethirdpersonview = 0;
        public static float offsetthirdpersonview = 0.35f;//at or over 1 to get a decent ootb working 3rdpersonview.

        public static int usejitterphysics = 0;
        public static int usejitterphysicsbuo = 0;
        public static int useArduinoOVRTouchKeymapper = 0;
        public static int useSendScreenToArduino = 0;

        public static JVector _world_gravity = new JVector(0, -9.81f, 0); //-9.81f base
        public static int worlditerations = 3; // as high as possible normally for higher precision
        public static int worldsmalliterations = 3; // as high as possible normally for higher precision
        public static float worldallowedpenetration = 0.00123f; //0.00123f  _world_gravity = new JVector(0, -9.81f, 0);
        public static bool allowdeactivation = true;

        public static int physicsengineinstancex = 1; //4
        public static int physicsengineinstancey = 1; //1
        public static int physicsengineinstancez = 1; //4

        public static int worldwidth = 1;
        public static int worldheight = 1;
        public static int worlddepth = 1;


        public static int exitedprogram = -1;
        public static scupdate updatescript;
        public static scsystemconfiguration config;
        public static int initdirectXmainswtch = -1;
        public static int initvrmainswtch = 2;
        public static int has_init_directx = 0;


        public static SharpDX.DirectInput.KeyboardState keyboardstate;
        //public static keyboardinput keyboardinput;
        //public static InputSimulator inputsim;
        //public static KeyboardSimulator keyboardsim;
        //public static MouseSimulator mousesim;


        static int MaxSizeMainObject = 16;
        public static int MaxSizeMessageObject = 32;
        static scmessageobject.scmessageobject[] mainreceivedmessages;//
        static scmessageobjectjitter[][] sccsjittertasks = null;
        static jitter_sc[] jitter_sc;





        public static uint testGetWindowThreadProcessId;

        /*[DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        */




        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);

        /*
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        */

        /*
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);
        */






        /*
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        */
        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();


        /*private static readonly string WINDOW_NAME = "TestTitle";  //name of the window
        private const int GWL_STYLE = -16;              //hex constant for style changing
        private const int WS_BORDER = 0x00800000;       //window with border
        private const int WS_CAPTION = 0x00C00000;      //window with a title bar
        private const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
        private const int WS_MINIMIZEBOX = 0x00020000;  //window with minimizebox
        */

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();
        //[DllImport(@"user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);



        /*public static void makeBorderless()
        {
            // Get the handle of self
            IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
            RECT rect;
            // Get the rectangle of self (Size)
            GetWindowRect(window, out rect);
            // Get the handle of the desktop
            IntPtr HWND_DESKTOP = GetDesktopWindow();
            // Attempt to get the location of self compared to desktop
            MapWindowPoints(HWND_DESKTOP, window, ref rect, 2);
            // update self
            SetWindowLong(window, GWL_STYLE, WS_SYSMENU);
            // rect.left rect.top should work but they're returning negative values for me. I probably messed up
            SetWindowPos(window, -2, 100, 75, rect.bottom, rect.right, 0x0040);
            DrawMenuBar(window);
        }*/
        const int SwHide = 0;
        const int SwShow = 5;


        /*
        public static void makePanelBorderless()
        {
            // Get the handle of self
            IntPtr window = hWndOriginalParent;// FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
            RECT rect;
            // Get the rectangle of self (Size)
            GetWindowRect(window, out rect);
            // Get the handle of the desktop
            IntPtr HWND_DESKTOP = GetDesktopWindow();
            // Attempt to get the location of self compared to desktop
            MapWindowPoints(HWND_DESKTOP, window, ref rect, 2);
            // update self
            SetWindowLong(window, GWL_STYLE, WS_SYSMENU);
            // rect.left rect.top should work but they're returning negative values for me. I probably messed up
            SetWindowPos(window, -2, 100, 75, rect.bottom, rect.right, 0x0040);
            DrawMenuBar(window);
        }
        */






        static System.Drawing.Imaging.BitmapData bmpData1;
        static DataBox dataBox1;

        static int memoryBitmapStride;
        static int columns;
        static int rows;
        static IntPtr interptr1;






        static ShaderResourceView shaderResourceView;
        static int startmainthread = 0;
        static int bitmapcounter = 0;
        public static int textureresetswtc = 0;
        static System.Drawing.Bitmap _bitmap;

        static System.Drawing.Bitmap _bitmap1;
        static System.Drawing.Rectangle boundsRect;
        static int _bytesTotal;
        static Texture2DDescription _textureDescription;
        static byte[] _textureByteArray;


        static byte[] onboardcomputeiconpixeldata;

        static Texture2D _texture2d;

        static ShaderResourceView lastshaderresourceview;
        static System.Drawing.Bitmap lastbitmap;

        static Texture2D lasttexture2D0;
        static Texture2D lasttexture2D1;

        static private IntPtr hWndParent;
        static private Process pDocked;
        static private IntPtr hWndOriginalParent;
        static private IntPtr hWndDocked;

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        /*[DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        */

        //https://stackoverflow.com/questions/5836176/docking-window-inside-another-window
        static private void dockIt()
        {

            //Panel somepanel = new System.Windows.Controls.Panel();



            //if (hWndDocked != IntPtr.Zero) //don't do anything if there's already a window docked.
            //    return;

            //var somewindow = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);





            //hWndParent = someform.Handle;// sccsr14sc.Form1.theHandle;// IntPtr.Zero;

            /*pDocked = Process.Start(@"notepad");
            while (hWndDocked == IntPtr.Zero)
            {
                pDocked.WaitForInputIdle(1000); //wait for the window to be ready for input;
                pDocked.Refresh();              //update process info
                if (pDocked.HasExited)
                {
                    return; //abort if the process finished before we got a handle.
                }
                hWndDocked = pDocked.MainWindowHandle;  //cache the window handle
            }*/

            //hWndDocked = hWndParent;// sccsr14sc.Form1.thepanel.Handle;// form.Handle;


            //Windows API call to change the parent of the target window.
            //It returns the hWnd of the window's parent prior to this call.


            hWndDocked = consoleHandle;
            hWndOriginalParent = SetParent(consoleHandle, sccsr14sc.Form1.thepanel.Handle);
            //hWndOriginalParent = SetParent(consoleHandle, someform.Handle);// sccsr14sc.Form1.thepanel.Handle);// sccsr14sc.Form1.thepanel.Handle);
            //hWndOriginalParent = SetParent(someform.Handle, consoleHandle);// sccsr14sc.Form1.thepanel.Handle);

            //Wire up the event to keep the window sized to match the control
            sccsr14sc.Form1.thepanel.SizeChanged += new EventHandler(thepanel_Resize);

            //sccsr14sc.Form1.size



            //SetWindowLong(someform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT));
            //SetLayeredWindowAttributes(someform.Handle, 0, 255, LWA_ALPHA);



            //Perform an initial call to set the size.
            //thepanel_Resize(new Object(), new EventArgs());

            thepanel_Resize(new Object(), new EventArgs());
        }

        static private void undockIt()
        {
            //Restores the application to it's original parent.
            SetParent(someform.Handle, hWndOriginalParent);
        }

        static private void thepanel_Resize(object sender, EventArgs e)
        {

            //MessageBox((IntPtr)0, "thepanel_Resize", "scmsg", 0);
            //Change the docked windows size to match its parent's size. 
            MoveWindow(consoleHandle, 0, 0, sccsr14sc.Form1.thepanel.Width, sccsr14sc.Form1.thepanel.Height, true);
        }







        //public static Panel thepanel;


        //https://www.unknowncheats.me/forum/c/62019-c-non-hooked-external-directx-overlay.html

        private static Margins marg;

        internal struct Margins
        {
            public int Left, Right, Top, Bottom;
        }

        /*[DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);*/


        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x00080000; //0x80000
        //public const int WS_EX_TRANSPARENT = 0x20;

        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;
        public const long WS_EX_TOPMOST = 0x00000008L;
        public const long WS_NOACTIVATE = 0x08000000L;
        const long WS_EX_WINDOWEDGE = 0x00000100L;
        public const long WS_EX_TRANSPARENT = 0x00000020L;
        public const long WS_EX_CLIENTEDGE = 0x00000200L;
        const long WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE;


        [DllImport("user32.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Margins pMargins);



        /*[DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, bottom, right;
        }*/
        //public static RenderForm form;


        public static readonly string WINDOW_NAME = "voidexpanse";  //name of the window
        public const int GWL_STYLE = -16;              //hex constant for style changing
        public const int WS_BORDER = 0x00800000;       //window with border
        public const int WS_CAPTION = 0x00C00000;      //window with a title bar
        public const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
        public const int WS_MINIMIZEBOX = 0x00020000;  //window with minimizebox



        public static IntPtr window;
        public static RECT rect;

        //static int _init_main = 1;
        public static IntPtr HWND_DESKTOP;

        //[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        /*
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);*/
        public static IntPtr vewindowsfoundedz;

        //private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        public delegate int Callback(int hWnd, int lParam);
        static Callback myCallBack = new Callback(EnumChildGetValue);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        private static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowsProc childProc = new EnumWindowsProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }




        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool GetCursorPos(ref Win32Point pt);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(out Win32Point pt);



        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        private static bool EnumWindow(IntPtr hWnd, IntPtr lParam)
        {
            GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

            if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
            {
                return false;
            }

            List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
            childHandles.Add(hWnd);

            return true;
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);




        public static int EnumChildGetValue(int hWnd, int lParam)
        {
            StringBuilder formDetails = new StringBuilder(256);
            string editText = "";
            StringBuilder ClassName = new StringBuilder(256);
            var nRet = GetClassName(new IntPtr(hWnd), ClassName, ClassName.Capacity);
            Console.WriteLine("Control Caption : " + editText + " hWnd : " + hWnd.ToString("X") + " Class Name : " + ClassName);
            Trace.WriteLine("Class Name : " + ClassName);
            Console.ReadLine();

            if (ClassName.ToString().Equals("Edit"))
            {
                Console.WriteLine("Edit Control Found");
                Console.WriteLine("Current Control : " + hWnd.ToString("X"));
                Console.WriteLine("Disabling Notepad Edit Component");
                EnableWindow(new IntPtr(hWnd), true);
            }
            return 1;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };





        [DllImport("user32.dll")]
        static extern int ShowCursor(bool bShow);


        public static int getwindowthreadprocessidint = 0;
        //public static DInput keynmouseinput;
        static void ProcessExitHandler(object sender, EventArgs e)
        {




            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            if (last_hWnd != IntPtr.Zero)
            {

                int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);

                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                Program.RECT therect = new Program.RECT();

                //if (Program.vewindowsfoundedz != IntPtr.Zero)
                {

                    param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                    therect = new Program.RECT();

                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = screenHeight;
                    therect.Right = screenWidth;

                    param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);

                    /*Program.SetWindowPlacement(last_hWnd, ref param);
                   */

                    Program.SetWindowPos(last_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    Program.SetWindowPos(last_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                    DeleteObject(last_hWnd);
                    GC.SuppressFinalize(last_hWnd);

                    /*
                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);*/
                    
                }
            }

            if (vewindowsfoundedz != IntPtr.Zero)
            {

                int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);

                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                Program.RECT therect = new Program.RECT();

                //if (Program.vewindowsfoundedz != IntPtr.Zero)
                {

                    param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                    therect = new Program.RECT();

                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = screenHeight;
                    therect.Right = screenWidth;

                    param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    
                    /*
                    Program.SetWindowPlacement(last_hWnd, ref param);
                   */

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                    DeleteObject(Program.vewindowsfoundedz);
                    GC.SuppressFinalize(Program.vewindowsfoundedz);

                    /*
                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);*/

                }
            }
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM

            //Program.exitedprogram = 1;


            if (updatescript.captureMethod != null)
            {
                updatescript.captureMethod.StopCapture();// (sccsr14sc.Form1.theHandle, updatescript.device, factoryy); //

                //updatescript.captureMethod.Dispose();
                //updatescript.captureMethod = null;
            }




            ShowCursor(true);
            exitedprogram = 1;

            //updatescript.heightmapthread.Abort();
            //updatescript.heightmapthread.Suspend();


            /*if (updatescript != null)
            {
                updatescript.threadupdateswtc = -1;
                updatescript.canworkphysics = -1;
                scupdate.stopovr = -1;

                if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                {
                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop != null)
                    {
                        for (int i = 0; i < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; i++)
                        {

                            for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; j++)
                            {
                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j].ShutDown();
                            }

                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].ShutDown();
                        }
                    }
                }
                //updatescript.heightmapthread = null;

            }

            //updatescript.scgraphicssecpackagemessage.scgraphicssec.
            scdirectx.D3D.ShutDown();
            if (updatescript != null)
            {
                updatescript = null;
            }


            if (_mainThread != null)
            {
                _mainThread.Abort();
                _mainThread = null;
            }*/

            int isexitingprogram = 0;
        threadexitloop:





            if (updatescript.exitthread0 == 0)
            {
                updatescript.exitthread0 = 1;
            }
            if (updatescript.exitthread1 == 0)
            {
                updatescript.exitthread1 = 1;
            }



            if (updatescript.exitthread0 == 2 || updatescript.main_thread_update0 == null)
            {
                if (updatescript.main_thread_update0 == null)
                {
                    updatescript.hasfinishedframe0 = 1;
                }
                else
                {
                    updatescript.main_thread_update0 = null;
                }

                updatescript.exitthread0 = 3;
            }
            if (updatescript.exitthread1 == 2 || updatescript.main_thread_update1 == null)
            {
                if (updatescript.main_thread_update1 == null)
                {
                    updatescript.hasfinishedframe1 = 1;
                }
                else
                {
                    updatescript.main_thread_update1 = null;
                }
                updatescript.exitthread1 = 3;
            }



            if (updatescript.exitthread0 == 3 && updatescript.exitthread1 == 3)
            {
                
                /*if (updatescript.captureMethod != null)
                {
                    updatescript.captureMethod.StopCapture();// (sccsr14sc.Form1.theHandle, updatescript.device, factoryy); //

                    updatescript.captureMethod.Dispose();
                    updatescript.captureMethod = null;
                }*/



                //sccs.Program.MessageBox((IntPtr)0, "capture reset0", "scmsg", 0);

                if (updatescript.hasfinishedframe0 == 1 && updatescript.hasfinishedframe1 == 1)
                {


                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                    {

                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop != null)
                        {
                            for (int i = 0; i < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; i++)
                            {


                                for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData.Length; j++)
                                {

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantLightBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantLightBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantLightBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantMatrixPosBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantMatrixPosBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantMatrixPosBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].ConstantTessellationBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].ConstantTessellationBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].ConstantTessellationBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].IndicesBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].IndicesBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].IndicesBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBufferHeightmap != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBufferHeightmap.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBufferHeightmap = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocD != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocD.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocD = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocH != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocH.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocH = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocW != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocW.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocW = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferFORWARD != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferFORWARD.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferFORWARD = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferRIGHT != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferRIGHT.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferRIGHT = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferUP != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferUP.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferUP = null;
                                    }


                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].mapBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].mapBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].mapBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].vertexBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].vertexBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].vertexBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].DomainShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].DomainShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].DomainShader = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].GeometryShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].GeometryShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].GeometryShader = null;
                                    }


                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].HullShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].HullShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].HullShader = null;
                                    }


                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].PixelShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].PixelShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].PixelShader = null;
                                    }


                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].VertexShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].VertexShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].VertexShader = null;
                                    }

                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j] = null;
                                }

                                for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; j++)
                                {
                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j].ShutDown();
                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j] = null;
                                }

                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].shaderOfChunk = null;
                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].ShutDown();

                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i] = null;
                            }
                        }
                    }


                    if (sccsjittertasks != null)
                    {
                        if (sccsjittertasks[0] != null)
                        {
                            if (sccsjittertasks[0].Length > 0)
                            {
                                if (sccsjittertasks[0][0].shaderresource != null)
                                {

                                    sccsjittertasks[0][0].shaderresource.Dispose();
                                    sccsjittertasks[0][0].shaderresource = null;
                                }

                                if (sccsjittertasks[0][0].frameByteArray != null)
                                {
                                    sccsjittertasks[0][0].frameByteArray = null;
                                }
                                //sccsjittertasks[0][0] = null;
                            }
                            sccsjittertasks[0] = null;
                        }
                        sccsjittertasks = null;
                    }


                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                    {
                        updatescript.scgraphicssecpackagemessage.scgraphicssec.Shutdown();
                        updatescript.scgraphicssecpackagemessage.scgraphicssec = null;
                    }




              
                    /*if (sometest != null)
                    {
                        sometest.Dispose();
                        sometest = null;
                    }
                    if (somegcap != null)
                    {
                        somegcap.Dispose();
                        somegcap = null;
                    }*/


                    if (swapChain1 != null)
                    {
                        swapChain1.Dispose();
                        swapChain1 = null;
                    }





                    if (updatescript.SwapChain != null)
                    {
                        updatescript.SwapChain.Dispose();
                        updatescript.SwapChain = null;
                    }

                    if (factoryy != null)
                    {
                        factoryy.Dispose();
                        factoryy = null;
                    }


                    updatescript.ShutDownGraphics();

                    if (sccs.scgraphics.scdirectx.D3D != null)
                    {
                        sccs.scgraphics.scdirectx.D3D.ShutDown();
                        sccs.scgraphics.scdirectx.D3D = null;
                    }


                    updatescript.exitthread0 = 0;
                    updatescript.exitthread1 = 0;
                    updatescript = null;





                    /*if (screencapturetype == 2)
                    {
                        usesharpdxscreencapture = 1;
                    }
                    else
                    {
                        if (usesharpdxscreencapture != 0)
                        {
                            usesharpdxscreencapture = 0;
                        }
                    }*/


                    if (shaderResourceView != null)
                    {
                        shaderResourceView.Dispose();
                        shaderResourceView = null;
                    }

                    if (lastshaderresourceview != null)
                    {
                        lastshaderresourceview.Dispose();
                        lastshaderresourceview = null;
                    }

                    if (texture2d != null)
                    {
                        texture2d.Dispose();
                        texture2d = null;
                    }

                    if (_texture2d != null)
                    {
                        _texture2d.Dispose();
                        _texture2d = null;
                    }

                    if (_bitmap != null)
                    {
                        _bitmap.Dispose();
                        _bitmap = null;
                    }



                    /*
                    if (sccsjittertasks[0][0].shaderresource != null)
                    {
                        if (sccsjittertasks[0][0].shaderresource.Resource != null)
                        {
                            sccsjittertasks[0][0].shaderresource.Resource.Dispose();
                            //sccsjittertasks[0][0].shaderresource.Resource = null;
                        }
                    }*/



                    GC.SuppressFinalize(vewindowsfoundedz);

                    DeleteObject(vewindowsfoundedz);


                    getwindowthreadprocessidint = 0;
                    textureresetswtc = 0;
                    //GC.Collect();
                    changedscreencapturetype = 0;
                    isexitingprogram = 1;
                    //createinputsswtc = 0;
                    //sccs.Program.MessageBox((IntPtr)0, "capture reset1", "scmsg", 0);


                    /*if (lastscreencapturetype != screencapturetype) //2 == sharpdx11.1 screencapture
                    {


                    }*/
                }
            }
            if (isexitingprogram == 0)
            {

                Thread.Sleep(1);
                goto threadexitloop;
            }
            else
            {
                if (backgroundWorker != null)
                {
                    backgroundWorker.Dispose();
                    backgroundWorker = null;
                }

                if (_mainThread != null)
                {
                    _mainThread.Abort();
                    _mainThread = null;
                }

            }


            //MessageBox((IntPtr)0, "exiting", "scmsg", 0);
            //throw new NotImplementedException("program has exited");
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        static Thread _mainThread;
        static int initthread = 0;
        // static int initform = 0;

        static SharpDX.DXGI.Factory factoryy;
        //static sccssharpdxscreencapture sharpdxscreencapture;
        //static sccsscreenframe screencaptureframe;

        static Stopwatch panelchangedwatch = new Stopwatch();
        static int panelchangedswtc = 0;
        static int counterpanelchanged = 0;
        static int counterpanelchangedmax = 100;


        static int createinputsswtc = 0;


        static int gccollectcounter = 0;
        static int gccollectcountermax = 1000;

        static Stopwatch gccollectstopwatch = new Stopwatch();




        static Stopwatch heightmaptrackbarchangedwatch = new Stopwatch();
        static int heightmaptrackbarchangedswtc = 0;


        /*// Constant values from the "winuser.h" header file.
        internal const int WM_LBUTTONUP = 0x0202,
                           WM_RBUTTONUP = 0x0205;

        internal static IntPtr ApplicationMessageFilter(
            IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Handle messages passed to the visual.
            switch (message)
            {
                // Handle the left and right mouse button up messages.
                case WM_LBUTTONUP:
                case WM_RBUTTONUP:
                    System.Windows.Point pt = new System.Windows.Point();
                    pt.X = (uint)lParam & (uint)0x0000ffff;  // LOWORD = x
                    pt.Y = (uint)lParam >> 16;               // HIWORD = y
                    //MyShape.OnHitTest(pt, message);

                    break;
            }

            return IntPtr.Zero;
        }*/


        public static int hasresettedcapture = 0;
        internal static class WinCursors
        {
            [DllImport("user32.dll")]
            private static extern int ShowCursor(bool bShow);


            internal static void ShowCursor()
            {
                while (ShowCursor(true) < 0)
                {
                    ShowCursor(true);
                }
            }

            internal static void HideCursor()
            {
                while (ShowCursor(false) >= 0)
                {
                    ShowCursor(false);
                }
            }
        }


        public static int progcanpause = 0;


        public static keyboardinput keynmouseinput;
        //public static DInput keynmouseinput;
        public static InputSimulator inputsim;
        public static KeyboardSimulator keyboardsim;
        public static MouseSimulator mousesim;
        public static void createinputs(IntPtr thehandle)
        {
            inputsim = new InputSimulator();
            keyboardsim = new KeyboardSimulator(inputsim);


            /*inputsim = new InputSimulator();
            mousesim = new MouseSimulator(inputsim);
            keyboardsim = new KeyboardSimulator(inputsim);

            keynmouseinput = new DInput();
            keynmouseinput.Initialize(Program.config, SCGLOBALSACCESSORS.SCCONSOLECORE.handle); //sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle //thehandle
            */

            keynmouseinput = new keyboardinput();
        }



        static sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS;
        static BackgroundWorker backgroundWorker;

        static RECT Rectsccs;// = new RECT();
        static RECT capturedprogramRectsccs = new RECT();

        static RECT lastRectsccsformovement;

        static RECT Rectsccscapturedwindowlast = new RECT();







        [STAThread]
        static void Main()
        {

            thestopwatch.Restart();

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {


                if (Program.vewindowsfoundedz != IntPtr.Zero)
                {

                    //https://stackoverflow.com/questions/36952600/determine-if-a-window-is-topmost-or-not


                    int exStyle = sccsr14sc.Form1.GetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE);
                    var result=  (exStyle & WS_EX_TOPMOST) == WS_EX_TOPMOST;

                    if (!result)
                    {
                        SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);

                    }


                    /*int exstyle = GetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE);

                    var result = (exstyle & WS_EX_TOPMOST) == WS_EX_TOPMOST;

                    if (!result)
                    {
                        SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);

                    }*/










                    capturedprogramRectsccs = new RECT();
                    GetWindowRect(Program.vewindowsfoundedz, ref capturedprogramRectsccs);
                }


                Rectsccs = new RECT();
                GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);






                while (Program.exitedprogram == -1)
                {










                    if (initform == 2)
                    {
                        //MessageBox((IntPtr)0, "_thread_looper0", "scmsg", 0);
                        // ReSharper disable AccessToDisposedClosure
                        if (screencapturetype == 0 && changedscreencapturetype == 0 || screencapturetype == 1 && changedscreencapturetype == 0 || capturedprogramRectsccs.Left != Rectsccs.Left || capturedprogramRectsccs.Right != Rectsccs.Right || capturedprogramRectsccs.Top != Rectsccs.Top || capturedprogramRectsccs.Bottom != Rectsccs.Bottom)
                        {



                            if (hasstartedcapture == 2)
                            {

                                //if (iswtchingcapturetypesmaybe == 1)
                                {
                                    //lastscreencapturetype = screencapturetype;


                                    if (getwindowthreadprocessidint == 1 && firstframededicatedthread== 1 && firstframededicatedbackgroundworker == 1)
                                    {

                                        var windowHandler = vewindowsfoundedz;// GetActiveWindowHandle();

                                        //var windowRec = GetWindowRect(windowHandler);
                                        // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
                                        //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, Screen.AllScreens[monitor].WorkingArea.Left, Screen.AllScreens[monitor].WorkingArea.Top, windowRec.Size.Width + 16, windowRec.Size.Height + 38, SetWindowPosFlags.SWP_SHOWWINDOW);
                                        //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, SetWindowPosFlags.SWP_FRAMECHANGED | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOOWNERZORDER )//SetWindowPosFlags.SWP_SHOWWINDOW);
                                        //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, (SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER));//SetWindowPosFlags.SWP_SHOWWINDOW);




                                        GetWindowThreadProcessId(vewindowsfoundedz, out testGetWindowThreadProcessId);






                                        /*
                                        var rect = new RECT();
                                        GetWindowRect(Program.vewindowsfoundedz, out rect);
                                        */
                                        //Console.WriteLine("testGetWindowThreadProcessId:" + testGetWindowThreadProcessId);

                                        if (vewindowsfoundedz == IntPtr.Zero || testGetWindowThreadProcessId == 0)
                                        {
                                            //Console.WriteLine("testGetWindowThreadProcessId == " + testGetWindowThreadProcessId);

                                            /*GC.SuppressFinalize(vewindowsfoundedz);

                                            DeleteObject(vewindowsfoundedz);
                                            */

                                            getwindowthreadprocessidint = 0;
                                            textureresetswtc = 0;
                                            //GC.Collect();
                                            changedscreencapturetype = 1;
                                            //createinputsswtc = 0;
                                            hasresettedcapture = 1;
                                            //initform = 1;

                                            Program.screencapturetype = sccsr14sc.Form1.screencaptureindextype;

                                            sccsr14sc.Form1.lastscreencaptureindextype = sccsr14sc.Form1.screencaptureindextype;
                                            Program.lastcapturetypevalue = sccsr14sc.Form1.selectedscreencaptureindex;

                                            //Program.changedscreencapturetype = 1;
                                            iswtchingcapturetypesmaybe = 1;
                                        }

                                        

                                        /*//https://stackoverflow.com/questions/18015193/how-to-check-if-application-is-running
                                        if (Process.GetProcessesByName(capturedwindowname).Length > 0)
                                        {
                                            // Is running
                                        }
                                        else
                                        {

                                            DeleteObject(vewindowsfoundedz);

                                            getwindowthreadprocessidint = 0;
                                            textureresetswtc = 0;
                                            GC.Collect();
                                            changedscreencapturetype = 1;
                                            //createinputsswtc = 0;
                                            hasresettedcapture = 1;
                                            //initform = 1;

                                            Program.screencapturetype = sccsr14sc.Form1.screencaptureindextype;

                                            sccsr14sc.Form1.lastscreencaptureindextype = sccsr14sc.Form1.screencaptureindextype;
                                            Program.lastcapturetypevalue = sccsr14sc.Form1.selectedscreencaptureindex;

                                            //Program.changedscreencapturetype = 1;
                                            iswtchingcapturetypesmaybe = 1;
                                        }*/



                                    }


                                    //getwindowthreadprocessidint = 0;


                                }
                                

                            }





                            if (vewindowsfoundedz == IntPtr.Zero && hasstartedcapture == 2)
                            {
                                if (changedscreencapturetype == 0)
                                {
                                     if (iswtchingcapturetypesmaybe == 2)
                                    {
                                        //Console.WriteLine("captured name not null and selected item is the same. Gotta restart capturing a program. 00");

                                        /*Program.screencapturetype = sccsr14sc.Form1.screencaptureindextype;

                                        sccsr14sc.Form1.lastscreencaptureindextype = sccsr14sc.Form1.screencaptureindextype;
                                        Program.lastcapturetypevalue = sccsr14sc.Form1.selectedscreencaptureindex;

                                        Program.changedscreencapturetype = 1;
                                        iswtchingcapturetypesmaybe = 1;*/
                                     }
                                    //changedscreencapturetype = 1;


                                    if ((string)sccsr14sc.Form1.comboboxcapturelist.SelectedItem == lastcapturetypevalue)
                                    {

                                        //Console.WriteLine("captured name not null and selected item is the same. Gotta restart capturing a program. 11");
                                        /*
                                        Program.screencapturetype = sccsr14sc.Form1.screencaptureindextype;
                                        Program.changedscreencapturetype = 1;

                                        sccsr14sc.Form1.lastscreencaptureindextype = sccsr14sc.Form1.screencaptureindextype;
                                        Program.lastcapturetypevalue = sccsr14sc.Form1.selectedscreencaptureindex;
                                        */





                                        //lastscreencaptureindextype = screencaptureindextype; lastscreencaptureindextype = screencaptureindextype;

                                        //initform = 

                                        /*
                                        sccsr14sc.Form1.comboboxcapturelist.Invoke((MethodInvoker)delegate
                                        {
                                            sccsr14sc.Form1.counterscreencapturechanged = 2;

                                            sccsr14sc.Form1.comboboxcapturelist.BeginUpdate();
                                            sccsr14sc.Form1.comboboxcapturelist.Select(0, 0);
                                            sccsr14sc.Form1.comboboxcapturelist.EndUpdate();
                                        });*/
                                    }







                                    /*else if ((string)sccsr14sc.Form1.comboboxcapturelist.SelectedItem == lastcapturetypevalue) //(sccsr14sc.Form1.comboboxcapturelist.Visible == true)
                                    {
                                        sccsr14sc.Form1.comboboxcapturelist.Invoke((MethodInvoker)delegate
                                        {
                                            //sccsr14sc.Form1.comboboxcapturelist.Visible = false;
                                        });
                                    }*/


                                }
                            }
                            else
                            {


                                Rectsccs = new Program.RECT();
                                Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);



                                if (vewindowsfoundedz != IntPtr.Zero)
                                {
                                    var Rectsccscapturedwindow = new RECT();
                                    //GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);
                                    Program.GetWindowRect(vewindowsfoundedz, ref Rectsccscapturedwindow);






                                    /*if (Rectsccscapturedwindow.Left != Rectsccscapturedwindowlast.Left || Rectsccscapturedwindow.Top != Rectsccscapturedwindowlast.Top)
                                    {
                                        //Console.WriteLine("testGetWindowThreadProcessId == " + testGetWindowThreadProcessId);

                                        //DeleteObject(vewindowsfoundedz);

                                        /*getwindowthreadprocessidint = 0;
                                        textureresetswtc = 0;
                                        GC.Collect();
                                        changedscreencapturetype = 2;
                                        //createinputsswtc = 0;
                                        hasresettedcapture = 1;
                                        //initform = 1;

                                        Program.screencapturetype = sccsr14sc.Form1.screencaptureindextype;

                                        sccsr14sc.Form1.lastscreencaptureindextype = sccsr14sc.Form1.screencaptureindextype;
                                        Program.lastcapturetypevalue = sccsr14sc.Form1.selectedscreencaptureindex;

                                        //Program.changedscreencapturetype = 1;
                                        iswtchingcapturetypesmaybe = 1;

                                        //swtc = 0;

                                        //screencapturetype = -1;
                                        //typeofwindowpicker = 1;

                                        //textureresetswtc = 0;


                                        typeofwindowpicker = 1;
                                        //DeleteObject(vewindowsfoundedz);

                                        getwindowthreadprocessidint = 0;
                                        textureresetswtc = 0;
                                        GC.Collect();
                                        changedscreencapturetype = 1;
                                        //createinputsswtc = 0;
                                        hasresettedcapture = 1;
                                        //initform = 1;

                                        Program.screencapturetype = sccsr14sc.Form1.screencaptureindextype;

                                        sccsr14sc.Form1.lastscreencaptureindextype = sccsr14sc.Form1.screencaptureindextype;
                                        Program.lastcapturetypevalue = sccsr14sc.Form1.selectedscreencaptureindex;

                                        //Program.changedscreencapturetype = 1;
                                        iswtchingcapturetypesmaybe = 1;

                                    }*/




                                    string altcapturedwindowname = capturedwindowname.ToLower();

                                    if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                                    {
                                        //var altcapturedwindownameindex = altcapturedwindowname.IndexOf("- microsoft? edge");

                                        //var altcapturedwindownamediff = 

                                        //Console.WriteLine("altcapturedwindownameindex:" + altcapturedwindownameindex);
                                        //var altaltcapturedwindowname = altcapturedwindowname.Substring(altcapturedwindownameindex - 1, 17);

                                        if (altcapturedwindowname.Contains(" - microsoft") && altcapturedwindowname.Contains(" edge")) //altaltcapturedwindowname == "microsoft? edge")//altcapturedwindowname.Contains("microsoft? edge"))
                                        {
                                            if (lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top) //|| lastRectsccsformovement.Right != Rectsccs.Right || lastRectsccsformovement.Bottom != Rectsccs.Bottom
                                                                                                                                              //(lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))//
                                                                                                                                              //if ( vewindowsfoundedz != IntPtr.Zero &&(lastRectsccsformovement.Left  - lastRectsccsformovement.Right) != (Rectsccs.Left  - Rectsccs.Right) ||vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Top  - lastRectsccsformovement.Bottom) != (Rectsccs.Top - Rectsccs.Bottom ))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                                                                                                                              //if (vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                            {



                                                IntPtr id;
                                                Program.RECT Rect = new Program.RECT();
                                                //Thread.Sleep(2000);
                                                id = vewindowsfoundedz;// GetForegroundWindow();
                                                Random myRandom = new Random();
                                                Program.GetWindowRect(id, ref Rect);
                                                Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);


                                                int screenWidth = Program.GetSystemMetrics(0);
                                                int screenHeight = Program.GetSystemMetrics(1);




                                                int iHandle = FindWindow(null, capturedwindowname);// "VoidExpanse");

                                                //Console.WriteLine("*************EDGE*****************:" + iHandle);



                                                Console.WriteLine("Program.csLine4196=>capturedwindowname:" + capturedwindowname);

                                                //Console.WriteLine(capturedwindowname);
                                                //Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                                                //Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, lastRectsccsformovement.Left, lastRectsccsformovement.Top, Rectsccscapturedwindow.Right, Rectsccscapturedwindow.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW


                                                SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, SWP_SHOWWINDOW);




                                                /*sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                //WORKING
                                                sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                //WORKING*/


                                                //iHandle = scgraphicssec.FindWindow(null, sccsr14sc.Form1.capturedwindownameform1);
                                                //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                                                //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                                                /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                                                sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);

                                                sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                                                sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                */
                                                /*Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                                                Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/
                                                /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                                Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Right, Rectsccs.Bottom, Rectsccs.Left, Rectsccs.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW


                                                */





                                                /*var param = new Program.WINDOWPLACEMENT();
                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                var therect = new Program.RECT();
                                                therect.Left = 0;
                                                therect.Top = 0;
                                                therect.Bottom = Rectsccs.Bottom - Rectsccs.Top;
                                                therect.Right = (Rectsccs.Right - Rectsccs.Left);
                                                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                                int screenWidth = GetSystemMetrics(0);
                                                int screenHeight = GetSystemMetrics(1);
                                                */
                                                /*var param = new Program.WINDOWPLACEMENT();
                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                Program.RECT therect = new Program.RECT();
                                                therect.Left = Rectsccs.Left;
                                                therect.Top = Rectsccs.Top;
                                                therect.Bottom = Rectsccs.Bottom;
                                                therect.Right = (Rectsccs.Right);
                                                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                Program.SetWindowPlacement(vewindowsfoundedz, ref param);

                                                Program.GetWindowRect(vewindowsfoundedz, ref rect);
                                                */










                                                /*
                                                SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));//WS_EX_OVERLAPPEDWINDOW

                                                SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                */


                                                //id = vewindowsfoundedz;// GetForegroundWindow();
                                                //Random myRandom = new Random();


                                                //MoveWindow(id, myRandom.Next(1024), myRandom.Next(768), Rect.right - Rect.left, Rect.bottom - Rect.top, true);
                                                //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (therect.Right - therect.Left), therect.Bottom - therect.Top, true);
                                                //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (lastRectsccsformovement.Right - lastRectsccsformovement.Left), lastRectsccsformovement.Bottom - lastRectsccsformovement.Top, true);


                                                //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);




                                                /*
                                                SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);
                                                */
                                                //SetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE, (IntPtr)WS_EX_TOPMOST);//WS_EX_OVERLAPPEDWINDOW




























                                                //progcanpause = 1;
                                                if (progcanpause == 1)
                                                {

                                                }
                                                else
                                                {
                                                    //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Bottom, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, SWP_SHOWWINDOW);

                                                    /*param = new Program.WINDOWPLACEMENT();
                                                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                    therect = new Program.RECT();
                                                    therect.Left = lastRectsccsformovement.Left;
                                                    therect.Top = lastRectsccsformovement.Top;
                                                    therect.Bottom = lastRectsccsformovement.Bottom;
                                                    therect.Right = lastRectsccsformovement.Right;
                                                    param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                    Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/

                                                    //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);

                                                }




                                                /*
                                                param = new Program.WINDOWPLACEMENT();
                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                therect = new Program.RECT();
                                                therect.Left = Rectsccs.Top;
                                                therect.Top = Rectsccs.Left;
                                                therect.Bottom = Rectsccs.Bottom - Rectsccs.Top;
                                                therect.Right = (Rectsccs.Right - Rectsccs.Left);
                                                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/


                                                /*
                                                sccsr14sc.Form1.someform.Size = new System.Drawing.Size(1920, 1080);
                                                sccsr14sc.Form1.someform.FormBorderStyle = FormBorderStyle.None;
                                                sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                                sccsr14sc.Form1.someform.TopMost = true;
                                                SetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));//WS_EX_OVERLAPPEDWINDOW
                                                */



                                                /*
                                                therect = new Program.RECT();
                                                therect.Left = 0;
                                                therect.Top = 0;
                                                therect.Bottom = Rectsccs.Bottom - Rectsccs.Top;
                                                therect.Right = (Rectsccs.Right - Rectsccs.Left);
                                                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/

                                                //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                                            }
                                        }

                                    }
                                    else if (altcapturedwindowname.Contains("firefox"))
                                    {
                                        if (lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top) //|| lastRectsccsformovement.Right != Rectsccs.Right || lastRectsccsformovement.Bottom != Rectsccs.Bottom
                                                                                                                                          //(lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))//
                                                                                                                                          //if ( vewindowsfoundedz != IntPtr.Zero &&(lastRectsccsformovement.Left  - lastRectsccsformovement.Right) != (Rectsccs.Left  - Rectsccs.Right) ||vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Top  - lastRectsccsformovement.Bottom) != (Rectsccs.Top - Rectsccs.Bottom ))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                                                                                                                          //if (vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                        {
                                            var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                                            if (RunningProcessPaths.Contains("firefox.exe"))
                                            {
                                                //firefox is running
                                                //Console.WriteLine("firefox is running");

                                                IntPtr id;
                                                Program.RECT Rect = new Program.RECT();
                                                //Thread.Sleep(2000);
                                                id = vewindowsfoundedz;// GetForegroundWindow();
                                                Random myRandom = new Random();
                                                Program.GetWindowRect(id, ref Rect);
                                                Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);


                                                int screenWidth = Program.GetSystemMetrics(0);
                                                int screenHeight = Program.GetSystemMetrics(1);




                                                screenWidth = Program.GetSystemMetrics(0);
                                                screenHeight = Program.GetSystemMetrics(1);



                                                int iHandle = FindWindow(null, capturedwindowname);// "VoidExpanse");

                                                //Console.WriteLine(iHandle);

                                                //Console.WriteLine(capturedwindowname);
                                                Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

                                                /*sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                //WORKING
                                                sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                //WORKING*/


                                                //iHandle = scgraphicssec.FindWindow(null, sccsr14sc.Form1.capturedwindownameform1);
                                                //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                                                //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                                                /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                                                sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                                                */
                                                /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                                                sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                */
                                                /*Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                                                Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/



                                                /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                                */

                                                Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW








                                                /*var param = new Program.WINDOWPLACEMENT();
                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                var therect = new Program.RECT();
                                                therect.Left = 0;
                                                therect.Top = 0;
                                                therect.Bottom = Rectsccs.Bottom - Rectsccs.Top;
                                                therect.Right = (Rectsccs.Right - Rectsccs.Left);
                                                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                                int screenWidth = GetSystemMetrics(0);
                                                int screenHeight = GetSystemMetrics(1);
                                                */
                                                var param = new Program.WINDOWPLACEMENT();
                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                Program.RECT therect = new Program.RECT();
                                                therect.Left = Rectsccs.Left;
                                                therect.Top = Rectsccs.Top;
                                                therect.Bottom = Rectsccs.Bottom;
                                                therect.Right = (Rectsccs.Right);
                                                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                Program.SetWindowPlacement(vewindowsfoundedz, ref param);

                                                Program.GetWindowRect(vewindowsfoundedz, ref rect);











                                                /*
                                                SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));//WS_EX_OVERLAPPEDWINDOW

                                                SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                */


                                                //id = vewindowsfoundedz;// GetForegroundWindow();
                                                //Random myRandom = new Random();


                                                //MoveWindow(id, myRandom.Next(1024), myRandom.Next(768), Rect.right - Rect.left, Rect.bottom - Rect.top, true);
                                                //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (therect.Right - therect.Left), therect.Bottom - therect.Top, true);
                                                //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (lastRectsccsformovement.Right - lastRectsccsformovement.Left), lastRectsccsformovement.Bottom - lastRectsccsformovement.Top, true);


                                                //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);





                                                SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);

                                                //SetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE, (IntPtr)WS_EX_TOPMOST);//WS_EX_OVERLAPPEDWINDOW


                                            }
                                        }
                                        /*if (RunningProcessPaths.Contains("chrome.exe"))
                                        {
                                            //Google Chrome is running
                                            Console.WriteLine("chrome is running");
                                        }*/

                                    }
                                    else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program") && altcapturedwindowname.Length == 30)
                                    {
                                        if (lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top) //|| lastRectsccsformovement.Right != Rectsccs.Right || lastRectsccsformovement.Bottom != Rectsccs.Bottom
                                                                                                                                          //(lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))//
                                                                                                                                          //if ( vewindowsfoundedz != IntPtr.Zero &&(lastRectsccsformovement.Left  - lastRectsccsformovement.Right) != (Rectsccs.Left  - Rectsccs.Right) ||vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Top  - lastRectsccsformovement.Bottom) != (Rectsccs.Top - Rectsccs.Bottom ))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                                                                                                                          //if (vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                        {



                                            IntPtr id;
                                            Program.RECT Rect = new Program.RECT();
                                            //Thread.Sleep(2000);
                                            id = vewindowsfoundedz;// GetForegroundWindow();
                                            Random myRandom = new Random();
                                            Program.GetWindowRect(id, ref Rect);
                                            Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);


                                            int screenWidth = Program.GetSystemMetrics(0);
                                            int screenHeight = Program.GetSystemMetrics(1);




                                            screenWidth = Program.GetSystemMetrics(0);
                                            screenHeight = Program.GetSystemMetrics(1);



                                            int iHandle = FindWindow(null, capturedwindowname);// "VoidExpanse");

                                            Console.WriteLine(iHandle);

                                            Console.WriteLine(capturedwindowname);
                                            Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

                                            /*sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING
                                            sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING*/


                                            //iHandle = scgraphicssec.FindWindow(null, sccsr14sc.Form1.capturedwindownameform1);
                                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                                            /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                                            */
                                            /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            */
                                            /*Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                                            Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/



                                            /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                            */

                                            Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW








                                            /*var param = new Program.WINDOWPLACEMENT();
                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                            var therect = new Program.RECT();
                                            therect.Left = 0;
                                            therect.Top = 0;
                                            therect.Bottom = Rectsccs.Bottom - Rectsccs.Top;
                                            therect.Right = (Rectsccs.Right - Rectsccs.Left);
                                            param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                            int screenWidth = GetSystemMetrics(0);
                                            int screenHeight = GetSystemMetrics(1);
                                            */
                                            var param = new Program.WINDOWPLACEMENT();
                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                            Program.RECT therect = new Program.RECT();
                                            therect.Left = Rectsccs.Left;
                                            therect.Top = Rectsccs.Top;
                                            therect.Bottom = Rectsccs.Bottom;
                                            therect.Right = (Rectsccs.Right);
                                            param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);

                                            Program.GetWindowRect(vewindowsfoundedz, ref rect);











                                            /*
                                            SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));//WS_EX_OVERLAPPEDWINDOW

                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                            */


                                            //id = vewindowsfoundedz;// GetForegroundWindow();
                                            //Random myRandom = new Random();


                                            //MoveWindow(id, myRandom.Next(1024), myRandom.Next(768), Rect.right - Rect.left, Rect.bottom - Rect.top, true);
                                            //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (therect.Right - therect.Left), therect.Bottom - therect.Top, true);
                                            //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (lastRectsccsformovement.Right - lastRectsccsformovement.Left), lastRectsccsformovement.Bottom - lastRectsccsformovement.Top, true);


                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);





                                            SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);

                                            //SetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE, (IntPtr)WS_EX_TOPMOST);//WS_EX_OVERLAPPEDWINDOW










                                        }
                                    }
                                    else if (altcapturedwindowname.Contains("void") && altcapturedwindowname.Contains("expanse"))
                                    {


                                        if (lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top) //|| lastRectsccsformovement.Right != Rectsccs.Right || lastRectsccsformovement.Bottom != Rectsccs.Bottom
                                                                                                                                          //(lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))//
                                                                                                                                          //if ( vewindowsfoundedz != IntPtr.Zero &&(lastRectsccsformovement.Left  - lastRectsccsformovement.Right) != (Rectsccs.Left  - Rectsccs.Right) ||vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Top  - lastRectsccsformovement.Bottom) != (Rectsccs.Top - Rectsccs.Bottom ))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                                                                                                                          //if (vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                        {



                                            IntPtr id;
                                            Program.RECT Rect = new Program.RECT();
                                            //Thread.Sleep(2000);
                                            id = vewindowsfoundedz;// GetForegroundWindow();
                                            Random myRandom = new Random();
                                            Program.GetWindowRect(id, ref Rect);
                                            Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);


                                            int screenWidth = Program.GetSystemMetrics(0);
                                            int screenHeight = Program.GetSystemMetrics(1);




                                            screenWidth = Program.GetSystemMetrics(0);
                                            screenHeight = Program.GetSystemMetrics(1);



                                            int iHandle = FindWindow(null, capturedwindowname);// "VoidExpanse");

                                            //Console.WriteLine("*************EDGE*****************:" + iHandle);

                                            Console.WriteLine(capturedwindowname);
                                            //Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                                            //Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, lastRectsccsformovement.Left, lastRectsccsformovement.Top, Rectsccscapturedwindow.Right, Rectsccscapturedwindow.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW


                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, SWP_SHOWWINDOW);




                                            /*sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING
                                            sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING*/


                                            //iHandle = scgraphicssec.FindWindow(null, sccsr14sc.Form1.capturedwindownameform1);
                                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                                            /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);

                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            */
                                            /*Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                                            Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/
                                            /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                            Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Right, Rectsccs.Bottom, Rectsccs.Left, Rectsccs.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW


                                            */





                                            /*var param = new Program.WINDOWPLACEMENT();
                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                            var therect = new Program.RECT();
                                            therect.Left = 0;
                                            therect.Top = 0;
                                            therect.Bottom = Rectsccs.Bottom - Rectsccs.Top;
                                            therect.Right = (Rectsccs.Right - Rectsccs.Left);
                                            param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                            int screenWidth = GetSystemMetrics(0);
                                            int screenHeight = GetSystemMetrics(1);
                                            */
                                            /*var param = new Program.WINDOWPLACEMENT();
                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                            Program.RECT therect = new Program.RECT();
                                            therect.Left = Rectsccs.Left;
                                            therect.Top = Rectsccs.Top;
                                            therect.Bottom = Rectsccs.Bottom;
                                            therect.Right = (Rectsccs.Right);
                                            param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);

                                            Program.GetWindowRect(vewindowsfoundedz, ref rect);
                                            */










                                            /*
                                            SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));//WS_EX_OVERLAPPEDWINDOW

                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                            */


                                            //id = vewindowsfoundedz;// GetForegroundWindow();
                                            //Random myRandom = new Random();


                                            //MoveWindow(id, myRandom.Next(1024), myRandom.Next(768), Rect.right - Rect.left, Rect.bottom - Rect.top, true);
                                            //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (therect.Right - therect.Left), therect.Bottom - therect.Top, true);
                                            //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (lastRectsccsformovement.Right - lastRectsccsformovement.Left), lastRectsccsformovement.Bottom - lastRectsccsformovement.Top, true);


                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);




                                            /*
                                            SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);
                                            */
                                            //SetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE, (IntPtr)WS_EX_TOPMOST);//WS_EX_OVERLAPPEDWINDOW




























                                            //progcanpause = 1;
                                            if (progcanpause == 1)
                                            {

                                            }
                                            else
                                            {
                                                //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Bottom, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, SWP_SHOWWINDOW);

                                                /*param = new Program.WINDOWPLACEMENT();
                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                therect = new Program.RECT();
                                                therect.Left = lastRectsccsformovement.Left;
                                                therect.Top = lastRectsccsformovement.Top;
                                                therect.Bottom = lastRectsccsformovement.Bottom;
                                                therect.Right = lastRectsccsformovement.Right;
                                                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/

                                                //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);

                                            }




                                            /*
                                            param = new Program.WINDOWPLACEMENT();
                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                            therect = new Program.RECT();
                                            therect.Left = Rectsccs.Top;
                                            therect.Top = Rectsccs.Left;
                                            therect.Bottom = Rectsccs.Bottom - Rectsccs.Top;
                                            therect.Right = (Rectsccs.Right - Rectsccs.Left);
                                            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/


                                            /*
                                            sccsr14sc.Form1.someform.Size = new System.Drawing.Size(1920, 1080);
                                            sccsr14sc.Form1.someform.FormBorderStyle = FormBorderStyle.None;
                                            sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                            sccsr14sc.Form1.someform.TopMost = true;
                                            SetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));//WS_EX_OVERLAPPEDWINDOW
                                            */



                                            /*
                                            therect = new Program.RECT();
                                            therect.Left = 0;
                                            therect.Top = 0;
                                            therect.Bottom = Rectsccs.Bottom - Rectsccs.Top;
                                            therect.Right = (Rectsccs.Right - Rectsccs.Left);
                                            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/

                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                                        }
                                    }






                                    //Program.MessageBox((IntPtr)0, "altcapturedwindowname:" + altcapturedwindowname, "sccsmsg", 0);
                                    



                                    
                                    else if (altcapturedwindowname.Contains("ilb"))
                                    {


                                        if (lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top) //|| lastRectsccsformovement.Right != Rectsccs.Right || lastRectsccsformovement.Bottom != Rectsccs.Bottom
                                                                                                                                          //(lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))//
                                                                                                                                          //if ( vewindowsfoundedz != IntPtr.Zero &&(lastRectsccsformovement.Left  - lastRectsccsformovement.Right) != (Rectsccs.Left  - Rectsccs.Right) ||vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Top  - lastRectsccsformovement.Bottom) != (Rectsccs.Top - Rectsccs.Bottom ))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                                                                                                                          //if (vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                        {



                                            IntPtr id;
                                            Program.RECT Rect = new Program.RECT();
                                            //Thread.Sleep(2000);
                                            id = vewindowsfoundedz;// GetForegroundWindow();
                                            Random myRandom = new Random();
                                            Program.GetWindowRect(id, ref Rect);
                                            Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);


                                            int screenWidth = Program.GetSystemMetrics(0);
                                            int screenHeight = Program.GetSystemMetrics(1);




                                            screenWidth = Program.GetSystemMetrics(0);
                                            screenHeight = Program.GetSystemMetrics(1);



                                            int iHandle = FindWindow(null, capturedwindowname);// "VoidExpanse");

                                            //Console.WriteLine("*************EDGE*****************:" + iHandle);

                                            Console.WriteLine(capturedwindowname);
                                            //Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                                            //Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, lastRectsccsformovement.Left, lastRectsccsformovement.Top, Rectsccscapturedwindow.Right, Rectsccscapturedwindow.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW


                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, SWP_SHOWWINDOW);

                                        }
                                    }

                                    else if (altcapturedwindowname.Contains("factorio") || altcapturedwindowname.Contains("terraria") || altcapturedwindowname.Contains("starbound"))
                                    {


                                        if (lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top) //|| lastRectsccsformovement.Right != Rectsccs.Right || lastRectsccsformovement.Bottom != Rectsccs.Bottom
                                                                                                                                          //(lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))//
                                                                                                                                          //if ( vewindowsfoundedz != IntPtr.Zero &&(lastRectsccsformovement.Left  - lastRectsccsformovement.Right) != (Rectsccs.Left  - Rectsccs.Right) ||vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Top  - lastRectsccsformovement.Bottom) != (Rectsccs.Top - Rectsccs.Bottom ))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                                                                                                                          //if (vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Right - lastRectsccsformovement.Left) != (Rectsccs.Right - Rectsccs.Left) || vewindowsfoundedz != IntPtr.Zero && (lastRectsccsformovement.Bottom - lastRectsccsformovement.Top) != (Rectsccs.Bottom - Rectsccs.Top))////lastRectsccsformovement.Left != Rectsccs.Left || lastRectsccsformovement.Top != Rectsccs.Top)
                                        {



                                            IntPtr id;
                                            Program.RECT Rect = new Program.RECT();
                                            //Thread.Sleep(2000);
                                            id = vewindowsfoundedz;// GetForegroundWindow();
                                            Random myRandom = new Random();
                                            Program.GetWindowRect(id, ref Rect);
                                            Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);


                                            int screenWidth = Program.GetSystemMetrics(0);
                                            int screenHeight = Program.GetSystemMetrics(1);




                                            screenWidth = Program.GetSystemMetrics(0);
                                            screenHeight = Program.GetSystemMetrics(1);



                                            int iHandle = FindWindow(null, capturedwindowname);// "VoidExpanse");

                                            //Console.WriteLine("*************EDGE*****************:" + iHandle);

                                            Console.WriteLine(capturedwindowname);
                                            //Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                                            //Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, lastRectsccsformovement.Left, lastRectsccsformovement.Top, Rectsccscapturedwindow.Right, Rectsccscapturedwindow.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW


                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, SWP_SHOWWINDOW);

                                        }
                                    }

                                    Rectsccscapturedwindowlast = Rectsccscapturedwindow;

                                    lastRectsccsformovement = Rectsccs;
                                }



                                /*
                                if (gccollectcounter >= gccollectcountermax || gccollectstopwatch.Elapsed.Seconds >= 5)
                                {
                                    gccollectstopwatch.Stop();
                                    gccollectstopwatch.Reset();
                                    gccollectstopwatch.Restart();
                                    GC.Collect();
                                    gccollectcounter = 0;
                                }
                                gccollectcounter++;*/





                                /*
                                if (createinputsswtc == 0)
                                {
                                    if (sccsr14sc.Form1.someform != null)
                                    {
                                        if (sccsr14sc.Form1.theHandle != IntPtr.Zero)
                                        {
                                            var refreshDXEngineAction = new Action(delegate
                                            {
                                                //sccs.scgraphics.scupdate.createinputs(sccsr14sc.Form1.theHandle);
                                                createinputs(sccsr14sc.Form1.theHandle);

                                                //someform = new RenderForm("sccsr14");
                                                /*someform.Size = new System.Drawing.Size(1920, 1080);
                                                someform.FormBorderStyle = FormBorderStyle.None;
                                                someform.WindowState = FormWindowState.Maximized;

                                                //sccsr14sc.Form1.someform.deactivatecursor();

                                                /*var _hwndSource = HwndSource.FromHwnd(someform.Handle);
                                                if (_hwndSource != null)
                                                    _hwndSource.AddHook(WndProc);


                                                SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                                                    SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                                //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                                                //    SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                                SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
                                                // SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;
                                            });
                                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                            //sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                            createinputsswtc = 1;
                                            //MessageBox((IntPtr)0, "createinputs ", "scmsg", 0);
                                        }
                                    }
                                }
                                */

                                /*if (createinputs == 1)
                                {

                                    if (Program.vewindowsfoundedz != IntPtr.Zero)
                                    {

                                        var refreshDXEngineAction = new Action(delegate
                                        {
                                            if (sccsr14sc.Form1.someform.WindowState != FormWindowState.Maximized)
                                            {
                                                //MessageBox((IntPtr)0, "wstate " + sccsr14sc.Form1.someform.WindowState, "scmsg", 0);
                                                //MessageBox((IntPtr)0, "wstate " + sccsr14sc.Form1.someform.WindowState, "scmsg", 0);


                                                sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                                //MessageBox((IntPtr)0, "wstate " + sccsr14sc.Form1.someform.WindowState, "scmsg", 0);

                                                createinputs = 2;
                                            }
                                            else
                                            {
                                                //MessageBox((IntPtr)0, "wstate " + sccsr14sc.Form1.someform.WindowState, "scmsg", 0);
                                            }
                                        });
                                        System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                    }
                                }*/






                                if (sccsr14sc.Form1.someform != null)
                                {



                                    //if (hasstartedcapture == 1)
                                    {

                                        /*
                                        if (hasstartedcapture == 1)
                                        {
                                            var refreshDXEngineAction0 = new Action(delegate
                                            {
                                                //Console.WriteLine("thebutton Visible");
                                                //stackoverflow 661561 for invoking panel changes.
                                                //sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                //{
                                                //    sccsr14sc.Form1.checkbox1.Checked = true;
                                                //});



                                                /*
                                                sccsr14sc.Form1.checkbox3.Invoke((MethodInvoker)delegate
                                                {
                                                    if (sccsr14sc.Form1.checkbox3.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox3.Checked = false;
                                                    }
                                                    else if (!sccsr14sc.Form1.checkbox3.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox3.Checked = true;
                                                    }
                                                });
                                                


                                                sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                {
                                                    if (sccsr14sc.Form1.checkbox1.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox1.Checked = false;
                                                    }
                                                    else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox1.Checked = true;
                                                    }
                                                });

                                               


                                                //sccsr14sc.Form1.haspressedf9 = 1;

                                                //sccsr14sc.Form1.haspressedf9 = 1;

                                            });
                                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                                            hasstartedcapture = 2;
                                        }
                                        */





                                        if (panelchangedswtc == 0)
                                        {
                                            panelchangedwatch.Stop();
                                            panelchangedwatch.Reset();
                                            panelchangedwatch.Restart();

                                            counterpanelchanged = 0;
                                            panelchangedswtc = 1;
                                        }




                                        if (panelchangedwatch.Elapsed.Milliseconds >= 10 && counterpanelchanged >= counterpanelchangedmax)
                                        {

                                            if (sccsr14sc.Form1.haspressedf9 == 1)
                                            {


                                                /*if (sccsr14sc.Form1.thebutton4.Text == "Shrink")
                                                {
                                                    sccsr14sc.Form1.thebutton4.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.theapplistbox.Visible = false;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.theapplistbox.Visible == false)
                                                {
                                                    sccsr14sc.Form1.theapplistbox.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.theapplistbox.Visible = true;
                                                    });
                                                }*/





                                                /*var refreshDXEngineAction0 = new Action(delegate
                                                {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.

                                                    //sccsr14sc.Form1.haspressedf9 = 1;

                                                    sccsr14sc.Form1.checkbox3.Invoke((MethodInvoker)delegate
                                                    {
                                                        if (sccsr14sc.Form1.checkbox3.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox3.Checked = false;
                                                        }
                                                        else if (!sccsr14sc.Form1.checkbox3.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox3.Checked = true;
                                                        }
                                                    });
                                                });
                                                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                                                */

                                                /*var refreshDXEngineAction0 = new Action(delegate
                                                {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.

                                                    //sccsr14sc.Form1.haspressedf9 = 1;

                                                    sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                    {
                                                        if (sccsr14sc.Form1.checkbox1.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox1.Checked = false;
                                                        }
                                                        else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox1.Checked = true;
                                                        }
                                                    });
                                                });
                                                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);*/

                                                /*var refreshDXEngineAction0 = new Action(delegate
                                                {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.

                                                    //sccsr14sc.Form1.haspressedf9 = 1;

                                                    sccsr14sc.Form1.checkbox2.Invoke((MethodInvoker)delegate
                                                    {
                                                        if (sccsr14sc.Form1.checkbox2.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox2.Checked = false;
                                                        }
                                                        else if (!sccsr14sc.Form1.checkbox2.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox2.Checked = true;
                                                        }
                                                    });
                                                });
                                                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                                                */
                                                //sccsr14sc.Form1.haspressedf9 = 2;

                                                /*
                                                sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                {
                                                    if (sccsr14sc.Form1.checkbox1.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox1.Checked = false;

                                                        sccsr14sc.Form1.thebutton4.Invoke((MethodInvoker)delegate
                                                        {
                                                            sccsr14sc.Form1.thebutton4.Text = "Shrink";
                                                        });
                                                    }
                                                    else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox1.Checked = true;


                                                        sccsr14sc.Form1.thebutton4.Invoke((MethodInvoker)delegate
                                                        {
                                                            sccsr14sc.Form1.thebutton4.Text = "Maximize";
                                                        });



                                                    }
                                                });*/
                                                

                                                sccsr14sc.Form1.haspressedf9 = 2;
                                            }











                                            if (sccsr14sc.Form1.haspressedescape == 1)
                                            {
                                                var refreshDXEngineAction0 = new Action(delegate
                                                {

                                                    





                                                    sccsr14sc.Form1.checkbox3.Invoke((MethodInvoker)delegate
                                                    {
                                                        if (sccsr14sc.Form1.checkbox3.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox3.Checked = false;
                                                        }
                                                        else if (!sccsr14sc.Form1.checkbox3.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox3.Checked = true;
                                                        }
                                                    });
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.
                                                    /*sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.checkbox1.Checked = true;
                                                    });
                                                    */


                                                    /*
                                                    sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                    {
                                                        if (sccsr14sc.Form1.checkbox1.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox1.Checked = false;
                                                        }
                                                        else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox1.Checked = true;
                                                        }
                                                    });*/

                                                    //sccsr14sc.Form1.haspressedf9 = 1;

                                                    //sccsr14sc.Form1.haspressedf9 = 1;

                                                });
                                                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);



                                                /*
                                                var refreshDXEngineAction0 = new Action(delegate
                                                {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.

                                                    //sccsr14sc.Form1.haspressedf9 = 1;

                                                    sccsr14sc.Form1.checkbox3.Invoke((MethodInvoker)delegate
                                                    {
                                                        if (sccsr14sc.Form1.checkbox3.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox3.Checked = false;
                                                        }
                                                        else if (!sccsr14sc.Form1.checkbox3.Checked)
                                                        {
                                                            sccsr14sc.Form1.checkbox3.Checked = true;
                                                        }
                                                    });
                                                });
                                                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                                                */

                                                /* */





                                                /*sccsr14sc.Form1.haspressedescape = 2;

                                                /*else if (sccsr14sc.Form1.someform.haspressedf9 == 1)
                                                {


                                                }

                                                sccsr14sc.Form1.someform.haspressedsomekeyboardkey = 0;
                                                counterpanelchanged = 0;

                                                panelchangedswtc = 0;
                                                //sccsr14sc.Form1.someform.haspressedf9 = 2;
                                                panelchangedwatch.Stop();*/


                                                if (sccsr14sc.Form1.button1exit.Visible == false)
                                                {
                                                    sccsr14sc.Form1.button1exit.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.button1exit.Visible = true;
                                                    });



                                                    /*var refreshDXEngineAction0 = new Action(delegate
                                                    {
                                                        //Console.WriteLine("thebutton Visible");
                                                        //stackoverflow 661561 for invoking panel changes.

                                                        //sccsr14sc.Form1.haspressedf9 = 1;

                                                        sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                        {
                                                            if (sccsr14sc.Form1.checkbox1.Checked)
                                                            {
                                                                sccsr14sc.Form1.checkbox1.Checked = false;
                                                            }
                                                            else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                            {
                                                                sccsr14sc.Form1.checkbox1.Checked = true;
                                                            }
                                                        });
                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);*/




                                                }
                                                else if (sccsr14sc.Form1.button1exit.Visible == true)
                                                {
                                                    sccsr14sc.Form1.button1exit.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.button1exit.Visible = false;
                                                    });
                                                }


                                                /*
                                                if (sccsr14sc.Form1.button2changeprog.Visible == false)
                                                {
                                                    sccsr14sc.Form1.button2changeprog.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.button2changeprog.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.button2changeprog.Visible == true)
                                                {
                                                    sccsr14sc.Form1.button2changeprog.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.button2changeprog.Visible = false;
                                                    });
                                                }*/








                                                if (sccsr14sc.Form1.someform.checkbox4proj.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.checkbox4proj.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.checkbox4proj.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.checkbox4proj.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.checkbox4proj.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.checkbox4proj.Visible = false;
                                                    });
                                                }






                                                /*
                                                if (sccsr14sc.Form1.checkbox1.Visible == false)
                                                {
                                                    sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.checkbox1.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.checkbox1.Visible == true)
                                                {
                                                    sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.checkbox1.Visible = false;
                                                    });
                                                }*/

                                                if (sccsr14sc.Form1.someform.labeltext0.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.labeltext0.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.labeltext0.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.labeltext0.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.labeltext0.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.labeltext0.Visible = false;
                                                    });
                                                }





                                                /*
                                                if (sccsr14sc.Form1.someform.labeltext1.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.labeltext1.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.labeltext1.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.labeltext1.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.labeltext1.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.labeltext1.Visible = false;
                                                    });
                                                }*/




                                                if (sccsr14sc.Form1.someform.labeltext2.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.labeltext2.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.labeltext2.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.labeltext2.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.labeltext2.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.labeltext2.Visible = false;
                                                    });
                                                }


                                                if (sccsr14sc.Form1.someform.labeltext3.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.labeltext3.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.labeltext3.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.labeltext3.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.labeltext3.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.labeltext3.Visible = false;
                                                    });
                                                }
























                                                /*

                                                if (sccsr14sc.Form1.someform.trackBar1.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.trackBar1.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.trackBar1.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.trackBar1.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.trackBar1.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.trackBar1.Visible = false;
                                                    });
                                                }
                                                */




                                                if (sccsr14sc.Form1.someform.colortrackBar2.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar2.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar2.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.colortrackBar2.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar2.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar2.Visible = false;
                                                    });
                                                }





                                                if (sccsr14sc.Form1.someform.colortrackBar3.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar3.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar3.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.colortrackBar3.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar3.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar3.Visible = false;
                                                    });
                                                }





                                                if (sccsr14sc.Form1.someform.colortrackBar4.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar4.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar4.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.colortrackBar4.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar4.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar4.Visible = false;
                                                    });
                                                }





                                                if (sccsr14sc.Form1.someform.colortrackBar5.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar5.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar5.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.colortrackBar5.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar5.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar5.Visible = false;
                                                    });
                                                }





                                                if (sccsr14sc.Form1.someform.colortrackBar6.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar6.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar6.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.colortrackBar6.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar6.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar6.Visible = false;
                                                    });
                                                }



                                                if (sccsr14sc.Form1.someform.colortrackBar7.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar7.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar7.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.colortrackBar7.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.colortrackBar7.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.colortrackBar7.Visible = false;
                                                    });
                                                }









                                                /*
                                                if (sccsr14sc.Form1.someform.numericUpDown01.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown01.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown01.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.numericUpDown01.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown01.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown01.Visible = false;
                                                    });
                                                }





                                                if (sccsr14sc.Form1.someform.numericUpDown02.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown02.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown02.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.numericUpDown02.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown02.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown02.Visible = false;
                                                    });
                                                }





                                                if (sccsr14sc.Form1.someform.numericUpDown03.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown03.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown03.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.numericUpDown03.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown03.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown03.Visible = false;
                                                    });
                                                }





                                                if (sccsr14sc.Form1.someform.numericUpDown04.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown04.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown04.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.numericUpDown04.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown04.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown04.Visible = false;
                                                    });
                                                }





                                                if (sccsr14sc.Form1.someform.numericUpDown05.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown05.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown05.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.numericUpDown05.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown05.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown05.Visible = false;
                                                    });
                                                }





                                                if (sccsr14sc.Form1.someform.numericUpDown06.Visible == false)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown06.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown06.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.someform.numericUpDown06.Visible == true)
                                                {
                                                    sccsr14sc.Form1.someform.numericUpDown06.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.someform.numericUpDown06.Visible = false;
                                                    });
                                                }*/













                                                /*
                                                if (sccsr14sc.Form1.checkbox2.Visible == false)
                                                {
                                                    sccsr14sc.Form1.checkbox2.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.checkbox2.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.checkbox2.Visible == true)
                                                {
                                                    sccsr14sc.Form1.checkbox2.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.checkbox2.Visible = false;
                                                    });
                                                }*/


                                                if (sccsr14sc.Form1.comboboxcapturelist.Visible == false)
                                                {
                                                    sccsr14sc.Form1.comboboxcapturelist.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.comboboxcapturelist.Visible = true;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.comboboxcapturelist.Visible == true)
                                                {
                                                    sccsr14sc.Form1.comboboxcapturelist.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.comboboxcapturelist.Visible = false;
                                                    });
                                                }

                                                /*
                                                if (sccsr14sc.Form1.thepanel.Visible == false)
                                                {
                                                    var refreshDXEngineAction = new Action(delegate
                                                    {
                                                        //Console.WriteLine("thebutton Visible");
                                                        //stackoverflow 661561 for invoking panel changes.
                                                        sccsr14sc.Form1.thepanel.Invoke((MethodInvoker)delegate
                                                        {
                                                            sccsr14sc.Form1.thepanel.Visible = true;
                                                        });

                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                }
                                                else if (sccsr14sc.Form1.thepanel.Visible == true)
                                                {
                                                    var refreshDXEngineAction = new Action(delegate
                                                    {
                                                        //Console.WriteLine("thebutton Visible");
                                                        //stackoverflow 661561 for invoking panel changes.
                                                        sccsr14sc.Form1.thepanel.Invoke((MethodInvoker)delegate
                                                        {
                                                            sccsr14sc.Form1.thepanel.Visible = false;
                                                        });

                                                        //sccsr14sc.Form1.someform.haspressedf9 = 2;
                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);

                                                }
                                                */


                                                if (sccsr14sc.Form1.trackbar.Visible == false)
                                                {
                                                    var refreshDXEngineAction = new Action(delegate
                                                    {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.
                                                        sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                                        {
                                                            sccsr14sc.Form1.trackbar.Visible = true;
                                                        });

                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                }
                                                else if (sccsr14sc.Form1.trackbar.Visible == true)
                                                {
                                                    var refreshDXEngineAction = new Action(delegate
                                                    {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.
                                                        sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                                        {
                                                            sccsr14sc.Form1.trackbar.Visible = false;
                                                        });

                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                }



                                                if (sccsr14sc.Form1.checkedlistbox.Visible == false)
                                                {
                                                    var refreshDXEngineAction = new Action(delegate
                                                    {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.
                                                        sccsr14sc.Form1.checkedlistbox.Invoke((MethodInvoker)delegate
                                                        {
                                                            sccsr14sc.Form1.checkedlistbox.Visible = true;
                                                        });

                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                }

                                                else if (sccsr14sc.Form1.checkedlistbox.Visible == true)
                                                {
                                                    var refreshDXEngineAction = new Action(delegate
                                                    {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.
                                                        sccsr14sc.Form1.checkedlistbox.Invoke((MethodInvoker)delegate
                                                        {
                                                            sccsr14sc.Form1.checkedlistbox.Visible = false;
                                                        });

                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                }




                                                







                                                if (sccsr14sc.Form1.thebutton3.Visible == true)
                                                {
                                                    sccsr14sc.Form1.thebutton3.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.thebutton3.Visible = false;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.thebutton3.Visible == false)
                                                {
                                                    sccsr14sc.Form1.thebutton3.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.thebutton3.Visible = true;
                                                    });
                                                }



                                                if (sccsr14sc.Form1.thebutton4.Visible == true)
                                                {
                                                    sccsr14sc.Form1.thebutton4.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.thebutton4.Visible = false;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.thebutton4.Visible == false)
                                                {
                                                    sccsr14sc.Form1.thebutton4.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.thebutton4.Visible = true;
                                                    });
                                                }

                                                if (sccsr14sc.Form1.theapplistbox.Visible == true)
                                                {
                                                    sccsr14sc.Form1.theapplistbox.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.theapplistbox.Visible = false;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.theapplistbox.Visible == false)
                                                {
                                                    sccsr14sc.Form1.theapplistbox.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.theapplistbox.Visible = true;
                                                    });
                                                }







                                                if (sccsr14sc.Form1.thebutton5.Visible == true)
                                                {
                                                    sccsr14sc.Form1.thebutton5.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.thebutton5.Visible = false;
                                                    });
                                                }
                                                else if (sccsr14sc.Form1.thebutton5.Visible == false)
                                                {
                                                    sccsr14sc.Form1.thebutton5.Invoke((MethodInvoker)delegate
                                                    {
                                                        sccsr14sc.Form1.thebutton5.Visible = true;
                                                    });
                                                }




                                                sccsr14sc.Form1.haspressedescape = 2;
                                            }
                                            /*else if (sccsr14sc.Form1.someform.haspressedf9 == 1)
                                            {


                                            }*/

                                            sccsr14sc.Form1.someform.haspressedsomekeyboardkey = 0;
                                            counterpanelchanged = 0;

                                            panelchangedswtc = 0;
                                            //sccsr14sc.Form1.someform.haspressedf9 = 2;
                                            panelchangedwatch.Stop();



                                        }
                                        counterpanelchanged++;











                                        if (progcanpause == 1)
                                        {
                                            /*var refreshDXEngineAction = new Action(delegate
                                            {
                                                //Console.WriteLine("thebutton Visible");
                                                //stackoverflow 661561 for invoking panel changes.


                                                sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                {
                                                    if (sccsr14sc.Form1.checkbox1.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox1.Checked = false;
                                                    }
                                                    else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox1.Checked = true;
                                                    }
                                                });

                                                //sccsr14sc.Form1.haspressedf9 = 1;

                                                //sccsr14sc.Form1.haspressedf9 = 1;

                                            });
                                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                            */
                                            /*if (sccsr14sc.Form1.checkbox.Visible == false)
                                            {
                                               }*/


                                            //Console.WriteLine("sccsr14sc.Form1.haspressedf9:" + sccsr14sc.Form1.haspressedf9);

                                            /*if (sccsr14sc.Form1.haspressedf9 == 0)
                                            {
                                                //progcanpause = 0; //2
                                                if (sccsr14sc.Form1.haspressedescape == 0)
                                                {
                                                    progcanpause = 0; //2
                                                }
                                                else //if (sccsr14sc.Form1.haspressedescape == 1)
                                                {
                                                    progcanpause = 0; //2

                                                }
                                            }
                                            else //if (sccsr14sc.Form1.haspressedf9 == 1)
                                            {
                                                //progcanpause = 2; //2
                                                if (sccsr14sc.Form1.haspressedescape == 0)
                                                {
                                                    progcanpause = 2; //2

                                                }
                                                else// if (sccsr14sc.Form1.haspressedescape == 1)
                                                {
                                                    progcanpause = 2; //2

                                                }
                                            }*/

                                            //progcanpause = 0;
                                        }

                                        else if (progcanpause == 2)
                                        {
                                            /*var refreshDXEngineAction = new Action(delegate
                                            {
                                                //Console.WriteLine("thebutton Visible");
                                                //stackoverflow 661561 for invoking panel changes.
                                                sccsr14sc.Form1.checkbox2.Invoke((MethodInvoker)delegate
                                                {
                                                    if (sccsr14sc.Form1.checkbox2.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox2.Checked = false;
                                                    }
                                                    else if (!sccsr14sc.Form1.checkbox2.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox2.Checked = true;
                                                    }
                                                });



                                            });
                                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                            */
                                            /*if (sccsr14sc.Form1.checkbox.Visible == false)
                                            {
                                               }*/
                                            //progcanpause = 0;
                                        }

                                        else if (progcanpause == 3)
                                        {





                                            /*var refreshDXEngineAction0 = new Action(delegate
                                            {
                                                //Console.WriteLine("thebutton Visible");
                                                //stackoverflow 661561 for invoking panel changes.

                                                //sccsr14sc.Form1.haspressedf9 = 1;

                                                sccsr14sc.Form1.checkbox3.Invoke((MethodInvoker)delegate
                                                {
                                                    if (sccsr14sc.Form1.checkbox3.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox3.Checked = false;
                                                    }
                                                    else if (!sccsr14sc.Form1.checkbox3.Checked)
                                                    {
                                                        sccsr14sc.Form1.checkbox3.Checked = true;
                                                    }
                                                });
                                            });
                                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                                            */


                                            //progcanpause = 0;
                                        }


                                    }

                                }











                                if (sccsr14sc.Form1.someform != null && updatescript != null)
                                {
                                    if (heightmaptrackbarchangedswtc == 0)
                                    {
                                        heightmaptrackbarchangedwatch.Stop();
                                        heightmaptrackbarchangedwatch.Reset();
                                        heightmaptrackbarchangedwatch.Restart();
                                        heightmaptrackbarchangedswtc = 1;
                                    }


                                    if (hasresettedcapture == 1)
                                    {
                                        //this.trackBar1.Value = -1000;

                                        var refreshDXEngineAction = new Action(delegate
                                        {
                                        //Console.WriteLine("thebutton Visible");
                                        //stackoverflow 661561 for invoking panel changes.

                                        //Console.WriteLine("thebutton Visible");
                                        //stackoverflow 661561 for invoking panel changes.

                                            sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                            {
                                            /*if (sccsr14sc.Form1.trackbar.Value > sccsr14sc.Form1.trackbar.Minimum)
                                            {
                                                sccsr14sc.Form1.trackbar.Value -= sccsr14sc.Form1.trackbar.TickFrequency;
                                            }*/
                                            //sccsr14sc.Form1.trackbar.Value = 0; //-1000
                                                sccsr14sc.Form1.someform.heightmapvalue = sccsr14sc.Form1.trackbar.Value;
                                            });

                                        //sccsr14sc.Form1.someform.haspressedf9 = 2;
                                        });
                                        System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);


                                        hasresettedcapture = 0;
                                    }




                                    if (updatescript.haspressedheightmapvaluedecrease == 1 || updatescript.haspressedheightmapvalueincrease == 1)
                                    {
                                        if (updatescript.haspressedheightmapvaluedecrease == 1)
                                        {
                                            if (panelchangedwatch.Elapsed.Ticks >= 1)
                                            {

                                                if (sccsr14sc.Form1.someform.heightmapvalue > sccsr14sc.Form1.someform.heightmapvaluemin + (sccsr14sc.Form1.someform.heightmapvaluetickfreq * 1))
                                                {
                                                    var refreshDXEngineAction = new Action(delegate
                                                    {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.

                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.

                                                        sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                                        {
                                                        /*if (sccsr14sc.Form1.trackbar.Value > sccsr14sc.Form1.trackbar.Minimum)
                                                        {
                                                            sccsr14sc.Form1.trackbar.Value -= sccsr14sc.Form1.trackbar.TickFrequency;
                                                        }*/
                                                            sccsr14sc.Form1.trackbar.Value -= sccsr14sc.Form1.trackbar.TickFrequency;
                                                        });

                                                    //sccsr14sc.Form1.someform.haspressedf9 = 2;
                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                }
                                                else
                                                {
                                                    //sccsr14sc.Form1.someform.heightmapvalue = sccsr14sc.Form1.someform.heightmapvaluemin + (sccsr14sc.Form1.someform.heightmapvaluetickfreq * 1);
                                                }
                                                updatescript.haspressedheightmapvaluedecrease = 0;
                                                heightmaptrackbarchangedswtc = 0;
                                            }

                                        }

                                        if (updatescript.haspressedheightmapvalueincrease == 1)
                                        {
                                            if (panelchangedwatch.Elapsed.Milliseconds >= 10)
                                            {
                                                if (sccsr14sc.Form1.someform.heightmapvalue < sccsr14sc.Form1.someform.heightmapvaluemax - (sccsr14sc.Form1.someform.heightmapvaluetickfreq * 1))
                                                {
                                                    var refreshDXEngineAction = new Action(delegate
                                                    {
                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.

                                                    //Console.WriteLine("thebutton Visible");
                                                    //stackoverflow 661561 for invoking panel changes.

                                                        sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                                        {
                                                        /*if (sccsr14sc.Form1.trackbar.Value > sccsr14sc.Form1.trackbar.Minimum)
                                                        {
                                                            sccsr14sc.Form1.trackbar.Value -= sccsr14sc.Form1.trackbar.TickFrequency;
                                                        }*/
                                                            sccsr14sc.Form1.trackbar.Value += sccsr14sc.Form1.trackbar.TickFrequency;
                                                        });

                                                    //sccsr14sc.Form1.someform.haspressedf9 = 2;
                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                }
                                                else
                                                {
                                                    //sccsr14sc.Form1.someform.heightmapvalue = (sccsr14sc.Form1.someform.heightmapvaluemax - (sccsr14sc.Form1.someform.heightmapvaluetickfreq * 1));
                                                }
                                                updatescript.haspressedheightmapvalueincrease = 0;
                                                heightmaptrackbarchangedswtc = 0;
                                            }
                                        }
                                    }
                                }
                            }





                        } //if (screencapturetype == 0 && changedscreencapturetype == 0 || screencapturetype == 1 && changedscreencapturetype == 0) 
                        else //if (screencapturetype == 0 && changedscreencapturetype == 0 || screencapturetype == 1 && changedscreencapturetype == 0) 
                        {

                            Console.WriteLine("captured name not null and selected item is the same. Gotta restart capturing a program. 1");

                            Console.WriteLine("vewindowsfoundedz == IntPtr.Zero" + IntPtr.Zero);

                            if (vewindowsfoundedz == IntPtr.Zero)
                            {
                                //changedscreencapturetype = 1;
                                Console.WriteLine("vewindowsfoundedz == IntPtr.Zero" + IntPtr.Zero);
                            }
                        }
                    }
                    else //if initform == 0 or == 1
                    {

                    }



























                    if (sccsr14sc.Form1.someform != null && updatescript != null)
                    {
                        /*if (heightmaptrackbarchangedswtc == 0)
                        {
                            heightmaptrackbarchangedwatch.Stop();
                            heightmaptrackbarchangedwatch.Reset();
                            heightmaptrackbarchangedwatch.Restart();
                            heightmaptrackbarchangedswtc = 1;
                        }

                        hasgot0*/
                    }






                    /*if (sccsr14sc.Form1.someform!= null)
                    {
                        Console.WriteLine(sccsr14sc.Form1.someform.heightmapvalue);
                    }*/










                    firstframededicatedbackgroundworker = 1;

                    lastRectsccsformovement = Rectsccs;
 
                    Thread.Sleep(1);


                }


            };

            backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
            {
                Console.WriteLine("worker ended prematurely ");
                MessageBox((IntPtr)0, "worker ended prematurely", "scmsg", 0);
            };

            backgroundWorker.RunWorkerAsync();




            //System.Windows.Forms.Cursor.Hide();
            //MessageBox((IntPtr)0, "form initiated0", "scmsg", 0);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);



            someform = new Form1();

            someform.Name = "sccs";
            someform.Text = "sccs";


            /*//textBox = this.textBox1;
            textBox = new System.Windows.Forms.TextBox() { Dock = DockStyle.Fill, Multiline = true, Text = "Interact with the mouse or the keyboard...\r\n", ReadOnly = true };
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Text = "Interact with the mouse or the keyboard...\r\n";
            textBox.ReadOnly = true;


            someform.Controls.Add(textBox);
            //this.Visible = true;


            var _hwndSource = HwndSource.FromHwnd(someform.Handle);
            if (_hwndSource != null)
                _hwndSource.AddHook(WndProc);


            //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
            //    SharpDX.RawInput.DeviceFlags.None, this.Handle);
            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                SharpDX.RawInput.DeviceFlags.None, someform.Handle);
            //SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;*/


            //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, SharpDX.RawInput.DeviceFlags.None);
            //SharpDX.RawInput.Device.KeyboardInput += (sender, args) => textBox.Invoke(new UpdateTextCallback(UpdateKeyboardText), args);





            //someform = new RenderForm("sccsr14");
            someform.Size = new System.Drawing.Size(640, 480); //1920 / 1080
            //someform.Size = new System.Drawing.Size(1920, 1080);

            //someform.CreateControl();
            //someform.TransparencyKey = System.Drawing.Color.Black;
            //someform.BackColor = System.Drawing.Color.Black;
            //someform.Activate();

            //someform.FormBorderStyle = FormBorderStyle.None;
            //someform.WindowState = FormWindowState.Maximized;
            //someform.Opacity = 0.5f;
            someform.TopMost = true;
            //someform.Activate();



            //WinCursors.HideCursor();









            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;

            //thepanel = new System.Windows.Controls.Panel();

            /*var _hwndSource = HwndSource.FromHwnd(sccsr14sc.Form1.theHandle);
            if (_hwndSource != null)
                _hwndSource.AddHook(WndProc);

            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                SharpDX.RawInput.DeviceFlags.None, sccsr14sc.Form1.theHandle);
            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                SharpDX.RawInput.DeviceFlags.None, sccsr14sc.Form1.theHandle);
            SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;*/

            mainreceivedmessages = new scmessageobject.scmessageobject[MaxSizeMainObject];

            for (int i = 0; i < mainreceivedmessages.Length; i++)
            {
                mainreceivedmessages[i] = new scmessageobject.scmessageobject();
                mainreceivedmessages[i]._received_switch_in = -1;
                mainreceivedmessages[i]._received_switch_out = -1;
                mainreceivedmessages[i]._sending_switch_in = -1;
                mainreceivedmessages[i]._sending_switch_out = -1;
                mainreceivedmessages[i]._timeOut0 = -1;
                mainreceivedmessages[i]._ParentTaskThreadID0 = -1;
                mainreceivedmessages[i]._main_cpu_count = 1;
                mainreceivedmessages[i]._passTest = "";
                mainreceivedmessages[i]._welcomePackage = -1;
                mainreceivedmessages[i]._work_done = -1;
                mainreceivedmessages[i]._current_menu = -1;
                mainreceivedmessages[i]._last_current_menu = -1;
                mainreceivedmessages[i]._main_menu = -1;
                mainreceivedmessages[i]._menuOption = "";
                mainreceivedmessages[i]._voRecSwtc = -1;
                mainreceivedmessages[i]._voRecMsg = "";
                mainreceivedmessages[i]._someData = new object[MaxSizeMainObject];

                for (int j = 0; j < mainreceivedmessages[i]._someData.Length; j++)
                {
                    mainreceivedmessages[i]._someData[j] = new object();
                }


                //mainreceivedmessages[0]._someData[0] = new object();


                //VOICE RECOGNITION. NOT IMPLEMENTED YET.
                /*if (i == MaxSizeMainObject - 1)
                {
                    mainreceivedmessages[i]._someData[0] = _keyboard_input._KeyboardState;
                    mainreceivedmessages[i]._voRecSwtc = 1;
                }*/
            }
            ///////////////////////////////
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////  
            ///message_thread_safe_kinda///   
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////


            config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);

            //SCGLOBALSACCESSORS AND GLOBALS CREATOR
            /*sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages);
            if (SCGLOBALSACCESSORS == null)
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS NULL", "Console");
            }
            else
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS !NULL", "Console");
            }*/
            //borderlessconsole console_ = new borderlessconsole();
            //SCGLOBALSACCESSORS AND GLOBALS CREATOR

            //var somewindow = new WindowInteropHelper(sccsr14sc.Form1);
            //var somewindow = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);
            //consoleHandle = somewindow.EnsureHandle();



            //keynmouseinput.IsMouseButtonDown
            //mousesim.


            int screencaptureresultswtc = 0;


            int lastwindowwidth = 0;
            int lastwindowheight = 0;



            /*inputsim = new InputSimulator();
            mousesim = new MouseSimulator(inputsim);
            keyboardsim = new KeyboardSimulator(inputsim);
            */
            /*keynmouseinput = new DInput();
            keynmouseinput.Initialize(config, sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle);
            */

            //SetWindowLong(SCGLOBALSACCESSORS.SCCONSOLECORE.handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(SCGLOBALSACCESSORS.SCCONSOLECORE.handle, GWL_EXSTYLE) | WS_EX_TOPMOST )); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW
            //SetLayeredWindowAttributes(SCGLOBALSACCESSORS.SCCONSOLECORE.handle, 0, 255, LWA_ALPHA);





            RenderLoop.Run(someform, () =>
            {











                if (createinputsswtc == 0)
                {
                    if (sccsr14sc.Form1.someform != null)
                    {
                        if (sccsr14sc.Form1.theHandle != IntPtr.Zero)
                        {

                            SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages, createconsole);
                            if (SCGLOBALSACCESSORS == null)
                            {
                                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS NULL", "Console");
                            }
                            else
                            {
                                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS !NULL", "Console");
                            }
                            createinputs(IntPtr.Zero);








                            consoleHandle = someform.Handle;// sccsr14sc.Form1.theHandle;
                            /*
                            var refreshDXEngineAction = new Action(delegate
                            {
                                //sccs.scgraphics.scupdate.createinputs(sccsr14sc.Form1.theHandle);
                                //createinputs(sccsr14sc.Form1.theHandle);

                                //someform = new RenderForm("sccsr14");
                                /*someform.Size = new System.Drawing.Size(1920, 1080);
                                someform.FormBorderStyle = FormBorderStyle.None;
                                someform.WindowState = FormWindowState.Maximized;

                                //sccsr14sc.Form1.someform.deactivatecursor();

                                /*var _hwndSource = HwndSource.FromHwnd(someform.Handle);
                                if (_hwndSource != null)
                                    _hwndSource.AddHook(WndProc);


                                SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                                    SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                                //    SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
                                // SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;
                            });
                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                            *///sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                            createinputsswtc = 1;
                            //MessageBox((IntPtr)0, "createinputs ", "scmsg", 0);
                        }
                    }
                }




                if (keynmouseinput != null)
                {

                    /*
                    var refreshDXEngineAction = new Action(delegate
                    {

                    });
                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                    */
                    keynmouseinput.ReadKeyboard();
                }



                // draw it
                //device.ImmediateContext.Draw(4, 0);
                //swapChain1.Present(1, PresentFlags.None, new PresentParameters());




                //mainthreadloop:


                /*
                if (updatescript != null)
                {
                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                    {

                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                        {
                            if (updatescript.scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (updatescript.scgraphicssecpackagemessage.scjittertasks.Length > 0)
                                {
                                    if (updatescript.scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        //if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                        {
                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);

                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);


                                            updatescript.scgraphicssecpackagemessage.scjittertasks = sccsjittertasks;
                                            sccsjittertasks = updatescript.StartRender(null,updatescript.scgraphicssecpackagemessage.scjittertasks);

                                            sccsjittertasks = updatescript.scgraphicssecpackagemessage.scgraphicssec.workonshaders(updatescript.scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                */


                int loadvetextures = 0;

                if (initthread == 0)
                {
                    _mainThread = new Thread((tester0000) =>
                    {
                    //sccs.Program.MessageBox((IntPtr)0, "Form1", "scmsg", 0);



                    _thread_main_loop:












                        /*if (keynmouseinput != null)
                        {
                            keynmouseinput.Frame();
                        }*/


                        //WinCursors.HideCursor();
                        /*if (someform != null)
                        {
                            if (someform.Handle != IntPtr.Zero)
                            {
                                //consoleHandle = someform.Handle;

                                //sccsr14sc.Form1.theHandle;
                                //consoleHandle = scconsolecore.handle;

                                /*inputsim = new InputSimulator();
                                mousesim = new MouseSimulator(inputsim);
                                keyboardsim = new KeyboardSimulator(inputsim);

                                keynmouseinput = new DInput();
                                keynmouseinput.Initialize(config, scconsolecore.handle);

                                initform = 2;
                            }
                        }*/


                        /*if (Console.WindowWidth != lastwindowwidth || Console.WindowHeight  != lastwindowheight)
                        {
                            keynmouseinput = new DInput();
                            keynmouseinput.Initialize(config, consoleHandle);
                        }
                        keynmouseinput.Frame();
                        lastwindowwidth = Console.WindowWidth;
                        lastwindowheight = Console.WindowHeight;*/




                        if (initform == 1 && sccsr14sc.Form1.initForm == 1)
                        {

                            /*
                            keynmouseinput = new DInput();
                            keynmouseinput.Initialize(config, sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle);
                            */
                            /*keynmouseinput = new DInput();
                            keynmouseinput.Initialize(config, sccsr14sc.Form1.theHandle); //scconsolecore.handle
                            */

                            /*var _hwndSource = HwndSource.FromHwnd(sccsr14sc.Form1.theHandle);
                            if (_hwndSource != null)
                                _hwndSource.AddHook(WndProc);

                            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                                SharpDX.RawInput.DeviceFlags.None, sccsr14sc.Form1.theHandle);
                            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                                SharpDX.RawInput.DeviceFlags.None, sccsr14sc.Form1.theHandle);
                            SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
                            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;*/



                            // sccs.Program.MessageBox((IntPtr)0, "sccsr14sc.Form1.initForm ", "scmsg", 0);
                            //consoleHandle = sccsr14sc.Form1.theHandle;

                            //System.Windows.Forms.Cursor.Hide();
                            //ShowCursor(false);


                            /*if (saveplayerposition != )
                            {

                            }*/

                            if (screencapturetype == 0)
                            {
                                /*if (updatescript!= null)
                                {
                                    updatescript.ca
                                }*/

                                var capturetype = new GraphicsCapture();



                                /*capturetype.typeofwindowpicker = typeofwindowpicker;
                                capturetype.capturedwindowname = lastcapturedwindowname;
                                capturetype._hWnd = lastcapturedhwnd;
                                capturetype.fullscreen = typeofwindowpicker;*/






                                updatescript = new scupdate(capturetype);

                                scupdate.OFFSETPOS = saveplayerposition;
                                scupdate.finalRotationMatrix = saveplayerfinalRotationMatrix;
                                scupdate.rotatingMatrix = saveplayerrotatingMatrixForPelvis;
                                scupdate.movePos = saveplayermovePos;
                                scupdate.rotx = saveplayerrotx;
                                scupdate.roty = saveplayerroty;
                                scupdate.rotz = saveplayerrotz;


                            }
                            else if (screencapturetype == 1)
                            {
                                var capturetype = new DwmSharedSurface();
                                //capturetype.typeofwindowpicker = typeofwindowpicker;

                                updatescript = new scupdate(capturetype);
                                scupdate.OFFSETPOS = saveplayerposition;
                                scupdate.finalRotationMatrix = saveplayerfinalRotationMatrix;
                                scupdate.rotatingMatrix = saveplayerrotatingMatrixForPelvis;
                                scupdate.movePos = saveplayermovePos;
                                scupdate.rotx = saveplayerrotx;
                                scupdate.roty = saveplayerroty;
                                scupdate.rotz = saveplayerrotz;


                            }
                            else if (screencapturetype == 2)
                            {

                                updatescript = new scupdate(null);
                                scupdate.OFFSETPOS = saveplayerposition;
                                scupdate.finalRotationMatrix = saveplayerfinalRotationMatrix;
                                scupdate.rotatingMatrix = saveplayerrotatingMatrixForPelvis;
                                scupdate.movePos = saveplayermovePos;
                                scupdate.rotx = saveplayerrotx;
                                scupdate.roty = saveplayerroty;
                                scupdate.rotz = saveplayerrotz;

                            }
                           /* else if (screencapturetype == -1)
                            {
                                var capturetype = new GraphicsCapture();
                                capturetype.typeofwindowpicker = 1;
                                updatescript = new scupdate(capturetype);

                                scupdate.OFFSETPOS = saveplayerposition;
                                scupdate.finalRotationMatrix = saveplayerfinalRotationMatrix;
                                scupdate.rotatingMatrix = saveplayerrotatingMatrixForPelvis;
                                scupdate.movePos = saveplayermovePos;
                                scupdate.rotx = saveplayerrotx;
                                scupdate.roty = saveplayerroty;
                                scupdate.rotz = saveplayerrotz;


                                /*updatescript = new scupdate(null);
                                scupdate.OFFSETPOS = saveplayerposition;
                                scupdate.finalRotationMatrix = saveplayerfinalRotationMatrix;
                                scupdate.rotatingMatrix = saveplayerrotatingMatrixForPelvis;
                                scupdate.movePos = saveplayermovePos;
                                scupdate.rotx = saveplayerrotx;
                                scupdate.roty = saveplayerroty;
                                scupdate.rotz = saveplayerrotz;
                                //typeofwindowpicker = 1;
                            }*/


                            //sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, sccs.scgraphics.scdirectx.D3D.device);
                            //sccs.Program.MessageBox((IntPtr)0, "scupdate initiated", "scmsg", 0);

                            if (usejitterphysics == 1)
                            {
                                /*jitter_sc = new jitter_sc[physicsengineinstancex * physicsengineinstancey * physicsengineinstancez];
                                sccsjittertasks = new scmessageobjectjitter[physicsengineinstancex * physicsengineinstancey * physicsengineinstancez][];

                                sc_jitter_data _sc_jitter_data = new sc_jitter_data();
                                _sc_jitter_data.alloweddeactivation = allowdeactivation;
                                _sc_jitter_data.allowedpenetration = worldallowedpenetration;
                                _sc_jitter_data.width = worldwidth;
                                _sc_jitter_data.height = worldheight;
                                _sc_jitter_data.depth = worlddepth;
                                _sc_jitter_data.gravity = _world_gravity;
                                _sc_jitter_data.smalliterations = worldsmalliterations;
                                _sc_jitter_data.iterations = worlditerations;

                                for (int xx = 0; xx < physicsengineinstancex; xx++)
                                {
                                    for (int yy = 0; yy < physicsengineinstancey; yy++)
                                    {
                                        for (int zz = 0; zz < physicsengineinstancez; zz++)
                                        {
                                            var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);
                                            //_jitter_physics[indexer00] = DoSpecialThing();
                                            sccsjittertasks[indexer00] = new scmessageobjectjitter[worldwidth * worldheight * worlddepth];

                                            for (int x = 0; x < worldwidth; x++)
                                            {
                                                for (int y = 0; y < worldheight; y++)
                                                {
                                                    for (int z = 0; z < worlddepth; z++)
                                                    {
                                                        var indexer01 = x + worldwidth * (y + worldheight * z);
                                                        sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                //Console.WriteLine("built0");
                                //jitter_sc = create_jitter_instances(jitter_sc, _sc_jitter_data);

                                for (int xx = 0; xx < physicsengineinstancex; xx++)
                                {
                                    for (int yy = 0; yy < physicsengineinstancey; yy++)
                                    {
                                        for (int zz = 0; zz < physicsengineinstancez; zz++)
                                        {
                                            var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);


                                            //if (jitter_sc.Length > 0)
                                            //{
                                            //    Console.WriteLine("built00");
                                            //}
                                            //
                                            //Console.WriteLine("index: " + indexer00);
                                            jitter_sc[indexer00]._sc_create_jitter_world(_sc_jitter_data);


                                            for (int x = 0; x < worldwidth; x++)
                                            {
                                                for (int y = 0; y < worldheight; y++)
                                                {
                                                    for (int z = 0; z < worlddepth; z++)
                                                    {
                                                        var indexer1 = x + worldwidth * (y + worldheight * z);

                                                        var world = jitter_sc[indexer00].return_world(indexer1);

                                                        if (world == null)
                                                        {
                                                            Console.WriteLine("null");
                                                        }
                                                        else
                                                        {
                                                            //Console.WriteLine("!null");

                                                            sccsjittertasks[indexer00][indexer1]._world_data = new object[2];
                                                            sccsjittertasks[indexer00][indexer1]._work_index = -1;
                                                            sccsjittertasks[indexer00][indexer1]._world_data[0] = world;
                                                            //Console.WriteLine("index: " + indexer1);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //, consoleHandle
                                sccsjittertasks = updatescript.init_update_variables(sccsjittertasks, config); //, SCGLOBALSACCESSORS.SCCONSOLEWRITER*/
                            }
                            else if (usejitterphysics == 0)
                            {
                                sccsjittertasks = new scmessageobjectjitter[1][];
                                sccsjittertasks[0] = new scmessageobjectjitter[1];
                                sccsjittertasks[0][0] = new scmessageobjectjitter();
                                /*for (int xx = 0; xx < physicsengineinstancex; xx++)
                                {
                                    for (int yy = 0; yy < physicsengineinstancey; yy++)
                                    {
                                        for (int zz = 0; zz < physicsengineinstancez; zz++)
                                        {
                                            var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);
                                            //_jitter_physics[indexer00] = DoSpecialThing();
                                            sccsjittertasks[indexer00] = new scmessageobjectjitter[worldwidth * worldheight * worlddepth];

                                            for (int x = 0; x < worldwidth; x++)
                                            {
                                                for (int y = 0; y < worldheight; y++)
                                                {
                                                    for (int z = 0; z < worlddepth; z++)
                                                    {
                                                        var indexer01 = x + worldwidth * (y + worldheight * z);
                                                        sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }*/

                                //, consoleHandle
                                updatescript.init_update_variables(sccsjittertasks, config); //, SCGLOBALSACCESSORS.SCCONSOLEWRITER
                            }

                            scupdate.Camera.SetPosition(saveplayermovePos.X, saveplayermovePos.Y, saveplayermovePos.Z);
                            scupdate.Camera.SetRotation(saveplayerrotx, saveplayerroty, saveplayerrotz);
                            //sccsjittertasks[0][0].device = updatescript.device;


                            /*swapChain1 = updatescript.SwapChain.QueryInterface<SwapChain1>();
                            // ignore all Windows events
                            factoryy = swapChain1.GetParent<SharpDX.DXGI.Factory>();
                            factoryy.MakeWindowAssociation(sccsr14sc.Form1.theHandle, WindowAssociationFlags.IgnoreAll);
                            */

                            initform = 2;
                        }



                        if (initform == 2)
                        {


                            if (loadvetextures == 0)
                            {
                                sccstextureloader _basicTexture = new sccstextureloader();

                                string currentsolutionpath = Environment.CurrentDirectory;

                                //sccs.Program.MessageBox((IntPtr)0, "" + currentsolutionpath, "sccsmsg", 0);

                                //bool _hasinit1 = _basicTexture.Initialize(scdirectx.D3D.device, Environment.CurrentDirectory + "OnBoardComputer.png");
                                bool _hasinit1 = _basicTexture.Initialize(scdirectx.D3D.device, "OnBoardComputer.png");

                                if (_hasinit1)
                                {
                                    //sccs.Program.MessageBox((IntPtr)0, "loaded texture", "sccsmsg", 0);
                                }
                                else
                                {
                                   // sccs.Program.MessageBox((IntPtr)0, "! loaded texture", "sccsmsg", 0);
                                }

                               


                                if (sccs.scgraphics.scgraphicssec.currentscgraphicssec.activatevrheightmapfeature == 1)
                                {

                                    //LOADING THE IMAGE TO MEMORY IN ORDER TO RETRIEVE THE BYTES OUT OF THE IMAGE USING MAPSUBRESOURCE 256X256IMAGE
                                    //LOADING THE IMAGE TO MEMORY IN ORDER TO RETRIEVE THE BYTES OUT OF THE IMAGE USING MAPSUBRESOURCE 256X256IMAGE
                                    //LOADING THE IMAGE TO MEMORY IN ORDER TO RETRIEVE THE BYTES OUT OF THE IMAGE USING MAPSUBRESOURCE 256X256IMAGE
                                    var factory = new SharpDX.WIC.ImagingFactory();
                                    var filename = "OnBoardComputer.png";


                                    var bitmapDecoder = new SharpDX.WIC.BitmapDecoder(
                                        factory,
                                        filename,
                                        SharpDX.WIC.DecodeOptions.CacheOnDemand
                                    );

                                    var bitmapsourceresult = new SharpDX.WIC.FormatConverter(factory);

                                    bitmapsourceresult.Initialize(
                                        bitmapDecoder.GetFrame(0),
                                        SharpDX.WIC.PixelFormat.Format32bppPRGBA,
                                        SharpDX.WIC.BitmapDitherType.None,
                                        null,
                                        0.0,
                                        SharpDX.WIC.BitmapPaletteType.Custom);


                                    SharpDX.Direct3D11.Texture2D thetexture2d;




                                    int stride = bitmapsourceresult.Size.Width * 4;
                                    _basicTexture.bufferfortests = new SharpDX.DataStream(bitmapsourceresult.Size.Height * stride, true, true);
                                    //using (SharpDX.DataStream buffer = new SharpDX.DataStream(bitmapSource.Size.Height * stride, true, true))
                                    {
                                        /*
                                        bitmapsourceresult.CopyPixels(stride, _basicTexture.bufferfortests);
                                        thetexture2d = new SharpDX.Direct3D11.Texture2D(scdirectx.D3D.device, new SharpDX.Direct3D11.Texture2DDescription()
                                        {
                                            Width = bitmapsourceresult.Size.Width,
                                            Height = bitmapsourceresult.Size.Height,
                                            ArraySize = 1,
                                            BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource | BindFlags.RenderTarget,
                                            Usage = SharpDX.Direct3D11.ResourceUsage.Default,
                                            CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None,
                                            Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                                            MipLevels = 1,
                                            OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                                            SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                                        }, new SharpDX.DataRectangle(_basicTexture.bufferfortests.DataPointer, stride));
                                        */


                                        
                                        bitmapsourceresult.CopyPixels(stride, _basicTexture.bufferfortests);
                                        thetexture2d =  new SharpDX.Direct3D11.Texture2D(scdirectx.D3D.device, new SharpDX.Direct3D11.Texture2DDescription()
                                        {
                                            CpuAccessFlags = CpuAccessFlags.Read,
                                            BindFlags = BindFlags.None,//BindFlags.None, //| BindFlags.RenderTarget
                                            Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                                            Width = bitmapsourceresult.Size.Width,
                                            Height = bitmapsourceresult.Size.Height,
                                            OptionFlags = ResourceOptionFlags.None,
                                            MipLevels = 1,
                                            ArraySize = 1,
                                            SampleDescription = { Count = 1, Quality = 0 },
                                            Usage = ResourceUsage.Staging
                                        }, new SharpDX.DataRectangle(_basicTexture.bufferfortests.DataPointer, stride));
                                    }



                                    var _textureDescriptionswapper = new Texture2DDescription
                                    {
                                        CpuAccessFlags = CpuAccessFlags.None,
                                        BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                                        Format = Format.B8G8R8A8_UNorm,
                                        Width = _basicTexture.texture.Description.Width,
                                        Height = _basicTexture.texture.Description.Height,
                                        OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                                        MipLevels = 1,
                                        ArraySize = 1,
                                        SampleDescription = { Count = 1, Quality = 0 },
                                        Usage = ResourceUsage.Default
                                    };


                                    Texture2D theonboardcomputertextureswapper = new Texture2D(scdirectx.D3D.device, _textureDescriptionswapper);
                                   
                                    var _textureDescriptionFINAL = new Texture2DDescription
                                    {
                                        CpuAccessFlags = CpuAccessFlags.Read,
                                        BindFlags = BindFlags.None,//BindFlags.None, //| BindFlags.RenderTarget
                                        Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                                        Width = _basicTexture.texture.Description.Width,
                                        Height = _basicTexture.texture.Description.Height,
                                        OptionFlags = ResourceOptionFlags.None,
                                        MipLevels = 1,
                                        ArraySize = 1,
                                        SampleDescription = { Count = 1, Quality = 0 },
                                        Usage = ResourceUsage.Staging
                                    };


                                    //sccs.Program.MessageBox((IntPtr)0, "width:" + _basicTexture.texture.Description.Width, "sccsmsg", 0);

                                    theonboardcomputertextureFINAL = new Texture2D(scdirectx.D3D.device, _textureDescriptionFINAL);


                                 
                                    //copying the texture
                                    scdirectx.D3D.Device.ImmediateContext.CopyResource(thetexture2d, theonboardcomputertextureFINAL);
                               

                                    //  scdirectx.D3D.Device.ImmediateContext.CopyResource(theonboardcomputertextureswapper, theonboardcomputertextureFINAL);

                           
                                    var dataBox1 = scdirectx.D3D.Device.ImmediateContext.MapSubresource(theonboardcomputertextureFINAL, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                                    //scdirectx.D3D.Device.ImmediateContext.UnmapSubresource(theonboardcomputertextureFINAL, 0);

                                    memoryBitmapStrideonboardcomputer = theonboardcomputertextureFINAL.Description.Width * 4;

                                    columnsonboardcomputer = theonboardcomputertextureFINAL.Description.Width;
                                    rowsonboardcomputer = theonboardcomputertextureFINAL.Description.Height;
                                    IntPtr interptr1 = dataBox1.DataPointer;

                                    onboardcomputeiconpixeldata = new byte[memoryBitmapStrideonboardcomputer * theonboardcomputertextureFINAL.Description.Height];

                                    // It can happen that the stride on the GPU is bigger then the stride on the bitmap in main memory (_width * 4)
                                    if (dataBox1.RowPitch == memoryBitmapStrideonboardcomputer)
                                    {
                                        //sccs.Program.MessageBox((IntPtr)0, "Stride is the same", "sccsmsg", 0);
                                        // Stride is the same
                                        Marshal.Copy(interptr1, onboardcomputeiconpixeldata, 0, memoryBitmapStrideonboardcomputer * theonboardcomputertextureFINAL.Description.Height);
                                    }
                                    else
                                    {
                                        // Stride not the same - copy line by line
                                        for (int y = 0; y < theonboardcomputertextureFINAL.Description.Height; y++)
                                        {
                                            Marshal.Copy(interptr1 + y * dataBox1.RowPitch, onboardcomputeiconpixeldata, y * memoryBitmapStrideonboardcomputer, memoryBitmapStrideonboardcomputer);
                                        }
                                    }

                                    
                                    somebitmap = new System.Drawing.Bitmap(columnsonboardcomputer, rowsonboardcomputer, columnsonboardcomputer * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(onboardcomputeiconpixeldata, 0));
                                    somebitmap.Save(@"C:\Users\steve\Desktop\screenRecord\" + bitmaponboardcomputercounter + "_" + rowsonboardcomputer.ToString("00") + columnsonboardcomputer.ToString("00") + ".png");
                                    bitmaponboardcomputercounter++;



                                    scdirectx.D3D.Device.ImmediateContext.UnmapSubresource(theonboardcomputertextureFINAL, 0);
                                    DeleteObject(interptr1);
                                    //LOADING THE IMAGE TO MEMORY IN ORDER TO RETRIEVE THE BYTES OUT OF THE IMAGE USING MAPSUBRESOURCE 256X256IMAGE
                                    //LOADING THE IMAGE TO MEMORY IN ORDER TO RETRIEVE THE BYTES OUT OF THE IMAGE USING MAPSUBRESOURCE 256X256IMAGE 
                                    //LOADING THE IMAGE TO MEMORY IN ORDER TO RETRIEVE THE BYTES OUT OF THE IMAGE USING MAPSUBRESOURCE 256X256IMAGE







                                    //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE
                                    //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE
                                    bitmaponboardcomputer = new System.Drawing.Bitmap(256, 256, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                    var boundsRect = new System.Drawing.Rectangle(0, 0, 256, 256);
                                    bmpData = bitmaponboardcomputer.LockBits(boundsRect, ImageLockMode.ReadOnly, bitmaponboardcomputer.PixelFormat);
                                    _bytesTotalobcpu = Math.Abs(bmpData.Stride) * bitmaponboardcomputer.Height;
                                    bitmaponboardcomputer.UnlockBits(bmpData);
                                    _textureByteArrayobcpu = new byte[_bytesTotalobcpu];
                                    bmpData = null;
                                    //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE
                                    //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE






                                    var boundsRect0 = new System.Drawing.Rectangle(0, 0, onboardcpuiconw, onboardcpuiconh);
                                    var bitmap = somebitmap.Clone(boundsRect0, somebitmap.PixelFormat);

                                    //var image0 = new System.Drawing.Bitmap(50, 40, 50 * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArrayobcpu, 0));
                                    /*image0.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png");
                                    bitmapcounter++;*/
                                    //https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-crop-and-scale-images?view=netframeworkdesktop-4.8
                                    int width = somebitmap.Width;
                                    int height = somebitmap.Height;
                                    System.Drawing.RectangleF destinationRect = new System.Drawing.RectangleF(
                                         0,
                                         0,
                                         width,
                                         height);


                                    /*Graphics g = Graphics.FromImage(somebitmap);
                                    // Draw a portion of the image. Scale that portion of the image
                                    // so that it fills the destination rectangle.
                                    System.Drawing.RectangleF sourceRect = new System.Drawing.RectangleF(0, 0, width ,height);
                                    g.DrawImage(
                                        bitmap,
                                        destinationRect,
                                        sourceRect,
                                        GraphicsUnit.Pixel);

                                    bitmap.Save(@"C:\Users\steve\Desktop\screenRecord\" + bitmapcounter + "_TEST_" + onboardcpuiconw.ToString("00") + onboardcpuiconh.ToString("00") + ".png");
                                    bitmapcounter++;
                                    */


                                    //https://social.msdn.microsoft.com/Forums/en-US/de2a9016-60f3-44a3-b63d-e1dabbf5efb2/resizing-images-with-c-with-no-quality-loss?forum=aspsystemdrawinggdi
                                    string newFullPath = @"C:\Users\steve\Desktop\screenRecord\" + bitmapcounter + "_" + onboardcpuiconw.ToString("00") + onboardcpuiconh.ToString("00") + ".png";
                                    dstImg = new System.Drawing.Bitmap(onboardcpuiconw, onboardcpuiconh);
                                    dstImg.SetResolution(1024, 1024); // 512//512
                                    Graphics g0 = Graphics.FromImage(dstImg);
                                    g0.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                                    /*g0.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                    g0.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                                    g0.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;*/

                                    // Resize the original
                                    g0.DrawImage(somebitmap, 0, -6, onboardcpuiconw, onboardcpuiconh * 1.25f);

                                    //g0.DrawImage(somebitmap, 0, 0, onboardcpuiconw, onboardcpuiconh * 1.5f);//moves imageup

                                    g0.Dispose();

                                    EncoderParameters encoderParameters = new EncoderParameters(1);
                                    encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 100);          // 100% Percent Compression
                                    dstImg.Save(newFullPath, ImageCodecInfo.GetImageEncoders()[1], encoderParameters);   // jpg format


                                    /*
                                    AzureImage.ModelInput sampleData = new AzureImage.ModelInput()
                                    {
                                        ImageSource = onboardcomputeiconpixeldata,
                                    };

                                    //Load model and predict output
                                    var result = AzureImage.Predict(sampleData);*/




                                    /*int difference = (int)(SimpleImageComparisonClassLibrary.ImageTool.GetPercentageDifference(somebitmap, dstImg) * 100);

                                    MessageBox((IntPtr)0, "diff:" + difference, "scmsg", 0);
                                    */
                                    //dstImg.Dispose();





                                    // Create MLContext to be shared across the model creation workflow objects
                                    // <SnippetCreateMLContext>
                                    mlContext = new MLContext();
                                    // </SnippetCreateMLContext>
                                    
                                    // <SnippetCallGenerateModel>
                                    model = GenerateModel(mlContext);
                                    // </SnippetCallGenerateModel>
                                    percentrecognitiondata = new percentrecognitionstruct[1];
                                    percentrecognitiondata[0].percentrecogfloat = 0.0f;
                                    





                                    
                                    mlContextForVEVD = new MLContext();
                                    // </SnippetCreateMLContext>
                                    // <SnippetCallGenerateModel>
                                    modelForVEVD = GenerateModelForVEVirtualScreen(mlContextForVEVD);
                                    // </SnippetCallGenerateModel>
                                    percentrecognitiondataVEVD = new percentrecognitionstruct[1];
                                    percentrecognitiondataVEVD[0].percentrecogfloat = 0.0f;




                                    
                                    






                                    /*
                                    // <SnippetCallClassifySingleImage>
                                    ClassifySingleImage(mlContext, model, newFullPath);
                                    // </SnippetCallClassifySingleImage>*/





                                    /*
                                    ModelInputBytes modelinput = new ModelInputBytes();
                                    modelinput.ImageBytes = onboardcomputeiconpixeldata;
                                    modelinput.Label = "currentscreencapture";



                                    var ouputprediction = PredictFromBytes(modelinput);*/



                                    /*
                                    HousingData[] housingData = new HousingData[]
                                    {
                                        new HousingData
                                        {
                                            Size = 600f,
                                            HistoricalPrices = new float[] { 100000f, 125000f, 122000f },
                                            CurrentPrice = 170000f
                                        },
                                        new HousingData
                                        {
                                            Size = 1000f,
                                            HistoricalPrices = new float[] { 200000f, 250000f, 230000f },
                                            CurrentPrice = 225000f
                                        },
                                        new HousingData
                                        {
                                            Size = 1000f,
                                            HistoricalPrices = new float[] { 126000f, 130000f, 200000f },
                                            CurrentPrice = 195000f
                                        }
                                    };*/

                                    /* ModelInputBytes[] modelinput = new ModelInputBytes[]
                                    {
                                        new ModelInputBytes
                                        {
                                              modelinput.ImageBytes = onboardcomputeiconpixeldata,
                                             modelinput.Label = "currentscreencapture"
                                        },
                                        new ModelInputBytes
                                        {
                                              modelinput.ImageBytes = onboardcomputeiconpixeldata,
                                             modelinput.Label = "currentscreencapture"
                                        }
                                     };*/

                                    /*
                                    ModelInputBytes[] modelinput = new ModelInputBytes[1];


                                    modelinput[0] = new ModelInputBytes();
                                    modelinput[0].ImageBytes = onboardcomputeiconpixeldata;
                                    modelinput[0].Label = "currentscreencapture";




                                    // Create MLContext
                                    MLContext mlContext = new MLContext();

                                    model = GenerateModel(mlContext, modelinput[0]);*/


                                    /* ModelInput[] modelinput = new ModelInput[1];


                                     modelinput[0] = new ModelInput();
                                     modelinput[0].ImageBytes = onboardcomputeiconpixeldata;
                                     modelinput[0].Label = "currentscreencapture";

                                     MLContext mlContext = new MLContext();


                                     // Load model & create prediction engine
                                     ITransformer mlModel = mlContext.Model.Load(_otherfolder, out var modelInputSchema);
                                     //IDataView mlModel = mlContext.Data.LoadFromTextFile<ImageData>(path: _testTagsTsv, hasHeader: false);

                                     ITransformer dataPreProcessTransform = LoadImageFromFileTransformer(modelinput[0], mlContext);

                                     var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(dataPreProcessTransform.Append(mlModel));

                                     ModelOutput result = predEngine.Predict(modelinput[0]);*/




                                    /*
                                    // Load Data
                                    IDataView data = mlContext.Data.LoadFromEnumerable<ModelInputBytes>(modelinput);


                                    EstimatorChain<RegressionPredictionTransformer<LinearRegressionModelParameters>> pipelineEstimator =
                                       mlContext.Transforms.Concatenate("Features", new string[] { "Size", "HistoricalPrices" })
                                           .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                                           .Append(mlContext.Regression.Trainers.Sdca());
                                    */

                                    //IDataView testData = mlContext.Data.LoadFromTextFile<ImageData>(path: _testTagsTsv, hasHeader: false);
                                    //IDataView predictions = model.Transform(testData);


                                    //IDataView data = mlContext.Data.LoadFromEnumerable<ModelInputBytes>(modelinput);
                                    //IDataView trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: _testTagsTsv, hasHeader: false);








                                    /*
                                    // Define data preparation estimator
                                    EstimatorChain<RegressionPredictionTransformer<LinearRegressionModelParameters>> pipelineEstimator =
                                         mlContext.Transforms.Concatenate("Features", new string[] { "Size", "HistoricalPrices" })
                                             .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                                             .Append(mlContext.Regression.Trainers.Sdca());
                                    */
                                    /*// Train model
                                    ITransformer trainedModel = pipelineEstimator.Fit(trainingData);

                                    // Save model
                                    mlContext.Model.Save(trainedModel, trainingData.Schema, "model.zip");*/
                                    // </SnippetDeclareGlobalVariables>








                                    loadvetextures = 1;
                                }
                            }





                            Console.WriteLine("Program.csLine7622=>Program.vewindowsfoundedz :" + Program.vewindowsfoundedz);

                            //MessageBox((IntPtr)0, "_thread_looper0", "scmsg", 0);
                            // ReSharper disable AccessToDisposedClosure
                            if (screencapturetype == 0 && changedscreencapturetype == 0  && vewindowsfoundedz  != IntPtr.Zero|| screencapturetype == 1 && changedscreencapturetype == 0 && vewindowsfoundedz != IntPtr.Zero)
                            {
                                if (updatescript != null)
                                {

                                    if (updatescript.scgraphicssec != null)
                                    {


                                        if (updatescript.scgraphicssec.activatevoxelinstancedvirtualdesktop == 1 && updatescript.scgraphicssec.activatevrheightmapfeature == 1)
                                        {

                                            if (updatescript.captureMethod != null)
                                            {

                                                if (!updatescript.captureMethod.IsCapturing)  //&& sccsr14sc.Form1.capturedprogram != IntPtr.Zero
                                                {



                                                    /*var refreshDXEngineAction0 = new Action(delegate
                                                    {
                                                        //Console.WriteLine("thebutton Visible");
                                                        //stackoverflow 661561 for invoking panel changes.

                                                        //sccsr14sc.Form1.haspressedf9 = 1;

                                                        sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                        {

                                                            sccsr14sc.Form1.checkbox1.Checked = true;

                                                            //if (sccsr14sc.Form1.checkbox1.Checked)
                                                            //{
                                                            //    sccsr14sc.Form1.checkbox1.Checked = false;
                                                            //}
                                                            //else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                            //{
                                                            //    sccsr14sc.Form1.checkbox1.Checked = true;
                                                            //}
                                                        });
                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);


                                                    sccsr14sc.Form1.someform.checkBox1_CheckedChangedint = 0;

                                                    sccsr14sc.Form1.haspressedf9 = 1;
                                                    */





                                                    //MessageBox((IntPtr)0, "capturedprogram:" + sccsr14sc.Form1.capturedprogram, "scmsg", 0);

                                                    /*EnumWindows(enumWindowProc, IntPtr.Zero);
                                                    */
                                                    //updatescript.captureMethod._hWnd = sccsr14sc.Form1.capturedprogram;




                                                    //_hWnd = picker.PickCaptureTarget(hWnd, getthecapturedappintptr, "");

                                                    updatescript.captureMethod.SelectedTitle = capturedwindowname;


                                                    updatescript.captureMethod._hWnd = vewindowsfoundedz;


                                                    //updatescript.captureMethod._hWnd = vewindowsfoundedz;



                                                    /*
                                                    if (capturedwindowname.Contains("void") && capturedwindowname.Contains("expanse"))
                                                    {
                                                        Program.MessageBox((IntPtr)0, "Program.cs=>capturedwindownameline10893:" + capturedwindowname, "sccsmsg", 0);
                                                    }*/



                                                    if (last_hWnd != updatescript.captureMethod._hWnd)
                                                    {
                                                        //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
                                                        //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
                                                        //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
                                                        if (last_hWnd != IntPtr.Zero)
                                                        {


                                                            /*if (sccsr14sc.Form1.checkbox1.Checked)
                                                            {
                                                                /*if (SelectedTitle != null)
                                                                {
                                                                    if (SelectedTitle != "")
                                                                    {
                                                                        executeModeChange();
                                                                        refresh();
                                                                    }
                                                                }


                                                                sccsr14sc.Form1.thebutton4.Invoke((MethodInvoker)delegate
                                                                {

                                                                    sccsr14sc.Form1.thebutton4.Text = "Shrink";
                                                                });

                                                                sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                                {

                                                                    sccsr14sc.Form1.checkbox1.Checked = false;
                                                                });

                                                                //sccsr14sc.Form1.haspressedf9 = 2;

                                                                //sccsr14sc.Form1.haspressedf9 = 1;
                                                            }
                                                            else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                            {
                                                                sccsr14sc.Form1.thebutton4.Invoke((MethodInvoker)delegate
                                                                {

                                                                    sccsr14sc.Form1.thebutton4.Text = "Maximize";
                                                                });

                                                                sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                                {

                                                                    sccsr14sc.Form1.checkbox1.Checked = true;
                                                                });
                                                                //sccsr14sc.Form1.haspressedf9 = 2;
                                                                //sccsr14sc.Form1.haspressedf9 = 1;

                                                            }*/

                                                            /*
                                                            int screenWidth = Program.GetSystemMetrics(0);
                                                            int screenHeight = Program.GetSystemMetrics(1);
                                                            */
                                                            /*
                                                            Program.SetWindowPos(last_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            Program.SetWindowPos(last_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            */

                                                            /*
                                                            DeleteObject(last_hWnd);
                                                            GC.SuppressFinalize(last_hWnd);*/

                                                            /*int screenWidth = Program.GetSystemMetrics(0);
                                                            int screenHeight = Program.GetSystemMetrics(1);

                                                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                                                            Program.RECT therect = new Program.RECT();

                                                            //if (Program.vewindowsfoundedz != IntPtr.Zero)
                                                            {

                                                                param = new Program.WINDOWPLACEMENT();
                                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                                                                therect = new Program.RECT();


                                                                
                                                                therect.Left = 0;
                                                                therect.Top = 0;
                                                                therect.Bottom = screenHeight;
                                                                therect.Right = screenWidth;

                                                                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                                Program.SetWindowPlacement(last_hWnd, ref param);


                                                                DeleteObject(last_hWnd);
                                                                GC.SuppressFinalize(last_hWnd);
                                                                //last_hWnd = null;

                                                                /*Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOP, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                                                

                                                            }*/
                                                        }
                                                        //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
                                                        //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
                                                        //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
                                                    }

                                                    //_hWnd








                                                    //iswtchingcapturetypesmaybe = 1;

                                                    /*var refreshDXEngineAction0 = new Action(delegate
                                                    {

                                                        updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);



                                                        //sccsr14sc.Form1.haspressedf9 = 1;

                                                        //sccsr14sc.Form1.haspressedf9 = 1;

                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                                                    */

                                                    updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);






                                                    /*var refreshDXEngineAction0 = new Action(delegate
                                                    {
                                                        //Console.WriteLine("thebutton Visible");
                                                        //stackoverflow 661561 for invoking panel changes.

                                                        //sccsr14sc.Form1.haspressedf9 = 1;

                                                        sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                        {
                                                            if (sccsr14sc.Form1.checkbox1.Checked)
                                                            {
                                                                sccsr14sc.Form1.checkbox1.Checked = false;
                                                            }
                                                            else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                            {
                                                                sccsr14sc.Form1.checkbox1.Checked = true;
                                                            }
                                                        });
                                                    });
                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                                                    */

                                                    /*
                                                    sccsr14sc.Form1.haspressedescape = 1;
                                                    sccsr14sc.Form1.someform.haspressedsomekeyboardkey = 1;*/




                                                    //updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);





                                                    if (getwindowthreadprocessidint == 0)
                                                    {
                                                        if (screencapturetype == 0)
                                                        {
                                                            //sometest = (DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface;
                                                            //var sometest = (GraphicsCapture)updatescript.captureMethod as GraphicsCapture;


                                                            //SetWindowPos(vewindowsfoundedz, SetWindowPosFlags.HWND_TOPMOST, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE);

                                                            //getwindowthreadprocessidint = 0;











                                                            /*
                                                            GC.SuppressFinalize(vewindowsfoundedz);

                                                            DeleteObject(vewindowsfoundedz);
                                                            */
                                                            



                                                            vewindowsfoundedz = ((GraphicsCapture)updatescript.captureMethod as GraphicsCapture)._hWnd;
                                                            //SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW
                                                            //SetWindowPos(vewindowsfoundedz, SpecialWindowHandles.HWND_TOPMOST, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE);
                                                            lastcapturedhwnd = vewindowsfoundedz;
                                                            capturedwindowname = ((GraphicsCapture)updatescript.captureMethod as GraphicsCapture).capturedwindowname;






                                                            //MessageBox((IntPtr)0, "capturedwindowname:" + capturedwindowname +  "/vewindowsfoundedz:" + vewindowsfoundedz, "scmsg", 0);

                                                            //Console.WriteLine("capturedwindowname:" + capturedwindowname);








                                                            /*
                                                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                                                            Program.RECT therect = new Program.RECT();
                                                            */
                                                            if (Program.vewindowsfoundedz != IntPtr.Zero)
                                                            {
                                                                

                                                                //sccsr14sc.Form1.haspressedf9 = 1;
                                                              

                                                                sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                                                {
                                                                    if (sccsr14sc.Form1.checkbox1.Checked)
                                                                    {
                                                                        sccsr14sc.Form1.thebutton4.Invoke((MethodInvoker)delegate
                                                                        {

                                                                            sccsr14sc.Form1.thebutton4.Text = "Shrink";
                                                                        });
                                                                        sccsr14sc.Form1.checkbox1.Checked = false;
                                                                    }
                                                                    else if (!sccsr14sc.Form1.checkbox1.Checked)
                                                                    {
                                                                        sccsr14sc.Form1.thebutton4.Invoke((MethodInvoker)delegate
                                                                        {

                                                                            sccsr14sc.Form1.thebutton4.Text = "Maximize";
                                                                        });
                                                                        sccsr14sc.Form1.checkbox1.Checked = true;
                                                                    }
                                                                });

                                                            }



                                                            Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

                                                            int screenWidth = Program.GetSystemMetrics(0);
                                                            int screenHeight = Program.GetSystemMetrics(1);
                                                            var param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                                                            var therect = new Program.RECT();



                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = sccs.sccore.scsystemconfiguration.currentscsystemconfiguration.Height;// screenHeight;
                                                            therect.Right = sccs.sccore.scsystemconfiguration.currentscsystemconfiguration.Width;//screenWidth;

                                                            param.ShowCmd = Program.ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);


                                                            Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);




                                                            //BACKUP DONT TOUCH
                                                            //BACKUP DONT TOUCH
                                                            //BACKUP DONT TOUCH

                                                            /*int screenWidth = Program.GetSystemMetrics(0);
                                                            int screenHeight = Program.GetSystemMetrics(1);

                                                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                                                            Program.RECT therect = new Program.RECT();

                                                            if (Program.vewindowsfoundedz != IntPtr.Zero)
                                                            {

                                                                param = new Program.WINDOWPLACEMENT();
                                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                                                                therect = new Program.RECT();



                                                                therect.Left = 0;
                                                                therect.Top = 0;
                                                                therect.Bottom = screenHeight;
                                                                therect.Right = screenWidth;

                                                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                                Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);

                                                                Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                                                            }*/

                                                            /*therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;

                                                            param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);


                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;

                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                                                            */

                                                            //BACKUP DONT TOUCH
                                                            //BACKUP DONT TOUCH
                                                            //BACKUP DONT TOUCH




























                                                            //BACKUP DONT TOUCH
                                                            //BACKUP DONT TOUCH
                                                            //BACKUP DONT TOUCH
                                                            /*
                                                            int screenWidth = Program.GetSystemMetrics(0);
                                                            int screenHeight = Program.GetSystemMetrics(1);

                                                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                                                            Program.RECT therect = new Program.RECT();

                                                            if (Program.vewindowsfoundedz != IntPtr.Zero)
                                                            {

                                                                param = new Program.WINDOWPLACEMENT();
                                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                                                                therect = new Program.RECT();



                                                                therect.Left = 0;
                                                                therect.Top = 0;
                                                                therect.Bottom = screenHeight;
                                                                therect.Right = screenWidth;

                                                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                                Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);

                                                                Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                                                            }

                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;

                                                            param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);


                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;

                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                                                            */
                                                            //BACKUP DONT TOUCH
                                                            //BACKUP DONT TOUCH
                                                            //BACKUP DONT TOUCH

































                                                            //set the window to a borderless style
                                                            /*const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style.\nSorry.");
                                                            }
                                                            SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            *///MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                              //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                              //MIT-LICENSE-RichardBrass-BorderlessFullscreen

                                                            //sccs.Program.MessageBox((IntPtr)0, "_hwndSource", "scmsg", 0);


                                                            /*var _hwndSource = HwndSource.FromHwnd(vewindowsfoundedz);
                                                            if (_hwndSource != null)
                                                            {
                                                                _hwndSource.AddHook(WndProc);
                                                                sccs.Program.MessageBox((IntPtr)0, "HwndSource.FromHwnd(vewindowsfoundedz)", "scmsg", 0);
                                                            }*/


                                                            /*_hwndSource = HwndSource.FromHwnd(this.Handle);
                                                            if (_hwndSource != null)
                                                                _hwndSource.AddHook(WndProc);*/

                                                            /*
                                                            int screenWidth = Program.GetSystemMetrics(0);
                                                            int screenHeight = Program.GetSystemMetrics(1);
                                                            */









                                                            /*therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);               

                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */

                                                            //set the window to a borderless style
                                                            /*const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = Program.SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);


                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                            }

                                                            sult = Program.SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_MAXIMIZE);
                                                            */
                                                            /*

                                                            var sult = SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }
                                                            SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen*/
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen














                                                            //int iHandle = FindWindow(null, capturedwindowname);// "VoidExpanse");
                                                            //Console.WriteLine(iHandle);

                                                            //Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

                                                            /*sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                            //WORKING
                                                            sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                            //WORKING*/


                                                            //iHandle = scgraphicssec.FindWindow(null, sccsr14sc.Form1.capturedwindownameform1);
                                                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                                                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                                                            /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                                                            *//*
                                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                            */
                                                            /*Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                                                            Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/


                                                            /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                                            */














                                                            /*
                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;

                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);



                                                            int iHandle = FindWindow(null, capturedwindowname);// "VoidExpanse");

                                                            Console.WriteLine(iHandle);

                                                            Console.WriteLine(capturedwindowname);
                                                            Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

                                                            //sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                            //WORKING
                                                            //sccsr14sc.Form1.PostMessage(vewindowsfoundedz, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                            //WORKING


                                                            //iHandle = scgraphicssec.FindWindow(null, sccsr14sc.Form1.capturedwindownameform1);
                                                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                                                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                                                            
                                                            //sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                                                            //sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                                            
                                                            //Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                                                            //Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);
                                                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                                            Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                                                            */






                                                            /* PostMessage(Program.vewindowsfoundedz, (uint)WM_KEYDOWN, (int)VirtualKeyCode.F11, 0);
                                                             //WORKING
                                                             PostMessage(Program.vewindowsfoundedz, (uint)WM_KEYUP, (int)VirtualKeyCode.F11, 0);
                                                             //WORKING
                                                            */

                                                            //var windowHandler = vewindowsfoundedz;// GetActiveWindowHandle();

                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);


                                                            //var windowRec = GetWindowRect(windowHandler);
                                                            // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW



                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS); //SetWindowPosFlags.SWP_SHOWWINDOW






















                                                            lastcapturedwindowname = capturedwindowname;


                                                            //SetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE) | WS_EX_OVERLAPPEDWINDOW | (long)WindowStyles.WS_POPUP));


                                                            //if (typeofwindowpicker == 0)
                                                            {

                                                                
                                                                /*var sult = SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                                if (sult == IntPtr.Zero)
                                                                {
                                                                    //in some cases SWL just outright fails, so we can notify the user and abort
                                                                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                    //return;
                                                                    //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                    Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                                }
                                                                SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                                */
                                                                //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                                //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                                //MIT-LICENSE-RichardBrass-BorderlessFullscreen



                                                                /*
                                                                var Rectsccs = new Program.RECT();
                                                                Program.GetWindowRect(Program.vewindowsfoundedz, ref Rectsccs);
                                                                */

                                                                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                                Program.RECT therect = new Program.RECT();*/

                                                                /*
                                                                param = new Program.WINDOWPLACEMENT();
                                                                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                                therect = new Program.RECT();

                                                                therect.Left = 0;
                                                                therect.Top = 0;
                                                                therect.Bottom = 1080;
                                                                therect.Right = 1920;

                                                                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                                Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);
                                                                */


                                                                /*const int GWL_STYLE = -16; //want to change the window style
                                                                const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                                IntPtr sult = Program.SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                                if (sult == IntPtr.Zero)
                                                                {
                                                                    //in some cases SWL just outright fails, so we can notify the user and abort
                                                                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                    //return;
                                                                    Console.WriteLine("Unable to alter window style.\nSorry.");
                                                                }

                                                                Program.SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, 1920, 1080, Program.SWP_SHOWWINDOW);
                                                                */


                                                                //set the window to a borderless style
                                                                /*const int GWL_STYLE = -16; //want to change the window style
                                                                const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                                IntPtr sult = SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                                if (sult == IntPtr.Zero)
                                                                {
                                                                    //in some cases SWL just outright fails, so we can notify the user and abort
                                                                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                    //return;
                                                                    Console.WriteLine("Unable to alter window style.\nSorry.");
                                                                }
                                                                SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                                *///MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                                  //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                                  //MIT-LICENSE-RichardBrass-BorderlessFullscreen


                                                                /*
                                                                Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                                                */


                                                            }





























                                                            /*
                                                            if (sccsr14sc.Form1.haspressedescape == 0)
                                                            {
                                                                var refreshDXEngineAction0 = new Action(delegate
                                                                {


                                                                    sccsr14sc.Form1.checkbox3.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        if (sccsr14sc.Form1.checkbox3.Checked)
                                                                        {
                                                                            sccsr14sc.Form1.checkbox3.Checked = false;
                                                                        }
                                                                        else if (!sccsr14sc.Form1.checkbox3.Checked)
                                                                        {
                                                                            sccsr14sc.Form1.checkbox3.Checked = true;
                                                                        }
                                                                    });
                                                                    //Console.WriteLine("thebutton Visible");
                                                                    //stackoverflow 661561 for invoking panel changes.
                                                                    



                                                                    //sccsr14sc.Form1.haspressedf9 = 1;

                                                                    //sccsr14sc.Form1.haspressedf9 = 1;

                                                                });
                                                                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);




                                                                if (sccsr14sc.Form1.button1exit.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.button1exit.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.button1exit.Visible = true;
                                                                    });





                                                                }
                                                                else if (sccsr14sc.Form1.button1exit.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.button1exit.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.button1exit.Visible = false;
                                                                    });
                                                                }


                                                                if (sccsr14sc.Form1.button2changeprog.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.button2changeprog.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.button2changeprog.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.button2changeprog.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.button2changeprog.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.button2changeprog.Visible = false;
                                                                    });
                                                                }


                                                                if (sccsr14sc.Form1.someform.labeltext0.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.someform.labeltext0.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.labeltext0.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.someform.labeltext0.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.someform.labeltext0.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.labeltext0.Visible = false;
                                                                    });
                                                                }






                                                                if (sccsr14sc.Form1.someform.labeltext2.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.someform.labeltext2.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.labeltext2.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.someform.labeltext2.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.someform.labeltext2.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.labeltext2.Visible = false;
                                                                    });
                                                                }


                                                                if (sccsr14sc.Form1.someform.labeltext3.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.someform.labeltext3.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.labeltext3.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.someform.labeltext3.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.someform.labeltext3.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.labeltext3.Visible = false;
                                                                    });
                                                                }


                                                                if (sccsr14sc.Form1.someform.numericUpDown01.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown01.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown01.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.someform.numericUpDown01.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown01.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown01.Visible = false;
                                                                    });
                                                                }





                                                                if (sccsr14sc.Form1.someform.numericUpDown02.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown02.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown02.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.someform.numericUpDown02.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown02.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown02.Visible = false;
                                                                    });
                                                                }





                                                                if (sccsr14sc.Form1.someform.numericUpDown03.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown03.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown03.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.someform.numericUpDown03.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown03.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown03.Visible = false;
                                                                    });
                                                                }





                                                                if (sccsr14sc.Form1.someform.numericUpDown04.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown04.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown04.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.someform.numericUpDown04.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown04.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown04.Visible = false;
                                                                    });
                                                                }





                                                                if (sccsr14sc.Form1.someform.numericUpDown05.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown05.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown05.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.someform.numericUpDown05.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown05.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown05.Visible = false;
                                                                    });
                                                                }





                                                                if (sccsr14sc.Form1.someform.numericUpDown06.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown06.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown06.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.someform.numericUpDown06.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.someform.numericUpDown06.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.someform.numericUpDown06.Visible = false;
                                                                    });
                                                                }













                                                     

                                                                if (sccsr14sc.Form1.comboboxcapturelist.Visible == false)
                                                                {
                                                                    sccsr14sc.Form1.comboboxcapturelist.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.comboboxcapturelist.Visible = true;
                                                                    });
                                                                }
                                                                else if (sccsr14sc.Form1.comboboxcapturelist.Visible == true)
                                                                {
                                                                    sccsr14sc.Form1.comboboxcapturelist.Invoke((MethodInvoker)delegate
                                                                    {
                                                                        sccsr14sc.Form1.comboboxcapturelist.Visible = false;
                                                                    });
                                                                }

                                                             


                                                                if (sccsr14sc.Form1.trackbar.Visible == false)
                                                                {
                                                                    var refreshDXEngineAction = new Action(delegate
                                                                    {
                                                                        //Console.WriteLine("thebutton Visible");
                                                                        //stackoverflow 661561 for invoking panel changes.
                                                                        sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                                                        {
                                                                            sccsr14sc.Form1.trackbar.Visible = true;
                                                                        });

                                                                    });
                                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                                }
                                                                else if (sccsr14sc.Form1.trackbar.Visible == true)
                                                                {
                                                                    var refreshDXEngineAction = new Action(delegate
                                                                    {
                                                                        //Console.WriteLine("thebutton Visible");
                                                                        //stackoverflow 661561 for invoking panel changes.
                                                                        sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                                                        {
                                                                            sccsr14sc.Form1.trackbar.Visible = false;
                                                                        });

                                                                    });
                                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                                }



                                                                if (sccsr14sc.Form1.checkedlistbox.Visible == false)
                                                                {
                                                                    var refreshDXEngineAction = new Action(delegate
                                                                    {
                                                                        //Console.WriteLine("thebutton Visible");
                                                                        //stackoverflow 661561 for invoking panel changes.
                                                                        sccsr14sc.Form1.checkedlistbox.Invoke((MethodInvoker)delegate
                                                                        {
                                                                            sccsr14sc.Form1.checkedlistbox.Visible = true;
                                                                        });

                                                                    });
                                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                                }

                                                                else if (sccsr14sc.Form1.checkedlistbox.Visible == true)
                                                                {
                                                                    var refreshDXEngineAction = new Action(delegate
                                                                    {
                                                                        //Console.WriteLine("thebutton Visible");
                                                                        //stackoverflow 661561 for invoking panel changes.
                                                                        sccsr14sc.Form1.checkedlistbox.Invoke((MethodInvoker)delegate
                                                                        {
                                                                            sccsr14sc.Form1.checkedlistbox.Visible = false;
                                                                        });

                                                                    });
                                                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                                }



                                                                sccsr14sc.Form1.haspressedescape = 2;
                                                            }*/




                                                            /*WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));


                                                            RECT therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            therect = new RECT();
                                                            therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;

                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            //*/

                                                            //https://social.msdn.microsoft.com/forums/azure/fr-fr/6791b517-1bc5-4e08-ac21-d73d443784cd/c-setwindowpos-for-minimized-windows?forum=csharpgeneral
                                                            if (vewindowsfoundedz == null)
                                                            {

                                                            }



                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);


                                                            /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);*/
                                                            /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*var param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            var therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */


                                                            /*var sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }*/





                                                            /*
                                                            //set the window to a borderless style
                                                            const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style.\nSorry.");
                                                            }
                                                            SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);*/
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            /*
                                                            Rectsccs = new Program.RECT();
                                                            Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);



                                                            IntPtr id;
                                                            Program.RECT Rect = new Program.RECT();
                                                            //Thread.Sleep(2000);
                                                            id = vewindowsfoundedz;// GetForegroundWindow();
                                                            Random myRandom = new Random();
                                                            Program.GetWindowRect(id, ref Rect);
                                                            Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);


                                                             screenWidth = Program.GetSystemMetrics(0);
                                                             screenHeight = Program.GetSystemMetrics(1);*/

                                                            /*var param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            var therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = Rectsccs.Bottom - Rectsccs.Top;
                                                            therect.Right = (Rectsccs.Right - Rectsccs.Left);
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            int screenWidth = GetSystemMetrics(0);
                                                            int screenHeight = GetSystemMetrics(1);
                                                            */
                                                            /*
                                                            var param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = Rectsccs.Left;
                                                            therect.Top = Rectsccs.Top;
                                                            therect.Bottom = Rectsccs.Bottom;
                                                            therect.Right = (Rectsccs.Right);
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            Program.GetWindowRect(vewindowsfoundedz, ref rect);



                                                            */










                                                            /*
                                                            SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));//WS_EX_OVERLAPPEDWINDOW

                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            */


                                                            //id = vewindowsfoundedz;// GetForegroundWindow();
                                                            //Random myRandom = new Random();


                                                            //MoveWindow(id, myRandom.Next(1024), myRandom.Next(768), Rect.right - Rect.left, Rect.bottom - Rect.top, true);
                                                            //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (therect.Right - therect.Left), therect.Bottom - therect.Top, true);
                                                            //MoveWindow(vewindowsfoundedz, Rectsccs.Left, Rectsccs.Top, (lastRectsccsformovement.Right - lastRectsccsformovement.Left), lastRectsccsformovement.Bottom - lastRectsccsformovement.Top, true);


                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);


                                                            /*


                                                            SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (rect.Right - rect.Left), rect.Bottom - rect.Top, SWP_SHOWWINDOW);

                                                            */


                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW); //| (uint)SetWindowPosFlags.SWP_DRAWFRAME
                                                            /*
                                                            sccsr14sc.Form1.someform.Size = new System.Drawing.Size(1920, 1080);
                                                            sccsr14sc.Form1.someform.FormBorderStyle = FormBorderStyle.None;
                                                            sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                                            sccsr14sc.Form1.someform.TopMost = true;

                                                            SetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(sccsr14sc.Form1.someform.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));
                                                            */

                                                            /*
                                                            var param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            var therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Maximize; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/

                                                            //SetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(sccsr14sc.Form1.theHandle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));//WS_EX_OVERLAPPEDWINDOW

                                                            // SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                                                            //progcanpause = 1;


                                                            //set the window to a borderless style
                                                            //const int GWL_STYLE = -16; //want to change the window style
                                                            //const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            /*IntPtr sult = SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style.\nSorry.");
                                                            }
                                                            SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SW_MAX);*/
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

                                                            /*
                                                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);
                                                            /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));*/











                                                            //progcanpause = 1;





                                                            /*
                                                            param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                             therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/




                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_DRAWFRAME);

                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);



                                                            /*
                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/
                                                            /*
                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);
                                                            *//*

                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);


                                                            SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            */





                                                            //https://social.msdn.microsoft.com/forums/azure/fr-fr/6791b517-1bc5-4e08-ac21-d73d443784cd/c-setwindowpos-for-minimized-windows?forum=csharpgeneral


                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_NOSIZE | (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS);

                                                            /* if (vewindowsfoundedz == null)
                                                             {

                                                             }
                                                             //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                             //set the window to a borderless style
                                                             const int GWL_STYLE = -16; //want to change the window style
                                                             const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                             IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);


                                                             if (sult == IntPtr.Zero)
                                                             {
                                                                 //in some cases SWL just outright fails, so we can notify the user and abort
                                                                 //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                 //return;
                                                                 Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                             }

                                                             sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_MAXIMIZE);

                                                             if (sult == IntPtr.Zero)
                                                             {
                                                                 //in some cases SWL just outright fails, so we can notify the user and abort
                                                                 //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                 //return;
                                                                 Console.WriteLine("Unable to alter window style WS_VISIBLE.\nSorry.");
                                                             }


                                                             SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                             */
                                                            //executeModeChange
                                                            /*
                                                            SelectedTitle = capturedwindowname;
                                                            executeModeChange();

                                                            */
                                                            /*
                                                            WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                                                            RECT therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;

                                                            //

                                                            param.ShowCmd = ShowWindowCommands.Normal; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */

                                                            /*
                                                             SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | (long)WindowStyles.WS_MAXIMIZE));
                                                             *//*param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                             param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                             SetWindowPlacement(vewindowsfoundedz, ref param);



                                                             therect = new RECT();
                                                             therect.left = 0;
                                                             therect.top = 0;
                                                             therect.bottom = 500;
                                                             therect.right = 500;


                                                             param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                             param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                             SetWindowPlacement(vewindowsfoundedz, ref param);
                                                             */
                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */

                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;//scdirectx.D3D.SurfaceWidth;
                                                            therect.right = 500;// scdirectx.D3D.SurfaceHeight;


                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */




                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */

                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 0;// scdirectx.D3D.SurfaceWidth;
                                                            therect.right = 0;// scdirectx.D3D.SurfaceHeight;


                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */


                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                            //set the window to a borderless style
                                                            const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style.\nSorry.");
                                                            }
                                                            SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);*/
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);




                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);//




                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;

                                                            
                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
                                                            //IntPtr hWnd = vewindowsfoundedz;// FindWindow(null, SelectedTitle);
                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                            //set the window to a borderless style
                                                            const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style.\nSorry.");
                                                            }*/
                                                            //otherwise we need to resize and reposition the window to take up the full screen
                                                            //const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                                                            //int screenWidth = GetSystemMetrics(0);
                                                            //int screenHeight = GetSystemMetrics(1);
                                                            //SetWindowPos(hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

                                                            //ShowWindow(vewindowsfoundedz, 0);

                                                            //ShowWindowAsync(vewindowsfoundedz, 0);
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);


                                                            //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                                                            //SetWindowLong(someform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT));
                                                            /*SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_HIDEWINDOW);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_DRAWFRAME);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_FRAMECHANGED);


                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            */



















                                                            /*if (vewindowsfoundedz != IntPtr.Zero)
                                                            {
                                                                WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                                                                placement.Length = Marshal.SizeOf(ShowWindowCommands.ShowNoActivate); //Marshal.SizeOf(placement);
                                                                SetWindowPlacement(vewindowsfoundedz, ref placement);//WINDOWPLACEMENT.Default.ShowCmd);

                                                            }*/


                                                            /*
                                                            LONG lExStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                                                            lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);
                                                            SetWindowLong(hwnd, GWL_EXSTYLE, lExStyle);
                                                            And finally, to get your window to redraw with the changed styles, you can use SetWindowPos.

                                                            SetWindowPos(hwnd, NULL, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER);
                                                            */


                                                            /*var WS_EX_STATICEDGE = 0x00020000L;
                                                            var WS_EX_DLGMODALFRAME = 0x00000001L;


                                                            //var testwindowptr = (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU)// WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);

                                                            var testwindowptr = (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);

                                                            //var getwindowptr = (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED)
                                                            //IntPtr lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);
                                                            //SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, testwindowptr);

                                                            uint SWP_FRAMECHANGED = 0x0020;
                                                            uint SWP_NOMOVE = 0x0002;
                                                            uint SWP_NOSIZE = 0x0001;
                                                            uint SWP_NOZORDER = 0x0004;
                                                            uint SWP_NOOWNERZORDER = 0x0200;



                                                            //SetWindowPos(vewindowsfoundedz, null, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER);

                                                            //SetWindowPos(window, -2, 100, 75, rect.bottom, rect.right, 0x0040);


                                                            var windowHandler = vewindowsfoundedz;// GetActiveWindowHandle();

                                                            //var windowRec = GetWindowRect(windowHandler);
                                                            // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
                                                            //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, Screen.AllScreens[monitor].WorkingArea.Left, Screen.AllScreens[monitor].WorkingArea.Top, windowRec.Size.Width + 16, windowRec.Size.Height + 38, SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, SetWindowPosFlags.SWP_FRAMECHANGED | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOOWNERZORDER )//SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, (SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER));//SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            */

                                                            //capturedwindowname = "SC2_x64.exe";// sometest.capturedwindowname;

                                                            //MessageBox((IntPtr)0, "" + capturedwindowname, "scmsg", 0);


                                                            //GetWindowThreadProcessId(sometest._hWnd, out testGetWindowThreadProcessId);

                                                            //sccsr14sc.Form1.someform.deactivatecursor();

                                                            //EnableWindow(vewindowsfoundedz, false);
                                                        }
                                                        else if (screencapturetype == 1)
                                                        {




                                                            GC.SuppressFinalize(vewindowsfoundedz);

                                                            DeleteObject(vewindowsfoundedz);
                                                            
                                                            vewindowsfoundedz = ((DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface)._hWnd;
                                                            //SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW
                                                            //SetWindowPos(vewindowsfoundedz, SpecialWindowHandles.HWND_TOPMOST, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE);

                                                            int screenWidth = GetSystemMetrics(0);
                                                            int screenHeight = GetSystemMetrics(1);
                                                            //var windowHandler = vewindowsfoundedz;// GetActiveWindowHandle();

                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);


                                                            //var windowRec = GetWindowRect(windowHandler);
                                                            // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW



                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS); //SetWindowPosFlags.SWP_SHOWWINDOW


                                                            capturedwindowname = ((DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface).capturedwindowname;

                                                            progcanpause = 1;







                                                            //somegcap = (GraphicsCapture)updatescript.captureMethod as GraphicsCapture;
                                                            //var sometest = (GraphicsCapture)updatescript.captureMethod as GraphicsCapture;

                                                            /*DeleteObject(vewindowsfoundedz);
                                                            int screenWidth = GetSystemMetrics(0);
                                                            int screenHeight = GetSystemMetrics(1);
                                                            vewindowsfoundedz = ((DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface)._hWnd;
                                                            //SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW

                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS); //SetWindowPosFlags.SWP_SHOWWINDOW


                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_NOSIZE);


                                                            capturedwindowname = ((DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface).capturedwindowname;


                                                            if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            var sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //otherwise we need to resize and reposition the window to take up the full screen
                                                            const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                                                            screenWidth = GetSystemMetrics(0);
                                                            screenHeight = GetSystemMetrics(1);
                                                            SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

                                                            */

                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_DRAWFRAME);

                                                            /*
                                                            var param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            var therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*var sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }*/


                                                            /*var param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            var therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);
                                                            */
                                                            /*
                                                            sccsr14sc.Form1.someform.Size = new System.Drawing.Size(1920, 1080);
                                                            sccsr14sc.Form1.someform.FormBorderStyle = FormBorderStyle.None;
                                                            sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                                            sccsr14sc.Form1.someform.TopMost = true;

                                                            SetWindowLong(sccsr14sc.Form1.someform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(sccsr14sc.Form1.someform.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));
                                                            */
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);

                                                            /*
                                                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);*/

                                                            /*
                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);

                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);


                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);


                                                            SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            */


                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */



                                                            /*WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                                                            RECT therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            therect = new RECT();
                                                            therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;

                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            //*/

                                                            //https://social.msdn.microsoft.com/forums/azure/fr-fr/6791b517-1bc5-4e08-ac21-d73d443784cd/c-setwindowpos-for-minimized-windows?forum=csharpgeneral
                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            var sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);// | (uint)SetWindowPosFlags.SWP_NOSIZE | (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS
                                                            */
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_NOSIZE | (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS);

                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            var sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_MAXIMIZE);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs1", "sccs", 0);

                                                            }*/


                                                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                            /*//set the window to a borderless style
                                                            const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);


                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                            }
                                                            sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_MAXIMIZE);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style WS_VISIBLE.\nSorry.");
                                                            }


                                                            SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW  );
                                                            */
                                                            /*
                                                            SelectedTitle = capturedwindowname;
                                                            executeModeChange();*/

                                                            /*
                                                            WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                                                            RECT therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;

                                                            //

                                                            param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);


                                                            */







                                                            /*SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | (long)WindowStyles.WS_MAXIMIZE));
                                                            */
                                                            /*//https://social.msdn.microsoft.com/forums/azure/fr-fr/6791b517-1bc5-4e08-ac21-d73d443784cd/c-setwindowpos-for-minimized-windows?forum=csharpgeneral
                                                            WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                                                            RECT therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);



                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;//scdirectx.D3D.SurfaceWidth;
                                                            therect.right = 500;// scdirectx.D3D.SurfaceHeight;


                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/
                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/









                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/
                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/

                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 0;//scdirectx.D3D.SurfaceWidth;
                                                            therect.right =0;// scdirectx.D3D.SurfaceHeight;


                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */


                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
                                                            //IntPtr hWnd = vewindowsfoundedz;// FindWindow(null, SelectedTitle);
                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                            //set the window to a borderless style
                                                            const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style.\nSorry.");
                                                            }
                                                            SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            */

                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);//


                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //otherwise we need to resize and reposition the window to take up the full screen
                                                            //const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                                                            //int screenWidth = GetSystemMetrics(0);
                                                            //int screenHeight = GetSystemMetrics(1);
                                                            //SetWindowPos(hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

                                                            //ShowWindow(vewindowsfoundedz, 0);
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_FRAMECHANGED);

                                                            //ShowWindowAsync(vewindowsfoundedz, 0);
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);


                                                            /*WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                                                            placement.Length = Marshal.SizeOf(placement);
                                                            GetWindowPlacement(vewindowsfoundedz, ref placement);*/



                                                            //var test = Marshal.SizeOf(ShowWindowCommands.Maximize);//WINDOWPLACEMENT.Default.ShowCmd;// sizeof(WINDOWPLACEMENT);

                                                            /*if (vewindowsfoundedz != IntPtr.Zero)
                                                            {
                                                                WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                                                                placement.Length = Marshal.SizeOf(ShowWindowCommands.ShowNoActivate); //Marshal.SizeOf(placement);
                                                                SetWindowPlacement(vewindowsfoundedz, ref placement);//WINDOWPLACEMENT.Default.ShowCmd);

                                                            }*/
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            /*SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_HIDEWINDOW);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_DRAWFRAME);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_FRAMECHANGED);

                                                            
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            */



                                                            /*WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                                                            placement.show
                                                            WINDOWPLACEMENT.Default.*/




                                                            //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;


                                                            //capturedwindowname = "SC2_x64.exe";// sometest.capturedwindowname;

                                                            //MessageBox((IntPtr)0, "" + capturedwindowname, "scmsg", 0);

                                                            /*
                                                            var WS_EX_STATICEDGE = 0x00020000L;
                                                            var WS_EX_DLGMODALFRAME = 0x00000001L;



                                                            var testwindowptr = (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);

                                                            //var getwindowptr = (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED)6666666666000 
                                                            //IntPtr lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);
                                                            //SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, testwindowptr);

                                                            uint SWP_FRAMECHANGED = 0x0020;
                                                            uint SWP_NOMOVE = 0x0002;
                                                            uint SWP_NOSIZE = 0x0001;
                                                            uint SWP_NOZORDER = 0x0004;
                                                            uint SWP_NOOWNERZORDER = 0x0200;



                                                            //SetWindowPos(vewindowsfoundedz, null, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER);

                                                            //SetWindowPos(window, -2, 100, 75, rect.bottom, rect.right, 0x0040);


                                                            var windowHandler = vewindowsfoundedz;// GetActiveWindowHandle();

                                                            //var windowRec = GetWindowRect(windowHandler);
                                                            // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
                                                            //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, Screen.AllScreens[monitor].WorkingArea.Left, Screen.AllScreens[monitor].WorkingArea.Top, windowRec.Size.Width + 16, windowRec.Size.Height + 38, SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, SetWindowPosFlags.SWP_FRAMECHANGED | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOOWNERZORDER )//SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, (SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER));//SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            */
                                                            //GetWindowThreadProcessId(sometest._hWnd, out testGetWindowThreadProcessId);

                                                            //sccsr14sc.Form1.someform.deactivatecursor();

                                                            //EnableWindow(vewindowsfoundedz, false);
                                                        }

                                                        //MessageBox((IntPtr)0, "var " + testGetWindowThreadProcessId, "scmsg", 0);
                                                        getwindowthreadprocessidint = 1;
                                                    }



                                                    last_hWnd = updatescript.captureMethod._hWnd;

                                                    //updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);

                                                    //updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);

                                                    //MessageBox((IntPtr)0, "_thread_looper1", "scmsg", 0);


                                                }

                                                //updatescript.captureMethod.StopCapture();
                                                //updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);

                                            }

                                        }
                                    }
                                }
                            }

                            //using ()
                            {
                                if (screencapturetype == 0 && changedscreencapturetype == 0 || screencapturetype == 1 && changedscreencapturetype == 0)
                                {
                                    if (updatescript != null) //&& sccsr14sc.Form1.capturedprogram != IntPtr.Zero
                                    {
                                        if (updatescript.scgraphicssec != null)
                                        {
                                            if (updatescript.scgraphicssec.activatevoxelinstancedvirtualdesktop == 1 && updatescript.scgraphicssec.activatevrheightmapfeature == 1)
                                            {
                                                if (updatescript.captureMethod != null)
                                                {
                                                    //Thread.Sleep(0);
                                                    //Thread.Sleep(1);
                                                    texture2d = updatescript.captureMethod.TryGetNextFrameAsTexture2D(updatescript.device);

                                                
                                                    if (texture2d != null)
                                                    {


                                                        if (textureresetswtc == 0)
                                                        {
                                                            _textureDescription = new Texture2DDescription
                                                            {
                                                                CpuAccessFlags = CpuAccessFlags.Read,
                                                                BindFlags = BindFlags.None,// ShaderResource | BindFlags.RenderTarget,
                                                                Format = Format.B8G8R8A8_UNorm,
                                                                Width = texture2d.Description.Width,
                                                                Height = texture2d.Description.Height,
                                                                OptionFlags = ResourceOptionFlags.None,
                                                                MipLevels = 1,
                                                                ArraySize = 1,
                                                                SampleDescription = { Count = 1, Quality = 0 },
                                                                Usage = ResourceUsage.Staging
                                                            };
                                                            _texture2d = new Texture2D(updatescript.device, _textureDescription);



                                                            _bitmap = new System.Drawing.Bitmap(_texture2d.Description.Width, _texture2d.Description.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                                            boundsRect = new System.Drawing.Rectangle(0, 0, _texture2d.Description.Width, _texture2d.Description.Height);

                                                            ///rectanglebitmap = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                                                            //bitmapData = bitmap.LockBits(rectanglebitmap, System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

                                                            bmpData = _bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                                                            _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
                                                            _bitmap.UnlockBits(bmpData);
                                                            _textureByteArray = new byte[_bytesTotal];
                                                            bmpstride = bmpData.Stride;


                                                            //bmpData = null;
                                                            /*if (_bitmap != null)
                                                            {
                                                                _bitmap.Dispose();
                                                                _bitmap = null;
                                                            }*/

                                                            textureresetswtc = 1;
                                                        }



                                                        //var thebitmapofscreen = new System.Drawing.Bitmap(_texture2d.Description.Width, _texture2d.Description.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);





                                                        if (texture2d != null)
                                                        {
                                                            if (updatescript != null)
                                                            {
                                                                if (updatescript.device != null)
                                                                {
                                                                    updatescript.device.ImmediateContext.CopyResource(texture2d, _texture2d);

                                                                    //DISCARDED
                                                                    //DISCARDED
                                                                    //DISCARDED
                                                                    dataBox1 = updatescript.device.ImmediateContext.MapSubresource(_texture2d, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                                                                    memoryBitmapStride = _textureDescription.Width * 4;
                                                                    //8801024
                                                                    //MessageBox((IntPtr)0, "fail " + memoryBitmapStride, "scmsg", 0); 
                                                                    //int memoryBitmapStridey = _textureDescription.Height * 4;
                                                                    columns = _textureDescription.Width;
                                                                    rows = _textureDescription.Height;
                                                                    interptr1 = dataBox1.DataPointer;

                                                                    if (dataBox1.RowPitch == memoryBitmapStride)
                                                                    {
                                                                        Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                                                                        //Utilities.CopyMemory(interptr1, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0), _textureByteArray.Length);
                                                                        // Stride not the same - copy line by line
                                                                        // Stride is the same
                                                                        //MessageBox((IntPtr)0, "fail0", "scmsg", 0);
                                                                    }
                                                                    else
                                                                    {
                                                                        //7704 // memorymapstride 4*1920
                                                                        //7936 // databox.rowpitch
                                                                        //8801024 // databox.slicepitch


                                                                        //var rowStride = Math.Min(dataBox1.RowPitch, memoryBitmapStride);
                                                                        //_textureByteArray = new byte[rowStride * rows];
                                                                        //MessageBox((IntPtr)0, "fail " + memoryBitmapStride + " " + rowStride + " " + dataBox1.RowPitch + " " + dataBox1.SlicePitch, "scmsg", 0);

                                                                        //Utilities.CopyMemory(interptr1, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0), _textureByteArray.Length);
                                                                        for (int y = 0; y < rows; y++)
                                                                        {
                                                                            Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                                                                            //Utilities.CopyMemory(interptr1 + y , Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0)+y, memoryBitmapStride);
                                                                        
                                                                        }

                                                                        //MessageBox((IntPtr)0, "fail1", "scmsg", 0);
                                                                        //Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                                                                    }


                                                                    var somebitmap = new System.Drawing.Bitmap(columns, rows, columns * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray,0));
                                                                    //somebitmap.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows.ToString("00") + columns.ToString("00") + ".png");
                                                                    //bitmapcounter++;

                                                                    /*var boundsRect = new System.Drawing.Rectangle(0, 0, 50, 40);
                                                                    var bitmap = somebitmap.Clone(boundsRect, somebitmap.PixelFormat);
                                                                    var test = cropAtRect(somebitmap, boundsRect);

                                                                    test.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png");
                                                                    bitmapcounter++;*/























                                                                    updatescript.device.ImmediateContext.UnmapSubresource(_texture2d, 0);

                                                                    GC.SuppressFinalize(interptr1);
                                                                    DeleteObject(interptr1);











                                                                    int yyonboardcomputer = (1080 - 50) - 5 + 1 + 1 + 5 - 1 - 1;
                                                                    int xxonboardcomputer = (1920 - 1050) + 15 + 5 + 3 + 3 - 1 - 1;

                                                                    int heightOfRectanglePlayerShip = onboardcpuiconmodh;
                                                                    int widthOfRectanglePlayerShip = onboardcpuiconmodw;


                                                                    int diffratiox = 256 / onboardcpuiconmodw;
                                                                    int diffratioy = 256 / onboardcpuiconmodh;

                                                                    int bitmapcountermax = 10;

                                                                    int xpic = 0;
                                                                    int ypic = 0;

                                                                    int counterofsamecolors = 0;

                                                                    //byte[] thenewarray = new byte[memoryBitmapStrideonboardcomputer * theonboardcomputertextureFINAL.Description.Height];
                                                                    //byte[] thenewarray = new byte[heightOfRectanglePlayerShip * widthOfRectanglePlayerShip * 4];
                                                                    //byte[] thenewarray = new byte[heightOfRectanglePlayerShip * memoryBitmapStrideonboardcomputer];

                                                                    
                                                                    //for (int y = 0; y < rowsonboardcomputer; y++)
                                                                    //{
                                                                    //    //Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                                                                    //    //Utilities.CopyMemory(interptr1 + y , Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0)+y, memoryBitmapStride);
                                                                    //    //Utilities.CopyMemory();
                                                                    //}

                                                                    _textureByteArrayobcpu = new byte[heightOfRectanglePlayerShip * widthOfRectanglePlayerShip * 8];

                                                                   



                                                                    
                                                                    //var memoryBitmapStride0 = _textureDescription.Width * 4;
                                                                    //dataBox1 = updatescript.device.ImmediateContext.MapSubresource(_texture2d, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                                                                    //updatescript.device.ImmediateContext.UnmapSubresource(_texture2d, 0);


                                                                    if (_textureByteArrayobcpu != null)
                                                                    {
                                                                        if (_textureByteArrayobcpu.Length > 0)
                                                                        {


                                                                            //TO READD TO CUT OUT SPECIFIC PARTS OF THE SCREENCAPTURE - BY STEVE CHASSÉ
                                                                            //TO READD TO CUT OUT SPECIFIC PARTS OF THE SCREENCAPTURE - BY STEVE CHASSÉ
                                                                            //TO READD TO CUT OUT SPECIFIC PARTS OF THE SCREENCAPTURE - BY STEVE CHASSÉ
                                                                            /*fixed (byte* textureByteArray = _textureByteArray) //, totalArray = _totalArray
                                                                            {
                                                                                int posY = 0;

                                                                                for (int yyyy = yyonboardcomputer; yyyy < yyonboardcomputer + heightOfRectanglePlayerShip; yyyy++)
                                                                                {
                                                                                    for (int xxxx = xxonboardcomputer; xxxx < xxonboardcomputer + widthOfRectanglePlayerShip; xxxx++)
                                                                                    {
                                                                                        var otherbytecolor = ((ypic * onboardcpuiconmodw) + xpic) * 4;
                                                                                        var bytePoser = ((yyyy * 1920) + xxxx) * 4;

                                                                                        if (otherbytecolor < widthOfRectanglePlayerShip * heightOfRectanglePlayerShip * 8)
                                                                                        {
                                                                                            //if (onboardcomputeiconpixeldata[otherbytecolor + 0] == textureByteArray[bytePoser + 0] &&
                                                                                            //    onboardcomputeiconpixeldata[otherbytecolor + 1] == textureByteArray[bytePoser + 1] &&
                                                                                            //    onboardcomputeiconpixeldata[otherbytecolor + 2] == textureByteArray[bytePoser + 2])
                                                                                            //{
                                                                                            //    //Program.MessageBox((IntPtr)0, "samecolor", "sccsmsg", 0);
                                                                                            //    counterofsamecolors++;
                                                                                            //}
                                                                                            
                                                                                            //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser + 1];
                                                                                            //thenewarray[otherbytecolor + 1] = textureByteArray[bytePoser + 0];
                                                                                            //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser + 2];

                                                                                            _textureByteArrayobcpu[otherbytecolor + 0] = textureByteArray[bytePoser + 0];
                                                                                            _textureByteArrayobcpu[otherbytecolor + 1] = textureByteArray[bytePoser + 1];
                                                                                            _textureByteArrayobcpu[otherbytecolor + 2] = textureByteArray[bytePoser + 2];


                                                                                            _textureByteArrayobcpu[otherbytecolor + 3] = 255;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            //Program.MessageBox((IntPtr)0, "out of range" + otherbytecolor, "sccsmsg", 0);
                                                                                        }


                                                                                        //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser + 0];
                                                                                        //thenewarray[otherbytecolor + 1] = textureByteArray[bytePoser + 1];
                                                                                        //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser + 2];
                                                                                        //thenewarray[otherbytecolor + 3] = 255;


                                                                                        posY++;
                                                                                        xpic++;
                                                                                    }
                                                                                    ypic++;
                                                                                }
                                                                            }*/





                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            for (int yyyy = yyonboardcomputer; yyyy < yyonboardcomputer + heightOfRectanglePlayerShip; yyyy++)
                                                                            {
                                                                                for (int xxxx = xxonboardcomputer; xxxx < xxonboardcomputer + widthOfRectanglePlayerShip; xxxx++)
                                                                                {
                                                                                    var otherbytecolor = ((ypic * onboardcpuiconmodw) + xpic) * 4;
                                                                                    int bytePoser0 = ((yyyy * 1920) + xxxx) * 4;

                                                                                    if (otherbytecolor < widthOfRectanglePlayerShip * heightOfRectanglePlayerShip * 8)
                                                                                    {
                                                                                        //if (onboardcomputeiconpixeldata[otherbytecolor + 0] == textureByteArray[bytePoser0 + 0] &&
                                                                                        //    onboardcomputeiconpixeldata[otherbytecolor + 1] == textureByteArray[bytePoser0 + 1] &&
                                                                                        //    onboardcomputeiconpixeldata[otherbytecolor + 2] == textureByteArray[bytePoser0 + 2])
                                                                                        //{
                                                                                        //    //Program.MessageBox((IntPtr)0, "samecolor", "sccsmsg", 0);
                                                                                        //    counterofsamecolors++;
                                                                                        //}

                                                                                        //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser0 + 1];
                                                                                        //thenewarray[otherbytecolor + 1] = textureByteArray[bytePoser0 + 0];
                                                                                        //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser0 + 2];

                                                                                        _textureByteArrayobcpu[otherbytecolor + 0] = _textureByteArray[bytePoser0 + 0];
                                                                                        _textureByteArrayobcpu[otherbytecolor + 1] = _textureByteArray[bytePoser0 + 1];
                                                                                        _textureByteArrayobcpu[otherbytecolor + 2] = _textureByteArray[bytePoser0 + 2];


                                                                                        _textureByteArrayobcpu[otherbytecolor + 3] = 255;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        //Program.MessageBox((IntPtr)0, "out of range" + otherbytecolor, "sccsmsg", 0);
                                                                                    }


                                                                                    //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser0 + 0];
                                                                                    //thenewarray[otherbytecolor + 1] = textureBytsomebitmapeArray[bytePoser0 + 1];
                                                                                    //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser0 + 2];
                                                                                    //thenewarray[otherbytecolor + 3] = 255;


                                                                                    xpic++;
                                                                                }
                                                                                ypic++;
                                                                            }
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE





                                                                         


                                                                            //SAVE PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //SAVE PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //SAVE PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            string thepath = Environment.CurrentDirectory + @"\assets\images\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png";


                                                                            if (classifythreadswtc == 0)
                                                                            {
                                                                            classifythread = new Thread((tester0000) =>
                                                                            {
                                                                        threadclassifyloop:

                                                                            if (perframetheimagerecogcounter >= perframetheimagerecogcountermax)
                                                                            {
                                                                                //sccs.Program.MessageBox((IntPtr)0, "Form1", "scmsg", 0);
                                                                                //machinelearning image recognition
                                                                                var percentfloat = ClassifySingleImage(mlContext, model, thepath);
                                                                                //machinelearning image recognition
                                                                                percentrecognitiondata[0].percentrecogfloat = percentfloat;



                                                                                var image1 = new System.Drawing.Bitmap(onboardcpuiconmodw, onboardcpuiconmodh, onboardcpuiconmodw * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArrayobcpu, 0));

                                                                                //https://stackoverflow.com/questions/3115076/adjust-the-contrast-of-an-image-in-c-sharp-efficiently
                                                                                //image1 = AdjustContrast(image1,100.0f);


                                                                                image1.Save(thepath);// Environment.CurrentDirectory + @"\assets\images\" + "currentscreencapture" + ".png");

                                                                                //string thepath = @"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png";

                                                                                bitmapcounter++;

                                                                                FileInfo infofile = new FileInfo(thepath);

                                                                                infofile.Refresh();

                                                                                    
                                                                                /*if (lastbitmap0 != null)
                                                                                {
                                                                                    lastbitmap0.Dispose();
                                                                                }

                                                                                lastbitmap0 = image1;*/
                                                                                    
                                                                                    image1.Dispose();


                                                                                    Thread.Sleep(1);
                                                                                        perframetheimagerecogcounter = 0;
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        Thread.Sleep(10);
                                                                                    }

                                                                                    // <SnippetCallClassifySingleImage>
                                                                                    // </SnippetCallClassifySingleImage>
                                                                                    perframetheimagerecogcounter++;
                                                                                    //MessageBox((IntPtr)0, "threadm0", "scmsg", 0);
                                                                                    goto threadclassifyloop;
                                                                                    //MessageBox((IntPtr)0, "threadm -1", "scmsg", 0);
                                                                                    //_thread_start:
                                                                                }, 0); //100000 //999999999

                                                                                classifythread.IsBackground = true;
                                                                                classifythread.Priority = ThreadPriority.Normal; //AboveNormal
                                                                                classifythread.SetApartmentState(ApartmentState.MTA);
                                                                                classifythread.Start();

                                                                                classifythreadswtc = 1;
                                                                            }


                                                                            //Console.WriteLine("percent:" + percentrecognitiondata[0].percentrecogfloat);
                                                                            perframetheimagerecogcounterswtc = 0;
                                                                            if (percentrecognitiondata[0].percentrecogfloat < 0.985f)
                                                                            {

                                                                            }
                                                                            else
                                                                            {
                                                                                perframetheimagerecogcounterswtc = 1;
                                                                                //TO READD TO CUT OUT SPECIFIC PARTS OF THE SCREENCAPTURE - BY STEVE CHASSÉ
                                                                                //TO READD TO CUT OUT SPECIFIC PARTS OF THE SCREENCAPTURE - BY STEVE CHASSÉ
                                                                                //TO READD TO CUT OUT SPECIFIC PARTS OF THE SCREENCAPTURE - BY STEVE CHASSÉ

                                                                            }

                                                                            //SAVE PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //SAVE PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //SAVE PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE






























































                                                                            //icon GREEN OVERLAY
                                                                            //icon GREEN OVERLAY
                                                                            //icon GREEN OVERLAY
                                                                            /*if (perframetheimagerecogcounterswtc == 1)
                                                                            {
                                                                                ///fixed (byte* textureByteArray = _textureByteArray) //, totalArray = _totalArray
                                                                                {
                                                                                    //int posY = 0;

                                                                                    for (int yyyy = yyonboardcomputer; yyyy < yyonboardcomputer + heightOfRectanglePlayerShip; yyyy++)
                                                                                    {
                                                                                        for (int xxxx = xxonboardcomputer; xxxx < xxonboardcomputer + widthOfRectanglePlayerShip; xxxx++)
                                                                                        {
                                                                                            var otherbytecolor = ((ypic * onboardcpuiconmodw) + xpic) * 4;
                                                                                            var bytePoser = ((yyyy * 1920) + xxxx) * 4;

                                                                                            _textureByteArray[bytePoser + 0] = 0; //b
                                                                                            _textureByteArray[bytePoser + 1] = 255; //g
                                                                                            _textureByteArray[bytePoser + 2] = 0; //r
                                                                                            _textureByteArray[bytePoser + 3] = 255; //a

                                                                                            //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser + 0];
                                                                                            //thenewarray[otherbytecolor + 1] = textureByteArray[bytePoser + 1];
                                                                                            //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser + 2];
                                                                                            //thenewarray[otherbytecolor + 3] = 255;

                                                                                           // posY++;
                                                                                            xpic++;
                                                                                        }
                                                                                        ypic++;
                                                                                    }
                                                                                }
                                                                            }*/
                                                                            //icon GREEN OVERLAY
                                                                            //icon GREEN OVERLAY
                                                                            //icon GREEN OVERLAY








                                                                            //RADAR OVERLAY
                                                                            //RADAR OVERLAY
                                                                            //RADAR OVERLAY
                                                                            /*
                                                                           int yyradar = (0) + 10-2;
                                                                           int xxradar = (1920) - 305-2-1;
                                                                           int heightOfRectangleradar = 305;
                                                                           int widthOfRectangleradar = 305;

                                                                           float radius = 154.0f;

                                                                           for (int yyyy = yyradar, ypic1 = 0; yyyy < yyradar + heightOfRectangleradar; yyyy++, ypic1++)
                                                                           {
                                                                               for (int xxxx = xxradar, xpic1 = 0; xxxx < xxradar + widthOfRectangleradar; xxxx++, xpic1++)
                                                                               {
                                                                                   float midx = widthOfRectangleradar * 0.5f;
                                                                                   float midy = heightOfRectangleradar * 0.5f;

                                                                                   Vector2 circlepivot = new Vector2(midx, midy);
                                                                                   Vector2 currentpoint = new Vector2(xpic1, ypic1);

                                                                                   float distance = Vector2.Distance(circlepivot, currentpoint);

                                                                                   //Console.WriteLine("xpic1:" + xpic1 + "/ypic1:" + ypic1 + "_" + distance + "/circlepivot:" + circlepivot + "/currentpoint:" + currentpoint);

                                                                                   if (distance < radius)
                                                                                   {

                                                                                       var otherbytecolor = ((ypic1 * onboardcpuiconmodw) + xpic1) * 4;
                                                                                       var bytePoser = ((yyyy * 1920) + xxxx) * 4;


                                                                                       if (_textureByteArray[bytePoser + 0] < 100 && _textureByteArray[bytePoser + 1] >= 150 && _textureByteArray[bytePoser + 2] < 100 ||
                                                                                           //radar yellow repair platform and refuel platform
                                                                                           _textureByteArray[bytePoser + 0] > 65 &&  _textureByteArray[bytePoser + 0] < 85 && _textureByteArray[bytePoser + 1] >= 55  && _textureByteArray[bytePoser + 2] >= 55 ||
                                                                                           //radar yellow quest square outline
                                                                                           _textureByteArray[bytePoser + 0] < 75 && _textureByteArray[bytePoser + 1] >= 25 && _textureByteArray[bytePoser + 2] >= 25 ||

                                                                                           //white garbage items or player or player direction line
                                                                                           //_textureByteArray[bytePoser + 0] >= 175 && _textureByteArray[bytePoser + 1] >= 175 && _textureByteArray[bytePoser + 2] >= 175
                                                                                           _textureByteArray[bytePoser + 0] >= 150 && _textureByteArray[bytePoser + 1] >= 150 && _textureByteArray[bytePoser + 2] >= 150
                                                                                           )
                                                                                       {


                                                                                       }
                                                                                       else
                                                                                       {
                                                                                           _textureByteArray[bytePoser + 0] = 125; //b
                                                                                           _textureByteArray[bytePoser + 1] = 75; //g
                                                                                           _textureByteArray[bytePoser + 2] = 25; //r
                                                                                           _textureByteArray[bytePoser + 3] = 255; //a
                                                                                       }
                                                                                   }

                                                                                   // posY++;
                                                                               }
                                                                           }*/
                                                                            //RADAR OVERLAY
                                                                            //RADAR OVERLAY
                                                                            //RADAR OVERLAY


                                                                            /*ModelInputBytes modelinput = new ModelInputBytes();
                                                                            modelinput.ImageBytes = _textureByteArrayobcpu;
                                                                            modelinput.Label = "currentscreencapture";



                                                                            var ouputprediction = PredictFromBytes(modelinput);*/








                                                                            /*
                                                                            //Load sample data
                                                                            var imageBytes = File.ReadAllBytes(@"C:\Users\miaomiaomiao\Desktop\WeatherData2\Cloudy\cloudy1.jpg");
                                                                            AzureImage.ModelInput sampleData = new AzureImage.ModelInput()
                                                                            {
                                                                                ImageSource = imageBytes,
                                                                            };

                                                                            //Load model and predict output
                                                                            var result = AzureImage.Predict(sampleData);*/









                                                                            /*int difference = (int)(SimpleImageComparisonClassLibrary.ImageTool.GetPercentageDifference(dstImg, image1) * 100);


                                                                            ///image1.Dispose();

                                                                            if (difference < 60)
                                                                            {

                                                                            }
                                                                            else
                                                                            {
                                                                                //TO READD TO CUT OUT SPECIFIC PARTS OF THE SCREENCAPTURE - BY STEVE CHASSÉ
                                                                                //TO READD TO CUT OUT SPECIFIC PARTS OF THE SCREENCAPTURE - BY STEVE CHASSÉ
                                                                                //TO READD TO CUT OUT SPECIFIC PARTS OF THE SCREENCAPTURE - BY STEVE CHASSÉ
                                                                                fixed (byte* textureByteArray = _textureByteArray) //, totalArray = _totalArray
                                                                                {
                                                                                    int posY = 0;

                                                                                    for (int yyyy = yyonboardcomputer; yyyy < yyonboardcomputer + heightOfRectanglePlayerShip; yyyy++)
                                                                                    {
                                                                                        for (int xxxx = xxonboardcomputer; xxxx < xxonboardcomputer + widthOfRectanglePlayerShip; xxxx++)
                                                                                        {
                                                                                            var otherbytecolor = ((ypic * onboardcpuiconmodw) + xpic) * 4;
                                                                                            var bytePoser = ((yyyy * 1920) + xxxx) * 4;

                                                                                           

                                                                                            textureByteArray[bytePoser + 0] = 0; //g
                                                                                            textureByteArray[bytePoser + 1] = 0; //b
                                                                                            textureByteArray[bytePoser + 2] = 255; //r
                                                                                            textureByteArray[bytePoser + 3] = 255; //a


                                                                                            //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser + 0];
                                                                                            //thenewarray[otherbytecolor + 1] = textureByteArray[bytePoser + 1];
                                                                                            //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser + 2];
                                                                                            //thenewarray[otherbytecolor + 3] = 255;


                                                                                            posY++;
                                                                                            xpic++;
                                                                                        }
                                                                                        ypic++;
                                                                                    }
                                                                                }

                                                                            }*/
                                                                            //MessageBox((IntPtr)0, "diff:" + difference, "scmsg", 0);

                                                                            //Console.WriteLine("diff:" + difference);




                                                                        }
                                                                    }                                                                    
                                                               





                                                                    //https://stackoverflow.com/questions/152028/are-there-any-ok-image-recognition-libraries-for-net
                                                                    /*
                                                                    ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);

                                                                    // Compare two images
                                                                    TemplateMatch[] matchings = tm.ProcessImage(somebitmap, image1);

                                                                    Console.WriteLine("similarity" + matchings[0].Similarity);
                                                                    // Check similarity level
                                                                    if (matchings[0].Similarity > 0.95)
                                                                    {
                                                                        // Do something with quite similar images
                                                                   
                                                                    }*/




                                                                    /*var image0 = new System.Drawing.Bitmap(50,40, 50 * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArrayobcpu, 0));
                                                                    /*image0.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png");
                                                                    bitmapcounter++;

                                                                    int width = image0.Width;
                                                                    int height = image0.Height;
                                                                   System.Drawing.RectangleF destinationRect = new System.Drawing.RectangleF(
                                                                        150,
                                                                        20,
                                                                        1.3f * width,
                                                                        1.3f * height);


                                                                    Graphics g = Graphics.FromImage(image0);
                                                                    // Draw a portion of the image. Scale that portion of the image
                                                                    // so that it fills the destination rectangle.
                                                                    System.Drawing.RectangleF sourceRect = new System.Drawing.RectangleF(0, 0, .75f * width, .75f * height);
                                                                    g.DrawImage(
                                                                        image0,
                                                                        destinationRect,
                                                                        sourceRect,
                                                                        GraphicsUnit.Pixel);

                                                                    image0.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png");
                                                                    bitmapcounter++;*/





                                                                    /*
                                                                    var boundsRect = new System.Drawing.Rectangle(0, 0, 50, 40);
                                                                    var bitmap = image0.Clone(boundsRect, image0.PixelFormat);
                                                                    var test = cropAtRect(image0, boundsRect);
                                                                   
                                                                    test.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png");
                                                                    bitmapcounter++;*/





                                                                    /*
                                                                    var image0 = new System.Drawing.Bitmap(bitmaponboardcomputer.Width, bitmaponboardcomputer.Height, bmpData.Stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArrayobcpu, 0));
                                                                    image0.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png");
                                                                    bitmapcounter++;*/

                                                                    //Console.WriteLine("counterofsamecolors" + counterofsamecolors);
                                                                    //Program.MessageBox((IntPtr)0, "counterofsamecolors" + counterofsamecolors, "sccsmsg", 0);
                                                                    if (counterofsamecolors >= (widthOfRectanglePlayerShip * heightOfRectanglePlayerShip) * 0.25f)
                                                                    {
                                                                        //Program.MessageBox((IntPtr)0, "counterofsamecolors" + counterofsamecolors, "sccsmsg", 0);
                                                                        //Program.MessageBox((IntPtr)0, "this is the onboard computer icon", "sccsmsg", 0);
                                                                    }

                                                                    //int countgreenscreenpixelsareweinanoptiontransition = 0;



                                                                    














                                                                    int SAVE_RES_X = 1920 / 7;
                                                                    int SAVE_RES_Y = 1080 / 6;

                                                                    int currentzoomw = 274;
                                                                    int currentzoomh = 200;



                                                                    /*
                                                                    int currentzoomw = (int)Math.Floor(130.0f);
                                                                    int currentzoomh = (int)Math.Floor(77.0f);

                                                                    int SAVE_RES_X = currentzoomw;
                                                                    int SAVE_RES_Y = currentzoomh;*/


                                                                    /*//https://social.msdn.microsoft.com/Forums/sqlserver/en-US/8be9616f-0549-4836-829c-148cad31fc0d/c-resize-image-while-keeping-original-bit-depth?forum=winforms
                                                                    //Image oldImage = Image.FromFile(fileName);
                                                                    System.Drawing.Bitmap newBmp = new System.Drawing.Bitmap(SAVE_RES_X, SAVE_RES_Y, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                                                                    using (Graphics g = Graphics.FromImage(newBmp))
                                                                    {
                                                                        g.DrawImage(somebitmap, 0, 0, SAVE_RES_X, SAVE_RES_Y);
                                                                    }

                                                                    //somebitmap.Save(@"C:\Users\steve\Desktop\screenRecord\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png"); //Save(".\\ResizedImages\\oldImage.tif");
                                                                    //newBmp.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png"); //Save(".\\ResizedImages\\newImage.tif");

                                                                    byte[] rgbvaluesbytesarray = GetRGBValues(newBmp);
                                                                    */
                                                                    //var boundsRectscreen = new System.Drawing.Rectangle(0, 0, newBmp.Width, newBmp.Height);
                                                                    //var bmpDatascreen = newBmp.LockBits(boundsRectscreen, ImageLockMode.ReadOnly, bitmaponboardcomputer.PixelFormat);

                                                                    //var image0 = new System.Drawing.Bitmap(newBmp.Width, newBmp.Height, bmpDatascreen.Stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(rgbvaluesbytesarray, 0));
                                                                    //image0.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png");
                                                                    //bitmapcounter++;

                                                                    float tunegreen = 0.93f;
                                                                    int pixelcheckx = 10;





                                                                    float storecountgreenpixelsnumberofloopsy = -1.0f;
                                                                    float storecountgreenpixelsxatmaxy = -1.0f;

                                                                    storecountgreenpixelsx = -1;
                                                                    storecountgreenpixelsy = -1;

                                                                    int countgreenpixelsx = 0;
                                                                    int countgreenpixelsy = 0;

                                                                   
                                                                    int storecountswtc = 0;


                                                                    int countblacktexturecounterswtc = 0;
                                                                    int countblacktexturecounterx = 0;
                                                                    int countblacktexturecountery = 0;


                                                                    int bytePoser = 0;
                                                                    //fixed (byte* textureByteArray = _textureByteArray) //, totalArray = _totalArray
                                                                    {
                                                                        int posY = 0;
                                                                        for (int yyyy = 0; yyyy < 1080; yyyy++)
                                                                        {
                                                                            for (int xxxx = 0; xxxx < 1920; xxxx++)
                                                                            {
                                                                                bytePoser = ((yyyy * 1920) + xxxx) * 4;

                                                                                if (_textureByteArray[bytePoser + 0] <= 0.01f && _textureByteArray[bytePoser + 1] > 0.95f && _textureByteArray[bytePoser + 2] <= 0.01f)
                                                                                {
                                                                                    if (countblacktexturecounterswtc == 0)
                                                                                    {
                                                                                        if (countblacktexturecounterx >= pixelcheckx)// && countblacktexturecountery >= 60)
                                                                                        {

                                                                                            int onboardcpuiconmodw = currentzoomw;// 137.1428571428571f;
                                                                                            int onboardcpuiconmodh = currentzoomh;// 77.14285714285714f; //73.125f



                                                                                            int yyvirtualdesktopscreen = yyyy - 10;// (1080 - 50) - 5 + 1 + 1 + 5 - 1 - 1;
                                                                                            int xxvirtualdesktopscreen = xxxx - pixelcheckx;// (1920 - 1050) + 15 + 5 + 3 + 3 - 1 - 1;

                                                                                            int valueofxatinit = xxvirtualdesktopscreen;


                                                                                            int heightofvirtualdesktopscreen = onboardcpuiconmodh;
                                                                                            int widthofvirtualdesktopscreen = onboardcpuiconmodw;

                                                                                            int yii = 0;
                                                                                            int xii = 0;

                                                                                            if (yyvirtualdesktopscreen >= 0 && xxvirtualdesktopscreen >= 0)
                                                                                            {
                                                                                                for (int yi = yyvirtualdesktopscreen; yi < yyvirtualdesktopscreen + heightofvirtualdesktopscreen; yi++)
                                                                                                {
                                                                                                    for (int xi = xxvirtualdesktopscreen; xi < xxvirtualdesktopscreen + widthofvirtualdesktopscreen; xi++)
                                                                                                    {
                                                                                                        var otherbytecolor = ((yii * onboardcpuiconmodw) + xii) * 4;

                                                                                                        var bytePoserr = ((yi * 1920) + xi) * 4;

                                                                                                        if (bytePoserr + 0 < 1920 * 1080 * 4 && bytePoserr + 1 < 1920 * 1080 * 4 && bytePoserr + 2 < 1920 * 1080 * 4)
                                                                                                        {



                                                                                                         



                                                                                                            if (_textureByteArray[bytePoserr + 0] <= 0.01f && _textureByteArray[bytePoserr + 1] >= 0.95f && _textureByteArray[bytePoserr + 2] <= 0.01f)
                                                                                                            {

                                                                                                                //THE MOMENT THAT THE PIXEL IS GREEN THE FIRST TIME
                                                                                                                if (storecountgreenpixelsxatmaxy == -1)
                                                                                                                {
                                                                                                                    //IF THE HEIGHT OF THE VIRTUAL SCREEN IS REACHED IN VOID EXPANSE
                                                                                                                    if (yi >= (yyvirtualdesktopscreen + heightofvirtualdesktopscreen) - 110) //??? rectangle too much height
                                                                                                                    {
                                                                                                                        
                                                                                                                        //Program.MessageBox((IntPtr)0, "end of screen row", "sccsmsg", 0);

                                                                                                                        //STORE THE HEIGHT OF THE VIRTUAL SCREEN
                                                                                                                        storecountgreenpixelsnumberofloopsy = (heightofvirtualdesktopscreen)-110;// (yi - yyvirtualdesktopscreen);

                                                                                                                        //STORE THE DIFFERENCE IN THE X VALUE OF THE GREEN PIXEL, SO IN ORDER TO CHECK IF THE VIRTUAL SCREEN IS IN AN ANGLE OR IS IT VERTICAL FROM THE PERSPECTIVE OF THE VIEWER/PLAYER THAT LOOK AT THE VIRTUAL SCREEN THROUGH THE MONITOR.
                                                                                                                        storecountgreenpixelsxatmaxy = (xi - valueofxatinit);
                                                                                                                    }
                                                                                                                }

                                                                                                                countgreenpixelsx++;

                                                                                                                if (yii != countgreenpixelsy)
                                                                                                                {
                                                                                                                    countgreenpixelsy++;
                                                                                                                }


                                                                                                                /*if (rgbvaluesbytesarray != null)
                                                                                                                {
                                                                                                                    if (rgbvaluesbytesarray.Length > 0)
                                                                                                                    {

                                                                                                                        if (otherbytecolor + 0 < onboardcpuiconmodw * onboardcpuiconmodh * 4 &&
                                                                                                                            otherbytecolor + 1 < onboardcpuiconmodw * onboardcpuiconmodh * 4 &&
                                                                                                                            otherbytecolor + 2 < onboardcpuiconmodw * onboardcpuiconmodh * 4)
                                                                                                                        {
                                                                                                                            // posY++;
                                                                                                                            _textureByteArray[bytePoserr + 0] = rgbvaluesbytesarray[otherbytecolor + 0];//r
                                                                                                                            _textureByteArray[bytePoserr + 1] = rgbvaluesbytesarray[otherbytecolor + 1];//0;//g
                                                                                                                            _textureByteArray[bytePoserr + 2] = rgbvaluesbytesarray[otherbytecolor + 2];//240;//b

                                                                                                                        }
                                                                                                                    }
                                                                                                                }*/
                                                                                                                storecountswtc = 1;
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (storecountswtc == 1)
                                                                                                                {
                                                                                                                    if (storecountgreenpixelsx == -1)
                                                                                                                    {
                                                                                                                        storecountgreenpixelsx = countgreenpixelsx;
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        /*if (storecountgreenpixelsy == -1)
                                                                                                                        {
                                                                                                                            storecountgreenpixelsy = countgreenpixelsy;
                                                                                                                        }*/
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        xii++;
                                                                                                    }
                                                                                                    yii++;
                                                                                                }
                                                                                            }

                                                                                            countblacktexturecounterx = 0;
                                                                                            countblacktexturecountery = 0;

                                                                                            countblacktexturecounterswtc = 1;
                                                                                            //break;
                                                                                        }
                                                                                    }
                                                                                    countblacktexturecounterx++;

                                                                                    if (yyyy != countblacktexturecountery)
                                                                                    {
                                                                                        countblacktexturecountery++;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {

                                                                                    countblacktexturecounterx = 0;
                                                                                    countblacktexturecountery = 0;
                                                                                }
                                                                                posY++;
                                                                            }
                                                                        }
                                                                    }













                                                                    //267-274
                                                                    //249-256
                                                                    //198-204
                                                                    //167-173
                                                                    //145-149
                                                                    //127-132
                                                                    //114-117

                                                                    percentrecognitiondataVEVD[0].zoomtypevevdswtc = 0;

                                                                    if (storecountgreenpixelsx >= 113 && storecountgreenpixelsx <= 118 ||
                                                                        storecountgreenpixelsx >= 126 && storecountgreenpixelsx <= 133 ||
                                                                        storecountgreenpixelsx >= 144 && storecountgreenpixelsx <= 150 ||
                                                                        storecountgreenpixelsx >= 166 && storecountgreenpixelsx <= 174 ||
                                                                        storecountgreenpixelsx >= 197 && storecountgreenpixelsx <= 205 ||
                                                                        storecountgreenpixelsx >= 248 && storecountgreenpixelsx <= 257 ||
                                                                        storecountgreenpixelsx >= 266 && storecountgreenpixelsx <= 275
                                                                        )
                                                                    {
                                                                        percentrecognitiondataVEVD[0].zoomtypevevdswtc = 1;
                                                                        //Console.WriteLine("laststorecountgreenpixelsx:" + laststorecountgreenpixelsx);

                                                                        SAVE_RES_X = storecountgreenpixelsx;
    
                                                                        float theratio = 1920 / storecountgreenpixelsx;

                                                                        SAVE_RES_Y = ((SAVE_RES_X * 1080) / 1920);// (int)(1080 / (1080 / theratio)) / 2;

                                                                        //https://social.msdn.microsoft.com/Forums/sqlserver/en-US/8be9616f-0549-4836-829c-148cad31fc0d/c-resize-image-while-keeping-original-bit-depth?forum=winforms
                                                                        //Image oldImage = Image.FromFile(fileName);
                                                                        newBmp = new System.Drawing.Bitmap(SAVE_RES_X, SAVE_RES_Y, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                                                                        var boundsRect = new System.Drawing.Rectangle(0, 0, newBmp.Width, newBmp.Height);
                                                                        var bmpDataVEVD = newBmp.LockBits(boundsRect, ImageLockMode.ReadOnly, newBmp.PixelFormat);
                                                                        var _bytesTotalobcpuVEVD = Math.Abs(bmpDataVEVD.Stride) * newBmp.Height;
                                                                        newBmp.UnlockBits(bmpDataVEVD);
                                                                        byte[] _textureByteArrayobcpuVEVD = new byte[_bytesTotalobcpuVEVD];






                                                                        using (Graphics g = Graphics.FromImage(newBmp))
                                                                        {
                                                                            g.DrawImage(somebitmap, 0, 0, SAVE_RES_X, SAVE_RES_Y);
                                                                        }

                                                                        //somebitmap.Save(@"C:\Users\steve\Desktop\screenRecord\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png"); //Save(".\\ResizedImages\\oldImage.tif");
                                                                        //newBmp.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png"); //Save(".\\ResizedImages\\newImage.tif");

                                                                        byte[] rgbvaluesbytesarray = GetRGBValues(newBmp);

                                                                        int zoomtype = 0;

                                                                        if(storecountgreenpixelsx >= 113 && storecountgreenpixelsx <= 118)
                                                                        {
                                                                            zoomtype = 1;
                                                                        }
                                                                        else if (storecountgreenpixelsx >= 126 && storecountgreenpixelsx <= 133)
                                                                        {
                                                                            zoomtype = 2;
                                                                        }
                                                                        else if (storecountgreenpixelsx >= 144 && storecountgreenpixelsx <= 150)
                                                                        {
                                                                            zoomtype = 3;
                                                                        }
                                                                        else if (storecountgreenpixelsx >= 166 && storecountgreenpixelsx <= 174)
                                                                        {
                                                                            zoomtype = 4;
                                                                        }
                                                                        else if (storecountgreenpixelsx >= 197 && storecountgreenpixelsx <= 205)
                                                                        {
                                                                            zoomtype = 5;
                                                                        }
                                                                        else if (storecountgreenpixelsx >= 248 && storecountgreenpixelsx <= 257)
                                                                        {
                                                                            zoomtype = 6;
                                                                        }
                                                                        else if (storecountgreenpixelsx >= 266 && storecountgreenpixelsx <= 275)
                                                                        {
                                                                            zoomtype = 7;
                                                                        }










                                                                        //Console.WriteLine("zoomtype:" + zoomtype);








                                                                        //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE
                                                                        //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE
                                                                        /*bitmaponboardcomputer = new System.Drawing.Bitmap(256, 256, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                                                        var boundsRect = new System.Drawing.Rectangle(0, 0, 256, 256);
                                                                        bmpData = bitmaponboardcomputer.LockBits(boundsRect, ImageLockMode.ReadOnly, bitmaponboardcomputer.PixelFormat);
                                                                        _bytesTotalobcpu = Math.Abs(bmpData.Stride) * bitmaponboardcomputer.Height;
                                                                        bitmaponboardcomputer.UnlockBits(bmpData);
                                                                        _textureByteArrayobcpu = new byte[_bytesTotalobcpu];
                                                                        bmpData = null;*/
                                                                        //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE
                                                                        //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE




                                                                        //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                        //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                        //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                        /* for (int yyyy = yyonboardcomputer; yyyy < yyonboardcomputer + heightOfRectanglePlayerShip; yyyy++)
                                                                         {
                                                                             for (int xxxx = xxonboardcomputer; xxxx < xxonboardcomputer + widthOfRectanglePlayerShip; xxxx++)
                                                                             {
                                                                                 var otherbytecolor = ((ypic * onboardcpuiconmodw) + xpic) * 4;
                                                                                 int bytePoser0 = ((yyyy * 1920) + xxxx) * 4;

                                                                                 if (otherbytecolor < widthOfRectanglePlayerShip * heightOfRectanglePlayerShip * 8)
                                                                                 {
                                                                                     //if (onboardcomputeiconpixeldata[otherbytecolor + 0] == textureByteArray[bytePoser0 + 0] &&
                                                                                     //    onboardcomputeiconpixeldata[otherbytecolor + 1] == textureByteArray[bytePoser0 + 1] &&
                                                                                     //    onboardcomputeiconpixeldata[otherbytecolor + 2] == textureByteArray[bytePoser0 + 2])
                                                                                     //{
                                                                                     //    //Program.MessageBox((IntPtr)0, "samecolor", "sccsmsg", 0);
                                                                                     //    counterofsamecolors++;
                                                                                     //}

                                                                                     //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser0 + 1];
                                                                                     //thenewarray[otherbytecolor + 1] = textureByteArray[bytePoser0 + 0];
                                                                                     //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser0 + 2];

                                                                                     _textureByteArrayobcpu[otherbytecolor + 0] = _textureByteArray[bytePoser0 + 0];
                                                                                     _textureByteArrayobcpu[otherbytecolor + 1] = _textureByteArray[bytePoser0 + 1];
                                                                                     _textureByteArrayobcpu[otherbytecolor + 2] = _textureByteArray[bytePoser0 + 2];


                                                                                     _textureByteArrayobcpu[otherbytecolor + 3] = 255;
                                                                                 }
                                                                                 else
                                                                                 {
                                                                                     //Program.MessageBox((IntPtr)0, "out of range" + otherbytecolor, "sccsmsg", 0);
                                                                                 }



                                                                                 //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser0 + 0];
                                                                                 //thenewarray[otherbytecolor + 1] = textureBytsomebitmapeArray[bytePoser0 + 1];
                                                                                 //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser0 + 2];
                                                                                 //thenewarray[otherbytecolor + 3] = 255;


                                                                                 xpic++;
                                                                             }
                                                                             ypic++;
                                                                         }*/
                                                                        //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                        //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                        //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE


                                                                        currentzoomw = storecountgreenpixelsx;
                                                                        currentzoomh = SAVE_RES_Y;

                                                                        countgreenpixelsx = 0;
                                                                        countgreenpixelsy = 0;

                                                                        countblacktexturecounterswtc = 0;
                                                                        countblacktexturecounterx = 0;
                                                                        countblacktexturecountery = 0;


                                                                        bytePoser = 0;
                                                                        //fixed (byte* textureByteArray = _textureByteArray) //, totalArray = _totalArray
                                                                        {
                                                                            int posY = 0;
                                                                            for (int yyyy = 0; yyyy < 1080; yyyy++)
                                                                            {
                                                                                for (int xxxx = 0; xxxx < 1920; xxxx++)
                                                                                {
                                                                                    bytePoser = ((yyyy * 1920) + xxxx) * 4;

                                                                                    if (_textureByteArray[bytePoser + 0] <= 0.01f && _textureByteArray[bytePoser + 1] > tunegreen && _textureByteArray[bytePoser + 2] <= 0.01f)
                                                                                    {
                                                                                        if (countblacktexturecounterswtc == 0)
                                                                                        {
                                                                                            if (countblacktexturecounterx >= pixelcheckx)// && countblacktexturecountery >= 60)
                                                                                            {

                                                                                                int onboardcpuiconmodw = currentzoomw;// 137.1428571428571f;
                                                                                                int onboardcpuiconmodh = currentzoomh;// 77.14285714285714f; //73.125f

                                                                                                int yyvirtualdesktopscreen = yyyy - 10;// (1080 - 50) - 5 + 1 + 1 + 5 - 1 - 1;
                                                                                                int xxvirtualdesktopscreen = xxxx - pixelcheckx;// (1920 - 1050) + 15 + 5 + 3 + 3 - 1 - 1;

                                                                                                int heightofvirtualdesktopscreen = onboardcpuiconmodh;
                                                                                                int widthofvirtualdesktopscreen = onboardcpuiconmodw;

                                                                                                int yii = 0;
                                                                                                int xii = 0;

                                                                                                //storecountgreenpixelsxatmaxy
                                                                                                //storecountgreenpixelsnumberofloopsy

                                                                                         
                                                                                                if (yyvirtualdesktopscreen >= 0 && xxvirtualdesktopscreen >= 0)
                                                                                                {




                                                                                                    for (int yi = yyvirtualdesktopscreen; yi < yyvirtualdesktopscreen + heightofvirtualdesktopscreen; yi++)
                                                                                                    {
                                                                                                        //xxvirtualdesktopscreen += numberofxpery;

                                                                                                        for (int xi = xxvirtualdesktopscreen; xi < xxvirtualdesktopscreen + widthofvirtualdesktopscreen; xi++)
                                                                                                        {
                                                                                                            var otherbytecolor = ((yii * onboardcpuiconmodw) + xii) * 4;

                                                                                                            var bytePoserr = ((yi * 1920) + (xi )) * 4;

                                                                                                            if (bytePoserr + 0 < 1920 * 1080 * 4 && bytePoserr + 1 < 1920 * 1080 * 4 && bytePoserr + 2 < 1920 * 1080 * 4)
                                                                                                            {
                                                                                                                if (_textureByteArray[bytePoserr + 0] <= 0.01f && _textureByteArray[bytePoserr + 1] >= tunegreen && _textureByteArray[bytePoserr + 2] <= 0.01f)
                                                                                                                {

                                                                                                                    if (otherbytecolor + 0 < currentzoomw * currentzoomh * 4 &&
                                                                                                                        otherbytecolor + 1 < currentzoomw * currentzoomh * 4 &&
                                                                                                                        otherbytecolor + 2 < currentzoomw * currentzoomh * 4)
                                                                                                                    {
                                                                                                                        _textureByteArrayobcpuVEVD[otherbytecolor + 0] = _textureByteArray[bytePoserr + 0];
                                                                                                                        _textureByteArrayobcpuVEVD[otherbytecolor + 1] = _textureByteArray[bytePoserr + 1];
                                                                                                                        _textureByteArrayobcpuVEVD[otherbytecolor + 2] = _textureByteArray[bytePoserr + 2];

                                                                                                                        _textureByteArrayobcpuVEVD[otherbytecolor + 3] = 255;

                                                                                                                    }


                                                                                                                }
                                                                                                                else
                                                                                                                {


                                                                                                                }
                                                                                                            }
                                                                                                            xii++;
                                                                                                        }
                                                                                                        yii++;
                                                                                                    }
                                                                                                }

                                                                                                countblacktexturecounterx = 0;
                                                                                                countblacktexturecountery = 0;

                                                                                                countblacktexturecounterswtc = 1;
                                                                                                //break;
                                                                                            }
                                                                                        }
                                                                                        countblacktexturecounterx++;

                                                                                        if (yyyy != countblacktexturecountery)
                                                                                        {
                                                                                            countblacktexturecountery++;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {

                                                                                        countblacktexturecounterx = 0;
                                                                                        countblacktexturecountery = 0;
                                                                                    }
                                                                                    posY++;
                                                                                }
                                                                            }
                                                                        }



                                                                        
                                                                        percentrecognitiondataVEVD[0].zoomtypevevd = zoomtype;

                                                                        //SAVE PART OF THE SCREEN WHERE THE VOID EXPANSE VIRTUAL SCREEN IS
                                                                        //SAVE PART OF THE SCREEN WHERE THE VOID EXPANSE VIRTUAL SCREEN IS
                                                                        //SAVE PART OF THE SCREEN WHERE THE VOID EXPANSE VIRTUAL SCREEN IS
                                                                        //string thepath = Environment.CurrentDirectory + @"\assets\sccsvevirtualscreenimages\" + "screenzoom" + zoomtype + ".jpg";

                                                                        percentrecognitiondataVEVD[0].percentrecogfloat = 0;

                                                                        if (classifythreadswtcVEVD == 0)
                                                                        {
                                                                            
                                                                            classifythreadVEVD = new Thread((tester0000) =>
                                                                            {

                                                                               

                                                                            threadclassifyloopVEVD:

                                                                                if (perframetheimagerecogcounterVEVD >= perframetheimagerecogcountermaxVEVD && percentrecognitiondataVEVD[0].zoomtypevevdswtc == 1)
                                                                                {
                                                                                    string thepath = Environment.CurrentDirectory + @"\assets\sccsvevirtualscreenimages\" + "screenzoom" + percentrecognitiondataVEVD[0].zoomtypevevd + ".jpg";

                                                                                    //sccs.Program.MessageBox((IntPtr)0, "Form1", "scmsg", 0);
                                                                                    //machinelearning image recognition
                                                                                    var percentfloat = ClassifySingleImageVEVD(mlContextForVEVD, modelForVEVD, thepath);
                                                                                    //machinelearning image recognition
                                                                                    percentrecognitiondataVEVD[0].percentrecogfloat = percentfloat;

                                                                                    var image1 = new System.Drawing.Bitmap(SAVE_RES_X, SAVE_RES_Y, SAVE_RES_X * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArrayobcpuVEVD, 0));

                                                                                    //https://stackoverflow.com/questions/3115076/adjust-the-contrast-of-an-image-in-c-sharp-efficiently
                                                                                    //image1 = AdjustContrast(image1,100.0f);

                                                                                    image1.Save(thepath);// Environment.CurrentDirectory + @"\assets\images\" + "currentscreencapture" + ".png");

                                                                                    //string thepath = @"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png";

                                                                                    //bitmapcounterVEVD++;

                                                                                    FileInfo infofile = new FileInfo(thepath);

                                                                                    infofile.Refresh();




                                                                                    /*
                                                                                    if (lastbitmap1 != null)
                                                                                    {
                                                                                        lastbitmap1.Dispose();
                                                                                    }

                                                                                    lastbitmap1 = image1;*/

                                                                                    image1.Dispose();



                                                                                    Thread.Sleep(1);
                                                                                    perframetheimagerecogcounterVEVD = 0;
                                                                                }
                                                                                else
                                                                                {
                                                                                    //percentrecognitiondataVEVD[0].percentrecogfloat = 0;
                                                                                    Thread.Sleep(10);
                                                                                }

                                                                                // <SnippetCallClassifySingleImage>
                                                                                // </SnippetCallClassifySingleImage>
                                                                                perframetheimagerecogcounterVEVD++;
                                                                                //MessageBox((IntPtr)0, "threadm0", "scmsg", 0);
                                                                                goto threadclassifyloopVEVD;
                                                                                //MessageBox((IntPtr)0, "threadm -1", "scmsg", 0);
                                                                                //_thread_start:
                                                                            }, 0); //100000 //999999999

                                                                            classifythreadVEVD.IsBackground = true;
                                                                            classifythreadVEVD.Priority = ThreadPriority.Normal; //AboveNormal
                                                                            classifythreadVEVD.SetApartmentState(ApartmentState.MTA);
                                                                            classifythreadVEVD.Start();

                                                                            classifythreadswtcVEVD = 1;
                                                                        }


                                                                        //Console.WriteLine("percentVEVD:" + percentrecognitiondataVEVD[0].percentrecogfloat);
                                                                        perframetheimagerecogcounterswtcVEVD = 0;
                                                                        if (percentrecognitiondataVEVD[0].percentrecogfloat < 0.95f)
                                                                        {

                                                                        }
                                                                        else
                                                                        {
                                                                            perframetheimagerecogcounterswtcVEVD = 1;




                                                                        }

                                                                        //SAVE PART OF THE SCREEN WHERE THE VOID EXPANSE VIRTUAL SCREEN IS
                                                                        //SAVE PART OF THE SCREEN WHERE THE VOID EXPANSE VIRTUAL SCREEN IS
                                                                        //SAVE PART OF THE SCREEN WHERE THE VOID EXPANSE VIRTUAL SCREEN IS
























                                                                        /*
                                                                        currentzoomw = laststorecountgreenpixelsx * 2;
                                                                        currentzoomh = 77 * 2;*/

                                                                        currentzoomw = storecountgreenpixelsx;
                                                                        currentzoomh = SAVE_RES_Y;

                                                                        countgreenpixelsx = 0;
                                                                        countgreenpixelsy = 0;

                                                                        countblacktexturecounterswtc = 0;
                                                                        countblacktexturecounterx = 0;
                                                                        countblacktexturecountery = 0;


                                                                        bytePoser = 0;
                                                                        //fixed (byte* textureByteArray = _textureByteArray) //, totalArray = _totalArray
                                                                        {
                                                                            int posY = 0;
                                                                            for (int yyyy = 0; yyyy < 1080; yyyy++)
                                                                            {
                                                                                for (int xxxx = 0; xxxx < 1920; xxxx++)
                                                                                {
                                                                                    bytePoser = ((yyyy * 1920) + xxxx) * 4;

                                                                                    if (_textureByteArray[bytePoser + 0] <= 0.01f && _textureByteArray[bytePoser + 1] > tunegreen && _textureByteArray[bytePoser + 2] <= 0.01f)
                                                                                    {
                                                                                        if (countblacktexturecounterswtc == 0)
                                                                                        {
                                                                                            if (countblacktexturecounterx >= pixelcheckx)// && countblacktexturecountery >= 60)
                                                                                            {

                                                                                                int onboardcpuiconmodw = currentzoomw;// 137.1428571428571f;
                                                                                                int onboardcpuiconmodh = currentzoomh;// 77.14285714285714f; //73.125f

                                                                                                int yyvirtualdesktopscreen = yyyy - 10;// (1080 - 50) - 5 + 1 + 1 + 5 - 1 - 1;
                                                                                                int xxvirtualdesktopscreen = xxxx - pixelcheckx;// (1920 - 1050) + 15 + 5 + 3 + 3 - 1 - 1;

                                                                                                int heightofvirtualdesktopscreen = onboardcpuiconmodh;
                                                                                                int widthofvirtualdesktopscreen = onboardcpuiconmodw;

                                                                                                int yii = 0;
                                                                                                int xii = 0;

                                                                                                if (storecountgreenpixelsxatmaxy <= 0)
                                                                                                {
                                                                                                    storecountgreenpixelsxatmaxy = 1;
                                                                                                }

                                                                                                var numberofxpery = (int)Math.Ceiling((float)(Math.Ceiling((float)storecountgreenpixelsnumberofloopsy / (float)storecountgreenpixelsxatmaxy) / 2.0f)); // DIVIDE
                                                                                                //var numberofxpery = (storecountgreenpixelsnumberofloopsy / storecountgreenpixelsxatmaxy) / 2; // DIVIDE

                                                                                                /*if (storecountgreenpixelsxatmaxy == 1)
                                                                                                {   
                                                                                                    numberofxpery = 0;// storecountgreenpixelsnumberofloopsy = 0;
                                                                                                }*/

                                                                                                if (numberofxpery >= 2)
                                                                                                {
                                                                                                    numberofxpery = 1;
                                                                                                }

                                                                                                //var numberofxpery = 1; // (1) * 4;

                                                                                                Console.WriteLine("numberofxpery:" + numberofxpery);

                                                                                                //int thecounter = 0;
                                                                                                int thecountermax = 3;

                                                                                                int theindexstart = xxvirtualdesktopscreen;

                                                                                                var theotherxi = 0;// (xxvirtualdesktopscreen + xitwo);

                                                                                                if (yyvirtualdesktopscreen >= 0 && xxvirtualdesktopscreen >= 0)
                                                                                                {
                                                                                                    for (int yi = yyvirtualdesktopscreen, thecounter = 0, yitwo = 0; yi < yyvirtualdesktopscreen + heightofvirtualdesktopscreen; yi++,yitwo++, thecounter++)
                                                                                                    {
                                                                                                        theotherxi = 0;

                                                                                                        if (thecounter >= thecountermax)
                                                                                                        {
                                                                                                            theindexstart += numberofxpery;

                                                                                                            thecounter = 0;
                                                                                                        }
                                                                                                        




                                                                                                        for (int xi = theindexstart, xitwo = 0; xi < (theindexstart) + widthofvirtualdesktopscreen; xi++, xitwo++)
                                                                                                        //for (int xi = xxvirtualdesktopscreen + (numberofxpery * xii); xi < (xxvirtualdesktopscreen + (numberofxpery * xii)) + widthofvirtualdesktopscreen; xi++)
                                                                                                        {

                                                                                                            /*
                                                                                                            if (xitwo == 0)
                                                                                                            {
                                                                                                                theotherxi = (theindexstart + xitwo); //numberofxpery
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                theotherxi = theotherxi + xitwo;
                                                                                                            }
                                                                                                            */


                                                                                                            /*if (xitwo == 0)
                                                                                                            {
                                                                                                                theotherxi = (xxvirtualdesktopscreen + xitwo) + (1 * yitwo); //numberofxpery
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                theotherxi = theotherxi + xitwo ;   
                                                                                                            }*/


                                                                                                            //theotherxi = ((xxvirtualdesktopscreen) + xitwo);


                                                                                                            //var theotherxi = (xxvirtualdesktopscreen + xii) + (numberofxpery * xii);



                                                                                                            var otherbytecolor = ((yii * onboardcpuiconmodw) + xii) * 4;

                                                                                                            var bytePoserr = ((yi * 1920) + (xi)) * 4;


                                                                                                            if (xi < 1920)
                                                                                                            {

                                                                                                                //if (bytePoserr + 0 < _textureByteArray.Length - 1 && bytePoserr + 1 < _textureByteArray.Length - 1 && bytePoserr + 2 < _textureByteArray.Length - 1)
                                                                                                                if (bytePoserr + 0 < (1920 * 1080 * 4) - 1 && bytePoserr + 1 < (1920 * 1080 * 4) - 1 && bytePoserr + 2 < (1920 * 1080 * 4) - 1)
                                                                                                                {
                                                                                                                    if (_textureByteArray[bytePoserr + 0] <= 0.01f && _textureByteArray[bytePoserr + 1] >= tunegreen && _textureByteArray[bytePoserr + 2] <= 0.01f)
                                                                                                                    {
                                                                                                                        if (rgbvaluesbytesarray != null)
                                                                                                                        {
                                                                                                                            if (rgbvaluesbytesarray.Length > 0)
                                                                                                                            {

                                                                                                                                if (otherbytecolor + 0 < rgbvaluesbytesarray.Length - 1 && //onboardcpuiconmodw * onboardcpuiconmodh * 4 &&
                                                                                                                                    otherbytecolor + 1 < rgbvaluesbytesarray.Length - 1 && // onboardcpuiconmodw * onboardcpuiconmodh * 4 &&
                                                                                                                                    otherbytecolor + 2 < rgbvaluesbytesarray.Length - 1 // onboardcpuiconmodw * onboardcpuiconmodh * 4)
                                                                                                                                    )
                                                                                                                                {
                                                                                                                                    if (bytePoserr + 0 < _textureByteArray.Length - 1 && bytePoserr + 1 < _textureByteArray.Length - 1 && bytePoserr + 2 < _textureByteArray.Length - 1)
                                                                                                                                    {

                                                                                                                                        /*var valx = rgbvaluesbytesarray[otherbytecolor + 0];
                                                                                                                                        var valy = rgbvaluesbytesarray[otherbytecolor + 1];
                                                                                                                                        var valz = rgbvaluesbytesarray[otherbytecolor + 2];*/

                                                                                                                                        /*var valx = _textureByteArray[bytePoserr + 0];
                                                                                                                                        var valy = _textureByteArray[bytePoserr + 1];
                                                                                                                                        var valz = _textureByteArray[bytePoserr + 2];*/

                                                                                                                                        _textureByteArray[bytePoserr + 0] = rgbvaluesbytesarray[otherbytecolor + 0];//r
                                                                                                                                        _textureByteArray[bytePoserr + 1] = rgbvaluesbytesarray[otherbytecolor + 1];//0;//g
                                                                                                                                        _textureByteArray[bytePoserr + 2] = rgbvaluesbytesarray[otherbytecolor + 2];//240;//b

                                                                                                                                    }

                                                                                                                                    // posY++;
                                                                                                                                    /*_textureByteArray[bytePoserr + 0] = rgbvaluesbytesarray[otherbytecolor + 0];//r
                                                                                                                                    _textureByteArray[bytePoserr + 1] = rgbvaluesbytesarray[otherbytecolor + 1];//0;//g
                                                                                                                                    _textureByteArray[bytePoserr + 2] = rgbvaluesbytesarray[otherbytecolor + 2];//240;//b
                                                                                                                                    //_textureByteArray[bytePoserr + 3] = 255;*/
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {


                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                break;
                                                                                                            }
                                                                                                            xii++;
                                                                                                            //xxvirtualdesktopscreen += 1;
                                                                                                        }

                                                                                                        yii++;
                                                                                                    }
                                                                                                }

                                                                                                countblacktexturecounterx = 0;
                                                                                                countblacktexturecountery = 0;

                                                                                                countblacktexturecounterswtc = 1;
                                                                                                //break;
                                                                                            }
                                                                                        }
                                                                                        countblacktexturecounterx++;

                                                                                        if (yyyy != countblacktexturecountery)
                                                                                        {
                                                                                            countblacktexturecountery++;
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {

                                                                                        countblacktexturecounterx = 0;
                                                                                        countblacktexturecountery = 0;
                                                                                    }
                                                                                    posY++;
                                                                                }
                                                                            }
                                                                        }































                                                                        /*
                                                                        if (lastnewbitmap != null)
                                                                        {
                                                                            lastnewbitmap.Dispose();
                                                                        }
                                                                        lastnewbitmap = newBmp;
                                                                        */

                                                                        newBmp.Dispose();

                                                                        laststorecountgreenpixelsx = storecountgreenpixelsx;
                                                                        laststorecountgreenpixelsy = storecountgreenpixelsy;


                                                                    }
                                                                    else
                                                                    {



                                                                        if (laststorecountgreenpixelsx > 0)
                                                                        {
                                                                            //percentrecognitiondataVEVD[0].zoomtypevevdswtc = 1;
                                                                            //Console.WriteLine("laststorecountgreenpixelsx:" + laststorecountgreenpixelsx);

                                                                            SAVE_RES_X = laststorecountgreenpixelsx;

                                                                            float theratio = 1920 / laststorecountgreenpixelsx;

                                                                            SAVE_RES_Y = ((SAVE_RES_X * 1080) / 1920);// (int)(1080 / (1080 / theratio)) / 2;

                                                                            //https://social.msdn.microsoft.com/Forums/sqlserver/en-US/8be9616f-0549-4836-829c-148cad31fc0d/c-resize-image-while-keeping-original-bit-depth?forum=winforms
                                                                            //Image oldImage = Image.FromFile(fileName);

                                                                            //var image = new System.Drawing.Bitmap(columns, rows, memoryBitmapStride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0));

                                                                            newBmp = new System.Drawing.Bitmap(SAVE_RES_X, SAVE_RES_Y, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                                                                            var boundsRect = new System.Drawing.Rectangle(0, 0, newBmp.Width, newBmp.Height);
                                                                            var bmpDataVEVD = newBmp.LockBits(boundsRect, ImageLockMode.ReadOnly, newBmp.PixelFormat);
                                                                            var _bytesTotalobcpuVEVD = Math.Abs(bmpDataVEVD.Stride) * newBmp.Height;
                                                                            newBmp.UnlockBits(bmpDataVEVD);
                                                                            var _textureByteArrayobcpuVEVD = new byte[_bytesTotalobcpuVEVD];



                                                                            
                                                                            

                                                                            using (Graphics g = Graphics.FromImage(newBmp))
                                                                            {
                                                                                g.DrawImage(somebitmap, 0, 0, SAVE_RES_X, SAVE_RES_Y);
                                                                            }

                                                                            //somebitmap.Save(@"C:\Users\steve\Desktop\screenRecord\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png"); //Save(".\\ResizedImages\\oldImage.tif");
                                                                            //newBmp.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png"); //Save(".\\ResizedImages\\newImage.tif");

                                                                            byte[] rgbvaluesbytesarray = GetRGBValues(newBmp);

                                                                            int zoomtype = 0;

                                                                            if (laststorecountgreenpixelsx >= 113 && laststorecountgreenpixelsx <= 118)
                                                                            {
                                                                                zoomtype = 1;
                                                                            }
                                                                            else if (laststorecountgreenpixelsx >= 126 && laststorecountgreenpixelsx <= 133)
                                                                            {
                                                                                zoomtype = 2;
                                                                            }
                                                                            else if (laststorecountgreenpixelsx >= 144 && laststorecountgreenpixelsx <= 150)
                                                                            {
                                                                                zoomtype = 3;
                                                                            }
                                                                            else if (laststorecountgreenpixelsx >= 166 && laststorecountgreenpixelsx <= 174)
                                                                            {
                                                                                zoomtype = 4;
                                                                            }
                                                                            else if (laststorecountgreenpixelsx >= 197 && laststorecountgreenpixelsx <= 205)
                                                                            {
                                                                                zoomtype = 5;
                                                                            }
                                                                            else if (laststorecountgreenpixelsx >= 248 && laststorecountgreenpixelsx <= 257)
                                                                            {
                                                                                zoomtype = 6;
                                                                            }
                                                                            else if (laststorecountgreenpixelsx >= 266 && laststorecountgreenpixelsx <= 275)
                                                                            {
                                                                                zoomtype = 7;
                                                                            }
                                                                            
                                                                            








                                                                            //Console.WriteLine("zoomtype:" + zoomtype);








                                                                            //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE
                                                                            //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE
                                                                            /*bitmaponboardcomputer = new System.Drawing.Bitmap(256, 256, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                                                            var boundsRect = new System.Drawing.Rectangle(0, 0, 256, 256);
                                                                            bmpData = bitmaponboardcomputer.LockBits(boundsRect, ImageLockMode.ReadOnly, bitmaponboardcomputer.PixelFormat);
                                                                            _bytesTotalobcpu = Math.Abs(bmpData.Stride) * bitmaponboardcomputer.Height;
                                                                            bitmaponboardcomputer.UnlockBits(bmpData);
                                                                            _textureByteArrayobcpu = new byte[_bytesTotalobcpu];
                                                                            bmpData = null;*/
                                                                            //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE
                                                                            //CALCULATING THE NUMBER OF BYTES THAT ARE IN A NEW BITMAP OF THE SAME SIZE AS THE ONE LOADED FROM FILE




                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            /* for (int yyyy = yyonboardcomputer; yyyy < yyonboardcomputer + heightOfRectanglePlayerShip; yyyy++)
                                                                             {
                                                                                 for (int xxxx = xxonboardcomputer; xxxx < xxonboardcomputer + widthOfRectanglePlayerShip; xxxx++)
                                                                                 {
                                                                                     var otherbytecolor = ((ypic * onboardcpuiconmodw) + xpic) * 4;
                                                                                     int bytePoser0 = ((yyyy * 1920) + xxxx) * 4;

                                                                                     if (otherbytecolor < widthOfRectanglePlayerShip * heightOfRectanglePlayerShip * 8)
                                                                                     {
                                                                                         //if (onboardcomputeiconpixeldata[otherbytecolor + 0] == textureByteArray[bytePoser0 + 0] &&
                                                                                         //    onboardcomputeiconpixeldata[otherbytecolor + 1] == textureByteArray[bytePoser0 + 1] &&
                                                                                         //    onboardcomputeiconpixeldata[otherbytecolor + 2] == textureByteArray[bytePoser0 + 2])
                                                                                         //{
                                                                                         //    //Program.MessageBox((IntPtr)0, "samecolor", "sccsmsg", 0);
                                                                                         //    counterofsamecolors++;
                                                                                         //}

                                                                                         //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser0 + 1];
                                                                                         //thenewarray[otherbytecolor + 1] = textureByteArray[bytePoser0 + 0];
                                                                                         //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser0 + 2];

                                                                                         _textureByteArrayobcpu[otherbytecolor + 0] = _textureByteArray[bytePoser0 + 0];
                                                                                         _textureByteArrayobcpu[otherbytecolor + 1] = _textureByteArray[bytePoser0 + 1];
                                                                                         _textureByteArrayobcpu[otherbytecolor + 2] = _textureByteArray[bytePoser0 + 2];


                                                                                         _textureByteArrayobcpu[otherbytecolor + 3] = 255;
                                                                                     }
                                                                                     else
                                                                                     {
                                                                                         //Program.MessageBox((IntPtr)0, "out of range" + otherbytecolor, "sccsmsg", 0);
                                                                                     }


                                                                                     //thenewarray[otherbytecolor + 0] = textureByteArray[bytePoser0 + 0];
                                                                                     //thenewarray[otherbytecolor + 1] = textureBytsomebitmapeArray[bytePoser0 + 1];
                                                                                     //thenewarray[otherbytecolor + 2] = textureByteArray[bytePoser0 + 2];
                                                                                     //thenewarray[otherbytecolor + 3] = 255;


                                                                                     xpic++;
                                                                                 }
                                                                                 ypic++;
                                                                             }*/
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE
                                                                            //GRAB PART OF THE SCREEN WHERE THE ONBOARD COMPUTER ICON IS IN VOID EXPANSE

                                                                            
                                                                            currentzoomw = laststorecountgreenpixelsx;
                                                                            currentzoomh = SAVE_RES_Y;

                                                                            countgreenpixelsx = 0;
                                                                            countgreenpixelsy = 0;

                                                                            countblacktexturecounterswtc = 0;
                                                                            countblacktexturecounterx = 0;
                                                                            countblacktexturecountery = 0;


                                                                            bytePoser = 0;
                                                                            //fixed (byte* textureByteArray = _textureByteArray) //, totalArray = _totalArray
                                                                            {
                                                                                int posY = 0;
                                                                                for (int yyyy = 0; yyyy < 1080; yyyy++)
                                                                                {
                                                                                    for (int xxxx = 0; xxxx < 1920; xxxx++)
                                                                                    {
                                                                                        bytePoser = ((yyyy * 1920) + xxxx) * 4;

                                                                                        if (_textureByteArray[bytePoser + 0] <= 0.01f && _textureByteArray[bytePoser + 1] > tunegreen && _textureByteArray[bytePoser + 2] <= 0.01f)
                                                                                        {
                                                                                            if (countblacktexturecounterswtc == 0)
                                                                                            {
                                                                                                if (countblacktexturecounterx >= pixelcheckx)// && countblacktexturecountery >= 60)
                                                                                                {

                                                                                                    int onboardcpuiconmodw = currentzoomw;// 137.1428571428571f;
                                                                                                    int onboardcpuiconmodh = currentzoomh;// 77.14285714285714f; //73.125f

                                                                                                    int yyvirtualdesktopscreen = yyyy - 10;// (1080 - 50) - 5 + 1 + 1 + 5 - 1 - 1;
                                                                                                    int xxvirtualdesktopscreen = xxxx - pixelcheckx;// (1920 - 1050) + 15 + 5 + 3 + 3 - 1 - 1;

                                                                                                    int heightofvirtualdesktopscreen = onboardcpuiconmodh;
                                                                                                    int widthofvirtualdesktopscreen = onboardcpuiconmodw;

                                                                                                    int yii = 0;
                                                                                                    int xii = 0;

                                                                                                    if (yyvirtualdesktopscreen >= 0 && xxvirtualdesktopscreen >= 0)
                                                                                                    {
                                                                                                        for (int yi = yyvirtualdesktopscreen; yi < yyvirtualdesktopscreen + heightofvirtualdesktopscreen; yi++)
                                                                                                        {
                                                                                                            for (int xi = xxvirtualdesktopscreen; xi < xxvirtualdesktopscreen + widthofvirtualdesktopscreen; xi++)
                                                                                                            {
                                                                                                                var otherbytecolor = ((yii * onboardcpuiconmodw) + xii) * 4;

                                                                                                                var bytePoserr = ((yi * 1920) + xi) * 4;

                                                                                                                if (bytePoserr + 0 < 1920 * 1080 * 4 && bytePoserr + 1 < 1920 * 1080 * 4 && bytePoserr + 2 < 1920 * 1080 * 4)
                                                                                                                {
                                                                                                                    if (_textureByteArray[bytePoserr + 0] <= 0.01f && _textureByteArray[bytePoserr + 1] >= tunegreen && _textureByteArray[bytePoserr + 2] <= 0.01f)
                                                                                                                    {

                                                                                                                        if (otherbytecolor + 0 < currentzoomw * currentzoomh * 4 &&
                                                                                                                            otherbytecolor + 1 < currentzoomw * currentzoomh * 4 &&
                                                                                                                            otherbytecolor + 2 < currentzoomw * currentzoomh * 4)
                                                                                                                        {
                                                                                                                            _textureByteArrayobcpuVEVD[otherbytecolor + 0] = _textureByteArray[bytePoserr + 0];
                                                                                                                            _textureByteArrayobcpuVEVD[otherbytecolor + 1] = _textureByteArray[bytePoserr + 1];
                                                                                                                            _textureByteArrayobcpuVEVD[otherbytecolor + 2] = _textureByteArray[bytePoserr + 2];

                                                                                                                            _textureByteArrayobcpuVEVD[otherbytecolor + 3] = 255;

                                                                                                                        }


                                                                                                                    }
                                                                                                                    else
                                                                                                                    {


                                                                                                                    }
                                                                                                                }
                                                                                                                xii++;
                                                                                                            }
                                                                                                            yii++;
                                                                                                        }
                                                                                                    }

                                                                                                    countblacktexturecounterx = 0;
                                                                                                    countblacktexturecountery = 0;

                                                                                                    countblacktexturecounterswtc = 1;
                                                                                                    //break;
                                                                                                }
                                                                                            }
                                                                                            countblacktexturecounterx++;

                                                                                            if (yyyy != countblacktexturecountery)
                                                                                            {
                                                                                                countblacktexturecountery++;
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {

                                                                                            countblacktexturecounterx = 0;
                                                                                            countblacktexturecountery = 0;
                                                                                        }
                                                                                        posY++;
                                                                                    }
                                                                                }
                                                                            }






























                                                                            //currentzoomw = laststorecountgreenpixelsx * 2;
                                                                            //currentzoomh = 77 * 2;

                                                                            currentzoomw = laststorecountgreenpixelsx;
                                                                            currentzoomh = SAVE_RES_Y;

                                                                            countgreenpixelsx = 0;
                                                                            countgreenpixelsy = 0;

                                                                            countblacktexturecounterswtc = 0;
                                                                            countblacktexturecounterx = 0;
                                                                            countblacktexturecountery = 0;


                                                                            bytePoser = 0;
                                                                            //fixed (byte* textureByteArray = _textureByteArray) //, totalArray = _totalArray
                                                                            {
                                                                                int posY = 0;
                                                                                for (int yyyy = 0; yyyy < 1080; yyyy++)
                                                                                {
                                                                                    for (int xxxx = 0; xxxx < 1920; xxxx++)
                                                                                    {
                                                                                        bytePoser = ((yyyy * 1920) + xxxx) * 4;

                                                                                        if (_textureByteArray[bytePoser + 0] <= 0.01f && _textureByteArray[bytePoser + 1] > tunegreen && _textureByteArray[bytePoser + 2] <= 0.01f)
                                                                                        {
                                                                                            if (countblacktexturecounterswtc == 0)
                                                                                            {
                                                                                                if (countblacktexturecounterx >= pixelcheckx)// && countblacktexturecountery >= 60)
                                                                                                {

                                                                                                    int onboardcpuiconmodw = currentzoomw;// 137.1428571428571f;
                                                                                                    int onboardcpuiconmodh = currentzoomh;// 77.14285714285714f; //73.125f

                                                                                                    int yyvirtualdesktopscreen = yyyy - 10;// (1080 - 50) - 5 + 1 + 1 + 5 - 1 - 1;
                                                                                                    int xxvirtualdesktopscreen = xxxx - pixelcheckx;// (1920 - 1050) + 15 + 5 + 3 + 3 - 1 - 1;

                                                                                                    int heightofvirtualdesktopscreen = onboardcpuiconmodh;
                                                                                                    int widthofvirtualdesktopscreen = onboardcpuiconmodw;

                                                                                                    int yii = 0;
                                                                                                    int xii = 0;

                                                                                                    if (storecountgreenpixelsxatmaxy <= 0)
                                                                                                    {
                                                                                                        storecountgreenpixelsxatmaxy = 1;
                                                                                                    }

                                                                                                    var numberofxpery = (int)Math.Ceiling((float)(Math.Ceiling((float)storecountgreenpixelsnumberofloopsy / (float)storecountgreenpixelsxatmaxy) / 2.0f)); // DIVIDE
                                                                                                                                                                                                                                                           //var numberofxpery = (storecountgreenpixelsnumberofloopsy / storecountgreenpixelsxatmaxy) / 2; // DIVIDE

                                                                                                    /*if (storecountgreenpixelsxatmaxy == 1)
                                                                                                    {   
                                                                                                        numberofxpery = 0;// storecountgreenpixelsnumberofloopsy = 0;
                                                                                                    }*/

                                                                                                    if (numberofxpery >= 2)
                                                                                                    {
                                                                                                        numberofxpery = 1;
                                                                                                    }

                                                                                                    //var numberofxpery = 1; // (1) * 4;

                                                                                                    Console.WriteLine("numberofxpery:" + numberofxpery);

                                                                                                    //int thecounter = 0;
                                                                                                    int thecountermax = 3;

                                                                                                    int theindexstart = xxvirtualdesktopscreen;

                                                                                                    var theotherxi = 0;// (xxvirtualdesktopscreen + xitwo);

                                                                                                    if (yyvirtualdesktopscreen >= 0 && xxvirtualdesktopscreen >= 0)
                                                                                                    {
                                                                                                        for (int yi = yyvirtualdesktopscreen, thecounter = 0, yitwo = 0; yi < yyvirtualdesktopscreen + heightofvirtualdesktopscreen; yi++, yitwo++, thecounter++)
                                                                                                        {
                                                                                                            theotherxi = 0;

                                                                                                            if (thecounter >= thecountermax)
                                                                                                            {
                                                                                                                theindexstart += numberofxpery;

                                                                                                                thecounter = 0;
                                                                                                            }





                                                                                                            for (int xi = theindexstart, xitwo = 0; xi < (theindexstart) + widthofvirtualdesktopscreen; xi++, xitwo++)
                                                                                                            //for (int xi = xxvirtualdesktopscreen + (numberofxpery * xii); xi < (xxvirtualdesktopscreen + (numberofxpery * xii)) + widthofvirtualdesktopscreen; xi++)
                                                                                                            {

                                                                                                                /*
                                                                                                                if (xitwo == 0)
                                                                                                                {
                                                                                                                    theotherxi = (theindexstart + xitwo); //numberofxpery
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    theotherxi = theotherxi + xitwo;
                                                                                                                }
                                                                                                                */


                                                                                                                /*if (xitwo == 0)
                                                                                                                {
                                                                                                                    theotherxi = (xxvirtualdesktopscreen + xitwo) + (1 * yitwo); //numberofxpery
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    theotherxi = theotherxi + xitwo ;   
                                                                                                                }*/


                                                                                                                //theotherxi = ((xxvirtualdesktopscreen) + xitwo);


                                                                                                                //var theotherxi = (xxvirtualdesktopscreen + xii) + (numberofxpery * xii);



                                                                                                                var otherbytecolor = ((yii * onboardcpuiconmodw) + xii) * 4;

                                                                                                                var bytePoserr = ((yi * 1920) + (xi)) * 4;


                                                                                                                if (xi < 1920)
                                                                                                                {

                                                                                                                    //if (bytePoserr + 0 < _textureByteArray.Length - 1 && bytePoserr + 1 < _textureByteArray.Length - 1 && bytePoserr + 2 < _textureByteArray.Length - 1)
                                                                                                                    if (bytePoserr + 0 < (1920 * 1080 * 4) - 1 && bytePoserr + 1 < (1920 * 1080 * 4) - 1 && bytePoserr + 2 < (1920 * 1080 * 4) - 1)
                                                                                                                    {
                                                                                                                        if (_textureByteArray[bytePoserr + 0] <= 0.01f && _textureByteArray[bytePoserr + 1] >= tunegreen && _textureByteArray[bytePoserr + 2] <= 0.01f)
                                                                                                                        {
                                                                                                                            if (rgbvaluesbytesarray != null)
                                                                                                                            {
                                                                                                                                if (rgbvaluesbytesarray.Length > 0)
                                                                                                                                {

                                                                                                                                    if (otherbytecolor + 0 < rgbvaluesbytesarray.Length - 1 && //onboardcpuiconmodw * onboardcpuiconmodh * 4 &&
                                                                                                                                        otherbytecolor + 1 < rgbvaluesbytesarray.Length - 1 && // onboardcpuiconmodw * onboardcpuiconmodh * 4 &&
                                                                                                                                        otherbytecolor + 2 < rgbvaluesbytesarray.Length - 1 // onboardcpuiconmodw * onboardcpuiconmodh * 4)
                                                                                                                                        )
                                                                                                                                    {
                                                                                                                                        if (bytePoserr + 0 < _textureByteArray.Length - 1 && bytePoserr + 1 < _textureByteArray.Length - 1 && bytePoserr + 2 < _textureByteArray.Length - 1)
                                                                                                                                        {

                                                                                                                                            /*var valx = rgbvaluesbytesarray[otherbytecolor + 0];
                                                                                                                                            var valy = rgbvaluesbytesarray[otherbytecolor + 1];
                                                                                                                                            var valz = rgbvaluesbytesarray[otherbytecolor + 2];*/

                                                                                                                                            /*var valx = _textureByteArray[bytePoserr + 0];
                                                                                                                                            var valy = _textureByteArray[bytePoserr + 1];
                                                                                                                                            var valz = _textureByteArray[bytePoserr + 2];*/

                                                                                                                                            _textureByteArray[bytePoserr + 0] = rgbvaluesbytesarray[otherbytecolor + 0];//r
                                                                                                                                            _textureByteArray[bytePoserr + 1] = rgbvaluesbytesarray[otherbytecolor + 1];//0;//g
                                                                                                                                            _textureByteArray[bytePoserr + 2] = rgbvaluesbytesarray[otherbytecolor + 2];//240;//b

                                                                                                                                        }

                                                                                                                                        // posY++;
                                                                                                                                        /*_textureByteArray[bytePoserr + 0] = rgbvaluesbytesarray[otherbytecolor + 0];//r
                                                                                                                                        _textureByteArray[bytePoserr + 1] = rgbvaluesbytesarray[otherbytecolor + 1];//0;//g
                                                                                                                                        _textureByteArray[bytePoserr + 2] = rgbvaluesbytesarray[otherbytecolor + 2];//240;//b
                                                                                                                                        //_textureByteArray[bytePoserr + 3] = 255;*/
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {


                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    break;
                                                                                                                }
                                                                                                                xii++;
                                                                                                                //xxvirtualdesktopscreen += 1;
                                                                                                            }

                                                                                                            yii++;
                                                                                                        }
                                                                                                    }

                                                                                                    countblacktexturecounterx = 0;
                                                                                                    countblacktexturecountery = 0;

                                                                                                    countblacktexturecounterswtc = 1;
                                                                                                    //break;
                                                                                                }
                                                                                            }
                                                                                            countblacktexturecounterx++;

                                                                                            if (yyyy != countblacktexturecountery)
                                                                                            {
                                                                                                countblacktexturecountery++;
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {

                                                                                            countblacktexturecounterx = 0;
                                                                                            countblacktexturecountery = 0;
                                                                                        }
                                                                                        posY++;
                                                                                    }
                                                                                }
                                                                            }




                                                                            /*if (lastnewbitmap != null)
                                                                            {
                                                                                lastnewbitmap.Dispose();
                                                                            }
                                                                            lastnewbitmap = newBmp;*/

                                                                            newBmp.Dispose();




                                                                        }
                                                                        else
                                                                        {
                                                                            //Console.WriteLine("here:" + laststorecountgreenpixelsx);
                                                                        }
                                                                    }



                                                                


                                                                    /*
                                                                    if (lastsomebitmap!= null)
                                                                    {
                                                                        lastsomebitmap.Dispose();
                                                                    }

                                                                    lastsomebitmap = somebitmap;*/
                                                                    
                                                                    somebitmap.Dispose();








































                                                                    //MY ORIGINAL THAT WORKS BUT NO ZOOMING IN VOID EXPANSE.
                                                                    //MY ORIGINAL THAT WORKS BUT NO ZOOMING IN VOID EXPANSE.
                                                                    /*
                                                                    int SAVE_RES_X = 1920 / 14;
                                                                    int SAVE_RES_Y = 1080 / 14;


                                                                    //https://social.msdn.microsoft.com/Forums/sqlserver/en-US/8be9616f-0549-4836-829c-148cad31fc0d/c-resize-image-while-keeping-original-bit-depth?forum=winforms
                                                                    //Image oldImage = Image.FromFile(fileName);
                                                                    System.Drawing.Bitmap newBmp = new System.Drawing.Bitmap(SAVE_RES_X, SAVE_RES_Y, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                                                                    using (Graphics g = Graphics.FromImage(newBmp))
                                                                    {
                                                                        g.DrawImage(somebitmap, 0, 0, SAVE_RES_X, SAVE_RES_Y);
                                                                    }

                                                                    //somebitmap.Save(@"C:\Users\steve\Desktop\screenRecord\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png"); //Save(".\\ResizedImages\\oldImage.tif");
                                                                    //newBmp.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png"); //Save(".\\ResizedImages\\newImage.tif");

                                                                    byte[] rgbvaluesbytesarray = GetRGBValues(newBmp);

                                                                    //var boundsRectscreen = new System.Drawing.Rectangle(0, 0, newBmp.Width, newBmp.Height);
                                                                    //var bmpDatascreen = newBmp.LockBits(boundsRectscreen, ImageLockMode.ReadOnly, bitmaponboardcomputer.PixelFormat);

                                                                    //var image0 = new System.Drawing.Bitmap(newBmp.Width, newBmp.Height, bmpDatascreen.Stride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(rgbvaluesbytesarray, 0));
                                                                    //image0.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png");
                                                                    //bitmapcounter++;

                                                              

                                                                    int countblacktexturecounterswtc = 0;
                                                                    int countblacktexturecounterx = 0;
                                                                    int countblacktexturecountery = 0;

                                                                    
                                                                    int bytePoser = 0;

                                                                    //fixed (byte* textureByteArray = _textureByteArray) //, totalArray = _totalArray
                                                                    {
                                                                        int posY = 0;
                                                                        for (int yyyy = 0; yyyy < 1080; yyyy++)
                                                                        {
                                                                            for (int xxxx = 0; xxxx < 1920; xxxx++)
                                                                            {
                                                                                bytePoser = ((yyyy * 1920) + xxxx) * 4;

                                                                                if (_textureByteArray[bytePoser + 0] <= 0.01f && _textureByteArray[bytePoser + 1] > 0.95f && _textureByteArray[bytePoser + 2] <= 0.01f)
                                                                                {
                                                                                    if (countblacktexturecounterswtc == 0)
                                                                                    {
                                                                                        if (countblacktexturecounterx >= pixelcheckx)// && countblacktexturecountery >= 60)
                                                                                        {

                                                                                            int onboardcpuiconmodw = 137;// 137.1428571428571f;
                                                                                            int onboardcpuiconmodh = 77;// 77.14285714285714f; //73.125f

                                                                                            int yyvirtualdesktopscreen = yyyy-10;// (1080 - 50) - 5 + 1 + 1 + 5 - 1 - 1;
                                                                                            int xxvirtualdesktopscreen = xxxx-40;// (1920 - 1050) + 15 + 5 + 3 + 3 - 1 - 1;

                                                                                            int heightofvirtualdesktopscreen = onboardcpuiconmodh;
                                                                                            int widthofvirtualdesktopscreen = onboardcpuiconmodw;

                                                                                            int yii = 0;
                                                                                            int xii = 0;

                                                                                            if (yyvirtualdesktopscreen >= 0 && xxvirtualdesktopscreen >= 0)
                                                                                            {
                                                                                                for (int yi = yyvirtualdesktopscreen; yi < yyvirtualdesktopscreen + heightofvirtualdesktopscreen; yi++)
                                                                                                {
                                                                                                    for (int xi = xxvirtualdesktopscreen; xi < xxvirtualdesktopscreen + widthofvirtualdesktopscreen; xi++)
                                                                                                    {
                                                                                                        var otherbytecolor = ((yii * onboardcpuiconmodw) + xii) * 4;

                                                                                                        var bytePoserr = ((yi * 1920) + xi) * 4;

                                                                                                        if (bytePoserr + 0 < 1920 * 1080 * 4 && bytePoserr + 1 < 1920 * 1080 * 4 && bytePoserr + 2 < 1920 * 1080 * 4)
                                                                                                        {
                                                                                                            if (_textureByteArray[bytePoserr + 0] <= 0.01f && _textureByteArray[bytePoserr + 1] > 0.95f && _textureByteArray[bytePoserr + 2] <= 0.01f)
                                                                                                            {
                                                                                                                if (rgbvaluesbytesarray != null)
                                                                                                                {
                                                                                                                    if (rgbvaluesbytesarray.Length > 0)
                                                                                                                    {

                                                                                                                        if (otherbytecolor + 0 < onboardcpuiconmodw * onboardcpuiconmodh * 4 &&
                                                                                                                            otherbytecolor + 1 < onboardcpuiconmodw * onboardcpuiconmodh * 4 &&
                                                                                                                            otherbytecolor + 2 < onboardcpuiconmodw * onboardcpuiconmodh * 4)
                                                                                                                        {  
                                                                                                                            // posY++;
                                                                                                                            _textureByteArray[bytePoserr + 0] = rgbvaluesbytesarray[otherbytecolor + 0];//r
                                                                                                                            _textureByteArray[bytePoserr + 1] = rgbvaluesbytesarray[otherbytecolor + 1];//0;//g
                                                                                                                            _textureByteArray[bytePoserr + 2] = rgbvaluesbytesarray[otherbytecolor + 2];//240;//b

                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        xii++;
                                                                                                    }
                                                                                                    yii++;
                                                                                                }
                                                                                            }
                                                                                            
                                                                                            countblacktexturecounterx = 0;
                                                                                            countblacktexturecountery = 0;

                                                                                            countblacktexturecounterswtc = 1;
                                                                                            //break;
                                                                                        }
                                                                                    }
                                                                                    countblacktexturecounterx++;

                                                                                    if (yyyy != countblacktexturecountery)
                                                                                    {
                                                                                        countblacktexturecountery++;
                                                                                    }
                                                                                }
                                                                                else
                                                                                {

                                                                                    countblacktexturecounterx = 0;
                                                                                    countblacktexturecountery = 0;
                                                                                }
                                                                                posY++;
                                                                            }
                                                                        }
                                                                    }


                                                                    newBmp.Dispose();
                                                                    somebitmap.Dispose();
                                                                    */
                                                                    //MY ORIGINAL THAT WORKS BUT NO ZOOMING IN VOID EXPANSE.
                                                                    //MY ORIGINAL THAT WORKS BUT NO ZOOMING IN VOID EXPANSE.















































                                                                    /*if (_textureByteArray[bytePoser + 0] <= 15 && _textureByteArray[bytePoser + 1] <= 15 && _textureByteArray[bytePoser + 2] <= 15)
                                                                    {
                                                                        _textureByteArray[bytePoser + 0] = 0; //g
                                                                        _textureByteArray[bytePoser + 1] = 0; //b 
                                                                        _textureByteArray[bytePoser + 2] = 0; //r
                                                                        _textureByteArray[bytePoser + 3] = 0;
                                                                    }*/




                                                                    /*
                                                                    if (_textureByteArray[bytePoser + 0] >= 240 && _textureByteArray[bytePoser + 1] >= 240 && _textureByteArray[bytePoser + 2] >= 240)
                                                                    {
                                                                        _textureByteArray[bytePoser + 0] = 0; //g
                                                                        _textureByteArray[bytePoser + 1] = 0; //b 
                                                                        _textureByteArray[bytePoser + 2] = 0; //r
                                                                        _textureByteArray[bytePoser + 3] = 0;
                                                                    }
                                                                    */
                                                                    /*
                                                                    if (_textureByteArray[bytePoser + 0] < 25 && _textureByteArray[bytePoser + 1] >= 240 && _textureByteArray[bytePoser + 2] < 25)
                                                                    {
                                                                        _textureByteArray[bytePoser + 0] = 255;
                                                                        _textureByteArray[bytePoser + 1] = 0;
                                                                        _textureByteArray[bytePoser + 2] = 0;
                                                                        _textureByteArray[bytePoser + 3] = 1;
                                                                        countgreenscreenpixelsareweinanoptiontransition++;
                                                                    }*/


                                                                    /*
                                                                      if (_textureByteArray[bytePoser + 0] >= 125 && _textureByteArray[bytePoser + 1] < 125 && _textureByteArray[bytePoser + 2] >= 125)
                                                                      {
                                                                          _textureByteArray[bytePoser + 0] = 0; //g
                                                                          _textureByteArray[bytePoser + 1] = 0; //b 
                                                                          _textureByteArray[bytePoser + 2] = 0; //r
                                                                          _textureByteArray[bytePoser + 3] = 0;
                                                                      }*/

                                                                    /*
                                                                    if (_textureByteArray[bytePoser + 0] <= 15 && _textureByteArray[bytePoser + 1] <= 15 && _textureByteArray[bytePoser + 2] <= 15)
                                                                    {
                                                                        _textureByteArray[bytePoser + 0] = 0; //g
                                                                        _textureByteArray[bytePoser + 1] = 0; //b 
                                                                        _textureByteArray[bytePoser + 2] = 0; //r
                                                                        _textureByteArray[bytePoser + 3] = 0;
                                                                    }*/


                                                                    //BACKUP LARGE PINK/PURPLE/BLUE COLOR
                                                                    /*
                                                                    if (textureByteArray[bytePoser + 0] >= 125 && textureByteArray[bytePoser + 1] < 125 && textureByteArray[bytePoser + 2] >= 125)
                                                                    {
                                                                        textureByteArray[bytePoser + 0] = 0; //g
                                                                        textureByteArray[bytePoser + 1] = 0; //b 
                                                                        textureByteArray[bytePoser + 2] = 0; //r
                                                                        textureByteArray[bytePoser + 3] = 0;
                                                                    }*/
                                                                    //BACKUP LARGE PINK/PURPLE/BLUE COLOR



                                                                    /*
                                                                  if (textureByteArray[bytePoser + 0] < 100 && textureByteArray[bytePoser + 1] >= 100 && textureByteArray[bytePoser + 2] >= 100)
                                                                  {
                                                                      textureByteArray[bytePoser + 0] = 0; //g
                                                                      textureByteArray[bytePoser + 1] = 0; //b 
                                                                      textureByteArray[bytePoser + 2] = 0; //r
                                                                  }*/




                                                                    /*
                                                                    if (textureByteArray[bytePoser + 0] < 3 && textureByteArray[bytePoser + 1] < 3 && textureByteArray[bytePoser + 2] < 3)
                                                                    {
                                                                        textureByteArray[bytePoser + 0] = 0;
                                                                        textureByteArray[bytePoser + 1] = 255;
                                                                        textureByteArray[bytePoser + 2] = 0;
                                                                    }


                                                                    if (textureByteArray[bytePoser + 0] >= 235 && textureByteArray[bytePoser + 1] >= 235 && textureByteArray[bytePoser + 2] >= 235)
                                                                    {
                                                                        textureByteArray[bytePoser + 0] = 0;
                                                                        textureByteArray[bytePoser + 1] = 0;
                                                                        textureByteArray[bytePoser + 2] = 0;
                                                                    }*/


                                                                    //textureByteArray[bytePoser + 0] = 0; //b
                                                                    //textureByteArray[bytePoser + 1] = 255;   //g
                                                                    //textureByteArray[bytePoser + 2] = 0;   //r
                                                                    //textureByteArray[bytePoser + 3] = 255; //a


                                                                    /*
                                                                    if (countgreenscreenpixelsareweinanoptiontransition >= (1920*1080) * 0.5f)
                                                                    {
                                                                        Program.MessageBox((IntPtr)0, "option transition", "sccsmsg", 0);
                                                                    }*/



                                                                    if (bitmapcounter >= bitmapcountermax)
                                                                    {
                                                                        bitmapcounter = 0;
                                                                    }


                                                                    /*
                                                                    var image = new System.Drawing.Bitmap(columns, rows, memoryBitmapStride, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0));
                                                                    image.Save(@"C:\Users\steve\Desktop\screenRecord\" + bitmapcounter + "_" + rows.ToString("00") + columns.ToString("00") + ".png");
                                                                    bitmapcounter++;
                                                                    */











                                                                    //DeleteObject(vewindowsfoundedz);



                                                                    /*_bitmap1 = new System.Drawing.Bitmap(texture2d.Description.Width, texture2d.Description.Height, texture2d.Description.Width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, interptr1);
                                                                    _bitSource01 = CreateBitmapSource(_bitmap1, bmpData1);
                                                                    shaderResourceView = Ab3d.DirectX.Materials.WpfMaterial.CreateTexture2D(scupdate._dxDevice, _bitSource01);

                                                                    if (lastbitmap!= null)
                                                                    {

                                                                        lastbitmap.Dispose();
                                                                        lastbitmap = null;

                                                                    }
                                                                    lastbitmap = _bitmap;

                                                                    if (_lastbitSource01!= null)
                                                                    {
                                                                        _bitSource01 = null;
                                                                    }
                                                                    _lastbitSource01 = _bitSource01;*/

                                                                    
                                                                    shaderResourceView = Ab3d.DirectX.TextureLoader.CreateShaderResourceView(updatescript.device, _textureByteArray, texture2d.Description.Width, texture2d.Description.Height, bmpstride, Format.B8G8R8A8_UNorm, true);
                                                                    

                                                                    //shaderResourceView = new ShaderResourceView(updatescript.device, texture2d);

                                                                    if (lastshaderresourceview != null)
                                                                    {
                                                                        lastshaderresourceview.Dispose();
                                                                        lastshaderresourceview = null;
                                                                    }
                                                                    lastshaderresourceview = shaderResourceView;

                                                                }
                                                            }
                                                        }

                                                        //device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);
                                                        //device.ImmediateContext.Draw(4, 0);
                                                        //swapChain1.Present(1, PresentFlags.None, new PresentParameters());

                                                        if (sccsjittertasks != null)
                                                        {
                                                            if (sccsjittertasks[0] != null)
                                                            {
                                                                if (sccsjittertasks[0].Length > 0)
                                                                {
                                                                    sccsjittertasks[0][0].frameByteArray = _textureByteArray;
                                                                    sccsjittertasks[0][0].shaderresource = shaderResourceView;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }





                            if (exitedprogram != 1 && changedscreencapturetype == 0)
                            {
                                if (usejitterphysics == 0)
                                {
                                    // Clear the depth buffer.
                                    //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                    // Clear the back buffer.
                                    //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);

                                    sccsjittertasks = updatescript.Update(null, sccsjittertasks);
                                    //Thread.Sleep(1);

                                    //if (!updatescript.Update(null, sccsjittertasks))
                                    //{
                                    //    updatescript.scgraphicssecpackagemessage.scgraphicssec.Shutdown();
                                    //    updatescript.captureMethod.Dispose();
                                    //    updatescript.ShutDownGraphics();
                                    //    sccs.scgraphics.scdirectx.D3D.ShutDown();
                                    //}
                                    //sccsjittertasks = updatescript.Update(null, sccsjittertasks);
                                }
                                else if (usejitterphysics == 1)
                                {
                                    // Clear the depth buffer.
                                    //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                    // Clear the back buffer.
                                    //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);

                                    sccsjittertasks = updatescript.Update(jitter_sc, sccsjittertasks);
                                    //Thread.Sleep(1);
                                    //if (!updatescript.Update(jitter_sc, sccsjittertasks))
                                    //{
                                    //    updatescript.scgraphicssecpackagemessage.scgraphicssec.Shutdown();
                                    //    updatescript.captureMethod.Dispose();
                                    //    updatescript.ShutDownGraphics();
                                    //    sccs.scgraphics.scdirectx.D3D.ShutDown();
                                    //}
                                }
                            }




                        }




















                        /*
                        if (iswtchingcapturetypesmaybe == 1)
                        {
                            changedscreencapturetype = 1;
                        }*/


                        if (changedscreencapturetype == 1 && thestopwatch.Elapsed.Seconds > 1) //(changedscreencapturetype == 1 //|| iswtchingcapturetypesmaybe == 1
                        {
                            //sccs.Program.MessageBox((IntPtr)0, "capture reset1", "scmsg", 0);


                            /*
                            sccsr14sc.Form1.someform.hasinitfullscreen = 0;
                            sccsr14sc.Form1.someform.hasclickedmouse = 0;
                            sccsr14sc.Form1.someform.checkBox1_CheckedChangedint = 0;
                            sccsr14sc.Form1.someform.checkBox3_CheckedChangedint = 0;
                            */




















                            /*refreshDXEngineAction0 = new Action(delegate
                            {
                                //Console.WriteLine("thebutton Visible");
                                //stackoverflow 661561 for invoking panel changes.

                                //sccsr14sc.Form1.haspressedf9 = 1;

                                sccsr14sc.Form1.checkbox3.Invoke((MethodInvoker)delegate
                                {
                                    if (sccsr14sc.Form1.checkbox3.Checked)
                                    {
                                        sccsr14sc.Form1.checkbox3.Checked = false;
                                    }
                                    else if (!sccsr14sc.Form1.checkbox3.Checked)
                                    {
                                        sccsr14sc.Form1.checkbox3.Checked = true;
                                    }
                                });
                            });
                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                            */

                            //sccsr14sc.Form1.someform.checkBox1_CheckedChangedint = 0;
                            //sccsr14sc.Form1.someform.checkBox2_CheckedChangedint = 0;
                            //sccsr14sc.Form1.someform.checkBox3_CheckedChangedint = 0;


                            /* var refreshDXEngineAction0 = new Action(delegate
                             {
                                 //Console.WriteLine("thebutton Visible");
                                 //stackoverflow 661561 for invoking panel changes.



                                 /*sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                 {
                                     sccsr14sc.Form1.checkbox1.Checked = false;
                                     /*if (sccsr14sc.Form1.checkbox1.Checked)
                                     {
                                         sccsr14sc.Form1.checkbox1.Checked = false;
                                     }
                                     else if (!sccsr14sc.Form1.checkbox1.Checked)
                                     {
                                         sccsr14sc.Form1.checkbox1.Checked = true;
                                     }
                                 });

                                 sccsr14sc.Form1.checkbox2.Invoke((MethodInvoker)delegate
                                 {
                                     sccsr14sc.Form1.checkbox2.Checked = false;
                                     /*if (sccsr14sc.Form1.checkbox2.Checked)
                                     {
                                         sccsr14sc.Form1.checkbox2.Checked = false;
                                     }
                                     else if (!sccsr14sc.Form1.checkbox2.Checked)
                                     {
                                         sccsr14sc.Form1.checkbox2.Checked = true;
                                     }
                                 });

                                 sccsr14sc.Form1.checkbox3.Invoke((MethodInvoker)delegate
                                 {
                                     sccsr14sc.Form1.checkbox3.Checked = false;
                                     /*if (sccsr14sc.Form1.checkbox3.Checked)
                                     {
                                         sccsr14sc.Form1.checkbox3.Checked = false;
                                     }
                                     else if (!sccsr14sc.Form1.checkbox3.Checked)
                                     {
                                         sccsr14sc.Form1.checkbox3.Checked = true;
                                     }
                                 });
                             });
                             System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);*/









































                            /*
                            public int hasinitfullscreen = 0;
                            public int checkBox1_CheckedChangedint = 0;
                            public int hasclickedmouse = 0;*/


                            /*
                            sccsr14sc.Form1.someform.hasinitfullscreen = 0;
                            sccsr14sc.Form1.someform.hasclickedmouse = 0;
                            sccsr14sc.Form1.someform.checkBox1_CheckedChangedint = 0;
                            sccsr14sc.Form1.someform.checkBox3_CheckedChangedint = 0;
                            */




                            if (updatescript.exitthread0 == 0)
                            {




                                

                                string altcapturedwindowname = sccsr14sc.Form1.capturedwindownameform1.ToLower();

                                if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                                {



                                }
                                else if (altcapturedwindowname.Contains("firefox"))
                                {
                                    var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                                    if (RunningProcessPaths.Contains("firefox.exe"))
                                    {


                                    }

                                    /*if (RunningProcessPaths.Contains("chrome.exe"))
                                    {
                                        //Google Chrome is running
                                        Console.WriteLine("chrome is running");
                                    }*/

                                }
                                else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program") && altcapturedwindowname.Length == 30)
                                {


                                }
                                else if (altcapturedwindowname.Contains("void") && altcapturedwindowname.Contains("expanse"))
                                {
                                    //if (altcapturedwindowname == "void expanse")
                                    {

                                        
                                    }
                                }
                                else
                                {


                                }




                                /*
                                GC.SuppressFinalize(vewindowsfoundedz);

                                DeleteObject(vewindowsfoundedz);
                                */

                                thestopwatch.Reset();
                                thestopwatch.Restart();



                                updatescript.exitthread0 = 1;
                            }
                            if (updatescript.exitthread1 == 0)
                            {
                                updatescript.exitthread1 = 1;
                            }



                            if (updatescript.exitthread0 == 2 || updatescript.main_thread_update0 == null)
                            {
                                if (updatescript.main_thread_update0 == null)
                                {
                                    updatescript.hasfinishedframe0 = 1;
                                }
                                else
                                {
                                    updatescript.main_thread_update0 = null;
                                }

                                updatescript.exitthread0 = 3;
                            }
                            if (updatescript.exitthread1 == 2 || updatescript.main_thread_update1 == null)
                            {
                                if (updatescript.main_thread_update1 == null)
                                {
                                    updatescript.hasfinishedframe1 = 1;
                                }
                                else
                                {
                                    updatescript.main_thread_update1 = null;
                                }
                                updatescript.exitthread1 = 3;
                            }



                            if (updatescript.exitthread0 == 3 && updatescript.exitthread1 == 3)
                            {



                                //sccs.Program.MessageBox((IntPtr)0, "capture reset0", "scmsg", 0);

                                if (updatescript.hasfinishedframe0 == 1 && updatescript.hasfinishedframe1 == 1)
                                {



                                    updatescript.captureMethod.StopCapture();

                                    if (updatescript.captureMethod != null)
                                    {
                                        //updatescript.captureMethod.Dispose();
                                        updatescript.captureMethod = null;
                                    }





                                    if (last_hWnd != IntPtr.Zero)
                                    {
                                        if (last_hWnd == Program.vewindowsfoundedz)
                                        {
                                            int screenWidth = Program.GetSystemMetrics(0);
                                            int screenHeight = Program.GetSystemMetrics(1);



                                            Program.SetWindowPos(last_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                            Program.SetWindowPos(last_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                                            DeleteObject(last_hWnd);
                                            GC.SuppressFinalize(last_hWnd);

                                            DeleteObject(vewindowsfoundedz);
                                            GC.SuppressFinalize(vewindowsfoundedz);


                                        }
                                        else
                                        {
                                            int screenWidth = Program.GetSystemMetrics(0);
                                            int screenHeight = Program.GetSystemMetrics(1);


                                            if (vewindowsfoundedz != IntPtr.Zero)
                                            {
                                                Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                                Program.SetWindowPos(vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                                                DeleteObject(vewindowsfoundedz);
                                                GC.SuppressFinalize(vewindowsfoundedz);
                                            }




                                        }
                                    }

                                 











                                    //sccs.Program.MessageBox((IntPtr)0, "capture reset1", "scmsg", 0);



                                    saveplayerposition = scupdate.OFFSETPOS;
                                    saveplayerfinalRotationMatrix = scupdate.finalRotationMatrix;

                                    saveplayerrotatingMatrixForPelvis = scupdate.rotatingMatrixForPelvis;

                                    saveplayermovePos = scupdate.movePos;

                                    saveplayerrotx = scupdate.rotx;
                                    saveplayerroty = scupdate.roty;
                                    saveplayerrotz = scupdate.rotz;




                                    double saveplayerrotationpitch = 0.0;
                                    double saveplayerrotationyaw = 0.0;
                                    double saveplayerrotationroll = 0.0;

                                    if (sccs.scgraphics.scdirectx.D3D != null)
                                    {
                                        sccs.scgraphics.scdirectx.D3D.ShutDown();
                                    }




                                    if (sccsjittertasks != null)
                                    {
                                        if (sccsjittertasks[0] != null)
                                        {
                                            if (sccsjittertasks[0].Length > 0)
                                            {
                                                if (sccsjittertasks[0][0].shaderresource != null)
                                                {

                                                    sccsjittertasks[0][0].shaderresource.Dispose();
                                                    sccsjittertasks[0][0].shaderresource = null;
                                                }

                                                if (sccsjittertasks[0][0].frameByteArray != null)
                                                {
                                                    sccsjittertasks[0][0].frameByteArray = null;
                                                }
                                                //sccsjittertasks[0][0] = null;
                                            }
                                            sccsjittertasks[0] = null;
                                        }
                                        sccsjittertasks = null;
                                    }




                                    if (shaderResourceView != null)
                                    {
                                        shaderResourceView.Dispose();
                                        shaderResourceView = null;
                                    }

                                    if (lastshaderresourceview != null)
                                    {
                                        lastshaderresourceview.Dispose();
                                        lastshaderresourceview = null;
                                    }

                                    if (texture2d != null)
                                    {
                                        texture2d.Dispose();
                                        texture2d = null;
                                    }

                                    if (_texture2d != null)
                                    {
                                        _texture2d.Dispose();
                                        _texture2d = null;
                                    }

                                    if (_bitmap != null)
                                    {
                                        _bitmap.Dispose();
                                        _bitmap = null;
                                    }


                                    _textureByteArray = null;// new byte[_bytesTotal];
                                    bmpData = null;









                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                                    {
                                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop != null)
                                        {
                                            for (int i = 0; i < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; i++)
                                            {
                                                if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i] != null)
                                                {
                                                    for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData.Length; j++)
                                                    {
                                                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j] != null)
                                                        {
                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantLightBuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantLightBuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantLightBuffer = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantMatrixPosBuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantMatrixPosBuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantMatrixPosBuffer = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].ConstantTessellationBuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].ConstantTessellationBuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].ConstantTessellationBuffer = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].IndicesBuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].IndicesBuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].IndicesBuffer = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBuffer = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBufferHeightmap != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBufferHeightmap.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBufferHeightmap = null;
                                                            }
                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocD != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocD.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocD = null;
                                                            }
                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocH != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocH.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocH = null;
                                                            }
                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocW != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocW.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocW = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferFORWARD != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferFORWARD.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferFORWARD = null;
                                                            }
                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferRIGHT != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferRIGHT.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferRIGHT = null;
                                                            }
                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferUP != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferUP.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferUP = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].mapBuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].mapBuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].mapBuffer = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].vertexBuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].vertexBuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].vertexBuffer = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].DomainShader != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].DomainShader.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].DomainShader = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].GeometryShader != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].GeometryShader.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].GeometryShader = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].HullShader != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].HullShader.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].HullShader = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].PixelShader != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].PixelShader.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].PixelShader = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].VertexShader != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].VertexShader.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].VertexShader = null;
                                                            }




                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfDeVectorMapTemp = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfDeVectorMapTempTwo = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfSomeMap = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfVertex = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].heightmapmatrix = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesbytemaps = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesccsbytemapxyz = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesIndex = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesLocationD = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesLocationH = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesLocationW = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrix = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixb = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixc = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixd = null;




                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbuffer = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferb != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferb.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferb = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferc != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferc.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferc = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferd != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferd.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferd = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].Layout != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].Layout.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].Layout = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].lightBuffer != null)
                                                            {

                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].lightBuffer = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].matrixBuffer != null)
                                                            {

                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].matrixBuffer = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].samplerState != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].samplerState.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].samplerState = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].someoculusdirbuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].someoculusdirbuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].someoculusdirbuffer = null;
                                                            }

                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].originalArrayOfIndices = null;

                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].SC_instancedChunk_Instances = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].SC_instancedChunk_InstancesFORWARD = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].SC_instancedChunk_InstancesRIGHT = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].SC_instancedChunk_InstancesUP = null;

                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].someovrdir = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].tessellationBuffer = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfVertex = null;

                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j] = null;

                                                        }

                                                    }

                                                    for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh.Length; j++)
                                                    {
                                                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j] != null)
                                                        {
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j].ShutDown();
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j] = null;
                                                        }
                                                    }


                                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].shaderOfChunk = null;
                                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].ShutDown();

                                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i] = null;
                                                }
                                            }
                                        }
                                    }

                                    if (updatescript.scgraphicssec != null)
                                    {
                                        updatescript.scgraphicssec.Shutdown();
                                        updatescript.scgraphicssec = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.Shutdown();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop = null;
                                        scgraphicssec.somevoxelvirtualdesktopglobals = null;
                                    }





                                    /*
                                    if (updatescript.captureMethod != null)
                                    {
                                        updatescript.captureMethod.StopCapture();// (sccsr14sc.Form1.theHandle, updatescript.device, factoryy); //

                                        updatescript.captureMethod.Dispose();
                                        updatescript.captureMethod = null;
                                    }*/
                                    if (scupdate.sharpdxscreencapture != null)
                                    {
                                        //scupdate.sharpdxscreencapture.releaseFrame();
                                        scupdate.sharpdxscreencapture.Disposer();
                                        scupdate.sharpdxscreencapture = null;
                                    }


                                    /*
                                    if (sometest != null)
                                    {
                                        sometest.Dispose();
                                        sometest = null;
                                    }
                                    if (somegcap != null)
                                    {
                                        somegcap.Dispose();
                                        somegcap = null;
                                    }*/


                                    if (swapChain1 != null)
                                    {
                                        swapChain1.Dispose();
                                        swapChain1 = null;
                                    }

                                    if (updatescript.SwapChain != null)
                                    {
                                        updatescript.SwapChain.Dispose();
                                        updatescript.SwapChain = null;
                                    }

                                    if (factoryy != null)
                                    {
                                        factoryy.Dispose();
                                        factoryy = null;
                                    }



                                    updatescript.device?.Dispose();
                                    updatescript.device = null;


                                    updatescript.ShutDownGraphics();




                                    updatescript.exitthread0 = 0;
                                    updatescript.exitthread1 = 0;
                                    updatescript = null;

                                    /*
                                    if (screencapturetype == 2)
                                    {
                                        usesharpdxscreencapture = 1;
                                    }
                                    else
                                    {
                                        if (usesharpdxscreencapture != 0)
                                        {
                                            usesharpdxscreencapture = 0;
                                        }
                                    }*/


                                    /*
                                    GC.SuppressFinalize(vewindowsfoundedz);

                                    DeleteObject(vewindowsfoundedz);*/

                                    getwindowthreadprocessidint = 0;
                                    textureresetswtc = 0;
                                    //GC.Collect();
                                    changedscreencapturetype = 0;
                                    //createinputsswtc = 0;
                                    hasresettedcapture = 1;
                                    initform = 1;
                                    //sccs.Program.MessageBox((IntPtr)0, "capture reset1", "scmsg", 0);
                                }
                            }

                            //iswtchingcapturetypesmaybe = 1;
                        }
                        else
                        {
                            if (changedscreencapturetype == 2) //(changedscreencapturetype == 1 //|| iswtchingcapturetypesmaybe == 1
                            {
                                //


                            }

                        }



                        if (hasstartedcapture == 0)
                        {
                            hasstartedcapture = 1;
                        }


                        lastscreencapturetype = screencapturetype;

                        //lastscreencapturetype = screencapturetype;
                        //sccs.Program.MessageBox((IntPtr)0, "thread initiated", "scmsg", 0);
                        Thread.Sleep(1);
                        firstframededicatedthread = 1;

                        //MessageBox((IntPtr)0, "threadm0", "scmsg", 0);
                        goto _thread_main_loop;
                        //MessageBox((IntPtr)0, "threadm -1", "scmsg", 0);
                        //_thread_start:
                    }, 0); //100000 //999999999

                    _mainThread.IsBackground = true;
                    _mainThread.Priority = ThreadPriority.Normal; //AboveNormal
                    _mainThread.SetApartmentState(ApartmentState.STA);
                    _mainThread.Start();



                    //sccs.Program.MessageBox((IntPtr)0, "thread initiated", "scmsg", 0);
                    initthread = 2;
                }


                if (initthread == 2)
                {
                    if (initform == 0)
                    {
                        initform = 1;


                        /*
                        if (createinputsswtc == 0)
                        {
                            if (sccsr14sc.Form1.someform != null)
                            {
                                if (sccsr14sc.Form1.theHandle != IntPtr.Zero)
                                {
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        //sccs.scgraphics.scupdate.createinputs(sccsr14sc.Form1.theHandle);
                                        createinputs(sccsr14sc.Form1.theHandle);

                                        //someform = new RenderForm("sccsr14");
                                        /*someform.Size = new System.Drawing.Size(1920, 1080);
                                        someform.FormBorderStyle = FormBorderStyle.None;
                                        someform.WindowState = FormWindowState.Maximized;

                                        //sccsr14sc.Form1.someform.deactivatecursor();

                                        /*var _hwndSource = HwndSource.FromHwnd(someform.Handle);
                                        if (_hwndSource != null)
                                            _hwndSource.AddHook(WndProc);


                                        SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                                            SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                        //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                                        //    SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                        SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
                                        // SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;
                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                    //sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                    createinputsswtc = 1;
                                    //MessageBox((IntPtr)0, "createinputs ", "scmsg", 0);
                                }
                            }
                        }*/
                        //System.Windows.Forms.Cursor.Hide();
                        //MessageBox((IntPtr)0, "form initiated0", "scmsg", 0);
                        /*System.Windows.Forms.Application.EnableVisualStyles();
                        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                        someform = new Form1();
                        */


                        /*
                        //textBox = this.textBox1;
                        textBox = new System.Windows.Forms.TextBox() { Dock = DockStyle.Fill, Multiline = true, Text = "Interact with the mouse or the keyboard...\r\n", ReadOnly = true };
                        textBox.Dock = DockStyle.Fill;
                        textBox.Multiline = true;
                        textBox.Text = "Interact with the mouse or the keyboard...\r\n";
                        textBox.ReadOnly = true;


                        someform.Controls.Add(textBox);
                        //this.Visible = true;


                        SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, SharpDX.RawInput.DeviceFlags.None);
                        SharpDX.RawInput.Device.KeyboardInput += (sender, args) => textBox.Invoke(new UpdateTextCallback(UpdateKeyboardText), args);
                        */



                        /*
                        //someform = new RenderForm("sccsr14");
                        someform.Size = new System.Drawing.Size(640, 480); //1920 / 1080


                        someform.CreateControl();
                        //someform.TransparencyKey = System.Drawing.Color.Black;
                        someform.BackColor = System.Drawing.Color.Black;
                        someform.Activate();

                        someform.FormBorderStyle = FormBorderStyle.None;
                        //someform.WindowState = FormWindowState.Minimized;
                        someform.TopMost = true;
                        */
                        //someform.FormBorderStyle = FormBorderStyle.None;
                        //




                        //System.Windows.Forms.Cursor.Hide();
                        //someform.Cursor = System.Windows.Forms.Cursors.None;
                        //System.Windows.Forms.Cursor.Hide();


                        //var somewindow = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);
                        //consoleHandle = somewindow.EnsureHandle();


                        //SetWindowLong(someform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT));
                        //SetLayeredWindowAttributes(someform.Handle, 0,
                        //makeBoraderless();
                        //dockIt();
                        //makeBorderless(); 255, LWA_ALPHA);


                        //IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
                        //SetWindowLong(window, GWL_STYLE, WS_SYSMENU);

                        //initform = 2;
                        //System.Windows.Forms.Application.Run(someform);
                        //System.Windows.Forms.Cursor.Hide();
                        //dockIt();
                        //makePanelBorderless();
                        //makeBorderless();

                        string WINDOW_NAME = "sccsr14";
                        //IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
                        //SetWindowLong(someform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_TRANSPARENT)); // | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW | WS_EX_LAYERED 
                        //SetLayeredWindowAttributes(someform.Handle, 0, 255, LWA_ALPHA);


                        /*if (System.Diagnostics.Debugger.IsAttached)
                        {
                            //MessageBox((IntPtr)0, "threadm1", "scmsg", 0);


                            Thread.Sleep(1);
                            goto mainthreadloop;
                        }
                        else
                        {
                            //MessageBox((IntPtr)0, "threadm2", "scmsg", 0);


                            Thread.Sleep(1);
                            goto mainthreadloop;
                        }*/

                        //MessageBox((IntPtr)0, "form initiated1", "scmsg", 0);

                    }
                    else if (initform == 2)
                    {
                        /*if (System.Diagnostics.Debugger.IsAttached)
                        {
                            //MessageBox((IntPtr)0, "threadm1", "scmsg", 0);
                            Thread.Sleep(1);
                            goto mainthreadloop;
                        }
                        else
                        {
                            //MessageBox((IntPtr)0, "threadm2", "scmsg", 0);
                            Thread.Sleep(1);
                            goto mainthreadloop;
                        }*/
                    }

                    /*if (System.Diagnostics.Debugger.IsAttached)
                    {
                        //MessageBox((IntPtr)0, "threadm1", "scmsg", 0);
                        Thread.Sleep(1);
                        goto mainthreadloop;
                    }
                    else
                    {
                        //MessageBox((IntPtr)0, "threadm2", "scmsg", 0);
                        Thread.Sleep(1);
                        goto mainthreadloop;
                    }*/
                }
                else
                {
                    //MessageBox((IntPtr)0, "threadm3", "scmsg", 0);
                    //AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;


                    //Thread.Sleep(1);
                    //goto mainthreadloop;
                }
                // ReSharper restore AccessToDisposedClosure





                /*
                if (updatescript != null)
                {
                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                    {

                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                        {
                            if (updatescript.scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (updatescript.scgraphicssecpackagemessage.scjittertasks.Length > 0)
                                {
                                    if (updatescript.scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        //if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                        {
                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);

                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);


                                            //updatescript.scgraphicssecpackagemessage.scjittertasks = sccsjittertasks;
                                            sccsjittertasks = updatescript.StopRender(null, updatescript.scgraphicssecpackagemessage.scjittertasks);

                                            //sccsjittertasks = updatescript.scgraphicssecpackagemessage.scgraphicssec.workonshaders(updatescript.scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }*/




                Thread.Sleep(1);
            });

            //Console.WriteLine("Hello World!");



            //sccs.Program.MessageBox((IntPtr)0, "Program loaded0 ", "scmsg", 0);

            /*

            sccs.Program.MessageBox((IntPtr)0, "Program loaded0 ", "scmsg", 0);


            //var window = new DxWindow(".NET Window Capture Samples - Win32.DwmSharedSurface", new DwmSharedSurface());
            //window.Show();

            mainreceivedmessages = new scmessageobject.scmessageobject[MaxSizeMainObject];

            for (int i = 0; i < mainreceivedmessages.Length; i++)
            {
                mainreceivedmessages[i] = new scmessageobject.scmessageobject();
                mainreceivedmessages[i]._received_switch_in = -1;
                mainreceivedmessages[i]._received_switch_out = -1;
                mainreceivedmessages[i]._sending_switch_in = -1;
                mainreceivedmessages[i]._sending_switch_out = -1;
                mainreceivedmessages[i]._timeOut0 = -1;
                mainreceivedmessages[i]._ParentTaskThreadID0 = -1;
                mainreceivedmessages[i]._main_cpu_count = 1;
                mainreceivedmessages[i]._passTest = "";
                mainreceivedmessages[i]._welcomePackage = -1;
                mainreceivedmessages[i]._work_done = -1;
                mainreceivedmessages[i]._current_menu = -1;
                mainreceivedmessages[i]._last_current_menu = -1;
                mainreceivedmessages[i]._main_menu = -1;
                mainreceivedmessages[i]._menuOption = "";
                mainreceivedmessages[i]._voRecSwtc = -1;
                mainreceivedmessages[i]._voRecMsg = "";
                mainreceivedmessages[i]._someData = new object[MaxSizeMainObject];

                for (int j = 0; j < mainreceivedmessages[i]._someData.Length; j++)
                {
                    mainreceivedmessages[i]._someData[j] = new object();
                }


                //mainreceivedmessages[0]._someData[0] = new object();


                //VOICE RECOGNITION. NOT IMPLEMENTED YET.
                /*if (i == MaxSizeMainObject - 1)
                {
                    mainreceivedmessages[i]._someData[0] = _keyboard_input._KeyboardState;
                    mainreceivedmessages[i]._voRecSwtc = 1;
                }
            }
            ///////////////////////////////
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////  
            ///message_thread_safe_kinda///   
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////

            //SCGLOBALSACCESSORS AND GLOBALS CREATOR
            sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages);
            if (SCGLOBALSACCESSORS == null)
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS NULL", "Console");
            }
            else
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS !NULL", "Console");
            }
            //borderlessconsole console_ = new borderlessconsole();
            //SCGLOBALSACCESSORS AND GLOBALS CREATOR




            //var somewindow = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);
            //consoleHandle = somewindow.EnsureHandle();
            consoleHandle = scconsolecore.handle;

            //form = new RenderForm("sccsr14");
            //consoleHandle = form.Handle;
            config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);

            //form = new RenderForm("sccsr14");


            int swtc0 = 0;*/





            /*RenderLoop.Run(someform, () =>
            {

                // draw it
                //device.ImmediateContext.Draw(4, 0);
                //swapChain1.Present(1, PresentFlags.None, new PresentParameters());

                // ReSharper restore AccessToDisposedClosure
                Thread.Sleep(1);
            });*/



            //scdirectx directx = new scdirectx( new DwmSharedSurface());



            //SetWindowLong(consoleHandle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT));
            //SetLayeredWindowAttributes(consoleHandle, 0, 255, LWA_ALPHA);


            //sccs.Program.MessageBox((IntPtr)0, "Program loaded", "scmsg", 0);

        }
        //public static RenderForm form;



        static System.Windows.Forms.TextBox textBox;








        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);



        //public static RenderForm form;
        /// <summary>
        /// Updates the mouse text.
        /// </summary>
        /// <param name="rawArgs">The <see cref="SharpDX.RawInput.RawInputEventArgs"/> instance containing the event data.</param>
        static void UpdateMouseText(object sender, MouseInputEventArgs rawArgs) // EventArgs e  //object sender,
        {
            //MessageBox((IntPtr)0, "test0", "scmsg", 0);
            var args = (MouseInputEventArgs)rawArgs;

            //textBox.AppendText(string.Format("(x,y):({0},{1}) Buttons: {2} State: {3} Wheel: {4}\r\n", args.X, args.Y, args.ButtonFlags, args.Mode, args.WheelDelta));
            //Console.WriteLine(args.X + " " + args.Y + " " + args.ButtonFlags + " "  + args.Mode);

            //mrbuttondown = 0;
            //mlbuttondown = 0;

            if (args.ButtonFlags == mouseleftdownflag)
            {
                mlbuttondown = 1;
                //mlbuttondown = 1;
                //MessageBox((IntPtr)0, "mldown", "scmsg", 0);
            }
            else
            {
                mlbuttondown = 0;
            }


            if (args.ButtonFlags == mouserightdownflag)
            {
                mrbuttondown = 1;
                //MessageBox((IntPtr)0, "mrdown", "scmsg", 0);
            }
            else
            {
                mrbuttondown = 0;
            }




            //Console.WriteLine(args.X + " " + args.Y + " " + args.ButtonFlags + " " + args.Mode + " " + mlbuttondown);
        }

        public static int mlbuttondown = 0;
        public static int mrbuttondown = 0;






        static MouseButtonFlags mouseleftdownflag = MouseButtonFlags.LeftButtonDown;
        static MouseButtonFlags mouserightdownflag = MouseButtonFlags.RightButtonDown;



        /// <summary>
        /// Updates the keyboard text.
        /// </summary>
        /// <param name="rawArgs">The <see cref="SharpDX.RawInput.RawInputEventArgs"/> instance containing the event data.</param>
        static void UpdateKeyboardText(object sender, KeyboardInputEventArgs rawArgs) //object sender,
        {
            //MessageBox((IntPtr)0, "test1", "scmsg", 0);
            var args = (KeyboardInputEventArgs)rawArgs;
            textBox.AppendText(string.Format("Key: {0} State: {1} ScanCodeFlags: {2}\r\n", args.Key, args.State, args.ScanCodeFlags));

            //Console.WriteLine(args.Key + " " + args.State);
        }

        /// <summary>
        /// Delegate use for printing events
        /// </summary>
        /// <param name="args">The <see cref="SharpDX.RawInput.RawInputEventArgs"/> instance containing the event data.</param>
        public delegate void UpdateTextCallback(RawInputEventArgs args);


        //protected override void WndProc(ref Message m)


        private const int WM_INPUT = 0x00FF;

        //ref System.Windows.Forms.Message m)//
        private static IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {



            /*
            if (m.Msg == 0x0112) // WM_SYSCOMMAND
            {
                // Check your window state here
                if (m.WParam == new IntPtr(0xF030)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    //checkBox1_CheckedChangedint = 0;
                    Console.WriteLine("maximized");
                    // THe window is being maximized
                }
                else if (m.WParam == new IntPtr(0XF020)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    
                    Console.WriteLine("minimized");
                    // THe window is being minimized
                }
                else if (m.WParam == new IntPtr(0xF120)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
  
                    //checkBox1_CheckedChangedint = 1;
                    Console.WriteLine("restore");
                    // THe window is being minimized
                }
            }
            
            base.WndProc(ref m);*/




            if (msg == WM_INPUT)
            {
                SharpDX.RawInput.Device.HandleMessage(lParam, hWnd);
            }


            //MessageBox((IntPtr)0, "error WndProc", "scmsg", 0);


            return IntPtr.Zero;

          

        }










        static BitmapSource _lastbitSource01;
        static BitmapSource _bitSource01;
        static BitmapSource source;
        static System.Drawing.Rectangle rectanglebitmap;
        static System.Drawing.Imaging.BitmapData bitmapData0;
        private static BitmapSource CreateBitmapSource(System.Drawing.Bitmap bitmap, System.Drawing.Imaging.BitmapData bitmapData)
        {
            try
            {
                rectanglebitmap = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);

                bitmapData0 = bitmap.LockBits(rectanglebitmap, System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
                source = BitmapSource.Create(bitmap.Width, bitmap.Height, 24, 24, PixelFormats.Pbgra32, null, bitmapData0.Scan0, bitmapData0.Stride * bitmap.Height, bitmapData0.Stride); //bitmap.HorizontalResolution, bitmap.VerticalResolution
                bitmap.UnlockBits(bitmapData0);

                bitmapData0 = null;
                bitmap.Dispose();

                return source;
            }
            catch
            {
                return null;
            }
        }


















        /// <summary>
        /// Windows Messages
        /// Defined in winuser.h from Windows SDK v6.1
        /// Documentation pulled from MSDN.
        /// </summary>
        public enum WM : uint
        {
            /// <summary>
            /// The WM_NULL message performs no operation. An application sends the WM_NULL message if it wants to post a message that the recipient window will ignore.
            /// </summary>
            NULL = 0x0000,
            /// <summary>
            /// The WM_CREATE message is sent when an application requests that a window be created by calling the CreateWindowEx or CreateWindow function. (The message is sent before the function returns.) The window procedure of the new window receives this message after the window is created, but before the window becomes visible.
            /// </summary>
            CREATE = 0x0001,
            /// <summary>
            /// The WM_DESTROY message is sent when a window is being destroyed. It is sent to the window procedure of the window being destroyed after the window is removed from the screen.
            /// This message is sent first to the window being destroyed and then to the child windows (if any) as they are destroyed. During the processing of the message, it can be assumed that all child windows still exist.
            /// /// </summary>
            DESTROY = 0x0002,
            /// <summary>
            /// The WM_MOVE message is sent after a window has been moved.
            /// </summary>
            MOVE = 0x0003,
            /// <summary>
            /// The WM_SIZE message is sent to a window after its size has changed.
            /// </summary>
            SIZE = 0x0005,
            /// <summary>
            /// The WM_ACTIVATE message is sent to both the window being activated and the window being deactivated. If the windows use the same input queue, the message is sent synchronously, first to the window procedure of the top-level window being deactivated, then to the window procedure of the top-level window being activated. If the windows use different input queues, the message is sent asynchronously, so the window is activated immediately.
            /// </summary>
            ACTIVATE = 0x0006,
            /// <summary>
            /// The WM_SETFOCUS message is sent to a window after it has gained the keyboard focus.
            /// </summary>
            SETFOCUS = 0x0007,
            /// <summary>
            /// The WM_KILLFOCUS message is sent to a window immediately before it loses the keyboard focus.
            /// </summary>
            KILLFOCUS = 0x0008,
            /// <summary>
            /// The WM_ENABLE message is sent when an application changes the enabled state of a window. It is sent to the window whose enabled state is changing. This message is sent before the EnableWindow function returns, but after the enabled state (WS_DISABLED style bit) of the window has changed.
            /// </summary>
            ENABLE = 0x000A,
            /// <summary>
            /// An application sends the WM_SETREDRAW message to a window to allow changes in that window to be redrawn or to prevent changes in that window from being redrawn.
            /// </summary>
            SETREDRAW = 0x000B,
            /// <summary>
            /// An application sends a WM_SETTEXT message to set the text of a window.
            /// </summary>
            SETTEXT = 0x000C,
            /// <summary>
            /// An application sends a WM_GETTEXT message to copy the text that corresponds to a window into a buffer provided by the caller.
            /// </summary>
            GETTEXT = 0x000D,
            /// <summary>
            /// An application sends a WM_GETTEXTLENGTH message to determine the length, in characters, of the text associated with a window.
            /// </summary>
            GETTEXTLENGTH = 0x000E,
            /// <summary>
            /// The WM_PAINT message is sent when the system or another application makes a request to paint a portion of an application's window. The message is sent when the UpdateWindow or RedrawWindow function is called, or by the DispatchMessage function when the application obtains a WM_PAINT message by using the GetMessage or PeekMessage function.
            /// </summary>
            PAINT = 0x000F,
            /// <summary>
            /// The WM_CLOSE message is sent as a signal that a window or an application should terminate.
            /// </summary>
            CLOSE = 0x0010,
            /// <summary>
            /// The WM_QUERYENDSESSION message is sent when the user chooses to end the session or when an application calls one of the system shutdown functions. If any application returns zero, the session is not ended. The system stops sending WM_QUERYENDSESSION messages as soon as one application returns zero.
            /// After processing this message, the system sends the WM_ENDSESSION message with the wParam parameter set to the results of the WM_QUERYENDSESSION message.
            /// </summary>
            QUERYENDSESSION = 0x0011,
            /// <summary>
            /// The WM_QUERYOPEN message is sent to an icon when the user requests that the window be restored to its previous size and position.
            /// </summary>
            QUERYOPEN = 0x0013,
            /// <summary>
            /// The WM_ENDSESSION message is sent to an application after the system processes the results of the WM_QUERYENDSESSION message. The WM_ENDSESSION message informs the application whether the session is ending.
            /// </summary>
            ENDSESSION = 0x0016,
            /// <summary>
            /// The WM_QUIT message indicates a request to terminate an application and is generated when the application calls the PostQuitMessage function. It causes the GetMessage function to return zero.
            /// </summary>
            QUIT = 0x0012,
            /// <summary>
            /// The WM_ERASEBKGND message is sent when the window background must be erased (for example, when a window is resized). The message is sent to prepare an invalidated portion of a window for painting.
            /// </summary>
            ERASEBKGND = 0x0014,
            /// <summary>
            /// This message is sent to all top-level windows when a change is made to a system color setting.
            /// </summary>
            SYSCOLORCHANGE = 0x0015,
            /// <summary>
            /// The WM_SHOWWINDOW message is sent to a window when the window is about to be hidden or shown.
            /// </summary>
            SHOWWINDOW = 0x0018,
            /// <summary>
            /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
            /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
            /// </summary>
            WININICHANGE = 0x001A,
            /// <summary>
            /// An application sends the WM_WININICHANGE message to all top-level windows after making a change to the WIN.INI file. The SystemParametersInfo function sends this message after an application uses the function to change a setting in WIN.INI.
            /// Note  The WM_WININICHANGE message is provided only for compatibility with earlier versions of the system. Applications should use the WM_SETTINGCHANGE message.
            /// </summary>
            SETTINGCHANGE = WININICHANGE,
            /// <summary>
            /// The WM_DEVMODECHANGE message is sent to all top-level windows whenever the user changes device-mode settings.
            /// </summary>
            DEVMODECHANGE = 0x001B,
            /// <summary>
            /// The WM_ACTIVATEAPP message is sent when a window belonging to a different application than the active window is about to be activated. The message is sent to the application whose window is being activated and to the application whose window is being deactivated.
            /// </summary>
            ACTIVATEAPP = 0x001C,
            /// <summary>
            /// An application sends the WM_FONTCHANGE message to all top-level windows in the system after changing the pool of font resources.
            /// </summary>
            FONTCHANGE = 0x001D,
            /// <summary>
            /// A message that is sent whenever there is a change in the system time.
            /// </summary>
            TIMECHANGE = 0x001E,
            /// <summary>
            /// The WM_CANCELMODE message is sent to cancel certain modes, such as mouse capture. For example, the system sends this message to the active window when a dialog box or message box is displayed. Certain functions also send this message explicitly to the specified window regardless of whether it is the active window. For example, the EnableWindow function sends this message when disabling the specified window.
            /// </summary>
            CANCELMODE = 0x001F,
            /// <summary>
            /// The WM_SETCURSOR message is sent to a window if the mouse causes the cursor to move within a window and mouse input is not captured.
            /// </summary>
            SETCURSOR = 0x0020,
            /// <summary>
            /// The WM_MOUSEACTIVATE message is sent when the cursor is in an inactive window and the user presses a mouse button. The parent window receives this message only if the child window passes it to the DefWindowProc function.
            /// </summary>
            MOUSEACTIVATE = 0x0021,
            /// <summary>
            /// The WM_CHILDACTIVATE message is sent to a child window when the user clicks the window's title bar or when the window is activated, moved, or sized.
            /// </summary>
            CHILDACTIVATE = 0x0022,
            /// <summary>
            /// The WM_QUEUESYNC message is sent by a computer-based training (CBT) application to separate user-input messages from other messages sent through the WH_JOURNALPLAYBACK Hook procedure.
            /// </summary>
            QUEUESYNC = 0x0023,
            /// <summary>
            /// The WM_GETMINMAXINFO message is sent to a window when the size or position of the window is about to change. An application can use this message to override the window's default maximized size and position, or its default minimum or maximum tracking size.
            /// </summary>
            GETMINMAXINFO = 0x0024,
            /// <summary>
            /// Windows NT 3.51 and earlier: The WM_PAINTICON message is sent to a minimized window when the icon is to be painted. This message is not sent by newer versions of Microsoft Windows, except in unusual circumstances explained in the Remarks.
            /// </summary>
            PAINTICON = 0x0026,
            /// <summary>
            /// Windows NT 3.51 and earlier: The WM_ICONERASEBKGND message is sent to a minimized window when the background of the icon must be filled before painting the icon. A window receives this message only if a class icon is defined for the window; otherwise, WM_ERASEBKGND is sent. This message is not sent by newer versions of Windows.
            /// </summary>
            ICONERASEBKGND = 0x0027,
            /// <summary>
            /// The WM_NEXTDLGCTL message is sent to a dialog box procedure to set the keyboard focus to a different control in the dialog box.
            /// </summary>
            NEXTDLGCTL = 0x0028,
            /// <summary>
            /// The WM_SPOOLERSTATUS message is sent from Print Manager whenever a job is added to or removed from the Print Manager queue.
            /// </summary>
            SPOOLERSTATUS = 0x002A,
            /// <summary>
            /// The WM_DRAWITEM message is sent to the parent window of an owner-drawn button, combo box, list box, or menu when a visual aspect of the button, combo box, list box, or menu has changed.
            /// </summary>
            DRAWITEM = 0x002B,
            /// <summary>
            /// The WM_MEASUREITEM message is sent to the owner window of a combo box, list box, list view control, or menu item when the control or menu is created.
            /// </summary>
            MEASUREITEM = 0x002C,
            /// <summary>
            /// Sent to the owner of a list box or combo box when the list box or combo box is destroyed or when items are removed by the LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT message. The system sends a WM_DELETEITEM message for each deleted item. The system sends the WM_DELETEITEM message for any deleted list box or combo box item with nonzero item data.
            /// </summary>
            DELETEITEM = 0x002D,
            /// <summary>
            /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_KEYDOWN message.
            /// </summary>
            VKEYTOITEM = 0x002E,
            /// <summary>
            /// Sent by a list box with the LBS_WANTKEYBOARDINPUT style to its owner in response to a WM_CHAR message.
            /// </summary>
            CHARTOITEM = 0x002F,
            /// <summary>
            /// An application sends a WM_SETFONT message to specify the font that a control is to use when drawing text.
            /// </summary>
            SETFONT = 0x0030,
            /// <summary>
            /// An application sends a WM_GETFONT message to a control to retrieve the font with which the control is currently drawing its text.
            /// </summary>
            GETFONT = 0x0031,
            /// <summary>
            /// An application sends a WM_SETHOTKEY message to a window to associate a hot key with the window. When the user presses the hot key, the system activates the window.
            /// </summary>
            SETHOTKEY = 0x0032,
            /// <summary>
            /// An application sends a WM_GETHOTKEY message to determine the hot key associated with a window.
            /// </summary>
            GETHOTKEY = 0x0033,
            /// <summary>
            /// The WM_QUERYDRAGICON message is sent to a minimized (iconic) window. The window is about to be dragged by the user but does not have an icon defined for its class. An application can return a handle to an icon or cursor. The system displays this cursor or icon while the user drags the icon.
            /// </summary>
            QUERYDRAGICON = 0x0037,
            /// <summary>
            /// The system sends the WM_COMPAREITEM message to determine the relative position of a new item in the sorted list of an owner-drawn combo box or list box. Whenever the application adds a new item, the system sends this message to the owner of a combo box or list box created with the CBS_SORT or LBS_SORT style.
            /// </summary>
            COMPAREITEM = 0x0039,
            /// <summary>
            /// Active Accessibility sends the WM_GETOBJECT message to obtain information about an accessible object contained in a server application.
            /// Applications never send this message directly. It is sent only by Active Accessibility in response to calls to AccessibleObjectFromPoint, AccessibleObjectFromEvent, or AccessibleObjectFromWindow. However, server applications handle this message.
            /// </summary>
            GETOBJECT = 0x003D,
            /// <summary>
            /// The WM_COMPACTING message is sent to all top-level windows when the system detects more than 12.5 percent of system time over a 30- to 60-second interval is being spent compacting memory. This indicates that system memory is low.
            /// </summary>
            COMPACTING = 0x0041,
            /// <summary>
            /// WM_COMMNOTIFY is Obsolete for Win32-Based Applications
            /// </summary>
            [Obsolete("Obsolete for Win32 Based Applications")]
            COMMNOTIFY = 0x0044,
            /// <summary>
            /// The WM_WINDOWPOSCHANGING message is sent to a window whose size, position, or place in the Z order is about to change as a result of a call to the SetWindowPos function or another window-management function.
            /// </summary>
            WINDOWPOSCHANGING = 0x0046,
            /// <summary>
            /// The WM_WINDOWPOSCHANGED message is sent to a window whose size, position, or place in the Z order has changed as a result of a call to the SetWindowPos function or another window-management function.
            /// </summary>
            WINDOWPOSCHANGED = 0x0047,
            /// <summary>
            /// Notifies applications that the system, typically a battery-powered personal computer, is about to enter a suspended mode.
            /// Use: POWERBROADCAST
            /// </summary>
            [Obsolete("Provided only for compatibility with 16-bit Windows-based applications")]
            POWER = 0x0048,
            /// <summary>
            /// An application sends the WM_COPYDATA message to pass data to another application.
            /// </summary>
            COPYDATA = 0x004A,
            /// <summary>
            /// The WM_CANCELJOURNAL message is posted to an application when a user cancels the application's journaling activities. The message is posted with a NULL window handle.
            /// </summary>
            CANCELJOURNAL = 0x004B,
            /// <summary>
            /// Sent by a common control to its parent window when an event has occurred or the control requires some information.
            /// </summary>
            NOTIFY = 0x004E,
            /// <summary>
            /// The WM_INPUTLANGCHANGEREQUEST message is posted to the window with the focus when the user chooses a new input language, either with the hotkey (specified in the Keyboard control panel application) or from the indicator on the system taskbar. An application can accept the change by passing the message to the DefWindowProc function or reject the change (and prevent it from taking place) by returning immediately.
            /// </summary>
            INPUTLANGCHANGEREQUEST = 0x0050,
            /// <summary>
            /// The WM_INPUTLANGCHANGE message is sent to the topmost affected window after an application's input language has been changed. You should make any application-specific settings and pass the message to the DefWindowProc function, which passes the message to all first-level child windows. These child windows can pass the message to DefWindowProc to have it pass the message to their child windows, and so on.
            /// </summary>
            INPUTLANGCHANGE = 0x0051,
            /// <summary>
            /// Sent to an application that has initiated a training card with Microsoft Windows Help. The message informs the application when the user clicks an authorable button. An application initiates a training card by specifying the HELP_TCARD command in a call to the WinHelp function.
            /// </summary>
            TCARD = 0x0052,
            /// <summary>
            /// Indicates that the user pressed the F1 key. If a menu is active when F1 is pressed, WM_HELP is sent to the window associated with the menu; otherwise, WM_HELP is sent to the window that has the keyboard focus. If no window has the keyboard focus, WM_HELP is sent to the currently active window.
            /// </summary>
            HELP = 0x0053,
            /// <summary>
            /// The WM_USERCHANGED message is sent to all windows after the user has logged on or off. When the user logs on or off, the system updates the user-specific settings. The system sends this message immediately after updating the settings.
            /// </summary>
            USERCHANGED = 0x0054,
            /// <summary>
            /// Determines if a window accepts ANSI or Unicode structures in the WM_NOTIFY notification message. WM_NOTIFYFORMAT messages are sent from a common control to its parent window and from the parent window to the common control.
            /// </summary>
            NOTIFYFORMAT = 0x0055,
            /// <summary>
            /// The WM_CONTEXTMENU message notifies a window that the user clicked the right mouse button (right-clicked) in the window.
            /// </summary>
            CONTEXTMENU = 0x007B,
            /// <summary>
            /// The WM_STYLECHANGING message is sent to a window when the SetWindowLong function is about to change one or more of the window's styles.
            /// </summary>
            STYLECHANGING = 0x007C,
            /// <summary>
            /// The WM_STYLECHANGED message is sent to a window after the SetWindowLong function has changed one or more of the window's styles
            /// </summary>
            STYLECHANGED = 0x007D,
            /// <summary>
            /// The WM_DISPLAYCHANGE message is sent to all windows when the display resolution has changed.
            /// </summary>
            DISPLAYCHANGE = 0x007E,
            /// <summary>
            /// The WM_GETICON message is sent to a window to retrieve a handle to the large or small icon associated with a window. The system displays the large icon in the ALT+TAB dialog, and the small icon in the window caption.
            /// </summary>
            GETICON = 0x007F,
            /// <summary>
            /// An application sends the WM_SETICON message to associate a new large or small icon with a window. The system displays the large icon in the ALT+TAB dialog box, and the small icon in the window caption.
            /// </summary>
            SETICON = 0x0080,
            /// <summary>
            /// The WM_NCCREATE message is sent prior to the WM_CREATE message when a window is first created.
            /// </summary>
            NCCREATE = 0x0081,
            /// <summary>
            /// The WM_NCDESTROY message informs a window that its nonclient area is being destroyed. The DestroyWindow function sends the WM_NCDESTROY message to the window following the WM_DESTROY message. WM_DESTROY is used to free the allocated memory object associated with the window.
            /// The WM_NCDESTROY message is sent after the child windows have been destroyed. In contrast, WM_DESTROY is sent before the child windows are destroyed.
            /// </summary>
            NCDESTROY = 0x0082,
            /// <summary>
            /// The WM_NCCALCSIZE message is sent when the size and position of a window's client area must be calculated. By processing this message, an application can control the content of the window's client area when the size or position of the window changes.
            /// </summary>
            NCCALCSIZE = 0x0083,
            /// <summary>
            /// The WM_NCHITTEST message is sent to a window when the cursor moves, or when a mouse button is pressed or released. If the mouse is not captured, the message is sent to the window beneath the cursor. Otherwise, the message is sent to the window that has captured the mouse.
            /// </summary>
            NCHITTEST = 0x0084,
            /// <summary>
            /// The WM_NCPAINT message is sent to a window when its frame must be painted.
            /// </summary>
            NCPAINT = 0x0085,
            /// <summary>
            /// The WM_NCACTIVATE message is sent to a window when its nonclient area needs to be changed to indicate an active or inactive state.
            /// </summary>
            NCACTIVATE = 0x0086,
            /// <summary>
            /// The WM_GETDLGCODE message is sent to the window procedure associated with a control. By default, the system handles all keyboard input to the control; the system interprets certain types of keyboard input as dialog box navigation keys. To override this default behavior, the control can respond to the WM_GETDLGCODE message to indicate the types of input it wants to process itself.
            /// </summary>
            GETDLGCODE = 0x0087,
            /// <summary>
            /// The WM_SYNCPAINT message is used to synchronize painting while avoiding linking independent GUI threads.
            /// </summary>
            SYNCPAINT = 0x0088,
            /// <summary>
            /// The WM_NCMOUSEMOVE message is posted to a window when the cursor is moved within the nonclient area of the window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCMOUSEMOVE = 0x00A0,
            /// <summary>
            /// The WM_NCLBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCLBUTTONDOWN = 0x00A1,
            /// <summary>
            /// The WM_NCLBUTTONUP message is posted when the user releases the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCLBUTTONUP = 0x00A2,
            /// <summary>
            /// The WM_NCLBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCLBUTTONDBLCLK = 0x00A3,
            /// <summary>
            /// The WM_NCRBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCRBUTTONDOWN = 0x00A4,
            /// <summary>
            /// The WM_NCRBUTTONUP message is posted when the user releases the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCRBUTTONUP = 0x00A5,
            /// <summary>
            /// The WM_NCRBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCRBUTTONDBLCLK = 0x00A6,
            /// <summary>
            /// The WM_NCMBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCMBUTTONDOWN = 0x00A7,
            /// <summary>
            /// The WM_NCMBUTTONUP message is posted when the user releases the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCMBUTTONUP = 0x00A8,
            /// <summary>
            /// The WM_NCMBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is within the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCMBUTTONDBLCLK = 0x00A9,
            /// <summary>
            /// The WM_NCXBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCXBUTTONDOWN = 0x00AB,
            /// <summary>
            /// The WM_NCXBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCXBUTTONUP = 0x00AC,
            /// <summary>
            /// The WM_NCXBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the nonclient area of a window. This message is posted to the window that contains the cursor. If a window has captured the mouse, this message is not posted.
            /// </summary>
            NCXBUTTONDBLCLK = 0x00AD,
            /// <summary>
            /// The WM_INPUT_DEVICE_CHANGE message is sent to the window that registered to receive raw input. A window receives this message through its WindowProc function.
            /// </summary>
            INPUT_DEVICE_CHANGE = 0x00FE,
            /// <summary>
            /// The WM_INPUT message is sent to the window that is getting raw input.
            /// </summary>
            INPUT = 0x00FF,
            /// <summary>
            /// This message filters for keyboard messages.
            /// </summary>
            KEYFIRST = 0x0100,
            /// <summary>
            /// The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.
            /// </summary>
            KEYDOWN = 0x0100,
            /// <summary>
            /// The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.
            /// </summary>
            KEYUP = 0x0101,
            /// <summary>
            /// The WM_CHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_CHAR message contains the character code of the key that was pressed.
            /// </summary>
            CHAR = 0x0102,
            /// <summary>
            /// The WM_DEADCHAR message is posted to the window with the keyboard focus when a WM_KEYUP message is translated by the TranslateMessage function. WM_DEADCHAR specifies a character code generated by a dead key. A dead key is a key that generates a character, such as the umlaut (double-dot), that is combined with another character to form a composite character. For example, the umlaut-O character (Ö) is generated by typing the dead key for the umlaut character, and then typing the O key.
            /// </summary>
            DEADCHAR = 0x0103,
            /// <summary>
            /// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user presses the F10 key (which activates the menu bar) or holds down the ALT key and then presses another key. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
            /// </summary>
            SYSKEYDOWN = 0x0104,
            /// <summary>
            /// The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user releases a key that was pressed while the ALT key was held down. It also occurs when no window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent to the active window. The window that receives the message can distinguish between these two contexts by checking the context code in the lParam parameter.
            /// </summary>
            SYSKEYUP = 0x0105,
            /// <summary>
            /// The WM_SYSCHAR message is posted to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. It specifies the character code of a system character key — that is, a character key that is pressed while the ALT key is down.
            /// </summary>
            SYSCHAR = 0x0106,
            /// <summary>
            /// The WM_SYSDEADCHAR message is sent to the window with the keyboard focus when a WM_SYSKEYDOWN message is translated by the TranslateMessage function. WM_SYSDEADCHAR specifies the character code of a system dead key — that is, a dead key that is pressed while holding down the ALT key.
            /// </summary>
            SYSDEADCHAR = 0x0107,
            /// <summary>
            /// The WM_UNICHAR message is posted to the window with the keyboard focus when a WM_KEYDOWN message is translated by the TranslateMessage function. The WM_UNICHAR message contains the character code of the key that was pressed.
            /// The WM_UNICHAR message is equivalent to WM_CHAR, but it uses Unicode Transformation Format (UTF)-32, whereas WM_CHAR uses UTF-16. It is designed to send or post Unicode characters to ANSI windows and it can can handle Unicode Supplementary Plane characters.
            /// </summary>
            UNICHAR = 0x0109,
            /// <summary>
            /// This message filters for keyboard messages.
            /// </summary>
            KEYLAST = 0x0108,
            /// <summary>
            /// Sent immediately before the IME generates the composition string as a result of a keystroke. A window receives this message through its WindowProc function.
            /// </summary>
            IME_STARTCOMPOSITION = 0x010D,
            /// <summary>
            /// Sent to an application when the IME ends composition. A window receives this message through its WindowProc function.
            /// </summary>
            IME_ENDCOMPOSITION = 0x010E,
            /// <summary>
            /// Sent to an application when the IME changes composition status as a result of a keystroke. A window receives this message through its WindowProc function.
            /// </summary>
            IME_COMPOSITION = 0x010F,
            IME_KEYLAST = 0x010F,
            /// <summary>
            /// The WM_INITDIALOG message is sent to the dialog box procedure immediately before a dialog box is displayed. Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box.
            /// </summary>
            INITDIALOG = 0x0110,
            /// <summary>
            /// The WM_COMMAND message is sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated.
            /// </summary>
            COMMAND = 0x0111,
            /// <summary>
            /// A window receives this message when the user chooses a command from the Window menu, clicks the maximize button, minimize button, restore button, close button, or moves the form. You can stop the form from moving by filtering this out.
            /// </summary>
            SYSCOMMAND = 0x0112,
            /// <summary>
            /// The WM_TIMER message is posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function.
            /// </summary>
            TIMER = 0x0113,
            /// <summary>
            /// The WM_HSCROLL message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar. This message is also sent to the owner of a horizontal scroll bar control when a scroll event occurs in the control.
            /// </summary>
            HSCROLL = 0x0114,
            /// <summary>
            /// The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar. This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control.
            /// </summary>
            VSCROLL = 0x0115,
            /// <summary>
            /// The WM_INITMENU message is sent when a menu is about to become active. It occurs when the user clicks an item on the menu bar or presses a menu key. This allows the application to modify the menu before it is displayed.
            /// </summary>
            INITMENU = 0x0116,
            /// <summary>
            /// The WM_INITMENUPOPUP message is sent when a drop-down menu or submenu is about to become active. This allows an application to modify the menu before it is displayed, without changing the entire menu.
            /// </summary>
            INITMENUPOPUP = 0x0117,
            /// <summary>
            /// The WM_MENUSELECT message is sent to a menu's owner window when the user selects a menu item.
            /// </summary>
            MENUSELECT = 0x011F,
            /// <summary>
            /// The WM_MENUCHAR message is sent when a menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key. This message is sent to the window that owns the menu.
            /// </summary>
            MENUCHAR = 0x0120,
            /// <summary>
            /// The WM_ENTERIDLE message is sent to the owner window of a modal dialog box or menu that is entering an idle state. A modal dialog box or menu enters an idle state when no messages are waiting in its queue after it has processed one or more previous messages.
            /// </summary>
            ENTERIDLE = 0x0121,
            /// <summary>
            /// The WM_MENURBUTTONUP message is sent when the user releases the right mouse button while the cursor is on a menu item.
            /// </summary>
            MENURBUTTONUP = 0x0122,
            /// <summary>
            /// The WM_MENUDRAG message is sent to the owner of a drag-and-drop menu when the user drags a menu item.
            /// </summary>
            MENUDRAG = 0x0123,
            /// <summary>
            /// The WM_MENUGETOBJECT message is sent to the owner of a drag-and-drop menu when the mouse cursor enters a menu item or moves from the center of the item to the top or bottom of the item.
            /// </summary>
            MENUGETOBJECT = 0x0124,
            /// <summary>
            /// The WM_UNINITMENUPOPUP message is sent when a drop-down menu or submenu has been destroyed.
            /// </summary>
            UNINITMENUPOPUP = 0x0125,
            /// <summary>
            /// The WM_MENUCOMMAND message is sent when the user makes a selection from a menu.
            /// </summary>
            MENUCOMMAND = 0x0126,
            /// <summary>
            /// An application sends the WM_CHANGEUISTATE message to indicate that the user interface (UI) state should be changed.
            /// </summary>
            CHANGEUISTATE = 0x0127,
            /// <summary>
            /// An application sends the WM_UPDATEUISTATE message to change the user interface (UI) state for the specified window and all its child windows.
            /// </summary>
            UPDATEUISTATE = 0x0128,
            /// <summary>
            /// An application sends the WM_QUERYUISTATE message to retrieve the user interface (UI) state for a window.
            /// </summary>
            QUERYUISTATE = 0x0129,
            /// <summary>
            /// The WM_CTLCOLORMSGBOX message is sent to the owner window of a message box before Windows draws the message box. By responding to this message, the owner window can set the text and background colors of the message box by using the given display device context handle.
            /// </summary>
            CTLCOLORMSGBOX = 0x0132,
            /// <summary>
            /// An edit control that is not read-only or disabled sends the WM_CTLCOLOREDIT message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the edit control.
            /// </summary>
            CTLCOLOREDIT = 0x0133,
            /// <summary>
            /// Sent to the parent window of a list box before the system draws the list box. By responding to this message, the parent window can set the text and background colors of the list box by using the specified display device context handle.
            /// </summary>
            CTLCOLORLISTBOX = 0x0134,
            /// <summary>
            /// The WM_CTLCOLORBTN message is sent to the parent window of a button before drawing the button. The parent window can change the button's text and background colors. However, only owner-drawn buttons respond to the parent window processing this message.
            /// </summary>
            CTLCOLORBTN = 0x0135,
            /// <summary>
            /// The WM_CTLCOLORDLG message is sent to a dialog box before the system draws the dialog box. By responding to this message, the dialog box can set its text and background colors using the specified display device context handle.
            /// </summary>
            CTLCOLORDLG = 0x0136,
            /// <summary>
            /// The WM_CTLCOLORSCROLLBAR message is sent to the parent window of a scroll bar control when the control is about to be drawn. By responding to this message, the parent window can use the display context handle to set the background color of the scroll bar control.
            /// </summary>
            CTLCOLORSCROLLBAR = 0x0137,
            /// <summary>
            /// A static control, or an edit control that is read-only or disabled, sends the WM_CTLCOLORSTATIC message to its parent window when the control is about to be drawn. By responding to this message, the parent window can use the specified device context handle to set the text and background colors of the static control.
            /// </summary>
            CTLCOLORSTATIC = 0x0138,
            /// <summary>
            /// Use WM_MOUSEFIRST to specify the first mouse message. Use the PeekMessage() Function.
            /// </summary>
            MOUSEFIRST = 0x0200,
            /// <summary>
            /// The WM_MOUSEMOVE message is posted to a window when the cursor moves. If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            MOUSEMOVE = 0x0200,
            /// <summary>
            /// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            LBUTTONDOWN = 0x0201,
            /// <summary>
            /// The WM_LBUTTONUP message is posted when the user releases the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            LBUTTONUP = 0x0202,
            /// <summary>
            /// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            LBUTTONDBLCLK = 0x0203,
            /// <summary>
            /// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            RBUTTONDOWN = 0x0204,
            /// <summary>
            /// The WM_RBUTTONUP message is posted when the user releases the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            RBUTTONUP = 0x0205,
            /// <summary>
            /// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            RBUTTONDBLCLK = 0x0206,
            /// <summary>
            /// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            MBUTTONDOWN = 0x0207,
            /// <summary>
            /// The WM_MBUTTONUP message is posted when the user releases the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            MBUTTONUP = 0x0208,
            /// <summary>
            /// The WM_MBUTTONDBLCLK message is posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            MBUTTONDBLCLK = 0x0209,
            /// <summary>
            /// The WM_MOUSEWHEEL message is sent to the focus window when the mouse wheel is rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
            /// </summary>
            MOUSEWHEEL = 0x020A,
            /// <summary>
            /// The WM_XBUTTONDOWN message is posted when the user presses the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            XBUTTONDOWN = 0x020B,
            /// <summary>
            /// The WM_XBUTTONUP message is posted when the user releases the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            XBUTTONUP = 0x020C,
            /// <summary>
            /// The WM_XBUTTONDBLCLK message is posted when the user double-clicks the first or second X button while the cursor is in the client area of a window. If the mouse is not captured, the message is posted to the window beneath the cursor. Otherwise, the message is posted to the window that has captured the mouse.
            /// </summary>
            XBUTTONDBLCLK = 0x020D,
            /// <summary>
            /// The WM_MOUSEHWHEEL message is sent to the focus window when the mouse's horizontal scroll wheel is tilted or rotated. The DefWindowProc function propagates the message to the window's parent. There should be no internal forwarding of the message, since DefWindowProc propagates it up the parent chain until it finds a window that processes it.
            /// </summary>
            MOUSEHWHEEL = 0x020E,
            /// <summary>
            /// Use WM_MOUSELAST to specify the last mouse message. Used with PeekMessage() Function.
            /// </summary>
            MOUSELAST = 0x020E,
            /// <summary>
            /// The WM_PARENTNOTIFY message is sent to the parent of a child window when the child window is created or destroyed, or when the user clicks a mouse button while the cursor is over the child window. When the child window is being created, the system sends WM_PARENTNOTIFY just before the CreateWindow or CreateWindowEx function that creates the window returns. When the child window is being destroyed, the system sends the message before any processing to destroy the window takes place.
            /// </summary>
            PARENTNOTIFY = 0x0210,
            /// <summary>
            /// The WM_ENTERMENULOOP message informs an application's main window procedure that a menu modal loop has been entered.
            /// </summary>
            ENTERMENULOOP = 0x0211,
            /// <summary>
            /// The WM_EXITMENULOOP message informs an application's main window procedure that a menu modal loop has been exited.
            /// </summary>
            EXITMENULOOP = 0x0212,
            /// <summary>
            /// The WM_NEXTMENU message is sent to an application when the right or left arrow key is used to switch between the menu bar and the system menu.
            /// </summary>
            NEXTMENU = 0x0213,
            /// <summary>
            /// The WM_SIZING message is sent to a window that the user is resizing. By processing this message, an application can monitor the size and position of the drag rectangle and, if needed, change its size or position.
            /// </summary>
            SIZING = 0x0214,
            /// <summary>
            /// The WM_CAPTURECHANGED message is sent to the window that is losing the mouse capture.
            /// </summary>
            CAPTURECHANGED = 0x0215,
            /// <summary>
            /// The WM_MOVING message is sent to a window that the user is moving. By processing this message, an application can monitor the position of the drag rectangle and, if needed, change its position.
            /// </summary>
            MOVING = 0x0216,
            /// <summary>
            /// Notifies applications that a power-management event has occurred.
            /// </summary>
            POWERBROADCAST = 0x0218,
            /// <summary>
            /// Notifies an application of a change to the hardware configuration of a device or the computer.
            /// </summary>
            DEVICECHANGE = 0x0219,
            /// <summary>
            /// An application sends the WM_MDICREATE message to a multiple-document interface (MDI) client window to create an MDI child window.
            /// </summary>
            MDICREATE = 0x0220,
            /// <summary>
            /// An application sends the WM_MDIDESTROY message to a multiple-document interface (MDI) client window to close an MDI child window.
            /// </summary>
            MDIDESTROY = 0x0221,
            /// <summary>
            /// An application sends the WM_MDIACTIVATE message to a multiple-document interface (MDI) client window to instruct the client window to activate a different MDI child window.
            /// </summary>
            MDIACTIVATE = 0x0222,
            /// <summary>
            /// An application sends the WM_MDIRESTORE message to a multiple-document interface (MDI) client window to restore an MDI child window from maximized or minimized size.
            /// </summary>
            MDIRESTORE = 0x0223,
            /// <summary>
            /// An application sends the WM_MDINEXT message to a multiple-document interface (MDI) client window to activate the next or previous child window.
            /// </summary>
            MDINEXT = 0x0224,
            /// <summary>
            /// An application sends the WM_MDIMAXIMIZE message to a multiple-document interface (MDI) client window to maximize an MDI child window. The system resizes the child window to make its client area fill the client window. The system places the child window's window menu icon in the rightmost position of the frame window's menu bar, and places the child window's restore icon in the leftmost position. The system also appends the title bar text of the child window to that of the frame window.
            /// </summary>
            MDIMAXIMIZE = 0x0225,
            /// <summary>
            /// An application sends the WM_MDITILE message to a multiple-document interface (MDI) client window to arrange all of its MDI child windows in a tile format.
            /// </summary>
            MDITILE = 0x0226,
            /// <summary>
            /// An application sends the WM_MDICASCADE message to a multiple-document interface (MDI) client window to arrange all its child windows in a cascade format.
            /// </summary>
            MDICASCADE = 0x0227,
            /// <summary>
            /// An application sends the WM_MDIICONARRANGE message to a multiple-document interface (MDI) client window to arrange all minimized MDI child windows. It does not affect child windows that are not minimized.
            /// </summary>
            MDIICONARRANGE = 0x0228,
            /// <summary>
            /// An application sends the WM_MDIGETACTIVE message to a multiple-document interface (MDI) client window to retrieve the handle to the active MDI child window.
            /// </summary>
            MDIGETACTIVE = 0x0229,
            /// <summary>
            /// An application sends the WM_MDISETMENU message to a multiple-document interface (MDI) client window to replace the entire menu of an MDI frame window, to replace the window menu of the frame window, or both.
            /// </summary>
            MDISETMENU = 0x0230,
            /// <summary>
            /// The WM_ENTERSIZEMOVE message is sent one time to a window after it enters the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
            /// The system sends the WM_ENTERSIZEMOVE message regardless of whether the dragging of full windows is enabled.
            /// </summary>
            ENTERSIZEMOVE = 0x0231,
            /// <summary>
            /// The WM_EXITSIZEMOVE message is sent one time to a window, after it has exited the moving or sizing modal loop. The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border, or when the window passes the WM_SYSCOMMAND message to the DefWindowProc function and the wParam parameter of the message specifies the SC_MOVE or SC_SIZE value. The operation is complete when DefWindowProc returns.
            /// </summary>
            EXITSIZEMOVE = 0x0232,
            /// <summary>
            /// Sent when the user drops a file on the window of an application that has registered itself as a recipient of dropped files.
            /// </summary>
            DROPFILES = 0x0233,
            /// <summary>
            /// An application sends the WM_MDIREFRESHMENU message to a multiple-document interface (MDI) client window to refresh the window menu of the MDI frame window.
            /// </summary>
            MDIREFRESHMENU = 0x0234,
            /// <summary>
            /// Sent to an application when a window is activated. A window receives this message through its WindowProc function.
            /// </summary>
            IME_SETCONTEXT = 0x0281,
            /// <summary>
            /// Sent to an application to notify it of changes to the IME window. A window receives this message through its WindowProc function.
            /// </summary>
            IME_NOTIFY = 0x0282,
            /// <summary>
            /// Sent by an application to direct the IME window to carry out the requested command. The application uses this message to control the IME window that it has created. To send this message, the application calls the SendMessage function with the following parameters.
            /// </summary>
            IME_CONTROL = 0x0283,
            /// <summary>
            /// Sent to an application when the IME window finds no space to extend the area for the composition window. A window receives this message through its WindowProc function.
            /// </summary>
            IME_COMPOSITIONFULL = 0x0284,
            /// <summary>
            /// Sent to an application when the operating system is about to change the current IME. A window receives this message through its WindowProc function.
            /// </summary>
            IME_SELECT = 0x0285,
            /// <summary>
            /// Sent to an application when the IME gets a character of the conversion result. A window receives this message through its WindowProc function.
            /// </summary>
            IME_CHAR = 0x0286,
            /// <summary>
            /// Sent to an application to provide commands and request information. A window receives this message through its WindowProc function.
            /// </summary>
            IME_REQUEST = 0x0288,
            /// <summary>
            /// Sent to an application by the IME to notify the application of a key press and to keep message order. A window receives this message through its WindowProc function.
            /// </summary>
            IME_KEYDOWN = 0x0290,
            /// <summary>
            /// Sent to an application by the IME to notify the application of a key release and to keep message order. A window receives this message through its WindowProc function.
            /// </summary>
            IME_KEYUP = 0x0291,
            /// <summary>
            /// The WM_MOUSEHOVER message is posted to a window when the cursor hovers over the client area of the window for the period of time specified in a prior call to TrackMouseEvent.
            /// </summary>
            MOUSEHOVER = 0x02A1,
            /// <summary>
            /// The WM_MOUSELEAVE message is posted to a window when the cursor leaves the client area of the window specified in a prior call to TrackMouseEvent.
            /// </summary>
            MOUSELEAVE = 0x02A3,
            /// <summary>
            /// The WM_NCMOUSEHOVER message is posted to a window when the cursor hovers over the nonclient area of the window for the period of time specified in a prior call to TrackMouseEvent.
            /// </summary>
            NCMOUSEHOVER = 0x02A0,
            /// <summary>
            /// The WM_NCMOUSELEAVE message is posted to a window when the cursor leaves the nonclient area of the window specified in a prior call to TrackMouseEvent.
            /// </summary>
            NCMOUSELEAVE = 0x02A2,
            /// <summary>
            /// The WM_WTSSESSION_CHANGE message notifies applications of changes in session state.
            /// </summary>
            WTSSESSION_CHANGE = 0x02B1,
            TABLET_FIRST = 0x02c0,
            TABLET_LAST = 0x02df,
            /// <summary>
            /// An application sends a WM_CUT message to an edit control or combo box to delete (cut) the current selection, if any, in the edit control and copy the deleted text to the clipboard in CF_TEXT format.
            /// </summary>
            CUT = 0x0300,
            /// <summary>
            /// An application sends the WM_COPY message to an edit control or combo box to copy the current selection to the clipboard in CF_TEXT format.
            /// </summary>
            COPY = 0x0301,
            /// <summary>
            /// An application sends a WM_PASTE message to an edit control or combo box to copy the current content of the clipboard to the edit control at the current caret position. Data is inserted only if the clipboard contains data in CF_TEXT format.
            /// </summary>
            PASTE = 0x0302,
            /// <summary>
            /// An application sends a WM_CLEAR message to an edit control or combo box to delete (clear) the current selection, if any, from the edit control.
            /// </summary>
            CLEAR = 0x0303,
            /// <summary>
            /// An application sends a WM_UNDO message to an edit control to undo the last operation. When this message is sent to an edit control, the previously deleted text is restored or the previously added text is deleted.
            /// </summary>
            UNDO = 0x0304,
            /// <summary>
            /// The WM_RENDERFORMAT message is sent to the clipboard owner if it has delayed rendering a specific clipboard format and if an application has requested data in that format. The clipboard owner must render data in the specified format and place it on the clipboard by calling the SetClipboardData function.
            /// </summary>
            RENDERFORMAT = 0x0305,
            /// <summary>
            /// The WM_RENDERALLFORMATS message is sent to the clipboard owner before it is destroyed, if the clipboard owner has delayed rendering one or more clipboard formats. For the content of the clipboard to remain available to other applications, the clipboard owner must render data in all the formats it is capable of generating, and place the data on the clipboard by calling the SetClipboardData function.
            /// </summary>
            RENDERALLFORMATS = 0x0306,
            /// <summary>
            /// The WM_DESTROYCLIPBOARD message is sent to the clipboard owner when a call to the EmptyClipboard function empties the clipboard.
            /// </summary>
            DESTROYCLIPBOARD = 0x0307,
            /// <summary>
            /// The WM_DRAWCLIPBOARD message is sent to the first window in the clipboard viewer chain when the content of the clipboard changes. This enables a clipboard viewer window to display the new content of the clipboard.
            /// </summary>
            DRAWCLIPBOARD = 0x0308,
            /// <summary>
            /// The WM_PAINTCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area needs repainting.
            /// </summary>
            PAINTCLIPBOARD = 0x0309,
            /// <summary>
            /// The WM_VSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's vertical scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
            /// </summary>
            VSCROLLCLIPBOARD = 0x030A,
            /// <summary>
            /// The WM_SIZECLIPBOARD message is sent to the clipboard owner by a clipboard viewer window when the clipboard contains data in the CF_OWNERDISPLAY format and the clipboard viewer's client area has changed size.
            /// </summary>
            SIZECLIPBOARD = 0x030B,
            /// <summary>
            /// The WM_ASKCBFORMATNAME message is sent to the clipboard owner by a clipboard viewer window to request the name of a CF_OWNERDISPLAY clipboard format.
            /// </summary>
            ASKCBFORMATNAME = 0x030C,
            /// <summary>
            /// The WM_CHANGECBCHAIN message is sent to the first window in the clipboard viewer chain when a window is being removed from the chain.
            /// </summary>
            CHANGECBCHAIN = 0x030D,
            /// <summary>
            /// The WM_HSCROLLCLIPBOARD message is sent to the clipboard owner by a clipboard viewer window. This occurs when the clipboard contains data in the CF_OWNERDISPLAY format and an event occurs in the clipboard viewer's horizontal scroll bar. The owner should scroll the clipboard image and update the scroll bar values.
            /// </summary>
            HSCROLLCLIPBOARD = 0x030E,
            /// <summary>
            /// This message informs a window that it is about to receive the keyboard focus, giving the window the opportunity to realize its logical palette when it receives the focus.
            /// </summary>
            QUERYNEWPALETTE = 0x030F,
            /// <summary>
            /// The WM_PALETTEISCHANGING message informs applications that an application is going to realize its logical palette.
            /// </summary>
            PALETTEISCHANGING = 0x0310,
            /// <summary>
            /// This message is sent by the OS to all top-level and overlapped windows after the window with the keyboard focus realizes its logical palette.
            /// This message enables windows that do not have the keyboard focus to realize their logical palettes and update their client areas.
            /// </summary>
            PALETTECHANGED = 0x0311,
            /// <summary>
            /// The WM_HOTKEY message is posted when the user presses a hot key registered by the RegisterHotKey function. The message is placed at the top of the message queue associated with the thread that registered the hot key.
            /// </summary>
            HOTKEY = 0x0312,
            /// <summary>
            /// The WM_PRINT message is sent to a window to request that it draw itself in the specified device context, most commonly in a printer device context.
            /// </summary>
            PRINT = 0x0317,
            /// <summary>
            /// The WM_PRINTCLIENT message is sent to a window to request that it draw its client area in the specified device context, most commonly in a printer device context.
            /// </summary>
            PRINTCLIENT = 0x0318,
            /// <summary>
            /// The WM_APPCOMMAND message notifies a window that the user generated an application command event, for example, by clicking an application command button using the mouse or typing an application command key on the keyboard.
            /// </summary>
            APPCOMMAND = 0x0319,
            /// <summary>
            /// The WM_THEMECHANGED message is broadcast to every window following a theme change event. Examples of theme change events are the activation of a theme, the deactivation of a theme, or a transition from one theme to another.
            /// </summary>
            THEMECHANGED = 0x031A,
            /// <summary>
            /// Sent when the contents of the clipboard have changed.
            /// </summary>
            CLIPBOARDUPDATE = 0x031D,
            /// <summary>
            /// The system will send a window the WM_DWMCOMPOSITIONCHANGED message to indicate that the availability of desktop composition has changed.
            /// </summary>
            DWMCOMPOSITIONCHANGED = 0x031E,
            /// <summary>
            /// WM_DWMNCRENDERINGCHANGED is called when the non-client area rendering status of a window has changed. Only windows that have set the flag DWM_BLURBEHIND.fTransitionOnMaximized to true will get this message.
            /// </summary>
            DWMNCRENDERINGCHANGED = 0x031F,
            /// <summary>
            /// Sent to all top-level windows when the colorization color has changed.
            /// </summary>
            DWMCOLORIZATIONCOLORCHANGED = 0x0320,
            /// <summary>
            /// WM_DWMWINDOWMAXIMIZEDCHANGE will let you know when a DWM composed window is maximized. You also have to register for this message as well. You'd have other windowd go opaque when this message is sent.
            /// </summary>
            DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
            /// <summary>
            /// Sent to request extended title bar information. A window receives this message through its WindowProc function.
            /// </summary>
            GETTITLEBARINFOEX = 0x033F,
            HANDHELDFIRST = 0x0358,
            HANDHELDLAST = 0x035F,
            AFXFIRST = 0x0360,
            AFXLAST = 0x037F,
            PENWINFIRST = 0x0380,
            PENWINLAST = 0x038F,
            /// <summary>
            /// The WM_APP constant is used by applications to help define private messages, usually of the form WM_APP+X, where X is an integer value.
            /// </summary>
            APP = 0x8000,
            /// <summary>
            /// The WM_USER constant is used by applications to help define private messages for use by private window classes, usually of the form WM_USER+X, where X is an integer value.
            /// </summary>
            USER = 0x0400,

            /// <summary>
            /// An application sends the WM_CPL_LAUNCH message to Windows Control Panel to request that a Control Panel application be started.
            /// </summary>
            CPL_LAUNCH = USER + 0x1000,
            /// <summary>
            /// The WM_CPL_LAUNCHED message is sent when a Control Panel application, started by the WM_CPL_LAUNCH message, has closed. The WM_CPL_LAUNCHED message is sent to the window identified by the wParam parameter of the WM_CPL_LAUNCH message that started the application.
            /// </summary>
            CPL_LAUNCHED = USER + 0x1001,
            /// <summary>
            /// WM_SYSTIMER is a well-known yet still undocumented message. Windows uses WM_SYSTIMER for internal actions like scrolling.
            /// </summary>
            SYSTIMER = 0x118,

            /// <summary>
            /// The accessibility state has changed.
            /// </summary>
            HSHELL_ACCESSIBILITYSTATE = 11,
            /// <summary>
            /// The shell should activate its main window.
            /// </summary>
            HSHELL_ACTIVATESHELLWINDOW = 3,
            /// <summary>
            /// The user completed an input event (for example, pressed an application command button on the mouse or an application command key on the keyboard), and the application did not handle the WM_APPCOMMAND message generated by that input.
            /// If the Shell procedure handles the WM_COMMAND message, it should not call CallNextHookEx. See the Return Value section for more information.
            /// </summary>
            HSHELL_APPCOMMAND = 12,
            /// <summary>
            /// A window is being minimized or maximized. The system needs the coordinates of the minimized rectangle for the window.
            /// </summary>
            HSHELL_GETMINRECT = 5,
            /// <summary>
            /// Keyboard language was changed or a new keyboard layout was loaded.
            /// </summary>
            HSHELL_LANGUAGE = 8,
            /// <summary>
            /// The title of a window in the task bar has been redrawn.
            /// </summary>
            HSHELL_REDRAW = 6,
            /// <summary>
            /// The user has selected the task list. A shell application that provides a task list should return TRUE to prevent Windows from starting its task list.
            /// </summary>
            HSHELL_TASKMAN = 7,
            /// <summary>
            /// A top-level, unowned window has been created. The window exists when the system calls this hook.
            /// </summary>
            HSHELL_WINDOWCREATED = 1,
            /// <summary>
            /// A top-level, unowned window is about to be destroyed. The window still exists when the system calls this hook.
            /// </summary>
            HSHELL_WINDOWDESTROYED = 2,
            /// <summary>
            /// The activation has changed to a different top-level, unowned window.
            /// </summary>
            HSHELL_WINDOWACTIVATED = 4,
            /// <summary>
            /// A top-level window is being replaced. The window exists when the system calls this hook.
            /// </summary>
            HSHELL_WINDOWREPLACED = 13
        }
    }
}
