using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Tools;
using WeAutoCommon.Simulator;
using RapidOCRLib;
using Emgu.CV.OCR;
using WeAutoCommon.Configs;
using System.Threading.Tasks;
using RapidOCRLib.Models;
using Emgu.CV;
using Emgu.CV.Structure;
using WeChatAuto.Utils;
using System.Linq;
using FlaUI.Core.Input;
using WeAutoCommon.Utils;
using System.Drawing.Imaging;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace WeChatAuto.Services
{
	public class OCRService
	{
		private OcrLite _OcrEngin = null;
		private Semaphore semaphore = new Semaphore(1, 1);
		public OcrLite OcrEngin
		{
			get
			{
				if (_OcrEngin == null)
					throw new Exception("请先初始化OCR,请按下面步骤检查：1. 模型文件是否正确下载. 2. 是否在WecChatConfig中配置好模型位置.3.是否初始化模型引擎.");
				return _OcrEngin;
			}
		}

		/// <summary>
		/// 通过自动化元素获取OpenCV的Mat对象
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		public Mat GetMatFromElement(AutomationElement element)
		{
			element.Stability();
			var bmp = element.Capture();
			using Bitmap clone = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			using (Graphics g = Graphics.FromImage(clone))
			{
				g.DrawImage(bmp, 0, 0);
			}
			Mat mat = clone.ToMat();
			return mat;
		}
		public Mat GetMatFromBitmap(Bitmap bmp)
		{
			using Bitmap clone = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			using (Graphics g = Graphics.FromImage(clone))
			{
				g.DrawImage(bmp, 0, 0);
			}
			Mat mat = clone.ToMat();
			return mat;
		}
		/// <summary>
		/// OCR识别，找出特定文字的中心坐标,此坐标针对屏幕
		/// 仅支持竖向方向的截取
		/// </summary>
		/// <param name="rootElement">基础自动化元素</param>
		/// <param name="rioRadio">坚向方向的截取比例，如0.5,只取原因上半部分</param>
		/// <param name="findStr">找寻的字符串</param>
		/// <param name="isTest">是否测试</param>
		/// <param name="Index">索引，如果返回字符串有多个,以第几个为准</param>
		/// <param name="retryNumber">如果没有找到，重试次数</param>
		/// <returns></returns>
		public Point OCRVerticalDetect(AutomationElement rootElement, float rioRadio, string findStr, bool isTest = false, int Index = 0, int retryNumber = 2)
		{
			using Mat mat1 = GetMatFromElement(rootElement);
			Rectangle rio = new Rectangle(0, 0, mat1.Width, (int)(mat1.Height * rioRadio));
			using Mat mat2 = new Mat(mat1, rio);
			using Bitmap bitmap = mat2.ToBitmap();
			var index = 0;
			while (index < retryNumber)
			{
				OcrResult result = Detect(bitmap, 0, mat2.Width > mat2.Height ? mat2.Width : mat2.Height, 0.5f, 0.5f, 1.6f, false, false);
				var blockTexts = result.TextBlocks.Where(x => !string.IsNullOrWhiteSpace(x.Text) && (x.Text.Contains(findStr) || findStr.Contains(x.Text)));
				if (blockTexts.Count() > 0)
				{
					var count = 0;
					foreach (var item in blockTexts)
					{
						if (count == index)
						{
							var pointList = item.BoxPoints;
							var point = new Point(((pointList[2].X - pointList[0].X) / 2 + pointList[0].X), ((pointList[2].Y - pointList[0].Y) / 2) + pointList[0].Y);
							if (isTest)
							{
								CvInvoke.Imshow("wechatauto.sdk", result.BoxImg);
								CvInvoke.WaitKey();
							}
							return new Point(rootElement.BoundingRectangle.X + point.X, rootElement.BoundingRectangle.Y + point.Y);
						}
						count++;
					}
				}
				else
				{
					index++;
				}
			}
			return Point.Empty;
		}

		/// <summary>
		/// OCR识别，找出特定文字的中心坐标,此坐标针对屏幕
		/// 仅支持竖向方向的截取
		/// </summary>
		/// <param name="rootRect">基础自动化元素的Rectangle</param>
		/// <param name="rioRadio">坚向方向的截取比例，如0.5,只取原因上半部分</param>
		/// <param name="findStr">找寻的字符串</param>
		/// <param name="isTest">是否测试</param>
		/// <param name="Index">索引，如果返回字符串有多个,以第几个为准</param>
		/// <param name="retryNumber">如果没有找到，重试次数</param>
		/// <returns></returns>
		public Point OCRVerticalDetect(Rectangle rootRect, float rioRadio, string findStr, bool isTest = false, int Index = 0, int retryNumber = 2)
		{
			using Bitmap rootBitmap = CaptureRect(rootRect);
			using Mat mat1 = rootBitmap.ToMat();
			Rectangle rio = new Rectangle(0, 0, mat1.Width, (int)(mat1.Height * rioRadio));
			using Mat mat2 = new Mat(mat1, rio);
			using Bitmap bitmap = mat2.ToBitmap();
			var index = 0;
			while (index < retryNumber)
			{
				OcrResult result = Detect(bitmap, 0, mat2.Width > mat2.Height ? mat2.Width : mat2.Height, 0.5f, 0.5f, 1.6f, false, false);
				var blockTexts = result.TextBlocks.Where(x => !string.IsNullOrWhiteSpace(x.Text) && (x.Text.Contains(findStr) || findStr.Contains(x.Text)));
				if (blockTexts.Count() > 0)
				{
					var count = 0;
					foreach (var item in blockTexts)
					{
						if (count == index)
						{
							var pointList = item.BoxPoints;
							var point = new Point(((pointList[2].X - pointList[0].X) / 2 + pointList[0].X), ((pointList[2].Y - pointList[0].Y) / 2) + pointList[0].Y);
							if (isTest)
							{
								CvInvoke.Imshow("wechatauto.sdk", result.BoxImg);
								CvInvoke.WaitKey();
							}
							return new Point(rootRect.X + point.X, rootRect.Y + point.Y);
						}
						count++;
					}
				}
				else
				{
					index++;
				}
			}
			return Point.Empty;
		}

		public Point OCRVerticalLeftCuttingDetect(AutomationElement rootElement, float rioRadio, int leftCutting, string findStr, bool isTest = false, int Index = 0, int retryNumber = 2)
		{
			using Mat mat1 = GetMatFromElement(rootElement);
			Rectangle rio = new Rectangle(leftCutting, 0, mat1.Width - leftCutting, (int)(mat1.Height * rioRadio));
			using Mat mat2 = new Mat(mat1, rio);
			using Bitmap bitmap = mat2.ToBitmap();
			var index = 0;
			while (index < retryNumber)
			{
				OcrResult result = Detect(bitmap, 0, mat2.Width > mat2.Height ? mat2.Width : mat2.Height, 0.5f, 0.5f, 1.6f, false, false);
				var blockTexts = result.TextBlocks.Where(x => !string.IsNullOrWhiteSpace(x.Text) && (x.Text.Contains(findStr) || findStr.Contains(x.Text)));
				if (blockTexts.Count() > 0)
				{
					var count = 0;
					foreach (var item in blockTexts)
					{
						if (count == index)
						{
							var pointList = item.BoxPoints;
							var point = new Point(((pointList[2].X - pointList[0].X) / 2 + pointList[0].X), ((pointList[2].Y - pointList[0].Y) / 2) + pointList[0].Y);
							if (isTest)
							{
								CvInvoke.Imshow("wechatauto.sdk", result.BoxImg);
								CvInvoke.WaitKey();
							}
							return new Point(rootElement.BoundingRectangle.X + point.X + leftCutting, rootElement.BoundingRectangle.Y + point.Y);
						}
						count++;
					}
				}
				else
				{
					index++;
				}
			}
			return Point.Empty;
		}

		public Point OCRVerticalLeftCuttingDetect(Rectangle rootRect, float rioRadio, int leftCutting, string findStr, bool isTest = false, int Index = 0, int retryNumber = 2)
		{
			using Bitmap rootBitmap = CaptureRect(rootRect);
			using Mat mat1 = rootBitmap.ToMat();
			Rectangle rio = new Rectangle(leftCutting, 0, mat1.Width - leftCutting, (int)(mat1.Height * rioRadio));
			using Mat mat2 = new Mat(mat1, rio);
			using Bitmap bitmap = mat2.ToBitmap();
			var index = 0;
			while (index < retryNumber)
			{
				OcrResult result = Detect(bitmap, 0, mat2.Width > mat2.Height ? mat2.Width : mat2.Height, 0.5f, 0.5f, 1.6f, false, false);
				var blockTexts = result.TextBlocks.Where(x => !string.IsNullOrWhiteSpace(x.Text) && (x.Text.Contains(findStr) || findStr.Contains(x.Text)));
				if (blockTexts.Count() > 0)
				{
					var count = 0;
					foreach (var item in blockTexts)
					{
						if (count == index)
						{
							var pointList = item.BoxPoints;
							var point = new Point(((pointList[2].X - pointList[0].X) / 2 + pointList[0].X), ((pointList[2].Y - pointList[0].Y) / 2) + pointList[0].Y);
							if (isTest)
							{
								CvInvoke.Imshow("wechatauto.sdk", result.BoxImg);
								CvInvoke.WaitKey();
							}
							return new Point(rootRect.X + point.X + leftCutting, rootRect.Y + point.Y);
						}
						count++;
					}
				}
				else
				{
					index++;
				}
			}
			return Point.Empty;
		}

		//从屏幕剪切一个区域.
		public static Bitmap CaptureRect(Rectangle rect)
		{
			Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format24bppRgb);

			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.CopyFromScreen(
					rect.Left,
					rect.Top,
					0,
					0,
					rect.Size,
					CopyPixelOperation.SourceCopy);
			}

			return bmp;
		}


		//屏步进行ocr识别
		public async Task<OcrResult> DetectAsync(Bitmap bitmap, int padding = 0, int maxSideLen = 1024, float boxScoreThresh = 0.5f, float boxThresh = 0.3f,
			float unClipRatio = 1.6f, bool doAngle = true, bool mostAngle = false, bool isTest = false)
		{
			semaphore.WaitOne();
			try
			{
				var result = await OcrEngin.DetectAsync(bitmap, padding, maxSideLen, boxScoreThresh, boxThresh, unClipRatio, doAngle, mostAngle);
				if (isTest)
				{
					CvInvoke.Imshow("wechatauto.sdk", result.BoxImg);
					CvInvoke.WaitKey();
				}
				return result;
			}
			finally
			{
				semaphore.Release();
			}
		}
		//同步进行ocr识别
		public OcrResult Detect(Bitmap bitmap, int padding = 0, int maxSideLen = 1024, float boxScoreThresh = 0.5f, float boxThresh = 0.3f,
			float unClipRatio = 1.6f, bool doAngle = true, bool mostAngle = false, bool isTest = false)
		{
			semaphore.WaitOne();
			try
			{
				var result = OcrEngin.Detect(bitmap, padding, maxSideLen, boxScoreThresh, boxThresh, unClipRatio, doAngle, mostAngle);
				if (isTest)
				{
					CvInvoke.Imshow("wechatauto.sdk", result.BoxImg);
					CvInvoke.WaitKey();
				}
				return result;
			}
			finally
			{
				semaphore.Release();
			}
		}

		#region 针对微信昵称的OCR前置处理算法
		// /// <summary>
		// /// 对图像进行预处理，去除极宽空白并增强文字对比度
		// /// </summary>
		// /// <param name="src">源图</param>
		// /// <param name="padding">裁剪后四周保留的像素 padding</param>
		// /// <returns>处理后的 Mat 对象</returns>
		// public Mat ProcessImageForOcr(Mat src, int padding = 15)
		// {
		// 	// 2. 转为灰度图
		// 	using Mat gray = new Mat();
		// 	CvInvoke.CvtColor(src, gray, ColorConversion.Bgr2Gray);

		// 	// 3. 增强对比度（使用 CLAHE 自适应直方图均衡化）
		// 	using Mat enhancedGray = new Mat();
		// 	CvInvoke.CLAHE(gray, 3.0, new Size(8, 8), enhancedGray);

		// 	// 4. 反向二值化，寻找文字的有效外框
		// 	using Mat binary = new Mat();
		// 	CvInvoke.Threshold(gray, binary, 230, 255, ThresholdType.BinaryInv);

		// 	// 5. 寻找轮廓，确定文字区域边界
		// 	using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
		// 	using (Mat hierarchy = new Mat())
		// 	{
		// 		CvInvoke.FindContours(binary, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

		// 		Rectangle boundingBox = Rectangle.Empty;
		// 		for (int i = 0; i < contours.Size; i++)
		// 		{
		// 			Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);

		// 			// 过滤噪点（面积过小的忽略）
		// 			if (rect.Width * rect.Height < 10)
		// 				continue;

		// 			if (boundingBox.IsEmpty)
		// 			{
		// 				boundingBox = rect;
		// 			}
		// 			else
		// 			{
		// 				boundingBox = Rectangle.Union(boundingBox, rect);
		// 			}
		// 		}

		// 		// 如果没找到有效文字区，返回增强后的灰度图
		// 		if (boundingBox.IsEmpty)
		// 		{
		// 			return enhancedGray.Clone();
		// 		}

		// 		// 6. 给裁剪框加上适当 Padding
		// 		int x = Math.Max(0, boundingBox.X - padding);
		// 		int y = Math.Max(0, boundingBox.Y - padding);
		// 		int width = Math.Min(src.Width - x, boundingBox.Width + padding * 2);
		// 		int height = Math.Min(src.Height - y, boundingBox.Height + padding * 2);

		// 		Rectangle cropRect = new Rectangle(x, y, width, height);

		// 		// 7. 在增强后的灰度图上裁剪出文字区域
		// 		using Mat croppedMat = new Mat(enhancedGray, cropRect);
		// 		Mat result = croppedMat.Clone();

		// 		return result;
		// 	}
		// }

		/// <summary>
		/// 对图像进行预处理：仅切除无用极宽空白，保持文字原始清晰度与笔画边缘
		/// </summary>
		/// <param name="src">源图（彩色或灰度 Mat）</param>
		/// <param name="padding">裁剪后四周保留的像素 padding</param>
		/// <returns>处理后的 Mat 对象</returns>
		public Mat ProcessImageForOcr(Mat src, int padding = 15)
		{
			// 1. 转为灰度图（仅用于寻找文字轮廓定位，不影响输出图像）
			using Mat gray = new Mat();
			CvInvoke.CvtColor(src, gray, ColorConversion.Bgr2Gray);

			// 2. 反向二值化，把浅背景变黑、浅灰文字变白，方便寻框
			// 阈值 230 可保证浅灰文字被完整捕捉
			using Mat binary = new Mat();
			CvInvoke.Threshold(gray, binary, 230, 255, ThresholdType.BinaryInv);

			// 3. 寻找轮廓，确定文字区域有效边界
			using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
			using (Mat hierarchy = new Mat())
			{
				CvInvoke.FindContours(binary, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);

				Rectangle boundingBox = Rectangle.Empty;
				for (int i = 0; i < contours.Size; i++)
				{
					Rectangle rect = CvInvoke.BoundingRectangle(contours[i]);

					// 过滤极小噪点（面积小于 10 像素的忽略）
					if (rect.Width * rect.Height < 10)
						continue;

					if (boundingBox.IsEmpty)
					{
						boundingBox = rect;
					}
					else
					{
						boundingBox = Rectangle.Union(boundingBox, rect);
					}
				}

				// 如果没找到有效文字区，直接返回原图副本
				if (boundingBox.IsEmpty)
				{
					return src.Clone();
				}

				// 4. 给裁剪框加上适当 Padding，防止贴边切掉边缘笔画
				int x = Math.Max(0, boundingBox.X - padding);
				int y = Math.Max(0, boundingBox.Y - padding);
				int width = Math.Min(src.Width - x, boundingBox.Width + padding * 2);
				int height = Math.Min(src.Height - y, boundingBox.Height + padding * 2);

				Rectangle cropRect = new Rectangle(x, y, width, height);

				// 5. 关键修改：直接从原始源图 src（或未修饰的灰度图 gray）上进行裁剪
				using Mat croppedMat = new Mat(src, cropRect);

				return croppedMat.Clone();
			}
		}

		private void Test(Mat mat)
		{
			CvInvoke.Imshow("test", mat);
			CvInvoke.WaitKey();
		}
		#endregion
		/// <summary>
		/// 得到微信日期控件的某日期的坐标.
		/// </summary>
		/// <param name="src">源图形矩阵</param>
		/// <param name="date">待定位的日期</param>
		/// <param name="padding">被白，为了提高识别率，建议为：15</param>
		/// <param name="isTest">是否是测试</param>
		/// <returns>日期对应的坐标</returns>
		public Point GetPointFromDateTime(Mat src, DateOnly date, int padding, bool isTest = false)
		{
			var point = Point.Empty;
			var dateIndex = 0;
			var day = date.DayOfWeek;
			if (day == DayOfWeek.Sunday)
			{
				dateIndex = 6;
			}
			else
			{
				dateIndex = (int)day - 1;
			}
			using Mat mat1 = CropCalendarColumn(src, dateIndex); //被切割的矩阵
			double colWidthDouble = (double)src.Width / 7.0;
			int offsetX = (int)Math.Round(dateIndex * colWidthDouble);
			using Mat Mat2 = new Mat();  //放大2倍的矩阵
			CvInvoke.Resize(mat1, Mat2, Size.Empty, 2, 2, Emgu.CV.CvEnum.Inter.Cubic);
			using Mat gray = new Mat();
			CvInvoke.CvtColor(Mat2, gray, ColorConversion.Bgr2Gray);            //灰值化
			using Mat binary = new Mat();
			CvInvoke.Threshold(gray, binary, 180, 255, ThresholdType.Binary);  //二值化
			using var bitmap = binary.ToBitmap();
			var runCount = 0;
			float boxScoreThresh = 0.3f;
			while (runCount < 2)
			{
				var result = Detect(bitmap: bitmap, padding: padding, maxSideLen: bitmap.Height > bitmap.Width ? bitmap.Height : bitmap.Width, boxScoreThresh: boxScoreThresh, boxThresh: 0.5f, unClipRatio: 1.6f, doAngle: false, mostAngle: false, false);
				if (isTest)
				{
					CvInvoke.Imshow("wechatauto.sdk", result.BoxImg);
					CvInvoke.WaitKey();
				}
				var dateItem = result.TextBlocks.Where(u => !string.IsNullOrWhiteSpace(u.Text) && u.Text.Trim().Equals(date.Day.ToString())).FirstOrDefault();
				if (dateItem != null)
				{
					var centerPoint = new Point(dateItem.BoxPoints[0].X + (int)((dateItem.BoxPoints[2].X - dateItem.BoxPoints[0].X) / 2),
					dateItem.BoxPoints[0].Y + (int)((dateItem.BoxPoints[2].Y - dateItem.BoxPoints[0].Y) / 2));
					point = new Point(((centerPoint.X - padding) / 2) + offsetX, (centerPoint.Y - padding) / 2);
					break;
				}
				boxScoreThresh -= 0.1f;
				runCount++;
			}
			return point;
		}

		/// <summary>
		/// 微信日期控件处理.
		/// 根据指定的列索引（周几）裁剪出日历对应的单列图像
		/// </summary>
		/// <param name="src">原始图像Mat</param>
		/// <param name="colIndex">列索引（0 代表第 1 列，...，6 代表第 7 列）</param>
		/// <return>返回裁剪后的 Mat 对象</return>
		public Mat CropCalendarColumn(Mat src, int colIndex)
		{
			if (colIndex < 0 || colIndex > 6)
			{
				throw new ArgumentOutOfRangeException(nameof(colIndex), "列必须在 0 到 6 之间（代表周一到周日）");
			}

			int totalWidth = src.Width;
			int totalHeight = src.Height;

			// 2. 计算每一列的宽度（浮点数计算，避免直接整除丢失精度）
			double colWidthDouble = (double)totalWidth / 7.0;

			// 3. 计算目标列的像素起始 X 坐标和宽度
			int startX = (int)Math.Round(colIndex * colWidthDouble);
			int endX = (int)Math.Round((colIndex + 1) * colWidthDouble);
			int cropWidth = endX - startX;

			// 这里直接取精准的七等份划分
			Rectangle roi = new Rectangle(startX, 0, cropWidth, totalHeight);

			// 5. 裁剪图像
			Mat croppedColumn = new Mat(src, roi);

			return croppedColumn;
		}
		public void InitOCREngin(WeChatConfig config)
		{
			if (!config.EnableOCR)
				return;
			//检查配置是否正确
			_OcrEngin = _CheckModeFiles(config);
			//初始化引擎
			_ = Task.Run(async () =>
			{
				await _OcrEngin.InitModels();
			});
		}

		private OcrLite _CheckModeFiles(WeChatConfig config)
		{
			var detFileName = Path.IsPathRooted(config.OCRDetModelFilePath) ? config.OCRDetModelFilePath : Path.Combine(AppContext.BaseDirectory, "models", config.OCRDetModelFilePath);
			if (!File.Exists(detFileName))
				throw new Exception("错误：Det模型文件不存在！");
			var clsFileName = Path.IsPathRooted(config.OCRClsModelFilePath) ? config.OCRClsModelFilePath : Path.Combine(AppContext.BaseDirectory, "models", config.OCRClsModelFilePath);
			if (!File.Exists(clsFileName))
				throw new Exception("错误：cls模型文件不存在!");
			var recFileName = Path.IsPathRooted(config.OCRRecModelFilePath) ? config.OCRRecModelFilePath : Path.Combine(AppContext.BaseDirectory, "models", config.OCRRecModelFilePath);
			if (!File.Exists(recFileName))
				throw new Exception("错误：rec模型文件不存在!");
			var dictFileName = Path.IsPathRooted(config.OCRDictModelFilePath) ? config.OCRDictModelFilePath : Path.Combine(AppContext.BaseDirectory, "models", config.OCRDictModelFilePath);
			if (!File.Exists(dictFileName))
				throw new Exception("错误：dict字典文件不存在！");
			OcrLite ocrLite = new OcrLite()
			{
				DetPath = detFileName,
				ClsPath = clsFileName,
				RecPath = recFileName,
				KeyDicPath = dictFileName,
				ThreadNum = (int)(Environment.ProcessorCount * 0.7),
			};
			return ocrLite;
		}


		// 转灰度：兼容单通道（已灰度）、3 通道 BGR、4 通道 BGRA
		public void ToGray(Mat src, Mat dst)
		{
			switch (src.NumberOfChannels)
			{
				case 1:
					src.CopyTo(dst);
					break;
				case 4:
					CvInvoke.CvtColor(src, dst, ColorConversion.Bgra2Gray);
					break;
				default:
					CvInvoke.CvtColor(src, dst, ColorConversion.Bgr2Gray);
					break;
			}
		}

		// 检查源图中是否存在模板小图
		public bool ContainsTemplate(
			Mat source,
			Mat template,
			double threshold = 0.95
		)
		{
			// 空图或模板大于源图时，无法匹配
			if (source is null || template is null ||
				source.IsEmpty || template.IsEmpty ||
				template.Width > source.Width || template.Height > source.Height)
			{
				return false;
			}

			try
			{
				using var graySource = new Mat();
				using var grayTemplate = new Mat();
				ToGray(source, graySource);
				ToGray(template, grayTemplate);

				using var result = new Mat();
				CvInvoke.MatchTemplate(
					graySource,
					grayTemplate,
					result,
					TemplateMatchingType.CcoeffNormed
				);

				if (result.IsEmpty)
				{
					return false;
				}

				result.MinMax(
					out _,
					out double[] maxValues,
					out _,
					out _
				);

				double maxValue = maxValues.Length > 0 ? maxValues[0] : 0.0;
				System.Console.WriteLine($"maxValue = {maxValue}");
				return maxValue >= threshold;
			}
			catch (CvException)
			{
				// OpenCV 内部异常（如通道/格式不兼容），视为未匹配到
				return false;
			}
		}
	}
}