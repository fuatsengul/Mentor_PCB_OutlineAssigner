using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ClipperLib;
using MASKENGINEAUTOMATIONCONTROLLERLib;

namespace xFS_DrawingTools
{
    using Path = List<DoublePoint>;
    public static class DrawingTools
    {
        public static object[,] getPCBPath(Path _solution)
        {
            object[,] _OffsettedArray = new object[3, _solution.Count + 1];
            for (int i = 0; i < _solution.Count; i++)
            {
                _OffsettedArray[0, i] = ((double)_solution[i].X / 100);
                _OffsettedArray[1, i] = ((double)_solution[i].Y / 100);
                _OffsettedArray[2, i] = (double)0.0;
            }
            _OffsettedArray[0, _solution.Count] = _OffsettedArray[0, 0];
            _OffsettedArray[1, _solution.Count] = _OffsettedArray[1, 0];
            _OffsettedArray[2, _solution.Count] = _OffsettedArray[2, 0];
            return _OffsettedArray;

        }

        public static DoublePoint RotatePoint(DoublePoint input, double rotation, double dX, double dY)
        {
            double angle = rotation * Math.PI / 180;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            DoublePoint _rPoint = new DoublePoint();
            _rPoint.X = (cos * (input.X - dX) - sin * (input.Y - dY)) + dX;
            _rPoint.Y = (sin * (input.X - dX) + cos * (input.Y - dY)) + dY;
            return _rPoint;
        }

        public static Path RotatePath(Path input, double rotation, double dX, double dY)
        {
            Path _rPath = new Path();
            foreach (DoublePoint pnt in input)
            {
                _rPath.Add(RotatePoint(pnt, rotation, dX, dY));
            }
            return _rPath;
        }
        
        public static object[,] V2_Oversize(object[,] input, double offsetAsMils, int oversizeType = 0)
        {
            EMaskEngineOversizeType _type = EMaskEngineOversizeType.emeOversizeTypeRound;
            if (oversizeType != 0)
                _type = EMaskEngineOversizeType.emeOversizeTypeSquare;
            MaskEngine _me = new MaskEngine();


            Mask _BAmask = _me.Masks.Add();
            _BAmask.Shapes.AddByPointsArray(input.Length / 3, input, true, EMaskEngineUnit.emeUnitMils);
            _BAmask.Oversize(offsetAsMils, _type, EMaskEngineUnit.emeUnitMils);

            object[,] retVal = (object[,])_BAmask.Shapes.Item[1].get_PointsArray(MASKENGINEAUTOMATIONCONTROLLERLib.EMaskEngineUnit.emeUnitMils);
            Marshal.FinalReleaseComObject(_me);
            return retVal;
        }

        public class IndefiniteShape
        {
            public IndefiniteShape() { }
            public object[,] outline { set; get; }
            public bool solid { set; get; }
        }

        public static List<object[,]> V2_CutShape(object[,] input, List<object[,]> inShapes, List<object[,]> outShapes, double inShapeOffset, double outShapeOffset)
        {
            MaskEngine _me = new MaskEngine();
            Mask _inShapeMask = _me.Masks.Add();
            Mask _outShapeMask = _me.Masks.Add();



            if (inShapes != null)
            {
                foreach (object[,] _cutter in inShapes)
                {
                    _inShapeMask.Shapes.AddByPointsArray(_cutter.Length / 3, _cutter, true, EMaskEngineUnit.emeUnitMils);
                }
                if (inShapeOffset != 0)
                    _inShapeMask.Oversize(inShapeOffset, EMaskEngineOversizeType.emeOversizeTypeRound, EMaskEngineUnit.emeUnitMils);
            }
            if (outShapes != null)
            {
                foreach (object[,] _cutter in outShapes)
                {
                    _outShapeMask.Shapes.AddByPointsArray(_cutter.Length / 3, _cutter, true, EMaskEngineUnit.emeUnitMils);
                }
                if (outShapeOffset != 0)
                    _outShapeMask.Oversize(outShapeOffset, EMaskEngineOversizeType.emeOversizeTypeRound, EMaskEngineUnit.emeUnitMils);
            }


            Mask _objMask = _me.Masks.Add();
            _objMask.Shapes.AddByPointsArray(input.Length / 3, input, true, EMaskEngineUnit.emeUnitMils);

            Mask _finalMask = null;
            if (inShapes != null)
                _finalMask = _me.BooleanOp(EMaskEngineBooleanOp.emeBooleanOpAND, _objMask, _inShapeMask);
            else
                _finalMask = _objMask;
            if (outShapes != null)
                _finalMask = _me.BooleanOp(EMaskEngineBooleanOp.emeBooleanOpSubtract, _finalMask, _outShapeMask);

            List<object[,]> _returnList = new List<object[,]>();
            foreach (Shape _s in _finalMask.Shapes)
                _returnList.Add((object[,])_s.get_PointsArray(EMaskEngineUnit.emeUnitMils));

            return _returnList;
        }

        public static List<object[,]> V2_BooleanOp2Lines(object[,] _outline, List<object[,]> _splits)
        {
            MASKENGINEAUTOMATIONCONTROLLERLib.MaskEngine _me = new MaskEngine();
            Mask _m1 = _me.Masks.Add();
            _m1.Shapes.AddByPath(_outline.Length / 3, _outline, 0.001, true, EMaskEngineOversizeType.emeOversizeTypeRound, EMaskEngineUnit.emeUnitMils);

            Mask _m2 = _me.Masks.Add();
            foreach (object[,] _spline in _splits)
            {
                _m2.Shapes.AddByPath(_spline.Length / 3, _spline, 1, true, EMaskEngineOversizeType.emeOversizeTypeRound, EMaskEngineUnit.emeUnitMils);
            }

            _m1.BooleanOp(EMaskEngineBooleanOp.emeBooleanOpSubtract, _m2);

            List<object[,]> _returnList = new List<object[,]>();
            foreach (Shape _s in _m1.Shapes)
                _returnList.Add((object[,])_s.get_PointsArray(EMaskEngineUnit.emeUnitMils));

            //_m1.VectorizeByChordAngle(15);

            return _returnList;
        }

        public static bool Touches(object[,] poly1, object[,] poly2)
        {
            MaskEngine _me = new MaskEngine();
            Mask _m1 = _me.Masks.Add();
            Mask _m2 = _me.Masks.Add();

            _m1.Shapes.AddByPointsArray(poly1.Length / 3, poly1, true, EMaskEngineUnit.emeUnitMils);
            _m2.Shapes.AddByPointsArray(poly2.Length / 3, poly2, true, EMaskEngineUnit.emeUnitMils);

            return (_m1.Touch(_m2));
        }

        public class DoubleVector
        {
            public double X { set; get; }
            public double Y { set; get; }
            public double R { set; get; }

            public DoubleVector(double x = 0, double y = 0, double r = 0)
            {
                this.X = x; this.Y = y; this.R = r;
            }
            public DoubleVector(DoublePoint dp, double R)
            {
                this.X = dp.X; this.Y = dp.Y; this.R = R;
            }
            public DoubleVector(IntPoint ip, double R)
            {
                this.X = ip.X; this.Y = ip.Y; this.R = R;
            }
            public override string ToString()
            {
                return string.Format("X:{0}\tY:{1}\tR:{2}", this.X, this.Y, this.R);
            }
        };

        public static List<DoubleVector> GetPointsInPath(object[,] outlinepoints)
        {
            List<DoubleVector> Points = new List<DoubleVector>();

            #region Get Outline Points
            for (int i = 0; i < outlinepoints.GetLength(1) - 1; i++)
            {
                double pnt1X = Convert.ToDouble(outlinepoints[0, i]);
                double pnt1Y = Convert.ToDouble(outlinepoints[1, i]);
                double pnt2X = Convert.ToDouble(outlinepoints[0, i + 1]);
                double pnt2Y = Convert.ToDouble(outlinepoints[1, i + 1]);
                double pnt1R = Convert.ToDouble(outlinepoints[2, i]);
                double pnt2R = Convert.ToDouble(outlinepoints[2, i + 1]);

                if (pnt1X == pnt2X && Math.Abs(pnt2Y - pnt1Y) > 100)
                {
                    if (pnt1Y < pnt2Y)
                    {
                        for (double pnt = pnt1Y + (pnt2Y - pnt1Y) / 4; Math.Round(pnt) < Math.Round(pnt2Y); pnt += (pnt2Y - pnt1Y) / 4)
                        {
                            Points.Add(new DoubleVector(pnt1X, pnt, 270));
                        }
                    }
                    if (pnt2Y < pnt1Y)
                    {
                        for (double pnt = pnt2Y + (pnt1Y - pnt2Y) / 4; Math.Round(pnt) < Math.Round(pnt1Y); pnt += (pnt1Y - pnt2Y) / 4)
                        {
                            Points.Add(new DoubleVector(pnt1X, pnt, 90));
                        }
                    }
                }
                else if (pnt1Y == pnt2Y && Math.Abs(pnt2X - pnt1X) > 100)
                {
                    if (pnt1X < pnt2X)
                    {
                        for (double pnt = pnt1X + (pnt2X - pnt1X) / 4; Math.Round(pnt) < Math.Round(pnt2X); pnt += (pnt2X - pnt1X) / 4)
                        {
                            Points.Add(new DoubleVector(pnt, pnt1Y, 180));
                        }
                    }
                    if (pnt2X < pnt1X)
                    {
                        for (double pnt = pnt2X + (pnt1X - pnt2X) / 4; Math.Round(pnt) < Math.Round(pnt1X); pnt += (pnt1X - pnt2X) / 4)
                        {
                            Points.Add(new DoubleVector(pnt, pnt1Y, 0));
                        }
                    }
                }
                else
                {
                    if (pnt1R != 0 || pnt2R != 0)
                        continue;

                    double Xlenght, Ylenght;
                    Xlenght = pnt2X - pnt1X;
                    Ylenght = pnt2Y - pnt1Y;

                    double Xratio = Xlenght / 4;
                    double Yratio = Ylenght / 4;

                    double totLength = Math.Sqrt((Xlenght * Xlenght) + (Ylenght * Ylenght));

                    if (totLength < 250)
                        continue;


                    var atan = Math.Atan(Convert.ToDouble(Ylenght / Xlenght)) * 180 / Math.PI;

                    for (int h = 1; h < 4; h++)
                        Points.Add(
                            new DoubleVector(
                                pnt1X + (h * Xratio),
                                pnt1Y + (h * Yratio),
                                atan
                            )
                        );
                }


            }
            #endregion

            MaskEngine _me = new MaskEngine();
            Mask _m = _me.Masks.Add();
            _m.Shapes.AddByPointsArray(outlinepoints.Length / 3, outlinepoints, true, EMaskEngineUnit.emeUnitMils);


            foreach (DoubleVector dv in Points)
            {
                Path line = new Path();
                line.Add(new ClipperLib.DoublePoint(dv.X, dv.Y));
                line.Add(new ClipperLib.DoublePoint(dv.X, (dv.Y + 25)));

                line = RotatePath(line, dv.R, dv.X, dv.Y);
                if (_m.ContainsPoint(line[1].X, line[1].Y, EMaskEngineUnit.emeUnitMils))
                {
                    dv.R = (dv.R + 180) % 360;
                }
            }



            return Points;

        }

    }
}

