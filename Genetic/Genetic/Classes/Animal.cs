using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic.Classes
{
    public class Animal
    {
        public List<string> DNA = new List<string>();
        public string cromossomo;
        public int Saude = 255;

        private Random r = new Random();

        public string TransformToBinary(int i)
        {
            string binary = Convert.ToString(i, 2);
            Console.WriteLine(binary);

            return binary;
        }

        public Animal cruzar(Animal a2)
        {
            int mutationChance = 1;
            
            List<string> DNA1 = DNA;
            List<string> DNA2 = a2.DNA;

            Animal animal = new Animal();

            

            string cromossomo = "";

            for (int I = 0; I < 3; I++)
            {

                int a = 0;
                int crossOver = DNA1[I].Length / 2;

                for (int i = 0; i < crossOver && i <= 8; i++)
                {
                    int mutation = r.Next(0, 100);

                    if (mutation <= mutationChance)
                    {
                        if (DNA1[I][i].Equals('0'))
                        {
                            cromossomo += '1';
                        }
                        else
                        {
                            cromossomo += '0';
                        }
                    }
                    else
                    {
                        if (i < DNA1[I].Length)
                        {
                            cromossomo += DNA1[I][i];
                        }             
                    }
                    a = i;
                }

                for (int i = a; i < DNA2[I].Length - 1 && i <= 8; i++)
                {
                    int mutation = r.Next(0, 100);

                    
                    if (mutation <= mutationChance)
                    {
                        if (DNA2[I][i].Equals('0'))
                        {
                            cromossomo += '1';
                        }
                        else
                        {
                            cromossomo += '0';
                        }
                    }
                    else
                    {
                        if (i < DNA2[I].Length)
                        {
                            cromossomo += DNA2[I][i];
                        }                       
                    }
                }
                if(cromossomo.Length > 8)
                {
                    if (DNA2[4][453] == 'a')
                    {

                    }
                }
                animal.DNA.Add(cromossomo);
                cromossomo = "";
            }

            return animal;
        }

        public void Randomizar(int R,int G,int B)
        {

            DNA = new List<string>();

            DNA.Add(TransformToBinary(R));
            DNA.Add(TransformToBinary(G));
            DNA.Add(TransformToBinary(B));
        }
    }
}
