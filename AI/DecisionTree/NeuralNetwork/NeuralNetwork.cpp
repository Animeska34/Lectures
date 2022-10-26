#include <iostream>
#include<time.h>

using namespace std;

class Neuron {
    float weight[2];
    const float lambda = 0.001;

public:
    Neuron() {
        for (int i = 0; i < 2; i++) {
            weight[i] = (rand() % 100) / 1000.0f;
        }
    }

    int Answer(float input[]) {
        float sum = 0;
        for (int i = 0; i < 2; i++)
            sum += input[i] * weight[i];

        return sum >= 0 ? 1 : 0;
    }

    float Change(float input[], float result) {
        int actual = Answer(input);
        int mis = result - actual;

        if (mis != 0)
            for (int i = 0; i < 2; i++)
                weight[i] = lambda * input[i] * mis;
       // cout << mis << endl;
        return abs(mis);
    }
};

int main()
{
    //ntw announcsement
    Neuron network[2];

    //ntw learning
    float input[12][2];
    float output[12][2];

    //1
    input[0][0] = 7;
    input[0][1] = 4;
    output[0][0] = 0;
    output[0][1] = 0;

    input[4][0] = 24;
    input[4][1] = 11;
    output[4][0] = 0;
    output[4][1] = 0;

    input[8][0] = 2;
    input[8][1] = 1;
    output[8][0] = 0;
    output[8][1] = 0;
    //2
    input[1][0] = -5;
    input[1][1] = 5;
    output[1][0] = 1;
    output[1][1] = 0;

    input[5][0] = -24;
    input[5][1] = 100;
    output[5][0] = 1;
    output[5][1] = 0;

    input[9][0] = -7;
    input[9][1] = 45;
    output[9][0] = 1;
    output[9][1] = 0;

    //3
    input[2][0] = -7;
    input[2][1] = -3;
    output[2][0] = 1;
    output[2][1] = 1;

    input[6][0] = -43;
    input[6][1] = -72;
    output[6][0] = 1;
    output[6][1] = 1;

    input[10][0] = -2;
    input[10][1] = -6;
    output[10][0] = 1;
    output[10][1] = 1;
    //4
    input[3][0] = 5;
    input[3][1] = -5;
    output[3][0] = 0;
    output[3][1] = 1;

    input[7][0] = 23;
    input[7][1] = -112;
    output[7][0] = 0;
    output[7][1] = 1;

    input[11][0] = 4;
    input[11][1] = -5;
    output[11][0] = 0;
    output[11][1] = 1;

    int generation = 0;
    for (int i = 0; i < 10000; i++) {
        float mis = 0;
        for (int j = 0; j < 12; j++) {
            mis += network[0].Change(input[j], output[j][0]);
            mis += network[1].Change(input[j], output[j][1]);
        }
        if (mis == 0) {
            cout << "OK: " << generation << endl;
            break;
        }

        generation++;
    }
    
    //ntw usage
    float data[2];

    cout << "Input XY\n";
    cin >> data[0] >> data[1];

    int res[2];
    res[0] = network[0].Answer(data);
    res[1] = network[1].Answer(data);

    if (res[0] == 0)
        if (res[1] == 0)
            cout << "1st";
        else
            cout << "4th";
    else
        if (res[1] == 0)
            cout << "2nd";
        else
            cout << "3rd";

    cout << endl << res[0] << ", " << res[1] << endl;
}