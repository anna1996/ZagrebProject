using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using OxyPlot;
namespace NuclearProject
{

    public struct ResultPoint
    {
        public Vector3D Position;
        public double Energy;
    }

    class Model
    {
        EnvironmentPreset data; //массовые числа ядер и соответствующие макроконстанты
        double fuelParams;
        double alphaF;
        double coolantParams;
        float t0, coeffA, coeffB, coeffC, startPower;
        int i;
        public const float finalPoint = 4.0f;
        public const float step = 0.1f;
        public const float startPoint = 0.2f;
        public const double Et = 0.025;

        //конструктор
        public Model(EnvironmentPreset env, double fuelParams, double alphaF, double coolantParams, float t0, float coeffA, float coeffB, float coeffC, float startPower)
        {
            data = env;
            this.fuelParams = fuelParams;
            this.alphaF = alphaF;
            this.coolantParams = coolantParams;
            this.t0 = t0;
            this.coeffA = coeffA;
            this.coeffB = coeffB;
            this.coeffC = coeffC;
            this.startPower = startPower;
        }
        //свойства
        public EnvironmentPreset Data 
        {
            get { return data; }
            set { data = value; }
        }


        public List<DataPoint> getDeltaTtPoints()
        {
            List<DataPoint> deltaTtPoints = new List<DataPoint>();
            for(float point = startPoint; point < finalPoint; point += step)
            {
                deltaTtPoints.Add(new DataPoint(point, getDeltaTt(point, this.t0, this.fuelParams, this.alphaF, this.coolantParams, this.coeffA, this.coeffB, this.coeffC, this.startPower)));
            }

            return deltaTtPoints;
        }
        public List<DataPoint> getDeltaT2Points()
        {
            List<DataPoint> deltaT2Points = new List<DataPoint>();
            for (float point = startPoint; point < finalPoint; point += step)
            {
                deltaT2Points.Add(new DataPoint(point, getDeltaT2(point, this.t0, this.fuelParams, this.alphaF, this.coolantParams, this.coeffA, this.coeffB, this.coeffC, this.startPower)));
            }

            return deltaT2Points;
        }

        private static double getFirstEquationResult(double fuelParams, double alphaF, double deltaW, double deltaTt, double deltaT2)
        {

            return (1 / fuelParams) * (deltaW - (alphaF * (deltaTt - deltaT2 / 2)));
        }

        private static double getSecondEquationResult(double coolantParams, double alphaF, double t0, double deltaTt, double deltaT2)
        {
            return (2 / coolantParams) * (alphaF * (deltaTt - deltaT2 / 2) - (coolantParams / t0) * deltaT2);
        }

        private static double getDeltaW(float A, float B, float C, float startPower, double t)
        {
            return startPower + A * t + B * Math.Pow(t, 2) + C * Math.Pow(t, 3);
        }

        private static double getDeltaTt(double k, float t0, double fuelParams, double alphaF, double coolantParams, float A, float B, float C, float startPower)
        {
            if (k < t0)
            {
                return 0;
            }
            double prevDeltaTt = getDeltaTt(k - t0, t0, fuelParams, alphaF, coolantParams, A, B, C, startPower);

            double prevDeltaT2 = getDeltaT2(k - t0, t0, fuelParams, alphaF, coolantParams, A, B, C, startPower);

            double k1 = getFirstEquationResult(fuelParams, alphaF, getDeltaW(A, B, C, startPower, k - 1), prevDeltaTt, prevDeltaT2);

            double m1 = getSecondEquationResult(coolantParams, alphaF, t0, prevDeltaTt, prevDeltaT2);

            double k2 = getFirstEquationResult(fuelParams, alphaF, getDeltaW(A, B, C, startPower, k - 1 + t0 / 2), prevDeltaTt + k1 / 2, prevDeltaT2 + m1 / 2);

            double m2 = getSecondEquationResult(coolantParams, alphaF, t0, prevDeltaTt + k1 / 2, prevDeltaT2 + m1 / 2);

            double k3 = getFirstEquationResult(fuelParams, alphaF, getDeltaW(A, B, C, startPower, k - 1 + t0 / 2), prevDeltaTt + k2 / 2, prevDeltaT2 + m2 / 2);

            double m3 = getSecondEquationResult(coolantParams, alphaF, t0, prevDeltaTt + k2 / 2, prevDeltaT2 + m2 / 2);

            double k4 = getFirstEquationResult(fuelParams, alphaF, getDeltaW(A, B, C, startPower, k - 1 + t0), prevDeltaTt + k3, prevDeltaT2 + m3);

            double m4 = getSecondEquationResult(coolantParams, alphaF, t0, prevDeltaTt + k3, prevDeltaT2 + m3);

            return prevDeltaTt + (k1 + 2 * k2 + 2 * k3 + k4) * t0 / 6;
        }

        private static double getDeltaT2(double k, float t0, double fuelParams, double alphaF, double coolantParams, float A, float B, float C, float startPower)
        {
            if (k < t0)
            {
                return 0;
            }
            double prevDeltaTt = getDeltaTt(k - t0, t0, fuelParams, alphaF, coolantParams, A, B, C, startPower);
            double prevDeltaT2 = getDeltaT2(k - t0, t0, fuelParams, alphaF, coolantParams, A, B, C, startPower);
            double k1 = getFirstEquationResult(fuelParams, alphaF, getDeltaW(A, B, C, startPower, k - 1), prevDeltaTt, prevDeltaT2);
            double m1 = getSecondEquationResult(coolantParams, alphaF, t0, prevDeltaTt, prevDeltaT2);
            double k2 = getFirstEquationResult(fuelParams, alphaF, getDeltaW(A, B, C, startPower, k - 1 + t0 / 2), prevDeltaTt + k1 / 2, prevDeltaT2 + m1 / 2);
            double m2 = getSecondEquationResult(coolantParams, alphaF, t0, prevDeltaTt + k1 / 2, prevDeltaT2 + m1 / 2);
            double k3 = getFirstEquationResult(fuelParams, alphaF, getDeltaW(A, B, C, startPower, k - 1 + t0 / 2), prevDeltaTt + k2 / 2, prevDeltaT2 + m2 / 2);
            double m3 = getSecondEquationResult(coolantParams, alphaF, t0, prevDeltaTt + k2 / 2, prevDeltaT2 + m2 / 2);
            double k4 = getFirstEquationResult(fuelParams, alphaF, getDeltaW(A, B, C, startPower, k - 1 + t0), prevDeltaTt + k3, prevDeltaT2 + m3);
            double m4 = getSecondEquationResult(coolantParams, alphaF, t0, prevDeltaTt + k3, prevDeltaT2 + m3);
            return prevDeltaT2 + (m1 + 2 * m2 + 2 * m3 + m4) * t0 / 6;
        }
    }
}
