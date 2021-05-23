using System;

int grade;

int repetition = 0; //ilość poprawnych odpowiedzi z rzędu. Bazowa wartosc dla nowego elementu = 0
double efactor = 2.5;  //wspolczynnik łatwości zapamiętywania. Bazowa wartosc dla nowego elementu = 2.5
int interval = 0;   //ilość dni do następnej powtórki. Bazowa wartosc dla nowego elementu = 0

//output: nowe wartości: rep, ef, i
int nextRepetition = 0;
double nextEfactor = 0;
int nextInterval = 0;

Console.WriteLine("repetition: {0}   efactor: {1}   interval: {2}", repetition, efactor, interval);
Console.WriteLine();

for (int i = 0; i < 10; i++)
{
    Console.WriteLine("Podaj ocene:");
    grade = Int32.Parse(Console.ReadLine());
    if (grade < 0) grade = 0;
    else if (grade > 5) grade = 5;

    Console.WriteLine("Ocena: {0}", grade);

    if (grade >= 3) 
    {
        if (repetition == 0)
        {
            nextInterval = 1;
            nextRepetition = 1;
        }
        else if (repetition == 1)
        {
            nextInterval = 6;
            nextRepetition = 2;
        }
        else
        {
            nextInterval = (int)Math.Round(interval * efactor);
            nextRepetition = repetition + 1;
        }
    }
    else
    {
        nextInterval = 1;
        nextRepetition = 0;
    }

    nextEfactor = efactor + (0.1 - (5 - grade) * (0.08 + (5 - grade) * 0.02));

    if (nextEfactor < 1.3) nextEfactor = 1.3;

    Console.WriteLine();
    Console.WriteLine("New values: repetition: {0}   efactor: {1}   interval: {2}", nextRepetition, nextEfactor, nextInterval);

    repetition = nextRepetition;
    efactor = nextEfactor;
    interval = nextInterval;

    nextRepetition = 0;
    nextEfactor = 0;
    nextInterval = 0;
}

